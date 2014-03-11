using UnityEngine;
using System.Collections;

public class FadeGUIText : MonoBehaviour {

	public GUIText startText;

	public float startAlpha;
	public float fadeSpeed;

	public bool fadeIn;
	public bool fadeOut;

	private float origRed;
	private float origGreen;
	private float origBlue;

	private bool faded;

	void Start()
	{
		// cache original colors
		origRed = startText.font.material.color.r;
		origGreen = startText.font.material.color.g;
		origBlue = startText.font.material.color.b;

		// set text transparency
		startText.font.material.color = new Color (origRed, origGreen, origBlue, startAlpha);
	}

	void Update () 
	{

	}
	void FadeOut()
	{
		startText.material.color = new Color (origRed, origGreen, origBlue, startAlpha -= fadeSpeed);
	}
	void FadeIn()
	{
		startText.font.material.color = new Color (origRed, origGreen, origBlue, startAlpha += fadeSpeed);
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
}
