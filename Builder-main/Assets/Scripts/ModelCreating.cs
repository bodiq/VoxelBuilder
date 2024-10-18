using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Texture2D))]
public class ModelCreating : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    
    [SerializeField] private Renderer _voxel;
    
    [SerializeField] private int _slices;
    
    [SerializeField] private float voxelShiftConstraint = 0.15f;
    
    [SerializeField] private Transform cubesSpawnPoint;

    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private ProgressBar _progressBar;
    
    [SerializeField] private AudioSource cubeFallSound;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private float scaleOfCube = 4.0f; // 4.0f == if the scale of your cube have to be 0.4
    [SerializeField] private float deviationOfModelZ = 0.0f;
    [SerializeField] private float deviationOfModelX = 0.0f;
    

    private void Awake()
    {
        Application.targetFrameRate = 60;
        CreateModel();
        if (_texture != null)
        {
            layerHeight = _texture.height / _slices;
            _layerCount = layerHeight * _texture.width;
            _colorTexturedata = _texture.GetRawTextureData<Color32>();
        }

        indexColorArray = 0;
        pixelCount = 0;
        layerNum = 0;
        rowModel = 0;
        colModel = 0;

        _progressBar.maxValue = _targetCubesCount;

        realScaleOfCube = scaleOfCube / 10.0f;
    }
    
    private void Update()
    {
        /*if (Input.anyKeyDown)
        {
            for (int i = 0; i < 100; i++)
            {
                BuildOneStep();
            }
        }*/
    }
    
    public List<GameObject> TakeCubes(Player player, int countCubes)
    {
        Vector3 playerPos = player.transform.position;
        List<GameObject> cubes = new List<GameObject>();
        float tempFloat = 0.5f;
        int countCubesList = 0;

        if (indexCube >= _cubes.Count)
        {
            return null;
        }

        for (int i = 0; i < countCubes; i++)
        {
            if (indexCube >= _cubes.Count)
            {
                return cubes;
            }

            countCubesList++;
            CubeInfo cube = _cubes[indexCube];
            var instance = Instantiate(_voxel, cubesSpawnPoint.transform.position, Quaternion.identity);
            instance.GetComponent<CubeParticles>().TurnOffTrail();
            Vector3 headPosition = new Vector3(playerPos.x, playerPos.y + 2.0f + (tempFloat * i), playerPos.z + 0.25f);
            player.AddIndex(indexCube);
            player.SetTestCount(countCubesList);
            indexCube++;
            Timer(0.05f * (i + 1), () =>
            {
                instance.transform.DOJump(headPosition, 1.0f, 1, 0.2f);
                instance.transform.rotation = Quaternion.identity;
                instance.transform.SetParent(player.GetHead());
                instance.sharedMaterial = cube.GetMaterial();
                instance.name = cube.GetName();
            });
            cubes.Add(instance.gameObject);
        }
        return cubes;
    }
    
    public void ThrowCubes(Player player, int count)
    {
        int localIndex = count;
        int reverseCubesIndex = player.GetCountIndexes() - 1;

        for (int i = count - 1; i >= 0; i--)
        {
            CubeInfo info = _cubes[player.GetIndex(reverseCubesIndex--)];
            int _column      = info.GetCol();
            int _row         = info.GetRow();
            int _layerNum    = info.GetLayerNum();
            int _layerHeight = info.GetLayerHeight();

            float _shift = info.GetShift();
            
            Vector3 place = new Vector3(_column - (float)_texture.width / 2 + _shift - deviationOfModelX, _layerNum + _shift + 0.5f, _row - _layerNum * _layerHeight - _layerHeight / 2 + _shift - deviationOfModelZ) / 10.0f * scaleOfCube;
            
            var cube = player.Cubes[i];
            var cubeParticle = cube.GetComponent<CubeParticles>();
            
            Material colorCube = cube.GetComponent<Renderer>().sharedMaterial;
            cubeParticle.TurnOnTrail(colorCube);
            int j = count - i; //optimize
            Timer(0.05f * j, () =>
            {
                cube.transform.DOJump(place, 7.0f, 1, 0.7f).OnComplete( () =>
                { 
                    _moneyManager.EarnMoney();
                    cubeParticle.PlayCubeLandsParticle();
                    cubeParticle.TurnOffTrail();
                    /*cube.transform.DOScale(realScaleOfCube + 0.3f, 0.2f).SetEase(Ease.OutBounce)
                        .SetLoops(2, LoopType.Yoyo);*/
                    if (!cubeFallSound.isPlaying)
                    {
                        cubeFallSound.Play();
                    }
                });
                cube.transform.rotation = Quaternion.identity;
                cube.transform.SetParent(transform);

                _actualCubesCount++;
                _progressBar.SetValue(_actualCubesCount);
                if (_actualCubesCount == _targetCubesCount)
                {
                    Timer(1.0f, () =>
                    {
                        _gameManager.LevelEnd();
                    });
                }
            });
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void BuildOneStep()
    {
        if (rowModel < _texture.height)
        {
            if (colModel < _texture.width)
            {
                Color32 color32 = _colorTexturedata[indexColorArray++];
                pixelCount++;
                if (color32.a == 0)
                {
                    colModel++;
                    BuildOneStep();
                    return;
                }
                int materialKey = color32.GetHashCode();
                Material mat = null;
                if (_materialCache.ContainsKey(materialKey))
                {
                    mat = _materialCache[materialKey];
                }
                else
                {
                    mat = new Material(Shader.Find("Simple Toon/SToon Default")) //Optimize
                    {
                        color = color32
                    };
                    
                    mat.SetFloat(Steps, 15);
                    mat.SetFloat(LilOffset, 1.1f);
                    mat.SetFloat(MinLight, 0.45f);
                    mat.SetFloat(Smoothness, 0);

                    _materialCache[materialKey] = mat;
                }

                float randomShift = Random.Range(-voxelShiftConstraint, voxelShiftConstraint);
                
                var spawnCube = Instantiate(_voxel, new Vector3(colModel - (float)_texture.width / 2 + randomShift - deviationOfModelX, layerNum + randomShift + 0.5f, rowModel - layerNum * layerHeight - layerHeight / 2 + randomShift - deviationOfModelZ) / 10.0f * scaleOfCube, Quaternion.identity);
                spawnCube.GetComponent<Renderer>().sharedMaterial = mat;
                spawnCube.transform.SetParent(gameObject.transform);
                spawnCube.name = $"Voxel_{rowModel}_{colModel}_{layerNum}";
                colModel++;
            }
            else
            {
                rowModel++;
                colModel = 0;
            }

            if (pixelCount == _layerCount)
            {
                layerNum++;
                pixelCount = 0;
            }
        }
    }

    private void CreateModel()
    {
        var layerHeight = _texture.height / _slices;

        var layerCount = layerHeight * _texture.width;
        var pixelCount = 0;

        var data = _texture.GetRawTextureData<Color32>();
        var index = 0;
        var z = 0;

        for (var row = 0; row < _texture.height; row++)
        {
            for (var column = 0; column < _texture.width; column++)
            {
                pixelCount += 1;
                Color32 color32 = data[index++];
                if (color32.a != 0)
                {
                    var materialKey = color32.GetHashCode();
                    Material mat = null;
                    if (_materialCache.ContainsKey(materialKey))
                    {
                        mat = _materialCache[materialKey];
                    }
                    else
                    {
                        mat = new Material(Shader.Find("Simple Toon/SToon Default"))
                        {
                            color = color32
                        };

                        mat.SetFloat(Steps, 15);
                        mat.SetFloat(LilOffset, 1.1f);
                        mat.SetFloat(MinLight, 0.45f);
                        mat.SetFloat(Smoothness, 0);

                        _materialCache[materialKey] = mat;
                    }

                    var shift = UnityEngine.Random.Range(-voxelShiftConstraint, voxelShiftConstraint);
                    string name = $"Voxel_{row}_{column}_{z}";
                    _cubes.Add(new CubeInfo(mat, name, shift, column, row, z, layerHeight));
                }
            }
            
            if (pixelCount == layerCount)
            {
                z++;
                pixelCount = 0;
            }
        }

        _targetCubesCount = _cubes.Count;
    }

    public static void ResetStaticValues()
    {
        indexCube = 0;
    }
    
    private Tweener Timer(float delay, TweenCallback onCompleted)
    {
        return DOTween.To(value => { }, 0, 1, delay).OnComplete(onCompleted);
    }
    
    
    private readonly Dictionary<int, Material> _materialCache = new();
    
    private static readonly int Steps = Shader.PropertyToID("_Steps");
    private static readonly int LilOffset = Shader.PropertyToID("_Offset");
    private static readonly int MinLight = Shader.PropertyToID("_MinLight");
    private static readonly int Smoothness = Shader.PropertyToID("_Smoothness");

    private int layerHeight;
    private int _layerCount;
    
    private static int pixelCount;
    private static int indexColorArray;  //index
    private static int layerNum; // z
    private static int rowModel;
    private static int colModel;
    private static int indexCube = 0;

    private int _targetCubesCount = 0;
    private int _actualCubesCount = 0;

    private List<CubeInfo> _cubes = new List<CubeInfo>();

    private NativeArray<Color32> _colorTexturedata; // _data

    private float realScaleOfCube = 0.0f;
}
