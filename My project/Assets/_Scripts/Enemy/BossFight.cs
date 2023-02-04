using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public int phase;

    public GameObject[] lights;
    public Interactable[] holders;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPhase()
    {
        levelManager.RespawnPlayer();
    }
}
