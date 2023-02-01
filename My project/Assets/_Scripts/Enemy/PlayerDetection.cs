using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    MovementController2D movementController2D;
    PatrolEnemy enemy;
    public float fullDetectionDistance;
    // Start is called before the first frame update
    void Start()
    {
        movementController2D = GetComponentInParent<MovementController2D>();
        enemy=GetComponentInParent<PatrolEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
           
            if(Vector3.Distance(enemy.transform.position,other.transform.position)<=fullDetectionDistance)
            {
                //persecucion
                
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

    private void OnDrawGizmos()
    {
        Vector3 enemyCenter = transform.position + Vector3.up * 2.3f;
        Debug.DrawLine(enemyCenter,   enemyCenter + Vector3.down * 2   ,   Color.cyan);
    }
}
