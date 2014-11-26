using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject[,] board = new GameObject[3,3];

	// Use this for initialization
	void Start () 
    {
        Debug.Log("3x3: Initializing assets and drawing board.");
		// Find wooden board to get bounds
		var wood = GameObject.Find("Board");

		// Draw points and lines
		for (int i=0; i < 3; ++i) 
        {
			var point = GameObject.CreatePrimitive(PrimitiveType.Quad);
            point.renderer.material.color = Color.black;
            point.name = "0, " + i.ToString();
			point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            switch (i) 
            {
                case (0):
                    {
                        point.transform.position = new Vector3(-2.23f, wood.renderer.bounds.max.y - 1f, -0.01f);
                        break;
                    }
                case (1):
                    {
                        point.transform.position = new Vector3(0f, wood.renderer.bounds.max.y - 1f, -0.01f);
                        break;
                    }
                case (2):
                    {
                        point.transform.position = new Vector3(2.23f, wood.renderer.bounds.max.y - 1f, -0.01f);
                        break;
                    }
            }
            board[0, i] = point;
		}

        for (int i = 0; i < 3; ++i) 
        {
			var point = GameObject.CreatePrimitive(PrimitiveType.Quad);
            point.renderer.material.color = Color.black;
			point.name = "1, " + i.ToString();
			point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            switch (i)
            {
                case (0):
                    {
                        point.transform.position = new Vector3(-2.23f, 0f, -0.01f);
                        break;
                    }
                case (1):
                    {
                        point.transform.position = new Vector3(0f, 0f, -0.01f);
                        break;
                    }
                case (2):
                    {
                        point.transform.position = new Vector3(2.23f, 0f, -0.01f);
                        break;
                    }
            }
            board[1, i] = point;
		}

        for (int i = 0; i < 3; ++i)
        {
            var point = GameObject.CreatePrimitive(PrimitiveType.Quad);
            point.renderer.material.color = Color.black;
            point.name = "2, " + i.ToString();
            point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            switch (i)
            {
                case (0):
                    {
                        point.transform.position = new Vector3(-2.23f, -wood.renderer.bounds.max.y + 1f, -0.01f);
                        break;
                    }
                case (1):
                    {
                        point.transform.position = new Vector3(0f, -wood.renderer.bounds.max.y + 1f, -0.01f);
                        break;
                    }
                case (2):
                    {
                        point.transform.position = new Vector3(2.23f, -wood.renderer.bounds.max.y + 1f, -0.01f);
                        break;
                    }
            }
            board[2, i] = point;
        }
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10f, 10f, 65f, 25f), "← Back"))
        {
            Debug.Log("Loading 3x3 board.");
            Application.LoadLevel("StartGUI");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
