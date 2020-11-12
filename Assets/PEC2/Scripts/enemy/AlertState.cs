using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    public bool firstTimeAlert;
    enemyAI myEnemy;
    float currentRotationTime = 0;
    public AlertState(enemyAI enemy)
    {
        myEnemy = enemy;
        firstTimeAlert = true;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.yellow;
        myEnemy.transform.rotation *= Quaternion.Euler(0f, Time.deltaTime * 360 * 1.0f / myEnemy.rotationTime, 0f);
        if (currentRotationTime > myEnemy.rotationTime || firstTimeAlert)
        {
            firstTimeAlert = false;
            currentRotationTime = 0;
            GoToPatrolState();
        }
        else
        {
            if(currentRotationTime > myEnemy.rotationTime) GoToPatrolState();

            RaycastHit hit;
            if (Physics.Raycast(
                new Ray(
                new Vector3(myEnemy.transform.position.x, myEnemy.transform.position.y - 4f, myEnemy.transform.position.z),
                myEnemy.transform.forward * 100f),
                out hit))
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    var distance = Vector2.Distance(myEnemy.transform.position, hit.collider.transform.position);
                    if (distance <= myEnemy.GetComponent<SphereCollider>().radius * 3f) GoToAttackState();
                }
            }
        }
        currentRotationTime += Time.deltaTime;
    }

    public void Impact()
    {
        GoToAttackState();
    }

    public void GoToAlertState() { }

    public void GoToAttackState()
    {
        myEnemy.currentState = myEnemy.attackState;
    }

    public void GoToPatrolState()
    {
        myEnemy.navMeshAgent.isStopped = false;
        myEnemy.currentState = myEnemy.patrolState;
    }

    public void OnTriggerEnter(Collider col) { }
    public void OnTriggerStay(Collider col) { }
    public void OnTriggerExit(Collider col) { }
}
