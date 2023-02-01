using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ObjectClass objectHolded;
    public GameObject grabbedObject;
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
        grabbedSprite = grabbedObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float speedAux=speed;
        if (x != 0 && y != 0)
        {
            //AL MOVERSE EN DIAGONAL IBA DEMASIADO RAPIDO ENTONCES BAJE LA VELOCIDAD EN CASO DE QUE VAYA EN DIAGONAL
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
            //SI INTERACTABLE CONTIENE ALGUN OBJETO Y EL JUGADOR NO TIENE NINGUNO EN LA MANO:
            if(!(interactable.objectHolded==ObjectClass.None) && objectHolded==ObjectClass.None)
            {
                grabbedObject.SetActive(true);
                objectHolded = interactable.GrabObject(grabbedSprite);
            }
            //SI EL JUGADOR SI TIENE UN ITEM Y EL INTERACTABLE NO CONTIENE NINGUNO
            else if(interactable.objectHolded == ObjectClass.None && !(objectHolded == ObjectClass.None))
            {
                grabbedObject.SetActive(false);
                interactable.InsertObject(objectHolded,grabbedSprite);
                objectHolded = ObjectClass.None;
            }
              
        }
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            //SE GUARDA EL OBJETO CON EL QUE SE PUEDE INTERACTUAR AL ENTRAR EN COLISION
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
