using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float health = 100f;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            if(gameObject)  Destroy(gameObject);
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
