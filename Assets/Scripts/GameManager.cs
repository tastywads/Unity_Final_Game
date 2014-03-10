using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public CountdownTimer gameTimer;
	public CountdownTimer preGameTimer;
    public GUIText startText;

    private bool gameStart;

	void Start()
	{
        gameStart = false;
        preGameTimer.StartTimer();
	}
	void Update () 
	{
        if (preGameTimer.isEnd() && gameStart == false)
        {
            gameStart = true;

            preGameTimer.ToggleSeconds();
            preGameTimer.StopTimer();
            startText.text = string.Format("START!");

            gameTimer.StartTimer();
        }
	}
}