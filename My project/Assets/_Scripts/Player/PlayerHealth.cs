using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    ParticleSystem dmgParticles;
    public int healthPoints;
    public UnityEvent onDeathEvent=new UnityEvent();
    private SpriteRenderer sprite;
    Material baseMaterial;
    public Material takingDamageMaterial;
    private PlayerMovement playerMovement;
    public AudioSource takingDamageSound;
    [SerializeField] Sprite[] healthSprites;
    [SerializeField] Image healthSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 3;
        sprite = GetComponent<SpriteRenderer>();
        baseMaterial = sprite.material;
        playerMovement = GetComponent<PlayerMovement>();
        dmgParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TakingDamage()
    {
        sprite.material=takingDamageMaterial;
        yield return new WaitForSeconds(0.5f);
        sprite.material=baseMaterial;
    }

    public void TakeDamage(int damage)
    {
        takingDamageSound.Play();
        dmgParticles.Play();
        healthPoints -= damage;
        
        StartCoroutine(TakingDamage());
        if (healthPoints <= 0)
        {
            onDeathEvent.Invoke();
        }
        else {
            healthSpriteRenderer.sprite = healthSprites[3 - healthPoints];
        }
        Debug.Log("Player damaged!");
        playerMovement.DropItemWhenDamaged();
        LevelManager.Instance.RespawnPlayer();
    }
}
