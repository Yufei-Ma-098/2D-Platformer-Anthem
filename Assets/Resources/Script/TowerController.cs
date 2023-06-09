using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class TowerController : MonoBehaviour
{
    public GameObject Button;
    public Animator animator;
    public GameObject Player;
    private bool hasBeenCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player && !hasBeenCollected)
        {
            Button.SetActive(true);
            animator.SetBool("Lighting", false);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            Button.SetActive(false);
            animator.SetBool("Lighting", true);
        }
    }

    void Update()
    {
        if (Button != null && Button.activeSelf && Input.GetKeyDown(KeyCode.R) && !hasBeenCollected)
        {
            animator.SetTrigger("LightUp");

            Player.GetComponent<PlayerController>().InteractWithTower();

            Button.SetActive(false);

            hasBeenCollected = true;

            TowerVideoController videoController = FindObjectOfType<TowerVideoController>();
            if (videoController != null)
            {
                StartCoroutine(DelayedVideoPlay(videoController));
            }
            else
            {
                Debug.LogError("TowerVideoController not found in the scene");
            }
        }
    }

    IEnumerator DelayedVideoPlay(TowerVideoController videoController)
    {
        yield return new WaitForSeconds(3);  
        videoController.videoPlayer.loopPointReached += OnVideoEnd;  

        Player.GetComponent<PlayerController>().enabled = false;

        videoController.PlayVideo();
    }


    void OnVideoEnd(VideoPlayer vp)
    {
        vp.loopPointReached -= OnVideoEnd;  

        // Enable player controls
        Player.GetComponent<PlayerController>().enabled = true;
    }

}
