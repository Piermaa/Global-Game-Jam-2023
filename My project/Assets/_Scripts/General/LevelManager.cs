using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [System.Serializable]
  
    public class Area
    {
        public GameObject enemyParent;
        public PatrolEnemy[] enemies;
    }
    EnemyManager enemyManager;
    public static LevelManager Instance;
    public int areaIndex;
    public List<Area> areas = new List<Area>();
    GameObject player;
    Vector3 playerCheckPoint;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        foreach (var area in areas)
        {
            if (area.enemyParent!=null)
            {
                print("Area");
                area.enemies = area.enemyParent.GetComponentsInChildren<PatrolEnemy>();
            }

            
        }
        player = PlayerMovement.Instance.gameObject;
        playerCheckPoint=player.transform.position;
        enemyManager = EnemyManager.Instance;
    }


    public void RespawnPlayer()
    {
        player.transform.position= playerCheckPoint;
        enemyManager.StopChase();
    }
    public void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ChangeArea(Vector3 playerPos)
    {
        playerCheckPoint= playerPos;
        if (areas[areaIndex].enemies!=null)
        {
            foreach (var e in areas[areaIndex].enemies)
            {
                Destroy(e.gameObject);
            }
        }

        areaIndex++;
        enemyManager.enemies = areas[areaIndex].enemies;
    }
}
