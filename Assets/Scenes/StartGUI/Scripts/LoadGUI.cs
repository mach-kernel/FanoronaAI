using UnityEngine;
using System.Collections;

public class LoadGUI : MonoBehaviour {

	public float x, y;
	public GUISkin DefSkin;

	// Use this for initialization
	void Start () {
		x = Screen.width;
		y = Screen.height;

	}

	void OnGUI() {
		// Apply skin
		GUI.skin = Resources.Load("GUI/DefSkin") as GUISkin;

		// Draw main box
		GUI.Box(new Rect((x/2) - 200, (y/2) - 250, 400, 400), "Fanorona!");
		if (GUI.Button(new Rect((x/2) - 175, (y/2) - 125, 350, 50), "Play 3x3"))
		{
			Application.LoadLevel("3x3");
		}
		if (GUI.Button(new Rect((x/2) - 175, (y/2) - 50, 350, 50), "Play 5x5"))
		{
			//
		}
		if (GUI.Button(new Rect((x/2) - 175, (y/2) + 25, 350, 50), "Quit"))
		{
			Application.Quit();
		}

	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
