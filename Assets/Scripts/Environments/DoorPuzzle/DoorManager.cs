using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    [SerializeField] private float doorDirection = 5f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private  List<DoorKey> doorKeys;

    private float localX;

    private void Awake()
    {
        localX = gameObject.transform.localPosition.x;
    }

    private void Update()
    {
        CheckKeyStatus();
    }

    private void CheckKeyStatus()
    {
        foreach (var item in doorKeys)
        {
            if (item.unlocked == false)
            {
                if (gameObject.transform.localPosition.x != localX)
                {
                    gameObject.transform.DOLocalMoveX(localX, duration);
                }

                return;
            }           
        }

        gameObject.transform.DOLocalMoveX(doorDirection, duration);
    }
}
