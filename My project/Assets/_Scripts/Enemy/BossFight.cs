using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    [System.Serializable]
    public class Phase
    {
        public Interactable[] holdersToFill;
        public GameObject[] lightsToActivate;
    }
    public List<Phase> phases=new List<Phase>();
    public int phaseIndex;

    public List<LightEnemy> lights=new List<LightEnemy>();
    public Interactable[] holders;
    LevelManager levelManager;

    void Start()
    {
        levelManager = LevelManager.Instance;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextPhase();
        }
    }

    public void NextPhase()
    {
        foreach (var h in phases[phaseIndex].holdersToFill)
        {
            h.Fill();
        }
        foreach (var l in phases[phaseIndex].lightsToActivate)
        {
            l.SetActive(true);
            var le = l.GetComponent<LightEnemy>();
            lights.Add(le);
        }
        if (phaseIndex>0)
        {
           
            foreach (var a in lights)
            {
                a.InitAnimation();
            }
        }
       
        phaseIndex++;
        levelManager.RespawnPlayer();
    }
}
