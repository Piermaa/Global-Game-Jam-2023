using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Interactable[] interactables;
    // Start is called before the first frame update
    void Start()
    {
        interactables = GetComponentsInChildren<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CheckWire())
            {
                print("Todo ok");

            }
           
        }
    }

    public bool CheckWire()
    {
        foreach (var i in interactables)
        {
            if (!i.HasCorrectObject())
            {
                return false;
            }
        }

        return true;
    }
}
