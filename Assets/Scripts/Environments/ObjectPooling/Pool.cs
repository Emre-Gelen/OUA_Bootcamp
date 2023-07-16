using System;
using UnityEngine;

[Serializable]
public class Pool
{
    public PoolType poolType;
    public GameObject prefab;
    public int size;
}

public enum PoolType
{
    Arrow,
    Stone,
    FallingStoneType1
}