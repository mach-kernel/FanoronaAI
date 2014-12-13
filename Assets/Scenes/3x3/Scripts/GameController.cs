using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public enum PT {WHITE, BLACK}

    public struct Pos
    {
        public int x;
        public int y;
        public Pos(int xp, int yp)
        {
            x = xp;
            y = yp;
        }
    }

	public GameObject[,] board = new GameObject[3,3];
	public bool isPieceSelected = false;

	// Use this for initialization
	void Start () 
    {
        Debug.Log("3x3: Initializing assets and drawing board.");
		// Find wooden board to get bounds
		var wood = GameObject.Find("Board");

		//// START DRAW POSITIONS
		for (int i=0; i < 3; ++i) 
        {
			var point = GameObject.CreatePrimitive(PrimitiveType.Quad);
            point.renderer.material.color = Color.black;
            point.name = "0, " + i.ToString();
			point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            point.AddComponent("Position");
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
            point.AddComponent("Position");
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
            point.AddComponent("Position");
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
        //// END DRAW POSITIONS

		//// START ADD PIECES TO BOARD
		for (int i = 0; i < board.GetLength(0); ++i) {
			var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			piece.name = "Piece";
			piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			piece.renderer.material.color = Color.black;
			piece.transform.position = board[0,i].transform.position;
			piece.AddComponent("Piece");

            Position sc = (Position) board[0, i].GetComponent("Position");
            sc.gamePiece = piece;
		}

		for (int i = 0; i < board.GetLength(0); ++i) {
			var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			piece.name = "Piece";
			piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			piece.renderer.material.color = Color.white;
			piece.transform.position = board[2,i].transform.position;
			piece.AddComponent("Piece");

            Position sc = (Position)board[2, i].GetComponent("Position");
            sc.gamePiece = piece;
		}

		var pieceL = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		pieceL.name = "Piece";
		pieceL.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
		pieceL.renderer.material.color = Color.black;
		pieceL.transform.position = board[1,0].transform.position;
		pieceL.AddComponent("Piece");

        Position scL = (Position)board[1,0].GetComponent("Position");
        scL.gamePiece = pieceL;

		var pieceR = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		pieceR.name = "Piece";
		pieceR.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
		pieceR.renderer.material.color = Color.white;
		pieceR.transform.position = board[1,2].transform.position;
		pieceR.AddComponent("Piece");

        Position scR = (Position)board[1, 2].GetComponent("Position");
        scR.gamePiece = pieceL;
		//// END ADD PIECES TO BOARD

	}

    public void showValidMoves()
    {
        Pos p = new Pos(-1, -1);
        // Iterate through matrix to find selected object
        for (int y = 0; y < 3; ++y)
        {
            for (int x = 0; x < 3; ++x)
            {
                var sc = ((Position)board[y, x].GetComponent("Position"));
                if (sc.gamePiece && sc.gamePiece.renderer.material.color == Color.cyan)
                {
                    p.x = x;
                    p.y = y;
                }
            }
        }

        Debug.Log("Selected piece is " + p.y + ", " + p.x);

        if (p.x == -1 || p.y == -1)
        {
            // Lose condition
        }

        // Highlight valid spaces
        if (p.y > 0 && !((Position)board[p.y - 1, p.x].GetComponent("Position")).gamePiece)
        {
            board[p.y - 1, p.x].renderer.material.color = Color.magenta;
        }
        if (p.y < 2 && !((Position)board[p.y + 1, p.x].GetComponent("Position")).gamePiece)
        {
            board[p.y + 1, p.x].renderer.material.color = Color.magenta;
        }
        if (p.x > 0 && !((Position)board[p.y, p.x - 1].GetComponent("Position")).gamePiece)
        {
            board[p.y, p.x - 1].renderer.material.color = Color.magenta;
        }
        if (p.x < 2 && !((Position)board[p.y, p.x + 1].GetComponent("Position")).gamePiece)
        {
            board[p.y, p.x + 1].renderer.material.color = Color.magenta;
        }
        if ((p.x > 0 && p.y > 0) && !((Position)board[p.y - 1, p.x - 1].GetComponent("Position")).gamePiece)
        {
            board[p.y - 1, p.x - 1].renderer.material.color = Color.magenta;
        }
        if ((p.x > 0 && p.y < 2) && !((Position)board[p.y + 1, p.x - 1].GetComponent("Position")).gamePiece)
        {
            board[p.y + 1, p.x - 1].renderer.material.color = Color.magenta;
        }
        if ((p.x < 2 && p.y > 0) && !((Position)board[p.y - 1, p.x + 1].GetComponent("Position")).gamePiece)
        {
            board[p.y - 1, p.x + 1].renderer.material.color = Color.magenta;
        }
        if ((p.x < 2 && p.y < 2) && !((Position)board[p.y + 1, p.x + 1].GetComponent("Position")).gamePiece)
        {
            board[p.y + 1, p.x + 1].renderer.material.color = Color.magenta;
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

	//List<Time> showValidMoves();
	
	// Update is called once per frame
	void Update () {

        // Deselect pieces
        if (Input.GetMouseButtonDown(1))
        {
            isPieceSelected = false;
            for (int y = 0; y < 3; ++y)
            {
                for (int x = 0; x < 3; ++x)
                {
                    board[y, x].renderer.material.color = Color.black;
                    var sc = (Position)board[y, x].GetComponent("Position");
                    if (sc.gamePiece) 
                    {
                        sc.gamePiece.renderer.material.color = ((Piece)sc.gamePiece.GetComponent("Piece")).pieceType == 0 ? Color.white : Color.black;
                    }
                }
            }
        }
			
	}
}
