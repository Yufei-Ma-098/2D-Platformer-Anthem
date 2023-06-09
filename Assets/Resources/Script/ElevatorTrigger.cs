using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ElevatorController elevator = GetComponentInParent<ElevatorController>();
            if (elevator != null)
            {
                collision.transform.SetParent(elevator.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ElevatorController elevator = GetComponentInParent<ElevatorController>();
            if (elevator != null)
            {
                collision.transform.SetParent(null);
            }
        }
    }
}
