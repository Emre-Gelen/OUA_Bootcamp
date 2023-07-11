using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerManager : MonoBehaviour
{
    private KeyDoorManager doorManager;

    private void Start()
    {
        doorManager = GetComponentInParent<KeyDoorManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        doorManager.CheckPlayerKeys(other);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
