using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    EnemyManager enemyManager;
    public bool playerInVision;
    MovementController2D movementController2D;
    PatrolEnemy enemy;
    public float fullDetectionDistance;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        movementController2D = GetComponentInParent<MovementController2D>();
        enemy=GetComponentInParent<PatrolEnemy>();
        enemyManager = EnemyManager.Instance;
        playerMovement = PlayerMovement.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
           
            if(Vector3.Distance(enemy.transform.position,other.transform.position)<=fullDetectionDistance)
            {
                //persecucion
                if(playerMovement.hideState!=PlayerMovement.PlayerHideState.Hiding)
                {
                    enemyManager.Chase();
                    print("Player is near");
                }
               
            }
            else
            {
                enemy.Investigate(other.transform.position);
                print("Investigate!!");
                //va a la pos del player
            }
            playerInVision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInVision = false;
        }
    }
    private void OnDrawGizmos()
    {

        //Vector3 enemyCenter = enemy.transform.position;
        //Debug.DrawLine(enemyCenter,   enemyCenter + Vector3.up * 3.2f   ,   Color.cyan);
    }
}
