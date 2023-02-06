using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Transform down, left,right,up;

    public Transform holdedObject;
    public SpriteRenderer objectHoldedSprite;

    public Sprite[] playerWalkDown;
    public Sprite[] playerWalkUp;
    public Sprite[] playerWalkLeft;
    public Sprite[] playerWalkRight;

    public Sprite[] playerWalkDownH;
    public Sprite[] playerWalkUpH;
    public Sprite[] playerWalkLeftH;
    public Sprite[] playerWalkRightH;

    public Sprite[] actualAnim; 

    public float animTimeThreshold = 0.15f;

    //private Player player;

    public SpriteRenderer sr;

    public int state = 0;
    public float animTimer;
    ObjectClass objectHolded;
    PlayerMovement move;
    KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
        move = PlayerMovement.Instance;
        objectHolded = move.objectHolded;
        sr = GetComponent<SpriteRenderer>();
        actualAnim = playerWalkDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > animTimer)
        {
            sr.sprite = actualAnim[state % actualAnim.Length];
            state++;
            animTimer = Time.time + animTimeThreshold;
        }
    }
    private void OnGUI()
    {
        if (Input.anyKey)
        {
            KeyCode k = Event.current.keyCode;
            print(k);
            //if (k != key || key == KeyCode.None)
            {
              
                switch (k)
                {
                    case KeyCode.DownArrow:
                    case KeyCode.S:
                        actualAnim = move.objectHolded==ObjectClass.None? playerWalkDown : playerWalkDownH;
                        holdedObject.position = down.position;
                        objectHoldedSprite.sortingOrder = 2;
                        break;

                    case KeyCode.UpArrow:
                    case KeyCode.W:
                        actualAnim = move.objectHolded == ObjectClass.None ? playerWalkUp: playerWalkUpH;
                        holdedObject.position = down.position;
                        objectHoldedSprite.sortingOrder = -1;
                        break;

                    case KeyCode.LeftArrow:
                    case KeyCode.A:
                        objectHoldedSprite.sortingOrder = -1;
                        actualAnim = move.objectHolded == ObjectClass.None ? playerWalkLeft:playerWalkLeftH;
                        holdedObject.position = left.position;

                        break;

                    case KeyCode.RightArrow:
                    case KeyCode.D:
                        objectHoldedSprite.sortingOrder = -1;
                        holdedObject.position = right.position;
                        actualAnim = move.objectHolded == ObjectClass.None ? playerWalkRight: playerWalkRightH;
                        break;

                }
             
            }
        }
     
    }

}
