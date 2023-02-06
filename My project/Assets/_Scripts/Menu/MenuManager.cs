using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject creditsCanvas;
    public GameObject howToPlayCanvas;

    private void Awake()
    {
        var animator=creditsCanvas.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("begin");
            print("began");
        }
        else 
        {
            creditsCanvas.SetActive(false);
        }
     
        howToPlayCanvas.SetActive(false);

    }
    private void Start()
    {
       
         
    }
    public void HowToPlay()
    {
        howToPlayCanvas.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Credits()
    {
        creditsCanvas.SetActive(true);
    }

    public void BackMenu()
    {
        creditsCanvas.SetActive(false);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    
}
