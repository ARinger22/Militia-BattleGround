using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;
    [SerializeField] float chaseRange = 5f;
    NavMeshAgent agent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth enemyHealth;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.isDeadOrNot()) {
            enabled = false;
            agent.enabled = false;
            return;
        }
        if(target == null) return;
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            ChaseTarget();
        }
    }

    private void SetDestination()
    {
        if(target != null)
        {
            Vector3 targetVector = target.transform.position;
            agent.SetDestination(targetVector);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= agent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= agent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        agent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }
}
