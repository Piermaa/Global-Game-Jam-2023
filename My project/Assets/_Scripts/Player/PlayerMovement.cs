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
    public SpriteRenderer grabbedSprite;
    Rigidbody2D rb;
    public float normalSpeed;
    public float holdingObjectSpeed;
    float speed;
    private Interactable interactable;
    public static PlayerMovement Instance;

    Vector2 move;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;   
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grabbedSprite = grabbedObject.GetComponent<SpriteRenderer>();
        speed= normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {   
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        
        ObjectCarrying();
    }
    private void FixedUpdate()
    {
       if (hideState != PlayerHideState.Hiding)
        {
            rb.MovePosition(rb.position+move*speed*Time.fixedDeltaTime);
        }
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
                    speed = holdingObjectSpeed;
                }
                //SI EL JUGADOR SI TIENE UN ITEM Y EL INTERACTABLE NO CONTIENE NINGUNO
                else if (interactable.objectHolded == ObjectClass.None && !(objectHolded == ObjectClass.None))
                {
                    grabbedObject.SetActive(false);
                    interactable.InsertObject(objectHolded, grabbedSprite);
                    objectHolded = ObjectClass.None;
                    speed = normalSpeed;
                   
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
