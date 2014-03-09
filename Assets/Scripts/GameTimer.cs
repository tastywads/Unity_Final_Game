using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour 
{
	public float countdown;
	public int seconds;
	public int minutes;

	void Update () 
	{

	}

	void OnGUI()
	{

//		if (timeLeft < 10) 
//		{
//			GUI.backgroundColor = Color.red;
//			GUI.Button(new Rect(10,10,50,20), "" + timeLeft.ToString("F1"));
//		}
//		else if (timeLeft < 30 ) 
//		{
//			GUI.backgroundColor = Color.yellow;
//			GUI.Button(new Rect(10,10,50,20), "" + timeLeft.ToString("0"));				
//		}
//		else
//		{
//			GUI.Button(new Rect(10,10,50,20), "" + timeLeft.ToString("0"));
//		}
	}
}
