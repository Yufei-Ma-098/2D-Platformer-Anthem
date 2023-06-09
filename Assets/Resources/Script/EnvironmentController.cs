using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public Animator animator;
    public float detectionRange = 5f;
    public Transform player;
    private bool isPlayerInRange = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.Log("Player not found!");
        }
        else
        {
            Debug.Log("Player found!");
        }
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.Log("Player not found in Update!");
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                animator.SetBool("isMoving", true);
                Debug.Log("Player is in range. Setting isMoving to true.");
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                animator.SetBool("isMoving", false);
                Debug.Log("Player is out of range. Setting isMoving to false.");
            }
        }
    }

}
