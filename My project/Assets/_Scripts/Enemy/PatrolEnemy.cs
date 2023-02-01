using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    public enum EnemyState
    {
        Patrol,Investigate,Chase, Waiting
    }
    public PlayerDetection playerDetection;
    Transform playerTransform;
    Vector3 lastPos;
    float samePositionTimer;
    Vector3 investigationPosition =Vector3.zero;
    public EnemyState enemyState;
    MovementController2D movementController2D;
    public Transform[] waypoints;
    int index;
    public float patrolSpeed;
    public float chaseSpeed;
    public float investigateSpeed;

    float chaseTimer;
    public float chaseRefreshCooldown=2f;

    Vector3 playerPosition;
    private void Awake()
    {
        movementController2D = GetComponent<MovementController2D>();
      
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent <NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis= false;
        playerDetection = GetComponent<PlayerDetection>();


        agent.SetDestination(waypoints[index].position);
    }

   
    // Update is called once per frame
    void Update()
    {
        {
            //transform.position = Vector3.MoveTowards(transform.position, waypoints[index].position, Time.deltaTime * speed);

            //Dependiendo de en que estado esta el enemigo, corrobora si a cada frame si esta en la posicion indicada
            switch (enemyState)
            {
                case EnemyState.Patrol:
                    if (ReachedDestiny(waypoints[index].position))
                    {
                        index = index < 1 ? index++ : 0;
                        agent.SetDestination(waypoints[index].position);
                        //movementController2D.GetMoveCommand(waypoints[index].position);
                    }
                    //print("arrived");
                    break;

                case EnemyState.Investigate:
                    if (ReachedDestiny(investigationPosition))
                    {
                        BeginPatrolling();
                    }
                    break;

                case EnemyState.Chase:
                    if (ReachedDestiny(investigationPosition))
                    {
                        investigationPosition = playerTransform.position;
                        agent.SetDestination(investigationPosition);
                        //movementController2D.GetMoveCommand(investigationPosition);

                    }
                    break;
            }

            samePositionTimer = lastPos == transform.position ? samePositionTimer + Time.deltaTime : 0;
            lastPos = transform.position;
        }

    }
    public bool ReachedDestiny(Vector3 pos)
    {

        //return movementController2D.pathLeftToGo.Count > 0 && Vector3.Distance(movementController2D.pathLeftToGo.Last(), transform.position) < 0.5f;

        return Vector3.Distance(transform.position,pos)<1f;


    }
    private void LateUpdate()
    {
        //POR SI SE BUGUEA Y SE QUEDA QUIETO
        //samePositionTimer = lastPos == transform.position ? samePositionTimer + Time.deltaTime : 0;
        //if (samePositionTimer>=1.5f && (enemyState!=EnemyState.Waiting))
        //{
        //    BeginPatrolling();
        //}
        //lastPos = transform.position;
        
    }
    /// <summary>
    /// Se establece una posicion a investigar, se llama cuando se ve de lejos al jugador
    /// </summary>
    /// <param name="positionToInvestigate"></param>
    public void Investigate(Vector3 positionToInvestigate)
    {
        if (enemyState==EnemyState.Patrol)
        {
            enemyState = EnemyState.Investigate;
            investigationPosition = positionToInvestigate;
            movementController2D.GetMoveCommand(new Vector2(positionToInvestigate.x, positionToInvestigate.y));
        }

    }

    public void Chase(Transform player)
    {
        
        playerTransform = player;
        playerPosition =playerTransform.position;
        enemyState = EnemyState.Chase;
    }
    
    public void BeginPatrolling()
    {
        enemyState = EnemyState.Patrol;
        index = index < waypoints.Length ? index++ : 0;
        agent.SetDestination(waypoints[index].position);
    }
   
}
