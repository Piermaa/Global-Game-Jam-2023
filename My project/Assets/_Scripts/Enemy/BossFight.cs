using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public GameObject bossBarrier;
    [System.Serializable]
    public class Phase
    {
        public Interactable[] holdersToFill;
        public GameObject[] lightsToActivate;
    }
    public List<Phase> phases=new List<Phase>();
    public int phaseIndex;
    StunAbility playerStun;
    public List<LightEnemy> lights=new List<LightEnemy>();
    public Interactable[] sockets;
    LevelManager levelManager;
    public static BossFight Instance;
    public ParticleSystem dmgParticles;
    public Material dmgMaterial;
    Material material;
    public SpriteRenderer bossSprite;
    public SpriteRenderer shieldSprite;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        material = bossSprite.material;
        levelManager = LevelManager.Instance;
        StartCoroutine(SecondFrame());
        playerStun = FindObjectOfType<StunAbility>();
    }
    IEnumerator SecondFrame()
    {
        yield return new WaitForEndOfFrame();
        PhaseBegin();
      
    }

    void Update()
    {

    }

    public void TryPromotePhase()
    {

        switch (phaseIndex)
        {
            case 1:
                if (CheckInserteds()==1)
                {
                    SetBossVulnerable();
                }
                break;
            case 2:
                if (CheckInserteds() == 3)
                {
                    SetBossVulnerable();
                }
                break;
            case 3:
                if (CheckInserteds() == 6)
                {
                    SetBossVulnerable();
                }
                break;
        }
    }

    public void SetBossVulnerable()
    {
        bossBarrier.SetActive(false);
    }

    public void TriggerPhase()
    {
        StartCoroutine(WaitingToBeginPhase());
    }
    IEnumerator WaitingToBeginPhase()
    {
        bossSprite.material =dmgMaterial;
        dmgParticles.Play();
        yield return new WaitForSeconds(0.4f);
        bossSprite.material = material;

        yield return new WaitForSeconds(1);
        PhaseBegin();
    }
    public void PhaseBegin()
    {

        if (phaseIndex==3)
        {
            Time.timeScale = 0;
            levelManager.NextLevel();
            return;
        }
        bossBarrier.SetActive(true);
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
            foreach (var a in lights)
                a.InitAnimation();


        playerStun.stunCharged = true;
        phaseIndex++;
        levelManager.RespawnPlayer();
    }

    public int CheckInserteds()
    {
        int corrects=0;
        foreach (var s in sockets)
        {
            if (s.HasCorrectObject())
            {
                corrects++;
            }
        }
        return corrects;
    }
}
