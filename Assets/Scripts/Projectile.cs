using Enemies;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (!enemy)
        {
            return;
        }
        
        enemy.TakeDamage();
        Destroy(gameObject);
    }
}