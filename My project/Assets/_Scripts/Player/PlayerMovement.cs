using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform hidePosition;
    public enum PlayerHideState
    {
        Nothiding,Hiding,CanHide
    }
    public PlayerHideState hideState;
    public ObjectClass objectHolded;
    public GameObject grabbedObject;
    SpriteRenderer grabbedSprite;
    Rigidbody2D rb;
    public float speed;
    private Interactable interactable;
    public static PlayerMovement Instance;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;   
    }
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
        
       
        if (hideState!=PlayerHideState.Hiding)
        {
            rb.velocity = new Vector2(x, y) * speed;
        }
    
     
        ObjectCarrying();
    }
    private void ObjectCarrying()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if(interactable!=null)
            {
                //SI INTERACTABLE CONTIENE ALGUN OBJETO Y EL JUGADOR NO TIENE NINGUNO EN LA MANO:
                if (!(interactable.objectHolded == ObjectClass.None) && objectHolded == ObjectClass.None)
                {
                    grabbedObject.SetActive(true);
                    objectHolded = interactable.GrabObject(grabbedSprite);
                }
                //SI EL JUGADOR SI TIENE UN ITEM Y EL INTERACTABLE NO CONTIENE NINGUNO
                else if (interactable.objectHolded == ObjectClass.None && !(objectHolded == ObjectClass.None))
                {
                    grabbedObject.SetActive(false);
                    interactable.InsertObject(objectHolded, grabbedSprite);
                    objectHolded = ObjectClass.None;
                }
            }

            if (hideState == PlayerHideState.CanHide)
            {
                hideState = PlayerHideState.Hiding;
                EnemyManager.Instance.PlayerHid();
                transform.position = hidePosition.position;
            }
            else if (hideState == PlayerHideState.Hiding)
            {
                hideState = PlayerHideState.CanHide;
            }
           
              
        }
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Pickup":
                collision.TryGetComponent<Interactable>(out interactable);
                break;
            case "HideSpot":
                hidePosition = collision.transform;
                hideState = PlayerHideState.CanHide;
                break;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Pickup":
                interactable = null;

                break;
            case "HideSpot":
                hideState= PlayerHideState.Nothiding;
                break;

        }
    
    }

}
