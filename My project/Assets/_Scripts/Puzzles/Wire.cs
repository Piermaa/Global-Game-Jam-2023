using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wire : MonoBehaviour
{
    public Interactable[] interactables;
    public UnityEvent onPuzzleResolutionEvent=new UnityEvent();
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
                onPuzzleResolutionEvent.Invoke();

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
