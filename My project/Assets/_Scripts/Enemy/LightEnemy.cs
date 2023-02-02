using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : MonoBehaviour
{
    public enum Angles
    {
        _90,_180
    }
    public Angles angle;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (angle)
        {
            case Angles._90:
                animator.SetTrigger("90");
                break;
            case Angles._180:
                animator.SetTrigger("180");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
