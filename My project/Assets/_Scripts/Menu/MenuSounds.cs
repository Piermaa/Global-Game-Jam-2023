using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSounds : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        var buttons = FindObjectsOfType<Button>();
        foreach (var b in buttons)
        {
            b.onClick.AddListener(audio.Play); ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
