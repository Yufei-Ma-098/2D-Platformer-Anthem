using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoormanTalkButton : MonoBehaviour
{
    public GameObject Button;
    public TowerVideoController towerVideoController;

    public Animator animator;

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

    void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            towerVideoController.PlayVideo();
        }
    }
}
