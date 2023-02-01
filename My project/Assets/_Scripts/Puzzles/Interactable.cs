using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectClass
{ 
    Red,Green,None
}
public class Interactable : MonoBehaviour
{
    public ObjectClass objectRequired;
    public ObjectClass objectHolded;
    public bool hasItem;
    public SpriteRenderer itemSprite;

    // Start is called before the first frame update
    void Start()
    {
        itemSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }



    /// <summary>
    /// Se llama cuando el jugador agarra el objeto en el slot
    /// </summary>
    /// <param name="objectSpriteRenderer"></param>
    /// <returns></returns>
    public ObjectClass GrabObject(SpriteRenderer objectSpriteRenderer)
    {
        objectSpriteRenderer.sprite = itemSprite.sprite;
        hasItem = false;
        itemSprite.sprite = null;
        ObjectClass objectClass = objectHolded;
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
        objectHolded = objectInserted;
        hasItem = true;
        itemSprite.sprite = objectsprite.sprite;
        objectsprite.sprite = null;
    }

    public bool HasCorrectObject()
    {
        return objectHolded == objectRequired;
    }
}
