using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] public float Health { get { return _health; } set { _health = value; } }
    [SerializeField] private float _health = 1f;

    public void OnDamage(float damage)
    {
        if (Health - damage < 0) HandleDie();
        Health -= damage;
        //Damage animation
    }

    private void HandleDie()
    {
        Debug.Log("Died");
        Destroy(this.gameObject);
        //Die animation
        //Restart game
    }
}
