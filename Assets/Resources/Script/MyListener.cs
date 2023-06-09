using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    private SerialController serialController;
    public int command = 0;

    void Start()
    {
        serialController = GameObject.FindObjectOfType<SerialController>();

        if (serialController == null)
        {
            Debug.LogError("There is no SerialController object in the scene. Please add one.");
            return;
        }
    }

    void Update()
    {
        command = 0; 
    }

    public void OnMessageArrived(string msg)
    {
        Debug.Log("Received command: " + msg);
        command = int.Parse(msg);
        switch (msg.Trim())
        {
            case "1":
                command = 1;  // Move left
                break;
            case "2":
                command = 2;  // Move right
                break;
            case "3":
                command = 3;  // Jump
                break;
            case "4":
                command = 4;  // Interact
                break;
            default:
                Debug.LogError("Unknown command: " + msg);
                break;
        }
    }

    public void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
