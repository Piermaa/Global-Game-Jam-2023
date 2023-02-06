using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectClass
{ 
    Alpha,Beta,None
}
public class Interactable : MonoBehaviour
{
    public ObjectClass objectRequired;
    public ObjectClass objectHolded;
    ObjectClass aux;
    public SpriteRenderer objectSprite;
    public Wire wire;
    public bool dontFillOnStart;
    public Sprite alphaSprite;
    public Sprite betaSprite;

    // Start is called before the first frame update
    void Start()
    {
        
        wire=GetComponentInParent<Wire>();
        objectSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        objectSprite.sprite = null;

        //SE ESTABLECE EL SPRITE AUTOMATICAMENTE AL INICIAR EL NIVEL DEPENDIENDO DE QUE SE INFORMO QUE CONTIENE EN EL INSPECTOR
        if (dontFillOnStart)
        {
            aux = objectHolded;
            objectHolded = ObjectClass.None;
        }
        switch (objectHolded)
        {
            case ObjectClass.None:
                objectSprite.sprite = null;
                break;
            case ObjectClass.Alpha:
                objectSprite.sprite = alphaSprite;
                break;
            case ObjectClass.Beta:
                objectSprite.sprite = betaSprite;
                break;
        }
    }

    public void Fill()
    {
        objectHolded=aux;
        switch (objectHolded)
        {
            case ObjectClass.None:
                objectSprite.sprite = null;
                break;
            case ObjectClass.Alpha:
                objectSprite.sprite = alphaSprite;
                break;
            case ObjectClass.Beta:
                objectSprite.sprite = betaSprite;
                break;
        }
    }

    /// <summary>
    /// Se llama cuando el jugador agarra el objeto en el slot
    /// </summary>
    /// <param name="objectSpriteRenderer"></param>
    /// <returns> Devuelve la clase de objeto que ahora tiene en la mano el jugador </returns>
    public ObjectClass GrabObject(SpriteRenderer objectSpriteRenderer)
    {
        objectSpriteRenderer.sprite = objectSprite.sprite;
        objectSprite.sprite = null;
        ObjectClass objectClass = objectHolded; //AUX
        objectHolded = ObjectClass.None;
        return objectClass;
    }



    /// <summary>
    /// El jugador coloca un objeto nuevo en el slot
    /// </summary>
    /// <param name="objectInserted"></param>
    /// <param name="objectsprite"></param>
    public void InsertObject(ObjectClass objectInserted,SpriteRenderer objectsprite)
    {
        SetInsertObjectValues(objectInserted, objectsprite);
        if (wire!=null)
        {
            wire.CheckSolution();
        }
        else if (BossFight.Instance != null && HasCorrectObject())
        {
            BossFight.Instance.TryPromotePhase();
        }
    }

    public void SetInsertObjectValues(ObjectClass objectInserted, SpriteRenderer objectsprite)
    {
        objectHolded = objectInserted;
        objectSprite.sprite = objectsprite.sprite;
        objectsprite.sprite = null;
        print(gameObject.name);
    }

    public bool HasCorrectObject()
    {
        return objectHolded == objectRequired;
    }
}
