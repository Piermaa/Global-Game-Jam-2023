using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    bool isClosed = true;
    bool mustClose;
    public Sprite[] openDoorAnimation;
    public float animTimeThreshold = 0.15f;
    public SpriteRenderer sr;
    public int state = 0;
    public int reverseState = 5;
    public float animTimer;
    public Sprite openSprite;
    public Sprite closedSprite;
    public Collider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > animTimer && isClosed == false)
        {
            if (state == 4)
            {
                collider.enabled = false;
                sr.sprite = openSprite;
                return;
            }
            sr.sprite = openDoorAnimation[state % openDoorAnimation.Length];
            state++;
            animTimer = Time.time + animTimeThreshold;
        }

        if (Time.time > animTimer && mustClose == true)
        {
            sr.sprite = openDoorAnimation[reverseState % openDoorAnimation.Length];
            reverseState--;
            animTimer = Time.time + animTimeThreshold;
            if(reverseState == 0)
            {
                sr.sprite = closedSprite;
            }
        }

    }

    public void OpenDoor()
    {
        isClosed = false;
    }
       
    public void CloseDoor()
    {
        this.gameObject.SetActive(true);
        isClosed = true;
        mustClose = true;
        collider.enabled = true;
    }
}
