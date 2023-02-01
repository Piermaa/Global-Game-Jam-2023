using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    Transform player;
    PatrolEnemy[] enemies;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<PlayerHealth>().transform;
        enemies=FindObjectsOfType<PatrolEnemy>();
    }

    // Update is called once per frame
    public void Chase()
    {
        foreach (var e in enemies)
        {
            if (e.enemyState!=PatrolEnemy.EnemyState.Chase)
            {
                e.Chase(player);
            }
       
        }
    }

    public void PlayerHid()
    {
        if(!CheckDetection())
        {
            foreach (var e in enemies)
            {
                if(e.enemyState!=PatrolEnemy.EnemyState.Patrol)
                {
                    e.BeginPatrolling();
                }
               
            }
        }
    }

    bool CheckDetection()
    {
        foreach (var e in enemies)
        {
            if (e.playerDetection.playerInVision)
            {
                return true;
            }
           
        }

        return false;
    }
}
