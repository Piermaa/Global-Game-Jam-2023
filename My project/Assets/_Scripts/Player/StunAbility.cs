using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAbility : MonoBehaviour
{
    public bool stunCharged =true;
    bool canStunBoss;
    public PatrolEnemy enemyToStun;
    
    public AudioSource stunSound;
    // Start is called before the first frame update
    void Start()
    {
        stunCharged = true;
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
            if (enemyToStun!=null &&stunCharged)
            {
                stunCharged = false;
                enemyToStun.Stun();
                stunSound.Play();
            }
            if (canStunBoss && stunCharged)

            {
                stunCharged = false;
                //stunSound.Play();
                BossFight.Instance.PhaseBegin();

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
        if (collision.CompareTag("Boss"))
        {
            canStunBoss = true;
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
        if (collision.CompareTag("Boss"))
        {
            canStunBoss = false;
        }
    }
}
