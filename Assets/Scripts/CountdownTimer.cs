using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour 
{
    public GUIText timer;

    public int minutes;
    public int seconds;
    public float miliseconds;

    public bool detailed;
    private bool go;


    void Start() 
    {
        timer.text = "";
        // make sure all the variables are corectly set
        if( !guiText )
        {
            Debug.Log("This timer requires a GUIText component");
            enabled = false;
            return;
        }
        if (seconds > 60)
        {
            Debug.Log("Seconds set too high, setting to 59");
            seconds = 59;
        }
        if (miliseconds > 100)
        {
            Debug.Log("Miliseconds set to high, setting to 99");
            miliseconds = 99;
        }
    }

    void Update() 
    {
        //This is for testing, make sure to remove it after your done.
        if(Input.GetMouseButtonDown(0))
        {
            if(!go)
            {
                StartTimer();
            }
        }
        if (go)
        {
            StartTimer(); 
        }
    }

    public void StartTimer()
    {
        go = true;

        miliseconds -= Time.deltaTime * 100;

        // print first because if prited later, at 0:00:00 sec, 0:00:01 will be printed
        PrintTime();

        // check to see if time is up
        if (minutes == 0 && seconds == 0 )
        {
            miliseconds = 0;
            PrintTime();
            go = false;
            return;
            //end the game do other stuff?
        }

        if(miliseconds <= 0)
        {
            if(seconds <= 0)
            {
                minutes--;
                seconds = 59;
            }
            else if(seconds >= 0)
            {
                seconds--;
            }
            
            miliseconds = 99;
        }
    }

    public void PrintTime()
    {
        if (detailed)
        {
            DetailedTimer();
            return;
        }
        SimpleTimer();
    }

    void SimpleTimer()
    {
        // formatting so only sections with time left are printed - without miliseconds
        if (minutes > 0)
        {
            if (seconds < 10)
            {
                timer.text = string.Format("{0}:0{1}", minutes, seconds);
            }
            else
            {
                timer.text = string.Format("{0}:{1}", minutes, seconds);
            }   
        }
        else
        {
            timer.text = string.Format("{0}", seconds);
        }
    }

    void DetailedTimer()
    {
        // better looking formatting
        if (seconds < 10)
        {
            if (miliseconds < 10)
            {
                timer.text = string.Format("{0}:0{1}:0{2}", minutes, seconds, (int)miliseconds);
            }
            else
            {
                timer.text = string.Format("{0}:0{1}:{2}", minutes, seconds, (int)miliseconds);
            }
        }
        else
        {
            if (miliseconds < 10)
            {
                timer.text = string.Format("{0}:{1}:0{2}", minutes, seconds, (int)miliseconds);
            }
            else
            {
                timer.text = string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds);
            }
        }
    }


    void StopTimer()
    {
        go = false;
    }
}