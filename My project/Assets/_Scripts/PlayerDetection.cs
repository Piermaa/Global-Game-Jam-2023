using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    PatrolEnemy enemy;
    public float fullDetectionDistance;
    // Start is called before the first frame update
    void Start()
    {
     enemy=GetComponentInParent<PatrolEnemy>();
        print(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           
            if(Vector3.Distance(enemy.transform.position,other.transform.position)<=fullDetectionDistance)
            {
                //persecucion
                print("Player is near");
            }
            else
            {
                //va a la pos del player
            }
        }
    }
}
