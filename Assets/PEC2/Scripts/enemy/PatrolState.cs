using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    enemyAI myEnemy;
    private int nextWayPoint = 0;

    public PatrolState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.green;
        myEnemy.navMeshAgent.destination = myEnemy.waypoints[nextWayPoint].position;
        if (myEnemy.navMeshAgent.gameObject.transform.position.x == myEnemy.waypoints[nextWayPoint].position.x &&
            myEnemy.navMeshAgent.gameObject.transform.position.z == myEnemy.waypoints[nextWayPoint].position.z)
        {
            nextWayPoint++;
            if (nextWayPoint >= myEnemy.waypoints.Length) nextWayPoint = 0;
        }
    }

    public void Impact()
    {
        GoToAttackState();
    }

    public void GoToAlertState()
    {
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.alertState;
    }

    public void GoToAttackState()
    {
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.attackState;
    }

    public void GoToPatrolState(){}

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GoToAlertState();
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GoToAlertState();
        }
    }

    public void OnTriggerExit(Collider col){}
}
