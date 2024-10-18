using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform cameraSettingOnLevelFinish;
    
    [SerializeField] private ParticleSystem _Fireworks;

    [SerializeField] private CanvasManager _canvasManager;

    [SerializeField] private PlayerCreator _playerCreator;

    [SerializeField] private float sizeOfOthorgraphicCamera = 12.0f;

    [SerializeField] private Transform _model;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public void LevelEnd()
    {
        if (LevelManager.GetLevelCount() == 11)
        {
            _model.position = new Vector3(0.0f, 0.0f, 1.5f);
            _model.rotation = new Quaternion(0,0.639039576f,0,-0.76917392f);
        }
        else if (LevelManager.GetLevelCount() == 13)
        {
            _model.rotation = new Quaternion(0.10156400f, 0.406232148f, -0.0454906598f, -0.906967938f);
        }
        _playerCreator.TurnOffBooleansForPlayers();
        var position = cameraSettingOnLevelFinish.position;
        transform.DOMove(position, 1.5f).SetEase(Ease.InOutQuad);
        _playerCreator.MakeACelebration();
        transform.DORotate(cameraSettingOnLevelFinish.rotation.eulerAngles, 1.5f, RotateMode.Fast);
        DOTween.To(() => _camera.orthographicSize, x => _camera.orthographicSize = x, sizeOfOthorgraphicCamera, 1.5f);
        _Fireworks.Play();
        _canvasManager.TurnOffCanvaOnLevelFinish();
    }

    private Camera _camera;
}
