using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [SerializeField] private PuzzleManager _puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        _puzzleManager.CreatePuzzle();
    }
}
