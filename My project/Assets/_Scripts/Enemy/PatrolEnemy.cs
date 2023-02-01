using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,Investigate,Chase
    }
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

    private void Awake()
    {
        movementController2D = GetComponent<MovementController2D>();
   
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitialPatrolling());
    }

    IEnumerator InitialPatrolling()
    {
        yield return new WaitForEndOfFrame();
        movementController2D.GetMoveCommand(waypoints[index].position);
    }
    // Update is called once per frame
    void Update()
    {

        //transform.position = Vector3.MoveTowards(transform.position, waypoints[index].position, Time.deltaTime * speed);

        //Dependiendo de en que estado esta el enemigo, corrobora si a cada frame si esta en la posicion indicada
        switch (enemyState)
        {
            case EnemyState.Patrol:
                if (transform.position == waypoints[index].position)
                {
                    index = index < 1 ? 1 : 0;
                    movementController2D.GetMoveCommand(waypoints[index].position);
                    print("arrived");
                }
                break;

            case EnemyState.Investigate:
                if (transform.position==investigationPosition || transform.position == waypoints[index].position)
                {
                    BeginPatrolling();
                }
                break;
        }

      
      
    }
    private void LateUpdate()
    {
        //POR SI SE BUGEUA Y SE QUEDA QUIETO
        samePositionTimer = lastPos == transform.position ? samePositionTimer + Time.deltaTime : 0;
        if (samePositionTimer>=1)
        {
            BeginPatrolling();
        }
        lastPos = transform.position;
        
    }
    /// <summary>
    /// Se establece una posicion a investigar, se llama cuando se ve de lejos al jugador
    /// </summary>
    /// <param name="positionToInvestigate"></param>
    public void Investigate(Vector3 positionToInvestigate)
    {
        //if (enemyState==EnemyState.Patrol)
        {
            enemyState = EnemyState.Investigate;
            investigationPosition = positionToInvestigate;
            movementController2D.GetMoveCommand(new Vector2(positionToInvestigate.x, positionToInvestigate.y));
        }

    }

    public void Chase()
    {
        
    }
    
    void BeginPatrolling()
    {
        enemyState = EnemyState.Patrol;
        index = index < waypoints.Length ? index++ : 0;
        movementController2D.GetMoveCommand(waypoints[index].position);
    }
   
}
