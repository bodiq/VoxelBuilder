using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using GameAnalyticsSDK.Setup;

public class CanvasManager : MonoBehaviour
{
    [Header("Button Color Settings")]
    [SerializeField] private UnityEngine.UI.Image _buttonWorkers;
    [SerializeField] private UnityEngine.UI.Image _buttonSpeed;
    [SerializeField] private UnityEngine.UI.Image _buttonStrenght;
    [Header("-----------------------")]
    [SerializeField] private Color colorButtonNotUpgradable;
        
    [Header("Serialized Classes for Usage")]
    [SerializeField] private MoneyManager _bank;

    [Header("Level Finish")] 
    [SerializeField] private GameObject _stats;
    [SerializeField] private GameObject _upgrades;
    [SerializeField] private GameObject _deliever;
    [SerializeField] private GameObject _nextLevel;

    [Header("Tutorial")] 
    [SerializeField] private GameObject addWorkersTutorial;
    [SerializeField] private GameObject addSpeedTutorial;
    [SerializeField] private GameObject addCapacityTutorial;
    [SerializeField] private GameObject speedUpTutorial;
    [SerializeField] private GameObject handOfSpeedUpTutorial;

    private void Awake()
    {
        colorButtonUpgradableWorkers.a  = 1.0f;
        colorButtonUpgradableSpeed.a    = 1.0f;
        colorButtonUpgradableCapacity.a = 1.0f;
        
        colorButtonNotUpgradable.a = 1.0f;

        colorButtonUpgradableWorkers  = _buttonWorkers.color;
        colorButtonUpgradableSpeed    = _buttonSpeed.color;
        colorButtonUpgradableCapacity = _buttonStrenght.color;
    }

    private void Start()
    {
        int numLevel = LevelManager.GetLevelCount();
        if (numLevel == 1)
        {
            Timer(0.5f, () =>
            {
                AddWorkersTutorial();
            });
        }
    }

    public void AddWorkersTutorial()
    {
        addWorkersTutorial.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void AddSpeedTutorial()
    {
        addSpeedTutorial.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void AddCapacityTutorial()
    {
        addCapacityTutorial.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void SpeedUpTutorial()
    {
        speedUpTutorial.SetActive(true);
        Time.timeScale = 0.0f;
        handOfSpeedUpTutorial.transform.DOScale(1.3f, 0.2f).SetUpdate(true).SetEase(Ease.InOutBounce).SetLoops(-1, LoopType.Yoyo);
        Timer(2.0f, () =>
        {
            Time.timeScale = 1.0f;
            TurnOffTutorial();
        }).SetUpdate(true);
    }

    public void TurnOffTutorial()
    {
        addCapacityTutorial.SetActive(false);
        addSpeedTutorial.SetActive(false);
        addWorkersTutorial.SetActive(false);
        speedUpTutorial.SetActive(false);
    }

    public void TurnOffCanvaOnLevelFinish()
    {
        _stats.SetActive(false);
        _upgrades.SetActive(false);
        _deliever.SetActive(false);
        Timer(3.0f, (() =>
        {
            _nextLevel.SetActive(true);
        }));
    }
    // Can be optimized by making this not on the update but after buying some staff the 
    private void Update()
    {
        if (_bank.CanWorkersBeUpgraded())
        {
            _buttonWorkers.color = colorButtonUpgradableWorkers;
        }
        else
        {
            _buttonWorkers.color = colorButtonNotUpgradable;
        }
        
        if (_bank.CanSpeedBeUpgraded())
        {
            _buttonSpeed.color = colorButtonUpgradableSpeed;
        }
        else
        {
            _buttonSpeed.color = colorButtonNotUpgradable;
        }
        
        if (_bank.CanStrengthBeUpgraded())
        {
            _buttonStrenght.color = colorButtonUpgradableCapacity;
        }
        else
        {
            _buttonStrenght.color = colorButtonNotUpgradable;
        }
    }
    
    private Tweener Timer(float delay, TweenCallback onCompleted)
    {
        return DOTween.To(value => { }, 0, 1, delay).OnComplete(onCompleted);
    }

    private Color colorButtonUpgradableWorkers;
    private Color colorButtonUpgradableSpeed;
    private Color colorButtonUpgradableCapacity;

    private static bool isTutorialDone = false;
}