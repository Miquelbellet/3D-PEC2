using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    enemyAI myEnemy;
    float actualTimeBetweenShoots = 0;

    public AttackState(enemyAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.red;
        actualTimeBetweenShoots += Time.deltaTime;
    }

    public void Impact() { }

    public void GoToAttackState() { }
    
    public void GoToPatrolState() { }

    public void GoToAlertState()
    {
        myEnemy.currentState = myEnemy.alertState;
    }

    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerStay(Collider col)
    {
        Vector3 lookDirection = col.transform.position - myEnemy.transform.position;
        myEnemy.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(lookDirection.x, 0, lookDirection.z));
        if(actualTimeBetweenShoots > myEnemy.timeBetweenShoots)
        {
            actualTimeBetweenShoots = 0;
            myEnemy.GetComponent<AudioSource>().PlayOneShot(myEnemy.shootSound);
            if(Random.value <= 0.75f) col.gameObject.GetComponent<HealthScript>().Hit(myEnemy.damageForce);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        myEnemy.alertState.firstTimeAlert = true;
        GoToAlertState();
    }
}
