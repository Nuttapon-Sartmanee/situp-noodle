using System.IO.Ports;
using UnityEngine;

public class arduinoInput : MonoBehaviour
{
    float timeout = 0.001f;
    SerialPort serialPort = new SerialPort("COM5", 9600);

    public int leftInput = 0;
    public int rightInput = 0;

    void Start()
    {
        if (serialPort != null)
        {
            serialPort.ReadTimeout = (int)(timeout * 1000);
            serialPort.Open();
        }
        else
        {
            Debug.LogError("Serial port is not initialized.");
        }
    }

    void Update()
    {
        try
        {
            string data = serialPort.ReadLine();

            string[] parts = data.Split(',');
            string leftStr = parts[0];
            string rightStr = parts[1];
       

            if (int.TryParse(leftStr, out int leftValue) && int.TryParse(rightStr, out int rightValue))
            {
                //Debug.Log("Left: " + leftValue + ", Right: " + rightValue);

                if (leftValue == 1)
                {
                    rightInput = 0;
                    leftInput = 1;
                }
                else if (rightValue == 1)
                {
                    leftInput = 0;
                    rightInput = 1;
                }
                else 
                {
                    leftInput = 0;
                    rightInput = 0;
                }
            }
            else
            {
                //Debug.LogWarning("Failed to parse data as an integer.");
            }
        }
        catch (System.TimeoutException)
        {
            //Debug.LogWarning("Timeout reading from Serial port");
        }
        catch (System.Exception e)
        {
            //Debug.LogWarning(e.Message);
        }
    }

    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
