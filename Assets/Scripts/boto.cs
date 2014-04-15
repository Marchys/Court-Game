using UnityEngine;
using System.Collections;

public class boto : MonoBehaviour {

	void OnGUI() {
			
		if (GUI.Button(new Rect(Screen.width - 150,Screen.height - 100,100,50), "Click"))
			Debug.Log("Clicked the button with text");
		
	}
}
