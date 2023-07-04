using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorManager : MonoBehaviour
{

    [SerializeField] private float doorDirection = 5f;
    [SerializeField] private float duration = 1f;


    [SerializeField] private List<KeyItem> requiredKeys;
    [SerializeField] private bool unlocked;

    private float localX;

    private void Awake()
    {
        localX = gameObject.transform.localPosition.x;
    }

    private void Update()
    {
        CheckKeyStatus();
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    public void CheckPlayerKeys(Collider collider)
    {
        GameObject collidingObject = collider.gameObject;

        if (collidingObject.CompareTag("Player"))
        {
            List<KeyItem> playerKeys = collidingObject.GetComponent<PlayerKeyHolder>().KeyItems;
            int matchedKeys = 0;
            foreach (KeyItem playerKey in playerKeys)
            {
                foreach (KeyItem requiredKey in requiredKeys)
                {
                    if (playerKey == requiredKey)
                    {
                        matchedKeys++;
                    }
                }
            }
            if (matchedKeys >= requiredKeys.Count)
                unlocked = true;
        }
    }

    private void CheckKeyStatus()
    {
            if (unlocked == false)
            {
                if (gameObject.transform.localPosition.x != localX)
                {
                    gameObject.transform.DOLocalMoveX(localX, duration);
                }

                return;
            }

        gameObject.transform.DOLocalMoveX(doorDirection, duration);
    }
}
