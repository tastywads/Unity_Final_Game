using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public CountdownTimer preGameTimer;
	public CountdownTimer gameTimer;
    public FadeGUIText startText;
	public GameObject DropOneGroup;

	public int DropOneMin;
	public int DropOneSec;
	public int DropTwoMin;
	public int DropTwoSec;
	public int DropThreeMin;
	public int DropThreeSec;

    private bool gameStart;
	private bool finalCountdown;
	private bool bDropOne;

	
	void Start()
	{
        gameStart = false;
		finalCountdown = false;
		bDropOne = false;
		preGameTimer.StartTimer();
	}
	void Update () 
	{
        if (preGameTimer.isEnd() && gameStart == false)
        {
            gameStart = true;

			//quick fade in and slower fade out
			startText.Fade();
			startText.SwitchFadeType();
			startText.ResetFaded();
			//startText.Fade();

            preGameTimer.ToggleSeconds();
            preGameTimer.StopTimer();

            gameTimer.StartTimer();
        }
		if (gameStart) 
		{
			startText.Fade();
			if (gameTimer.minutes == 0 && gameTimer.seconds == 30 && !finalCountdown) 
			{
				gameTimer.ToggleMiliseconds();
				finalCountdown = true;
			}
			if (gameTimer.minutes == DropOneMin && gameTimer.seconds == DropOneSec && !bDropOne) 
			{
				//Debug.Log(toggleOneArray.Length);

				/*for (int i = 0; i < toggleOneArray.Length; i++)
				{
					toggleOneArray[i].ToggleGravity();
				}*/
				bDropOne = true;
			}
		}
	}
}