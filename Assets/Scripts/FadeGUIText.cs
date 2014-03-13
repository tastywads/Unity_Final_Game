using UnityEngine;
using System.Collections;

public class FadeGUIText : MonoBehaviour {

	public GUIText startText;

	public float startAlpha;
	public float fadeInSpeed;
	public float fadeOutSpeed;

	public bool fadeIn;
	public bool fadeOut;

	private float origRed;
	private float origGreen;
	private float origBlue;

	private bool faded;

	void Start()
	{
		if(fadeIn && fadeOut)
		{
			Debug.Log("Please Toggle Only One Fade Type, use public SwitchFadeType() in other scripts to flip the fade");
		}
		// cache original colors
		origRed = startText.font.material.color.r;
		origGreen = startText.font.material.color.g;
		origBlue = startText.font.material.color.b;

		// set text transparency
		startText.material.color = new Color (origRed, origGreen, origBlue, startAlpha);
	}

	void FadeOut()
	{
		startText.material.color = new Color (origRed, origGreen, origBlue, startAlpha -= fadeOutSpeed);
	}
	void FadeIn()
	{
		startText.material.color = new Color (origRed, origGreen, origBlue, startAlpha += fadeInSpeed);
	}

	public void Fade()
	{
		if(fadeIn && !faded)
		{
			FadeIn ();
		}
		else if(fadeOut && !faded)
		{
			FadeOut();
		}
		
		if ( startAlpha < 0) 
		{
			faded = true;
			startAlpha = 0;
		}
		else if (startAlpha > 1)
		{
			faded = true;
			startAlpha = 1;
		}
	}

	public void ResetFaded()
	{
		faded = false;
	}

	public void SwitchFadeType()
	{
		fadeIn = !fadeIn;
		fadeOut = !fadeOut;
	}
}
