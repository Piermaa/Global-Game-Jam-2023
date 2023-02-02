using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{

    public enum EnemyState
    {
        Patrol,Investigate,Chase, Waiting
    }
    [Header("Player")]
    [HideInInspector] public PlayerDetection playerDetection;
    private Transform playerTransform;
    private float samePositionTimer;
  
    public float patrolSpeed;
    public float chaseSpeed;
    public float investigateSpeed;

    float chaseTimer;
    public float chaseRefreshCooldown=2f;

    [Header("Navigation")]
    public Transform[] waypoints;
    public EnemyState enemyState;
    private int index;
    private NavMeshAgent agent;
    private Vector3 investigationPosition = Vector3.zero;
    private Vector3 lastPos;
 
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


            Vector3 dirToLookAt = agent.destination;
            Vector3 diff = new Vector3(dirToLookAt.x, dirToLookAt.y) - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

            transform.position = new Vector3(transform.position.x, transform.position.y, 0); //SE MUEVE EN Z NO SE PORQUE NO COMO ARREGLARLO OAAA

            //Dependiendo de en que estado esta el enemigo, corrobora si a cada frame si esta en la posicion indicada
            switch (enemyState)
            {
                case EnemyState.Patrol:
                 
                    if (ReachedDestiny(waypoints[index].position))
                    {
                        index = index < 1 ? 1 : 0;
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
        if (Vector3.Distance( agent.destination,transform.position)<1)
        {
            print("Near");
        }
    }
    public bool ReachedDestiny(Vector3 pos)
    {

        //return movementController2D.pathLeftToGo.Count > 0 && Vector3.Distance(movementController2D.pathLeftToGo.Last(), transform.position) < 0.5f
        return Vector3.Distance(transform.position,pos)<0.6f;


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
       // transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
            agent.SetDestination(positionToInvestigate);
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
        enemyState = EnemyState.Patrol;
        index = index < waypoints.Length ? index++ : 0;
        agent.SetDestination(waypoints[index].position);
    }
   
}