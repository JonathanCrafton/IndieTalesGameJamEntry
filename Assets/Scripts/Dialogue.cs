using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text dialogue;
    public Text respondText;
    public int num = 1;
    public GameManager gm;
    public GameObject ui;
    public GameObject dialogueBox;
    public PlayerScript p;
    public AudioClip door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respond()
    {
        num++;
        nextDialogue();

    }

    public void startDialogue()
    {
        p.GetComponent<AudioSource>().PlayOneShot(door);
        gm.paused = true;
        Time.timeScale = 0;
        ui.gameObject.SetActive(false);
        dialogueBox.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        nextDialogue();
    }

    void nextDialogue()
    {
        if(num == 1)
        {
            dialogue.text = "Hey, it's been a couple of weeks since you left your room. You ok in there?";
            respondText.text = "I will be";

        }
        else if(num == 2)
        {
            dialogue.text = "Do you need to talk about what happened?";
            respondText.text = "Not really";

        }
        else if (num == 3)
        {
            dialogue.text = "I understand. Take your time. I left you a sandwich on the dresser. See it?";
            respondText.text = "Yeah I saw it.";

        }
        else if(num== 4)
        {
            dialogue.text = "You need to eat. I'll leave you a new one every day. I hope it helps you feel better.";
            respondText.text = "Thanks";

        }
        else if (num == 5)
        {
            dialogue.text = "Ok. I'll leave you alone. I'll check up on you in a few days, give you time to cope. Remember your meds and to eat.";
            respondText.text = "Ok. Bye.";

        }
        else
        {
            quitDialogue();
        }
    }
    void quitDialogue()
    {
        gm.paused = false;
        Time.timeScale = 1;
        ui.gameObject.SetActive(true);
        dialogueBox.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
