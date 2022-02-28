using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Startinterface : MonoBehaviour
{
    public GameObject Guide;
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Guide.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {

        SceneManager.LoadScene(1);

    }
    public void QuitGame()
    {
        Application.Quit();

    }

    public void GuideGame()
    {
        if (Guide.activeSelf)
        {
            
            Guide.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }

        else if (!Guide.activeSelf)
        {
            Guide.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }

}
