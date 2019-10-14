using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private bool GameIsPaused = false;
    [SerializeField]
    private GameObject _pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    public void OpenPauseMenu()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = !GameIsPaused;
    }

    public void ClosePauseMenu()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = !GameIsPaused;
    }
}
