using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{

    Animator animator;
    Transform detectionCone;
    ParticleSystem dmgParticles;
    public enum EnemyState
    {
        Patrol,Investigate,Chase, Waiting, Stunned 
    }
    [Header("Player")]
    [HideInInspector] public PlayerDetection playerDetection;
    public Transform playerTransform;
    private float samePositionTimer;
  
    public float patrolSpeed;
    public float chaseSpeed;
    public float investigateSpeed;
    IEnumerator awaiting;
    bool mustWait; 
    float chaseTimer;
    public float chaseRefreshCooldown=2f;

    [Header("Navigation")]
    public Transform[] waypoints;
    public EnemyState enemyState;
    private int index;
    private NavMeshAgent agent;
    private Vector3 investigationPosition = Vector3.zero;
    private Vector3 lastPos;

    //animation
    public Sprite[] enemyWalkDown;
    public Sprite[] enemyWalkUp;
    public Sprite[] enemyWalkLeft;
    public Sprite[] enemyWalkRight;

    public float animTimeThreshold = 0.15f;

    public SpriteRenderer sr;

    public int state = 0;
    public float animTimer;


    // Start is called before the first frame update
    void Start()
    {
        dmgParticles = GetComponentInChildren<ParticleSystem>();
        detectionCone = transform.GetChild(0).transform;
        agent = GetComponent <NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis= false;
        playerDetection = GetComponentInChildren<PlayerDetection>();
        agent.SetDestination(waypoints[index].position);
        animator = GetComponent<Animator>();
    }

   
    // Update is called once per frame
    void Update()
    {

        Vector3 dirToLookAt = agent.destination;
        Vector3 diff = new Vector3(dirToLookAt.x, dirToLookAt.y) - transform.position;

        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            if (diff.x > 0)
            {
                if (enemyWalkRight != null && enemyWalkRight.Length < 0)
                {
                    if (Time.time > animTimer)
                    {
                        sr.sprite = enemyWalkRight[state % enemyWalkRight.Length];
                        state++;
                        animTimer = Time.time + animTimeThreshold;
                    }
                }
            }
            else
            {
                if (enemyWalkLeft != null && enemyWalkLeft.Length < 0)
                {
                    if (Time.time > animTimer)
                    {
                        sr.sprite = enemyWalkLeft[state % enemyWalkLeft.Length];
                        state++;
                        animTimer = Time.time + animTimeThreshold;
                    }
                }
            }
            //izq

        }
        else
        {
            if (diff.y > 0)
            {
                if (enemyWalkUp != null && enemyWalkUp.Length > 0)
                {
                    if (Time.time > animTimer)
                    {
                        sr.sprite = enemyWalkUp[state % enemyWalkUp.Length];
                        state++;
                        animTimer = Time.time + animTimeThreshold;
                    }
                }
            }
            else
            {
                if (enemyWalkDown != null && enemyWalkDown.Length > 0)
                {
                    if (Time.time > animTimer)
                    {
                        sr.sprite = enemyWalkDown[state % enemyWalkDown.Length];
                        state++;
                        animTimer = Time.time + animTimeThreshold;
                    }
                }


            }
        }

 
        diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            

            transform.position = new Vector3(transform.position.x, transform.position.y, 0); //SE MUEVE EN Z NO SE PORQUE NO COMO ARREGLARLO OAAA

        //Dependiendo de en que estado esta el enemigo, corrobora si a cada frame si esta en la posicion indicada
        switch (enemyState)
        {

            case EnemyState.Patrol:

                if (ReachedDestiny())
                {
                    index = index < waypoints.Length - 1 ? index + 1 : 0;
                    agent.SetDestination(waypoints[index].position);
                  
                }
                detectionCone.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                //print("arrived");
                break;

            case EnemyState.Investigate:
                if (ReachedDestiny())
                {
                    PlayerNotFound();
                }
                detectionCone.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                break;

            case EnemyState.Chase:
                agent.SetDestination(playerTransform.position);
                detectionCone.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                break;

        }

            samePositionTimer = lastPos == transform.position ? samePositionTimer + Time.deltaTime : 0;
            lastPos = transform.position;
    }
      
    
    public bool ReachedDestiny()
    {

        //return movementController2D.pathLeftToGo.Count > 0 && Vector3.Distance(movementController2D.pathLeftToGo.Last(), transform.position) < 0.5f
        return Vector3.Distance(transform.position,agent.destination)<0.5f;


    }
    
    /// <summary>
    /// Se establece una posicion a investigar, se llama cuando se ve de lejos al jugador
    /// </summary>
    /// <param name="positionToInvestigate"></param>
    public void Investigate(Vector3 positionToInvestigate)
    {
        if (enemyState==EnemyState.Patrol || enemyState == EnemyState.Waiting)
        {
            enemyState = EnemyState.Investigate;
            investigationPosition = positionToInvestigate;
            agent.SetDestination(positionToInvestigate);
            mustWait = true;
        }

    }

    public void Chase(Transform player)
    {
        playerTransform = player;
        agent.SetDestination(player.position);
        enemyState = EnemyState.Chase;
    }
    
    public void BeginPatrolling()
    {
        print("BeginPatrol");
        enemyState = EnemyState.Patrol;
        //index = index < waypoints.Length ? index++ : 0;
        agent.SetDestination(waypoints[0].position);
    }

    public void Stun()
    {
        dmgParticles.Play();
        agent.SetDestination(transform.position);
        enemyState = EnemyState.Stunned;
        animator.SetTrigger("Stun");
    }
    IEnumerator Stunned()
    {
        enemyState = EnemyState.Stunned;
        agent.SetDestination(transform.position);
        yield return new WaitForSeconds(3);
        BeginPatrolling();

    }

    void PlayerNotFound()
    {
        mustWait = false;
        StartCoroutine(Waiting());
    }
    IEnumerator Waiting()
    {
        enemyState = EnemyState.Waiting;
        yield return new WaitForSeconds(1);
        detectionCone.rotation = Quaternion.Euler(0f, 0f,  90);
        yield return new WaitForSeconds(1);
        detectionCone.rotation = Quaternion.Euler(0f, 0f, - 90);

        yield return new WaitForSeconds(1);
        if(!mustWait)
        {
            BeginPatrolling();
        }
      
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyState==EnemyState.Chase) 
        {
            collision.TryGetComponent<PlayerHealth>(out var player);
            player.TakeDamage(1);
        }
    }

}
