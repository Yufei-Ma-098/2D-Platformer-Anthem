using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGateController : MonoBehaviour
{
    public float openSpeed = 3f;
    public Vector3 openPosition;

    private bool isOpening = false;

    void Start()
    {
        openPosition = transform.position + new Vector3(0, 13, 0);
    }

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.Lerp(transform.position, openPosition, Time.deltaTime * openSpeed);
        }
    }

    public void OpenGate()
    {
        isOpening = true;
    }
}
