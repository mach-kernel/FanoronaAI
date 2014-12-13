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

		// Add pieces to board
		for (int i = 0; i < board.GetLength(0); ++i) {
			var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			piece.name = "Piece";
			piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			piece.renderer.material.color = Color.black;
			piece.transform.position = board[0,i].transform.position;
		}

		for (int i = 0; i < board.GetLength(0); ++i) {
			var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			piece.name = "Piece";
			piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			piece.renderer.material.color = Color.white;
			piece.transform.position = board[2,i].transform.position;
		}

		var pieceL = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		pieceL.name = "Piece";
		pieceL.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
		pieceL.renderer.material.color = Color.black;
		pieceL.transform.position = board[1,0].transform.position;

		var pieceR = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		pieceR.name = "Piece";
		pieceR.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
		pieceR.renderer.material.color = Color.white;
		pieceR.transform.position = board[1,2].transform.position;
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
