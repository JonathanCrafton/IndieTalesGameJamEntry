using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text name;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlickerText());
        if (Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void StartGame()
    {
        SceneManager.LoadScene("Room", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator FlickerText()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            name.CrossFadeAlpha(0, 0.2f, false);
            yield return new WaitForSeconds(0.2f);
            name.CrossFadeAlpha(1, 0.2f, false);

        }
    }
    
}
