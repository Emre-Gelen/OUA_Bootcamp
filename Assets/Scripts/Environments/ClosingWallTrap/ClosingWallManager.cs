using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClosingWallManager : MonoBehaviour
{
    [SerializeField] private float _triggerAfterSeconds;

    [Space(10)]
    [SerializeField]private ClosingWall[] _closingWalls = new ClosingWall[2];

    void Start()
    {
        TimedTrigger.CreateTimer(_triggerAfterSeconds, HandleMove);
    }

    void HandleMove()
    {
        _closingWalls[0].HandleMove();
        _closingWalls[1].HandleMove();
    }
}
