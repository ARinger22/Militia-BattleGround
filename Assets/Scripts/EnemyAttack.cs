using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float damage = 30f;
    PlayerHealth target;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if(target) target.TakeDamage(damage);
    }
}
