using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public CountdownTimer preGameTimer;
	public CountdownTimer gameTimer;
    public FadeGUIText startText;
	public Component dropOneGroup;
    public Component dropTwoGroup;
    public Component dropThreeGroup;

    // time for flatform drops
    public int dropOneMin;
	public int dropOneSec;
	public int dropTwoMin;
	public int dropTwoSec;
	public int dropThreeMin;
	public int dropThreeSec;

    // variables that deal with material changes
    public int secBeforewarn;
    public Material warningDrop;

	public GameplayManager gameplayScript;

    private bool bGameStart;
	private bool bFinalCountdown;
	private bool bDropOne;
    private bool bDropTwo;
    private bool bDropThree;

    private ToggleObjKenematic[] dropOneArray;
    private ToggleObjKenematic[] dropTwoArray;
    private ToggleObjKenematic[] dropThreeArray;

    private Renderer[] changeToWarning;
	
	void Start()
	{
        bGameStart = false;
		bFinalCountdown = false;
		preGameTimer.StartTimer();

        // putting child objects of each drop parent into arrays
        dropOneArray = dropOneGroup.GetComponentsInChildren<ToggleObjKenematic>();
        dropTwoArray = dropTwoGroup.GetComponentsInChildren<ToggleObjKenematic>();
        dropThreeArray = dropThreeGroup.GetComponentsInChildren<ToggleObjKenematic>();  
	}

	void Update () 
	{
        if (preGameTimer.isEnd() && bGameStart == false)
        {
			gameplayScript.CountdownDone();
            bGameStart = true;

			//quick fade in and slower fade out
			startText.Fade();
			startText.SwitchFadeType();
			startText.ResetFaded();
			//startText.Fade();

            preGameTimer.ToggleSeconds();
            preGameTimer.StopTimer();

            gameTimer.StartTimer();
        }
        if (bGameStart) 
		{
			startText.Fade();
            MaterialFade();
            DropCheck();
		}
	}

    void DropCheck()
    {
        if (gameTimer.minutes == 0 && gameTimer.seconds == 30 && !bFinalCountdown) 
        {
            gameTimer.ToggleMiliseconds();
            bFinalCountdown = true;
        }

        if (gameTimer.minutes == dropOneMin && gameTimer.seconds == dropOneSec && !bDropOne) 
        {
            Debug.Log("Drop one");
            
            foreach(ToggleObjKenematic child in dropOneArray)
            {
                Debug.Log("Toggle gravity");
                child.ToggleGravity();
            }
            
            bDropOne = true;
        }
        else if (gameTimer.minutes == dropTwoMin && gameTimer.seconds == dropTwoSec && !bDropTwo) 
        {
            Debug.Log("Drop Two");
            
            foreach(ToggleObjKenematic child in dropTwoArray)
            {
                Debug.Log("Toggle gravity");
                child.ToggleGravity();
            }
            
            bDropTwo = true;
        }
        else if (gameTimer.minutes == dropThreeMin && gameTimer.seconds == dropThreeSec && !bDropThree) 
        {
            Debug.Log("Drop Three");             
            
            foreach(ToggleObjKenematic child in dropThreeArray)
            {
                Debug.Log("Toggle gravity");
                child.ToggleGravity();
            }
            
            bDropThree = true;
        }
    }

    // this function switches the material to warn the player a platform is about to drop
    void MaterialFade()
    {
        if (gameTimer.minutes == dropOneMin && gameTimer.seconds == ( dropOneSec + secBeforewarn))
        {
            changeToWarning = dropOneGroup.GetComponentsInChildren<Renderer>();

            foreach(Renderer child in changeToWarning)
            {
                child.material = warningDrop;
            }
        }
        else if (gameTimer.minutes == dropTwoMin && gameTimer.seconds == (dropTwoSec + secBeforewarn))
        {
            changeToWarning = dropTwoGroup.GetComponentsInChildren<Renderer>();

            foreach(Renderer child in changeToWarning)
            {
                child.material = warningDrop;
            }
        }
        else if (gameTimer.minutes == dropThreeMin && gameTimer.seconds == (dropThreeSec + secBeforewarn))
        {
            changeToWarning = dropThreeGroup.GetComponentsInChildren<Renderer>();

            foreach(Renderer child in changeToWarning)
            {
                child.material = warningDrop;
            }
        }
    }

	public bool GetGameStart()
	{
		return bGameStart;
	}
}