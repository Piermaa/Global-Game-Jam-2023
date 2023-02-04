using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public static InteractableManager Instance;
    public Interactable[] intercatables;
    public PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Awake()
    {
        Instance = this;
    }


    public void LeftGrabbedObject()
    {   if(playerMovement.objectHolded.Equals(ObjectClass.Alpha) || playerMovement.objectHolded.Equals(ObjectClass.Beta))
        {
            foreach (var e in intercatables)
            {
                if (e.objectRequired.Equals(ObjectClass.None) && e.objectHolded.Equals(ObjectClass.None))
                {
                    e.InsertObject(playerMovement.objectHolded, playerMovement.grabbedSprite);
                }
            }
        }
    }

   
}
