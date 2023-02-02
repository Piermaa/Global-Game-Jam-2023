using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform rayCastOrigin;
    EnemyManager enemyManager;
    public bool playerInVision;
    PatrolEnemy enemy;
    public float fullDetectionDistance;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
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
            if (NoWallsBetween(other.transform))
            {
                Debug.Log("PlayerSeen");
                if (Vector3.Distance(enemy.transform.position, other.transform.position) <= fullDetectionDistance && playerMovement.hideState != PlayerMovement.PlayerHideState.Hiding)
                {
                    playerInVision = true;
                    //persecucion
                    
                        enemyManager.Chase();
                        print("Player is near");
                    

                }
                else
                {
                    enemy.Investigate(other.transform.position);
                    print("Investigate!!");
                    //va a la pos del player
                }
             
            }
       
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

    public bool NoWallsBetween(Transform player)
    {

        Vector3 direction = player.position - rayCastOrigin.position;
        Ray2D ray = new Ray2D(rayCastOrigin.position, direction);

        Debug.DrawRay(rayCastOrigin.position, direction,
           Color.green, Time.deltaTime, true);

        RaycastHit2D raycastHit= Physics2D.Raycast(rayCastOrigin.position,direction);
       
        
        
        return raycastHit.collider.tag=="Player" && enemy.enemyState!=PatrolEnemy.EnemyState.Stunned;
            
        
    }
       
    
}
