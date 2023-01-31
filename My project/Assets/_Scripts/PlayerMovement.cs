using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject grabbed;
    SpriteRenderer grabbedSprite;
    Rigidbody2D rb;
    public float speed;

    bool canGrab;
    bool canInsert;
    private Interactable interactable; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grabbedSprite = grabbed.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(x, y) * speed;

        Grab();

        //if (x!=0)
        //{
        //    if(y != 0)
        //    {
        //        rb.velocity = new Vector2(0, y) * speed;
        //    }
        //    else
        //    {
        //        rb.velocity = new Vector2(x, 0) * speed;

        //    }
        //}
        //if (y != 0)
        //{
        //    if (x != 0)
        //    {
        //        rb.velocity = new Vector2(x, 0) * speed;
        //    }
        //    else
        //    {
        //        rb.velocity = new Vector2(0, y) * speed;

        //    }
        //}

    }
    private void Grab()
    {
        if (Input.GetKeyDown(KeyCode.E)&&interactable!=null)
        {
            if(interactable.hasItem)
            {
                grabbed.SetActive(true);
                interactable.hasItem = false;
                grabbedSprite.sprite = interactable.itemSprite.sprite;
                interactable.itemSprite.sprite = null;
            }
            else
            {
                grabbed.SetActive(false);
                interactable.hasItem = true;
                interactable.itemSprite.sprite = grabbedSprite.sprite;
                grabbedSprite.sprite = null;
            }
              
        }
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            collision.TryGetComponent<Interactable>(out interactable);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            interactable = null;
        }
    }

}
