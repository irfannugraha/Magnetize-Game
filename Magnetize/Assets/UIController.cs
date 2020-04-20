using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeBtn;
    public GameObject levelClearTxt;
    public Scene currActiveScene;

    // Start is called before the first frame update
    void Start()
    {
        currActiveScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Pause_Game();
        }
    }

    public void Pause_Game(){
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void Resume_Game(){
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void Restart_Game(){
        Time.timeScale = 1;
        SceneManager.LoadScene(currActiveScene.name);
    }

    public void End_Game(){
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        resumeBtn.SetActive(false);
        levelClearTxt.SetActive(true);
    }

}
