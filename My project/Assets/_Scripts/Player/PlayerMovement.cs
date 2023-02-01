using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ObjectClass objectHolded;
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
        float speedAux=speed;
        if (x != 0 && y != 0)
        {
            speed *= 0.8f;
        }
        rb.velocity = new Vector2(x, y) * speed;
        speed = speedAux;
        ObjectCarrying();
    }
    private void ObjectCarrying()
    {
        if (Input.GetKeyDown(KeyCode.E)&&interactable!=null)
        {
            if(interactable.hasItem)
            {
                grabbed.SetActive(true);
                objectHolded = interactable.GrabObject(grabbedSprite);
            }
            else
            {
                grabbed.SetActive(false);
                interactable.InsertObject(objectHolded,grabbedSprite);
                objectHolded = ObjectClass.None;
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
