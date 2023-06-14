using System;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    [SerializeField] private KeyColor keyColor;
    [SerializeField] private float radius = -0.5f;
    [SerializeField] private LayerMask socketLayer;

    public bool unlocked = false;

    private void Update()
    {
        CheckSocket();
    }

    public void CheckSocket()
    {
        Collider[] collider = Physics.OverlapSphere(gameObject.transform.position, radius, socketLayer);
        
        foreach (var item in collider)
        {
            KeySocket keySocket = item.GetComponent<KeySocket>();

            if (keySocket.keySocketColor == keyColor)
            {
                unlocked = true;
            }
        }

        if (unlocked && collider.Length == 0 )
            unlocked = false;
    }
}

public enum KeyColor
{
    BlueKey, OrangeKey
}