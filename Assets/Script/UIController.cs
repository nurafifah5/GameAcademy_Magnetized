using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeBtn;
    public GameObject continueBtn;
    public GameObject levelClearTxt;
    public GameObject crashTxt;
    private Scene currActiveScene;
    private string activeSceneName;

    // Start is called before the first frame update
    void Start()
    {
        currActiveScene = SceneManager.GetActiveScene();
        activeSceneName = currActiveScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        if (activeSceneName == "MainScene")
        {
            SceneManager.LoadScene("level 2");
        }
        else if (activeSceneName == "level 2")
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(currActiveScene.name);
        SceneManager.LoadScene("MainScene");
        
    }

    public void endGame()
    {
        Debug.Log(activeSceneName);
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        resumeBtn.SetActive(false);
        continueBtn.SetActive(true);
        levelClearTxt.SetActive(true);
    }

    public void crashGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        resumeBtn.SetActive(false);
        crashTxt.SetActive(true);
    }
}
