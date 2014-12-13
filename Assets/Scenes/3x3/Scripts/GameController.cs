using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public enum PT {WHITE, BLACK, UNCHOSEN}

    /// <summary>
    /// Basic position struct since Mono/Unity 
    /// lacks support for tuples
    /// </summary>
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

    // Stats labels
    public string curTurnL = "n/a";
    public int turnCount = 0;

    // Turn logic
    public PT humanPlayer = PT.UNCHOSEN;
    public PT currentTurn = PT.WHITE;
    public bool turnInProgress = false;

    // Board logic
	public GameObject[,] board = new GameObject[3,3];
	public bool isPieceSelected = false;
    public GameObject selectedPiece;

	/// <summary>
	/// Responsible for rendering board positions
    /// onto screen, as well as GUI components and 
    /// initial piece placement. Also prompts user for which
    /// color they would like to be. 
	/// </summary>
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
        scR.gamePiece = pieceR;
		//// END ADD PIECES TO BOARD

	}

    /// <summary>
    /// Highlights all valid move spaces with the color Magenta
    /// </summary>
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

        // START HIGHLIGHT VALID SPACES
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
        // END HIGHLIGHT VALID SPACES
    }

    /// <summary>
    /// Clears selection and validity paths.
    /// </summary>
    public void clearSelect()
    {
        isPieceSelected = false;
        selectedPiece = null;
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

    /// <summary>
    /// Moves selected object from one valid space to the other,
    /// and completes any capture if applicable. 
    /// </summary>
    /// <param name="to">Destination position</param>
    public void doMove(GameObject to)
    {
        Pos fromp = new Pos();
        Pos top = new Pos();

        //// BEGIN MOVEMENT
        for (int y = 0; y < 3; ++y)
        {
            for (int x = 0; x < 3; ++x)
            {
                if (board[y,x].transform.position == selectedPiece.transform.position)
                {
                    var fromsc = (Position)board[y, x].GetComponent("Position");
                    fromsc.gamePiece = null;
                    fromp.x = x;
                    fromp.y = y;
                }
            }
        }

        selectedPiece.transform.position = to.transform.position;
        var tosc = (Position)to.GetComponent("Position");
        tosc.gamePiece = selectedPiece;

        for (int y = 0; y < 3; ++y)
        {
            for (int x = 0; x < 3; ++x)
            {
                if (board[y, x].transform.position == selectedPiece.transform.position)
                {
                    top.x = x;
                    top.y = y;
                }
            }
        }
        //// END MOVEMENT

        //// BEGIN CAPTURE
        if (fromp.y - top.y == 0)
        {
            for (int i = 0; i < 3; ++i)
            {
                var csc = (Position)board[top.y,i].GetComponent("Position");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece)csc.gamePiece.GetComponent("Piece");
                    var gpGPC = (Piece)selectedPiece.GetComponent("Piece");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                    }
                }
            }
        }
        if (fromp.x - top.x == 0)
        {
            for (int i = 0; i < 3; ++i)
            {
                var csc = (Position)board[i, top.x].GetComponent("Position");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece)csc.gamePiece.GetComponent("Piece");
                    var gpGPC = (Piece)selectedPiece.GetComponent("Piece");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                    }
                }
            }
        }
        if ((fromp.x - top.x > 0) && (fromp.y - top.y > 0) || (fromp.x - top.x < 0) && (fromp.y - top.y < 0))
        {
            int ty = top.y, tx = top.x;
            int ry = top.y, rx = top.x;
            while (ty >= 0 && tx >= 0)
            {
                var csc = (Position)board[ty, tx].GetComponent("Position");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece)csc.gamePiece.GetComponent("Piece");
                    var gpGPC = (Piece)selectedPiece.GetComponent("Piece");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                    }
                }
                --ty;
                --tx;
            }
            while (rx <= 2 && ry <= 2)
            {
                var csc = (Position)board[ry, rx].GetComponent("Position");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece)csc.gamePiece.GetComponent("Piece");
                    var gpGPC = (Piece)selectedPiece.GetComponent("Piece");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                    }
                }
                ++ry;
                ++rx;
            }
        }
        if ((fromp.x - top.x > 0) && (fromp.y - top.y < 0) || (fromp.x - top.x < 0) && (fromp.y - top.y > 0))
        {
            int ty = top.y, tx = top.x;
            int ry = top.y, rx = top.x;
            while (ty >= 0 && tx >= 0)
            {
                var csc = (Position)board[ty, tx].GetComponent("Position");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece)csc.gamePiece.GetComponent("Piece");
                    var gpGPC = (Piece)selectedPiece.GetComponent("Piece");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                    }
                }
                --ty;
                ++tx;
            }
            while (ry <= 2 && rx <= 2)
            {
                var csc = (Position)board[ry, rx].GetComponent("Position");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece)csc.gamePiece.GetComponent("Piece");
                    var gpGPC = (Piece)selectedPiece.GetComponent("Piece");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                    }
                }
                ++ry;
                --rx;
            }
        }
        //// END CAPTURE

        clearSelect();
    }

    void playAI()
    {
        // Choose random piece
        int randX = Random.Range(0, 2);
        int randY = Random.Range(0, 2);

        // Select it
        var pscr = (Piece)((Position)board[randY, randX].GetComponent("Position")).gamePiece.GetComponent("Piece");
        pscr.select();

        // Check for valid moves
        for (int y = 0; y < 3; ++y)
        {
            for (int x = 0; x < 3; ++x)
            {

            }
        }
    }

    void OnGUI()
    {
        if (humanPlayer == PT.UNCHOSEN)
        {
            GUI.Box(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 100, 200, 125), "Choose Color");
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) - 65, 150, 25), "Black"))
            {
                humanPlayer = PT.BLACK;
                Debug.Log("Human chooses black.");
            }
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) - 25, 150, 25), "White"))
            {
                humanPlayer = PT.WHITE;
                Debug.Log("Human chooses white.");
            }
        }

        GUI.Box(new Rect(10, (Screen.height / 2) - 100, 200, 75), "Stats");
        GUI.Label(new Rect(15, (Screen.height / 2) - 80, 200, 125), "Current Turn:");
        GUI.Label(new Rect(100, (Screen.height / 2) - 80, 200, 125), curTurnL);

        GUI.Label(new Rect(15, (Screen.height / 2) - 60, 200, 125), "Total turns:");
        GUI.Label(new Rect(100, (Screen.height / 2) - 60, 200, 125), turnCount.ToString());

        if (GUI.Button(new Rect(10f, 10f, 65f, 25f), "← Back"))
        {
            Application.LoadLevel("StartGUI");
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        // Deselect pieces
        if (Input.GetMouseButtonDown(1) && currentTurn == humanPlayer)
        {
            clearSelect();
        }

        curTurnL = humanPlayer == currentTurn ? "Human" : "Computer";

        if ((currentTurn != humanPlayer) && !turnInProgress)
        {
            turnInProgress = true;
            playAI();
        }
			
	}
}
