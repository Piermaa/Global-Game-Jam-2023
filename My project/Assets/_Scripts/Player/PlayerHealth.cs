using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int healthPoints;
    public UnityEvent onDeathEvent=new UnityEvent();

    private SpriteRenderer sprite;
    Material baseMaterial;
    public Material takingDamageMaterial;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        baseMaterial = sprite.material;
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
        healthPoints -= damage;
        StartCoroutine(TakingDamage());
        if(healthPoints<=0)
        {
            onDeathEvent.Invoke();
        }
        LevelManager.Instance.RespawnPlayer();
    }
}
