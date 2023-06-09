using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderTalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;

    public Animator animator;

    public GameObject Player;
    public GameObject dialogueNPC;

    public ArduinoController arduinoController;

    public ElderDialogue elderDialogue;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Button.SetActive(true);
        animator.SetBool("Turning", true);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
        animator.SetBool("Turning", false);
    }

    void Start()
    {
        arduinoController = GameObject.FindObjectOfType<ArduinoController>();
        if (arduinoController == null)
        {
            Debug.LogError("Cannot find an instance of ArduinoController!");
        }
    }

    void Update()
    {
        if ((Button.activeSelf && arduinoController.rPressed) || Input.GetKey(KeyCode.R))
        {
            talkUI.SetActive(true);

            SpriteRenderer playerSpriteRenderer = Player.GetComponent<SpriteRenderer>();
            SpriteRenderer npcSpriteRenderer = dialogueNPC.GetComponent<SpriteRenderer>();

            if (Player.transform.position.x < dialogueNPC.transform.position.x)
            {
                playerSpriteRenderer.flipX = false;
                npcSpriteRenderer.flipX = false;
            }
            else
            {
                playerSpriteRenderer.flipX = true;
                npcSpriteRenderer.flipX = false;
            }
        }
    }


}
