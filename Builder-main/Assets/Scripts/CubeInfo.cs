using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInfo
{
    public CubeInfo(Material mat, string name, float randomShift, int col, int row, int layerNum, int layerHeight)
    {
        _randomShift = randomShift;
        _col = col;
        _row = row;
        _layerNum = layerNum;
        _layerHeight = layerHeight;
        _mat = mat;
        _name = name;
    }

    public float GetShift()
    {
        return _randomShift;
    }
    
    public int GetCol()
    {
        return _col;
    }

    public int GetRow()
    {
        return _row;
    }

    public int GetLayerNum()
    {
        return _layerNum;
    }

    public int GetLayerHeight()
    {
        return _layerHeight;
    }

    public Material GetMaterial()
    {
        return _mat;
    }

    public string GetName()
    {
        return _name;
    }

    private float _randomShift;
    
    private int _col;
    private int _row;
    private int _layerNum;
    private int _layerHeight;
    
    private Material _mat;
    
    private string _name;
}
