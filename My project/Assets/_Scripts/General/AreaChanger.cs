using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChanger : MonoBehaviour
{
    bool playerCol;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&!playerCol)
        {
            playerCol = true;
            LevelManager.Instance.ChangeArea(this.transform.position);
            //Destroy(this.gameObject);
        }
    }
}
