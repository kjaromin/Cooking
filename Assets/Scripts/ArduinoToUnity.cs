using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class ArduinoToUnity : MonoBehaviour
{

    SerialPort dataStream = new SerialPort ("COM3", 9600);
    public GameObject cube;
    public float lerpSpeed = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataStream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log (dataStream.ReadLine());
        float value;
        float.TryParse(dataStream.ReadLine(), out value);
        cube.transform.position = Vector3.Lerp(cube.transform.position, new Vector3(0f, (1024-value)/1024f * 4f, 0f), lerpSpeed*Time.deltaTime);
    }
}
