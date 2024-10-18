using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CanvasManager _canvasManager;
    
    [SerializeField] private Transform storage;
    [SerializeField] private Transform deliever;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float speedAddUpgrade = 2.0f;
    [SerializeField] private float durationSpeedUp = 1.0f;

    [SerializeField] private ModelCreating _modelCreating;
    [SerializeField] private MoneyManager _bank;
    
    [SerializeField] private int power = 3;

    [SerializeField] private Vector3 finishLevelWorkersPosition = new Vector3(5.0f, 0.176f, -18.20f);

    public void Start()
    {
        _initialSpeed = speed;
        int newPlayersCount = LevelManager.GetLevelCount();
#if !UNITY_EDITOR
        DataUtility.TryLoad(ref newPlayersCount, "LevelCounter");
#endif
        newPlayersCount -= 1;
        for (int i = 0; i < newPlayersCount; i++)
        {
            Timer(1.0f * (i + 1), () =>
            {
                var _player = Instantiate(player, storage.position, Quaternion.identity);
                _player.Initialize(storage, deliever, _initialSpeed, speed, speedAddUpgrade, durationSpeedUp, _modelCreating, this);
                _players.Add(_player);
            });
        }
    }

    public void AddPlayer()
    {
        if (_bank.CanUpgradeCountWorkers())
        {
            if (isTutorial)
            {
                Time.timeScale = 1.0f;
                var _player = Instantiate(player, storage.position, Quaternion.identity);
                _player.Initialize(storage, deliever, _initialSpeed, speed, speedAddUpgrade, durationSpeedUp, _modelCreating, this);
                _players.Add(_player);
                Timer(0.3f, () =>
                {
                    _canvasManager.TurnOffTutorial();
                    _canvasManager.AddSpeedTutorial();
                });
            }
            else
            {
                var _player = Instantiate(player, storage.position, Quaternion.identity);
                _player.Initialize(storage, deliever, _initialSpeed, speed, speedAddUpgrade, durationSpeedUp, _modelCreating, this);
                _players.Add(_player);   
            }
        }
    }

    public void TurnOffBooleansForPlayers()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].TurnOffBooleansForMoving();   
        }
    }

    public void IncreaseSpeed()
    {
        if (_bank.CanUpgradeSpeed())
        {
            if (isTutorial)
            {
                Time.timeScale = 1.0f;
                _initialSpeed += speedAddUpgrade;
                int count = _players.Count;
                for (int i = 0; i < count; i++)
                {
                    _players[i].ButtonSpeedUp();
                    _players[i].SetInitialSpeed(_initialSpeed);
                }
                speed = _players[0].GetSpeed();
                Timer(0.3f, () =>
                {   
                    _canvasManager.TurnOffTutorial();
                    _canvasManager.AddCapacityTutorial();
                });
            }
            else
            {
                _initialSpeed += speedAddUpgrade;
                int count = _players.Count;
                for (int i = 0; i < count; i++)
                {
                    _players[i].ButtonSpeedUp();
                    _players[i].SetInitialSpeed(_initialSpeed);
                }
                speed = _players[0].GetSpeed();
            }
        }
    }

    public void IncreasePower()
    {
        if (_bank.CanUpgradeStrength())
        {
            if (isTutorial)
            {
                Time.timeScale = 1.0f;
                power++;
                int count = _players.Count;
                for (int i = 0; i < count; i++)
                {
                    _players[i].PlayParticleUpgrade();
                }

                Timer(0.3f, () =>
                {
                    _canvasManager.TurnOffTutorial();
                    _canvasManager.SpeedUpTutorial();
                    isTutorial = false;
                });
            }
            else
            {
                power++;
                int count = _players.Count;
                for (int i = 0; i < count; i++)
                {
                    _players[i].PlayParticleUpgrade();
                }
            }
        }
    }

    public int GetPower()
    {
        return power;
    }

    public void MakeACelebration()
    {
        int count = _players.Count;
        float tempX = 1.0f;
        float tempZ = 3.0f;
        float tempMinus = 1.0f;
        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                Vector3 pos = new Vector3(finishLevelWorkersPosition.x, finishLevelWorkersPosition.y, finishLevelWorkersPosition.z + (tempZ * tempMinus));
                tempMinus = -tempMinus;
                _players[i].GoCelebrate(pos);
            }
            else
            {
                Vector3 pos = new Vector3(finishLevelWorkersPosition.x - (tempX * i), finishLevelWorkersPosition.y, finishLevelWorkersPosition.z + (tempZ * tempMinus));
                tempMinus = -tempMinus;
                _players[i].GoCelebrate(pos);
            }
        }
    }
    
    private Tweener Timer(float delay, TweenCallback onCompleted)
    {
        return DOTween.To(value => { }, 0, 1, delay).OnComplete(onCompleted);
    }
    
    
    private List<Player> _players = new List<Player>();
    private float _initialSpeed;
    private Vector3 _startCelPos;

    private static bool isTutorial = true;
}
