using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float health = 100f;
    bool isDead = false;
    EnemyAi enemyAi;

    void Start()
    {
        enemyAi = GetComponent<EnemyAi>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
            CountEnemies countEnemies = FindObjectOfType<CountEnemies>();
            countEnemies.CountZero();
        }
        else
        {
            enemyAi.OnDamageTaken();
        }
    }
    void Die()
    {
        if(isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("Die");
    }
    public bool isDeadOrNot()
    {
        return isDead;
    }
}
