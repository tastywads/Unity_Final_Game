using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour 
{
	public float timeLeft;
	//public float gameTime;

	void Update () 
	{
		timeLeft -= Time.deltaTime;
	}

	void OnGUI()
	{
		GUI.backgroundColor = Color.red;
		if (timeLeft < 60) 
		{
			GUI.backgroundColor = Color.red;
			GUI.Box(new Rect(10,10,50,20), "" + timeLeft.ToString("F1"));
		}
		else 
		{
			GUI.Box(new Rect(10,10,50,20), "" + timeLeft.ToString("0"));
		}
	}

}
