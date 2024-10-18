using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform storage;
    [SerializeField] private Transform deliever;
    [SerializeField] private Transform transformToConnect;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float speedAddUpgrade = 2.0f;
    [SerializeField] private float durationSpeedUp = 1.0f;

    [SerializeField] private ModelCreating modelCreating;
    
    [SerializeField] private PlayerCreator _playerCreator;

    [SerializeField] private Animator _animator;

    [SerializeField] private ParticleSystem _speedUpParticle;
    [SerializeField] private ParticleSystem _levelUp;

    public void Initialize(Transform storage, Transform deliever, float _initialSpeed, float speed, float _speedAddUpgrade, float durationSpeed, ModelCreating _modelCreating, PlayerCreator _Creator)
    {
        this.storage         = storage;
        this.deliever        = deliever;
        this.speed           = speed;
        speedAddUpgrade      = _speedAddUpgrade;
        durationSpeedUp      = durationSpeed;
        modelCreating        = _modelCreating;
        _playerCreator       = _Creator;
        initialSpeed         = _initialSpeed;
    }

    private void Start()
    {
        normPositionStorage = new Vector3(storage.position.x,  storage.position.y,  storage.position.z);
        normPositionDeliver = new Vector3(deliever.position.x, deliever.position.y,  deliever.position.z);
        
        isGoingToStorage  = true;
        isGoingToDeliever = false;
        
        speedUp = initialSpeed * 3.0f;
        speed   = initialSpeed;

        transform.position = normPositionStorage;
        _speedUpParticle.Stop();
    }

    private void SpeedUp()
    {
        if (speedTweener.IsActive()) speedTweener.Kill();
        speedTweener = DOTween.To(() => speed, x => speed = x, speedUp, durationSpeedUp).OnComplete(() =>
            {
                DOTween.To(() => speed, x => speed = x, initialSpeed, 0.5f).OnComplete(() =>
                {
                    _speedUpParticle.Stop();
                });
            }
        );
    }

    private void Update()
    {
        step = speed * Time.deltaTime;
        
        _animator.SetFloat("WalkingSpeed", initialSpeed / 2f);
        _animator.SetFloat("RunningSpeed", speed / 3f);
        
        if (Input.touchCount > 0 && !IsTheEnd)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !touch.IsOverUIA())
            {
                SpeedUp();
                _speedUpParticle.Play();
            }
        }

        if (speed > initialSpeed  && !isIdling)
        {
            _animator.ResetTrigger("Walking");
            _animator.ResetTrigger("DynIdle");
            _animator.SetTrigger("Running");
        }
        else if (isIdling)
        {
            _animator.ResetTrigger("Walking");
            _animator.ResetTrigger("Running");
            _animator.SetTrigger("DynIdle");
            _speedUpParticle.Stop();
        }
        else if(!IsTheEnd)
        {
            _animator.ResetTrigger("Running");
            _animator.ResetTrigger("DynIdle");
            _animator.SetTrigger("Walking");
        }
        
        if(isGoingToDeliever)
        {
            GoToDeliverPoint();
        }
        else if(isGoingToStorage)
        {
            GoToStoragePoint();
        }
    }

    public void GoCelebrate(Vector3 pos)
    {
        transform.position = pos;
    }

    private void GoToDeliverPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, normPositionDeliver, step);
        transform.LookAt(normPositionDeliver);
        if(transform.position == normPositionDeliver)
        {
            isIdling = true;
            isGoingToDeliever = false;
            modelCreating.ThrowCubes(this, testCount);
            float delay = 0.06f * testCount;
            Timer(delay, () =>
            {
                indexes.Clear();
                ChangeDirection();
                isGoingToStorage = true;
                isIdling = false;
            });
        }
    }

    private void GoToStoragePoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, normPositionStorage, step);
        transform.LookAt(normPositionStorage);
        if(transform.position == normPositionStorage)
        {
            isIdling = true;
            isGoingToStorage = false;
            countPower = _playerCreator.GetPower();
            _cubes = modelCreating.TakeCubes(this, countPower);
            float delay = (countPower + 1) * 0.05f + 0.2f;
            Timer(delay, () => 
            {
                if (_cubes == null)
                {
                    return;
                }
                ChangeDirection();
                isGoingToDeliever = true;
                isIdling = false;
            });
        }
    }

    public GameObject Cube => _cube;
    public List<GameObject> Cubes => _cubes;


    private void ChangeDirection()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private Tweener Timer(float delay, TweenCallback onCompleted)
    {
        return DOTween.To(value => { }, 0, 1, delay).OnComplete(onCompleted);
    }

    public void TurnOffBooleansForMoving()
    {
        speedTweener.Kill();
        IsTheEnd = true;
        speed = 0.0f;
        initialSpeed = 0.0f;
        speedUp = 0.0f;
        isGoingToDeliever = false;
        isGoingToStorage = false;
        isIdling = false;
        _animator.ResetTrigger("Running");
        _animator.ResetTrigger("DynIdle");
        _animator.ResetTrigger("Walking");
        _animator.SetTrigger("WaveDance");
    }

    public Transform GetHead()
    {
        return transformToConnect;
    }
    
    public void ButtonSpeedUp()
    {
        speed += speedAddUpgrade;
        speedUp = initialSpeed * 3.0f;
        PlayParticleUpgrade();
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void AddIndex(int index)
    {
        indexes.Add(index);
    }

    public int GetIndex(int index)
    {
        return indexes[index];
    }

    public int GetCountIndexes()
    {
        return indexes.Count;
    }

    public void SetTestCount(int count)
    {
        testCount = count;
    }

    public void SetInitialSpeed(float _initSpeed)
    {
        initialSpeed = _initSpeed;
    }

    public void PlayParticleUpgrade()
    {
        _levelUp.Play();
    }

    private GameObject _cube;
    
    private int myIndex;
    private int countPower;
    private int testCount;

    private List<GameObject> _cubes = new();
    private List<int> indexes = new();

    private float step = 0.0f;
    private float initialSpeed;
    private float speedUp;

    private Vector3 normPositionStorage;
    private Vector3 normPositionDeliver;

    private bool isGoingToStorage;
    private bool isGoingToDeliever;
    private bool isIdling = true;
    private bool IsTheEnd = false;

    private Tweener speedTweener;
    
    
}

public static class TouchUtility
{
    private static Camera MainCamera
    {
        get
        {
            if (_mainCamera == null) _mainCamera = Camera.main;
            return _mainCamera;
        }
    }

    private static Camera _mainCamera;

    public static bool IsOverUIA(this Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }

    public static bool GetHitInfo(this Touch touch, out RaycastHit hitInfo, int layerMask = -5, float maxDistance = 100)
    {
        var screenPoint = new Vector3(touch.position.x, touch.position.y, 0.0f);
        var ray = MainCamera.ScreenPointToRay(screenPoint);
        return Physics.Raycast(ray, out hitInfo, maxDistance, layerMask);
    }

    public static Vector3 GetWorldPosition(this Touch touch, Vector3 clipPoint, Vector2 screenOffset = default)
    {
        var clipPlane = Vector3.Distance(MainCamera.transform.position, clipPoint);
        var screenPoint = new Vector3(touch.position.x + screenOffset.x, touch.position.y + screenOffset.y, clipPlane);
        return MainCamera.ScreenToWorldPoint(screenPoint);
    }
}
