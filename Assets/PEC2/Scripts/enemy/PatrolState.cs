using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    enemyAI myEnemy;
    private int nextWayPoint = 0;
    private bool[] visitedWaypoint;

    public PatrolState(enemyAI enemy)
    {
        myEnemy = enemy;
        visitedWaypoint = new bool[myEnemy.waypoints.Length];
        for (int i = 0; i < visitedWaypoint.Length; i++)
        {
            visitedWaypoint[i] = false;
        }
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.green;
        myEnemy.navMeshAgent.destination = myEnemy.waypoints[nextWayPoint].position;
        var distance = Vector3.Distance(myEnemy.navMeshAgent.gameObject.transform.position, myEnemy.waypoints[nextWayPoint].position);
        if (distance <= 10f && !visitedWaypoint[nextWayPoint])
        {
            visitedWaypoint[nextWayPoint] = true;
            nextWayPoint++;
            if (nextWayPoint >= myEnemy.waypoints.Length)
            {
                nextWayPoint = 0;
                for (int i = 0; i < visitedWaypoint.Length; i++)
                {
                    visitedWaypoint[i] = false;
                }
            }
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
