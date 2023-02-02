using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class EnemyManager : MonoBehaviour
{
    public TextMeshProUGUI chaseTimerText;
    float chaseTimer;
    public float chaseMaxTime=20;
    public static EnemyManager Instance;
    Transform player;
    PatrolEnemy[] enemies;
    bool chasing;

    StringBuilder stringBuilder;

    private void Awake()
    {
        stringBuilder= new StringBuilder();
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<PlayerHealth>().transform;
        enemies=FindObjectsOfType<PatrolEnemy>();
    }
    private void Update()
    {
        if (chasing)
        {
     
            chaseTimer = !CheckDetection() ? chaseTimer - Time.deltaTime : chaseMaxTime;
            DisplayTime((int)chaseTimer);
            if (chaseTimer<=0)
            {
                StopChase();
            }
        }
    }

    public void StopChase()
    {
        foreach (var e in enemies)
        {
            if (e.enemyState != PatrolEnemy.EnemyState.Patrol)
            {
                e.BeginPatrolling();
            }

        }
        chaseTimerText.gameObject.SetActive(false);
        chasing = false;
    }
    public void Chase()
    {
        chaseTimerText.gameObject.SetActive(true);
        chasing = true;
        foreach (var e in enemies)
        {
            if (e.enemyState!=PatrolEnemy.EnemyState.Chase)
            {
                e.Chase(player);
            }
 
        }
        chaseTimer = chaseMaxTime;
    }

    public void PlayerHid()
    {
        if(!CheckDetection())
        {
            StopChase();
        }
    }

    bool CheckDetection()
    {
        foreach (var e in enemies)
        {
            if (e.playerDetection.PlayerInVision())
            {
                return true;
            }
           
        }

        return false;
    }

    void DisplayTime(int time)
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(time);
        chaseTimerText.text = stringBuilder.ToString();
    }
}
