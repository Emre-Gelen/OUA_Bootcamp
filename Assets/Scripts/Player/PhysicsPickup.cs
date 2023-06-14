
using UnityEngine;
using UnityEngine.Events;

public class PhysicsPickup : MonoBehaviour
{
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private float radius = 1f;
    [SerializeField] private PlayerInputs playerInputs;
    private Rigidbody currentRigidbody;
    private Collider currentCollider;


    // Update is called once per frame
    void Update()
    {

        if (playerInputs.IsPickup())
        {
            Collider[] collider = Physics.OverlapSphere(gameObject.transform.position, radius, PickupMask);            

            foreach (var item in collider)
            {
                if (currentRigidbody)
                    return;

                currentRigidbody = item.GetComponent<Rigidbody>();
                currentCollider = item.GetComponent<Collider>();
                
                currentRigidbody.isKinematic = true;
                currentCollider.enabled = false;
            }

            if(!currentRigidbody)
                playerInputs.ChangePickupStatus();            
        }        
                

        if (currentRigidbody && !playerInputs.IsPickup())
        {
            currentRigidbody.isKinematic = false;
            currentCollider.enabled = true;

            currentRigidbody = null;
            currentCollider = null;
        }


        if (currentRigidbody)
        {
            currentRigidbody.position = gameObject.transform.position;
            currentRigidbody.rotation = gameObject.transform.rotation;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }
}
