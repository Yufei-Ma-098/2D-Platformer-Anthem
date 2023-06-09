using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Vector3 topPosition = new Vector3(216.8949f, 121.2f, -4.653402f);
    public Vector3 bottomPosition = new Vector3(216.8949f, 15.4f, -4.653402f);
    public float speed = 5f;  // 升降机上升速度

    private bool isMoving = false;
    private bool isAtTop = false;
    private Vector3 targetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
            SetTargetPosition();
            StartCoroutine(MoveElevator());
        }
    }

    private void SetTargetPosition()
    {
        if (isAtTop)
        {
            targetPosition = bottomPosition;
        }
        else
        {
            targetPosition = topPosition;
        }
    }

    private IEnumerator MoveElevator()
    {
        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        isMoving = false;
        isAtTop = !isAtTop;
    }
}
