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
    public SpriteRenderer objectSprite;

    // Start is called before the first frame update
    void Start()
    {
        objectSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        //SE ESTABLECE EL SPRITE AUTOMATICAMENTE AL INICIAR EL NIVEL DEPENDIENDO DE QUE SE INFORMO QUE CONTIENE EN EL INSPECTOR
        switch (objectHolded)
        {
            case ObjectClass.None:
                objectSprite.sprite = null;
                break;

            case ObjectClass.Green:
                break;
            case ObjectClass.Red:
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
        objectHolded = objectInserted;
        objectSprite.sprite = objectsprite.sprite;
        objectsprite.sprite = null;
    }

    public bool HasCorrectObject()
    {
        return objectHolded == objectRequired;
    }
}
