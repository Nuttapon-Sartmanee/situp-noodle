using System.IO.Ports;
using UnityEngine;

public class test_arduinoPort : MonoBehaviour
{
    float timeout = 0.001f;
    SerialPort serialPort = new SerialPort("COM5", 9600);

    public GameObject leftObject;
    public GameObject RightObject;

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
            //Debug.Log(data);

            string[] parts = data.Split(',');
            string leftStr = parts[0];
            string rightStr = parts[1];
       

            if (int.TryParse(leftStr, out int leftValue) && int.TryParse(rightStr, out int rightValue))
            {
                Debug.Log("Left: " + leftValue + ", Right: " + rightValue);

                if (leftValue == 1)
                {
                    leftObject.GetComponent<Renderer>().material.color = Color.red;
                }
                else if (rightValue == 1)
                {
                    RightObject.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    leftObject.GetComponent<Renderer>().material.color = Color.green;
                    RightObject.GetComponent<Renderer>().material.color = Color.green;
                }
            }
            else
            {
                Debug.LogWarning("Failed to parse data as an integer.");
            }
        }
        catch (System.TimeoutException)
        {
            Debug.LogWarning("Timeout reading from Serial port");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
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
