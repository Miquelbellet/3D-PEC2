using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IEnemyState currentState;
    
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform[] waypoints;

    public Light myLight;
    public AudioClip explosionClip;
    public GameObject explosionPS;
    public AudioClip shootSound;
    public float life;
    public float timeBetweenShoots;
    public float damageForce;
    public float rotationTime;
    public float shootHeight;
    public GameObject waypointsParent;

    private GameObject player;
    private GameObject gameManager;
    private bool dead;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);

        currentState = patrolState;
        navMeshAgent = GetComponent<NavMeshAgent>();
        waypoints = new Transform[waypointsParent.transform.childCount];
        for (var i = 0; i < waypointsParent.transform.childCount; i++)
        {
            waypoints[i] = waypointsParent.transform.GetChild(i);
        }
    }

    void Update()
    {
        currentState.UpdateState();
        if (life < 0 && !dead)
        {
            dead = true;
            player.GetComponent<ExtraItemsScript>().DropRandomItem(transform.position);
            if(Random.value > 0.5f) gameManager.GetComponent<UIScript>().PlusKeys();
            GetComponent<AudioSource>().PlayOneShot(explosionClip);
            var explosion = Instantiate(explosionPS, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(explosion, 5f);
            Destroy(gameObject, 1f);
        }
    }

    public void Hit(float damage)
    {
        life -= damage;
        currentState.Impact();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!dead) currentState.OnTriggerEnter(col);
    }

    private void OnTriggerStay(Collider col)
    {
        if (!dead) currentState.OnTriggerStay(col);
    }

    private void OnTriggerExit(Collider col)
    {
        if(!dead) currentState.OnTriggerExit(col);
    }
}
