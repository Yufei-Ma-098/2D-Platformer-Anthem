using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;
    private Vector3 offset;

    // 使用Awake来在游戏开始时获取对PlayerController的引用
    void Awake()
    {
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Player game object is not assigned in the ShadowController script.");
        }
    }

    void Update()
    {
        if (playerController != null)
        {
            transform.position = player.transform.position + playerController.ShadowPositionOffset;

            transform.rotation = player.transform.rotation;
        }
        else
        {
            Debug.LogError("Unable to access the PlayerController component.");
        }
    }
}
