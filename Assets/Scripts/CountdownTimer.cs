using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour 
{
    public GUIText timer;

    public int minutes;
    public int seconds;
    public float miliseconds;

    public bool bToggleMin;
    public bool bToggleSec;
    public bool bToggleMS;
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
        if (go)
        {
            clock();
        }
        // This is for testing, make sure to remove it on release.
        /*if(Input.GetMouseButtonDown(0))
        {
            if(!go)
            {
                StartTimer();
            }
        }
        if (Input.GetKeyDown("1"))
        {
            ToggleMinutes();
        }
        if (Input.GetKeyDown("2"))
        {
            ToggleSeconds();
        }
        if (Input.GetKeyDown("3"))
        {
            ToggleMiliseconds();
        }*/
        //-------------------- end testing section -----------------------
    }

    void clock()
    {
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

    void PrintTime()
    {
        // show minutes
        if (bToggleMin && !bToggleSec && !bToggleMS)
        {
            timer.text = string.Format("{0}", minutes);
        }
        // show seconds
        else if (!bToggleMin && bToggleSec && !bToggleMS)
        {
            timer.text = string.Format("{0}", seconds);
        }
        // show miliseconds
        else if (!bToggleMin && !bToggleSec && bToggleMS)
        {
            timer.text = string.Format("{0}", (int)miliseconds);
        }
        // show seconds and miliseconds
        else if (!bToggleMin && bToggleSec && bToggleMS)
        {
            if (miliseconds < 10)
            {
                timer.text = string.Format("{0}:0{1}", seconds, (int)miliseconds);
            }
            else
            {
                timer.text = string.Format("{0}:{1}", seconds, (int)miliseconds);
            }  
        }
        // show minutes and seconds
        else if (bToggleMin && bToggleSec && !bToggleMS)
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
        // show minutes, seconds, and miliseconds
        else if (bToggleMin && bToggleSec && bToggleMS)
        {
            // these if's are just for formatting
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
        // show nothing
        else if (!bToggleMin && !bToggleSec && !bToggleMS)
        {
            timer.text = string.Format("");
        }
        else
        {
            Debug.Log("That is a invalid setting, are you trying to toggle miliseconds and minutes?");
            timer.text = string.Format("");
        }
    }

    // these three toggle functions will change if minutes, seconds or miliseconds are shown
	public void ToggleMinutes()
	{
        bToggleMin = !bToggleMin;
        Debug.Log("Minutes toggle is now " + bToggleMin);
	}
    public void ToggleSeconds()
    {
        bToggleSec = !bToggleSec;
        Debug.Log("Seconds toggle is now " + bToggleSec);
    }
    public void ToggleMiliseconds()
    {
        bToggleMS = !bToggleMS;
        Debug.Log("Miliseconds Toggle is now " + bToggleMS);
    }

    //start and stop
    public void StartTimer()
    {
        go = true;
    }
    public void StopTimer()
    {
        go = false;
    }
}











