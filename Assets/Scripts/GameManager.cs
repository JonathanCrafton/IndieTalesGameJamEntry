using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timeText;
    public GameObject player;
    private int clock = 10;
    private int day = 1;

    private int goodDays = 0;

    public GameObject sandwichSpawn;
    public GameObject sandwich;
    public bool sandwichCheck;

    public GameObject ui;
    public GameObject pauseMenu;
    public bool paused = false;

    public Dialogue d;
    public GameObject playerSpawn;

    public GameObject goodDay;
    public GameObject badDay;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Clock());
        spawnSandwich();
        StartCoroutine(Dialogue());
        
    }

    IEnumerator Dialogue()
    {
        yield return new WaitForSeconds(1);
        d.startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Day: " + day + "\nTime: " + clock + "00";
        if (clock == 22)
        {
            newDay();
        }

        if(day > 5)
        {
            Ending();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
        
    }

    IEnumerator Clock()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            clock++;
        }
    }

    public void newDay()
    {
        day++;
        clock = 10;
        player.GetComponent<PlayerScript>().pillUses = 0;
        if(player.GetComponent<PlayerScript>().mood >= 2)
        {
            StartCoroutine(GoodDay());
        }
        else
        {
            StartCoroutine(BadDay());
        }
        player.GetComponent<PlayerScript>().mood = 0;
        if (!sandwichCheck)
        {
            spawnSandwich();
        }
    }

    IEnumerator GoodDay()
    {
        goodDay.SetActive(true);
        goodDays++;
        yield return new WaitForSeconds(5);
        player.transform.position = playerSpawn.transform.position;
        goodDay.SetActive(false);
    }

    IEnumerator BadDay()
    {
        badDay.SetActive(true);
        yield return new WaitForSeconds(5);
        player.transform.position = playerSpawn.transform.position;
        badDay.SetActive(false);
    }

    public void Ending()
    {
        if (goodDays >= 3)
        {
            SceneManager.LoadScene("GoodEnding");
        }
        else
        {
            SceneManager.LoadScene("BadEnding");
        }
    }

    private void spawnSandwich()
    {
        Instantiate(sandwich, sandwichSpawn.transform.position, Quaternion.identity);
    }

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
        ui.gameObject.SetActive(false);
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        ui.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
