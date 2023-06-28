using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyHolder : MonoBehaviour
{

    [SerializeField] public List<KeyItem> KeyItems;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum KeyItem
{
    Red = 1,
    Green = 2,
    Blue = 3,
    Yellow = 4
}