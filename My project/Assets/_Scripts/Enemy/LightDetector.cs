using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    Transform rayCastOrigin;
    // Start is called before the first frame update
    void Start()
    {
        rayCastOrigin = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (NoWallsBetween(collision.transform))
            {
                EnemyManager.Instance.Chase();
            }
         
        }
    }

    public bool NoWallsBetween(Transform player)
    {
        Vector3 direction = player.position - rayCastOrigin.position;
        Ray2D ray = new Ray2D(rayCastOrigin.position, direction);

        Debug.DrawRay(rayCastOrigin.position, direction,
           Color.green, Time.deltaTime, true);

        RaycastHit2D raycastHit = Physics2D.Raycast(rayCastOrigin.position, direction);

        print(raycastHit.collider.name);
        return raycastHit.collider.name == "Player" || raycastHit.collider.tag == "Player"; 
    }
}
