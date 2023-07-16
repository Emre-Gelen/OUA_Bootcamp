using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoom : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _objectsToDestroy.ForEach(currentObject => currentObject.SetActive(false));
        }
    }
}
