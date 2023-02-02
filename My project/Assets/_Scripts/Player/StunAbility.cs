using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAbility : MonoBehaviour
{
    int stunCharge = 1;
    bool canStun;
    public PatrolEnemy enemyToStun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyToStun!=null)
        {
            print(enemyToStun);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (enemyToStun!=null)
            {
                stunCharge--;
                enemyToStun.Stun();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.TryGetComponent<PatrolEnemy>(out enemyToStun);
            if (enemyToStun.enemyState!=PatrolEnemy.EnemyState.Patrol)
            {
                enemyToStun = null;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.TryGetComponent<PatrolEnemy>(out enemyToStun);
            if (enemyToStun.enemyState != PatrolEnemy.EnemyState.Patrol)
            {
                enemyToStun = null;
            }
        }
    }
  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyToStun = null;
        }
    }
}
