using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Sprite[] playerWalkDown;
    public Sprite[] playerWalkUp;
    public Sprite[] playerWalkLeft;
    public Sprite[] playerWalkRight;

    public float animTimeThreshold = 0.15f;

    //private Player player;

    public SpriteRenderer sr;

    public int state = 0;
    public float animTimer;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Caminata Abajo  ? 
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if(Time.time > animTimer)
            {
                sr.sprite = playerWalkDown[state % playerWalkDown.Length];
                state++;
                animTimer = Time.time + animTimeThreshold;
            }
        }

        //Caminata Arriba  ? 
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (Time.time > animTimer)
            {
                sr.sprite = playerWalkUp[state % playerWalkDown.Length];
                state++;
                animTimer = Time.time + animTimeThreshold;
            }
        }

        //Caminata Derecha  ? 
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (Time.time > animTimer)
            {
                sr.sprite = playerWalkRight[state % playerWalkDown.Length];
                state++;
                animTimer = Time.time + animTimeThreshold;
            }
        }

        //Caminata Izquierda  ? 
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (Time.time > animTimer)
            {
                sr.sprite = playerWalkLeft[state % playerWalkDown.Length];
                state++;
                animTimer = Time.time + animTimeThreshold;
            }
        }

    }
}
