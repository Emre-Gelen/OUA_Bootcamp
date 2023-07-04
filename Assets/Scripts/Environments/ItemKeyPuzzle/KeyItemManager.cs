using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemManager : MonoBehaviour
{
    [SerializeField] private KeyItem keyColor;

    private void OnCollisionEnter(Collision collision)
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidingObject = other.gameObject;

        if (collidingObject.CompareTag("Player"))
        {
            collidingObject.GetComponent<PlayerKeyHolder>().GiveKey(keyColor);
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
