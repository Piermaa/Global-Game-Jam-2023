using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InteractableManager : MonoBehaviour

{
    public static InteractableManager Instance;
    public Interactable[] interactables;
    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Awake()
    {
        Instance = this;
    }


    public void LeftGrabbedObject()
    {   if(!playerMovement.objectHolded.Equals(ObjectClass.None))
        {
            Debug.Log("has element");
            foreach (var e in interactables)
            {
                if (e.objectRequired.Equals(ObjectClass.None) && e.objectHolded.Equals(ObjectClass.None) && e.GetObjectLifted().Equals(playerMovement.objectHolded))
                {
                    e.InsertObject(playerMovement.objectHolded, playerMovement.grabbedSprite);
                    playerMovement.setNormalValuesAfterDropItem();
                }
            }
        }
    }

   
}
