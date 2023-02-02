using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    Transform detectionCone;
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
 
    // Start is called before the first frame update
    void Start()
    {
        detectionCone = transform.GetChild(0).transform;
        agent = GetComponent <NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis= false;
        playerDetection = GetComponentInChildren<PlayerDetection>();
        agent.SetDestination(waypoints[index].position);
    }

   
    // Update is called once per frame
    void Update()
    {
        
            Vector3 dirToLookAt = agent.destination;
            Vector3 diff = new Vector3(dirToLookAt.x, dirToLookAt.y) - transform.position;
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
        return Vector3.Distance(transform.position,agent.destination)<1f;


    }
    
    /// <summary>
    /// Se establece una posicion a investigar, se llama cuando se ve de lejos al jugador
    /// </summary>
    /// <param name="positionToInvestigate"></param>
    public void Investigate(Vector3 positionToInvestigate)
    {
        if (enemyState==EnemyState.Patrol|| enemyState == EnemyState.Waiting)
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
        enemyState = EnemyState.Patrol;
        //index = index < waypoints.Length ? index++ : 0;
        agent.SetDestination(waypoints[0].position);
    }

    public void Stun()
    {
        StartCoroutine(Stunned());
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

}
