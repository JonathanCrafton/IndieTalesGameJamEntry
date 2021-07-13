using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public GameManager gm;

    public Text prompt;

    CharacterController characterController;
    Vector3 moveDir = Vector3.zero;
    float rotationX = 0;

    private bool canMove = true;

    private AudioSource audio;
    public AudioClip pills;
    public AudioClip deathSound;

    public int pillUses = 0;
    public int mood = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.paused)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float curSpeedX = canMove ? (speed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (speed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDir.y;
            moveDir = (forward * curSpeedX) + (right * curSpeedY);

            characterController.Move(moveDir * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            if (prompt.text == "Press E to use pills")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    audio.PlayOneShot(pills);
                    pillUses++;
                    mood++;
                    if (pillUses > 1)
                    {
                        StartCoroutine(Death());
                    }
                }
            }
            if(prompt.text == "Press E to go to bed (skip day)")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    gm.newDay();
                }
            }

            if (prompt.text == "Press E to leave")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    prompt.text = "No, I'm not leaving";
                }
            }
            

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pills")
        {
            prompt.text = "Press E to use pills";
        }
        if(other.gameObject.tag == "Bed")
        {
            prompt.text = "Press E to go to bed (skip day)";
        }
        if(other.gameObject.tag == "Door")
        {
            prompt.text = "Press E to leave";
        }
        if(other.gameObject.tag == "Sandwich")
        {
            prompt.text = "Press E to eat";
        }
        if(other.gameObject.tag == "Yoga")
        {
            prompt.text = "My yoga ball. Supposed to help my posture.";
        }
        if (other.gameObject.tag == "Laptop")
        {
            prompt.text = "I don't even feel like playing my favorite game right now.";
        }
        if(other.gameObject.tag == "Picture")
        {
            prompt.text = "The picture is too much of a reminder.";
        }
        if(other.gameObject.tag == "Trash")
        {
            prompt.text = "This is how I feel right now.";
        }
        if(other.gameObject.tag == "Window")
        {
            prompt.text = "I'm not ready to go outside yet.";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Sandwich")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(other.gameObject);
                gm.sandwichCheck = false;
                mood++;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        prompt.text = "";
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        audio.PlayOneShot(deathSound);
        canMove = false;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("BadEnding");

    }
}
