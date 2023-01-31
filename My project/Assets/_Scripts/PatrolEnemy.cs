using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public Transform[] waypoints;
    int index;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.position = Vector3.MoveTowards(transform.position, waypoints[index].position, Time.deltaTime * speed);

        if (transform.position == waypoints[index].position)
        {
            index = index < 1 ? 1 : 0;   
        }
      
    }
   
}
