using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class ArduinoController : MonoBehaviour
{
    SerialPort sp;
    Thread readThread;
    bool isReading = false;
    private string receivedCommand = string.Empty;
    private string buffer = string.Empty;
    private readonly object lockObject = new object();

    public static string COMPort = "/dev/tty.usbmodem1101";
    public static int baudRate = 115200;

    public bool leftPressed = false;
    public bool rightPressed = false;
    public bool jumpPressed = false;
    public bool rPressed = false;



    void Start()
    {
        sp = new SerialPort(COMPort, baudRate);
        sp.Open();
        sp.ReadTimeout = 10;
        sp.Parity = Parity.None;
        sp.StopBits = StopBits.One;
        sp.DataBits = 8;
        sp.Handshake = Handshake.None;
        sp.RtsEnable = true;

        Debug.Log("Serial port opened: " + sp.IsOpen);

        if (sp.IsOpen)
        {
            isReading = true;
            readThread = new Thread(() => ReadSerialData(sp));
            readThread.Start();
        }
    }

    void Update()
    {
        string command = string.Empty;
        lock (lockObject)
        {
            command = receivedCommand;
            receivedCommand = string.Empty;
        }

        // reset commands
        leftPressed = false;
        rightPressed = false;
        jumpPressed = false;
        rPressed = false;

        switch (command)
        {
            case "LEFT":
                leftPressed = true;
                break;
            case "RIGHT":
                rightPressed = true;
                break;
            case "JUMP":
                jumpPressed = true;
                break;
            case "DIALOGUE":
                rPressed = true;
                break;
        }
    }



    void OnDestroy()
    {
        isReading = false;
        if (readThread != null && readThread.IsAlive)
        {
            readThread.Join();
        }

        if (sp != null && sp.IsOpen)
        {
            sp.Close();
        }
    }

    void ReadSerialData(SerialPort sp)
    {
        while (isReading)
        {
            try
            {
                while (sp.BytesToRead > 0)
                {
                    string data = sp.ReadExisting();
                    buffer += data;

                    int newlineIndex = buffer.IndexOf('\n');
                    while (newlineIndex > -1)
                    {
                        string command = buffer.Substring(0, newlineIndex).Trim();
                        buffer = buffer.Substring(newlineIndex + 1);

                        //Debug.Log("Received command from Arduino: " + command);
                        lock (lockObject)
                        {
                            receivedCommand = command;
                        }

                        newlineIndex = buffer.IndexOf('\n');
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error reading from serial port: " + ex.ToString());
            }
        }
    }
}
