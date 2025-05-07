using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoToBoing : MonoBehaviour
{

    SerialPort dataStream = new SerialPort ("/dev/tty.usbmodem101", 9600);
    public int parsedStream = 0;
    public float growSpeed = 0.1f;
    public float shrinkSpeed = 100.0f;

    public int oldParsedStream = 0;
    //public int largestParsedStream = 0;
    public bool playedSound = false;
    public float timeStamp = 0.0f;
    public float soundInterval = 0.25f;
    public AudioSource speaker;
    public AudioClip boingSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dataStream.Open();
    }

    // Update is called once per frame
    void Update()
    {
            // Parsing stream
            parsedStream = int.Parse(dataStream.ReadLine());          
            Debug.Log (parsedStream);

            // Transform based on raw parse data
            transform.localScale = transform.localScale + new Vector3 (parsedStream*growSpeed, parsedStream*growSpeed, parsedStream*growSpeed);
            
            // Sound based on current vs. old stream (play) and largest number as percentage (pitch)
            if (parsedStream > oldParsedStream + 10 && (Time.time - timeStamp) > soundInterval) {
                timeStamp = Time.time;
                /*
                if (largestParsedStream < parsedStream) {
                    largestParsedStream = parsedStream;
                }
                speaker.pitch = parsedStream/largestParsedStream;
                */
                speaker.pitch = parsedStream/360.0f * 10.0f;
                speaker.PlayOneShot(boingSound, 1.0F);
            }
            
            oldParsedStream = parsedStream;

            if (transform.localScale.x > 1.0f) {
                transform.localScale = transform.localScale - new Vector3 (transform.localScale.x*shrinkSpeed*Time.deltaTime, transform.localScale.y*shrinkSpeed*Time.deltaTime, transform.localScale.z*shrinkSpeed*Time.deltaTime);
            }
            if (transform.localScale.x <= 1.0f) {
                transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
            }
    }
}
