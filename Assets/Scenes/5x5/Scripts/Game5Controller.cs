using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game5Controller : MonoBehaviour {

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
    public int noBlack = 12;
    public int noWhite = 12;

    // Turn logic
    public PT humanPlayer = PT.UNCHOSEN;
    public PT currentTurn = PT.WHITE;
    public bool turnInProgress = false;

    // Board logic
	public GameObject[,] board = new GameObject[5,5];
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
        Debug.Log("5x5: Initializing assets and drawing board.");
		// Find wooden board to get bounds
		var wood = GameObject.Find("Board");

		//// START DRAW POSITIONS
		for (int i=0; i < 5; ++i) 
        {
			var point = GameObject.CreatePrimitive(PrimitiveType.Quad);
            point.renderer.material.color = Color.black;
            point.name = "0, " + i.ToString();
			point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            point.AddComponent("Position5");
            switch (i) 
            {
                case (0):
                    {
                        point.transform.position = new Vector3(-4.46f, wood.renderer.bounds.max.y - 1f, -0.01f);
                        break;
                    }
                case (1):
                    {
                        point.transform.position = new Vector3(-2.23f, wood.renderer.bounds.max.y - 1f, -0.01f);
                        break;
                    }
                case (2):
                    {
                        point.transform.position = new Vector3(0f, wood.renderer.bounds.max.y - 1f, -0.01f);
                        break;
                    }
                case (3):
                    {
                        point.transform.position = new Vector3(2.23f, wood.renderer.bounds.max.y - 1f, -0.01f);
                        break;
                    }
                case (4):
                    {
                        point.transform.position = new Vector3(4.46f, wood.renderer.bounds.max.y - 1f, -0.01f);
                        break;
                    }
            }
            board[0, i] = point;
		}

        for (int i = 0; i < 5; ++i)
        {
            var point = GameObject.CreatePrimitive(PrimitiveType.Quad);
            point.renderer.material.color = Color.black;
            point.name = "1, " + i.ToString();
            point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            point.AddComponent("Position5");
            switch (i)
            {
                case (0):
                    {
                        point.transform.position = new Vector3(-4.46f, wood.renderer.bounds.max.y - 3.5f, -0.01f);
                        break;
                    }
                case (1):
                    {
                        point.transform.position = new Vector3(-2.23f, wood.renderer.bounds.max.y - 3.5f, -0.01f);
                        break;
                    }
                case (2):
                    {
                        point.transform.position = new Vector3(0f, wood.renderer.bounds.max.y - 3.5f, -0.01f);
                        break;
                    }
                case (3):
                    {
                        point.transform.position = new Vector3(2.23f, wood.renderer.bounds.max.y - 3.5f, -0.01f);
                        break;
                    }
                case (4):
                    {
                        point.transform.position = new Vector3(4.46f, wood.renderer.bounds.max.y - 3.5f, -0.01f);
                        break;
                    }
            }
            board[1, i] = point;
        }

        for (int i = 0; i < 5; ++i) 
        {
			var point = GameObject.CreatePrimitive(PrimitiveType.Quad);
            point.renderer.material.color = Color.black;
			point.name = "2, " + i.ToString();
			point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            point.AddComponent("Position5");
            switch (i)
            {
                case (0):
                    {
                        point.transform.position = new Vector3(-4.46f, 0f, -0.01f);
                        break;
                    }
                case (1):
                    {
                        point.transform.position = new Vector3(-2.23f, 0f, -0.01f);
                        break;
                    }
                case (2):
                    {
                        point.transform.position = new Vector3(0f, 0f, -0.01f);
                        break;
                    }
                case (3):
                    {
                        point.transform.position = new Vector3(2.23f, 0f, -0.01f);
                        break;
                    }
                case (4):
                    {
                        point.transform.position = new Vector3(4.46f, 0f, -0.01f);
                        break;
                    }
            }
            board[2, i] = point;
		}

        for (int i = 0; i < 5; ++i)
        {
            var point = GameObject.CreatePrimitive(PrimitiveType.Quad);
            point.renderer.material.color = Color.black;
            point.name = "3, " + i.ToString();
            point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            point.AddComponent("Position5");
            switch (i)
            {
                case (0):
                    {
                        point.transform.position = new Vector3(-4.46f, -wood.renderer.bounds.max.y + 3.5f, -0.01f);
                        break;
                    }
                case (1):
                    {
                        point.transform.position = new Vector3(-2.23f, -wood.renderer.bounds.max.y + 3.5f, -0.01f);
                        break;
                    }
                case (2):
                    {
                        point.transform.position = new Vector3(0f, -wood.renderer.bounds.max.y + 3.5f, -0.01f);
                        break;
                    }
                case (3):
                    {
                        point.transform.position = new Vector3(2.23f, -wood.renderer.bounds.max.y + 3.5f, -0.01f);
                        break;
                    }
                case (4):
                    {
                        point.transform.position = new Vector3(4.46f, -wood.renderer.bounds.max.y + 3.5f, -0.01f);
                        break;
                    }
            }
            board[3, i] = point;
        }

        for (int i = 0; i < 5; ++i)
        {
            var point = GameObject.CreatePrimitive(PrimitiveType.Quad);
            point.renderer.material.color = Color.black;
            point.name = "4, " + i.ToString();
            point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            point.AddComponent("Position5");
            switch (i)
            {
                case (0):
                    {
                        point.transform.position = new Vector3(-4.46f, -wood.renderer.bounds.max.y + 1f, -0.01f);
                        break;
                    }
                case (1):
                    {
                        point.transform.position = new Vector3(-2.23f, -wood.renderer.bounds.max.y + 1f, -0.01f);
                        break;
                    }
                case (2):
                    {
                        point.transform.position = new Vector3(0f, -wood.renderer.bounds.max.y + 1f, -0.01f);
                        break;
                    }
                case (3):
                    {
                        point.transform.position = new Vector3(2.23f, -wood.renderer.bounds.max.y + 1f, -0.01f);
                        break;
                    }
                case (4):
                    {
                        point.transform.position = new Vector3(4.46f, -wood.renderer.bounds.max.y + 1f, -0.01f);
                        break;
                    }
            }
            board[4, i] = point;
        }
        //// END DRAW POSITIONS

		//// START ADD PIECES TO BOARD
		for (int i = 0; i < board.GetLength(0); ++i) {
			var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			piece.name = "Piece";
			piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			piece.renderer.material.color = Color.black;
			piece.transform.position = board[0,i].transform.position;
			piece.AddComponent("Piece5");

            Position5 sc = (Position5) board[0, i].GetComponent("Position5");
            sc.gamePiece = piece;
		}

		for (int i = 0; i < board.GetLength(0); ++i) {
			var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			piece.name = "Piece";
			piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			piece.renderer.material.color = Color.black;
			piece.transform.position = board[1,i].transform.position;
			piece.AddComponent("Piece5");

            Position5 sc = (Position5)board[1, i].GetComponent("Position5");
            sc.gamePiece = piece;
		}

        for (int i = 0; i < board.GetLength(0); ++i)
        {
            switch (i)
            {
                case (0):
                    {
                        var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        piece.name = "Piece";
                        piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        piece.renderer.material.color = Color.black;
                        piece.transform.position = board[2, i].transform.position;
                        piece.AddComponent("Piece5");

                        Position5 sc = (Position5)board[2, i].GetComponent("Position5");
                        sc.gamePiece = piece;
                        break;
                    }
                case (1):
                    {
                        var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        piece.name = "Piece";
                        piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        piece.renderer.material.color = Color.white;
                        piece.transform.position = board[2, i].transform.position;
                        piece.AddComponent("Piece5");

                        Position5 sc = (Position5)board[2, i].GetComponent("Position5");
                        sc.gamePiece = piece;
                        break;
                    }
                case (2):
                    {
                        continue;
                    }
                case (3):
                    {
                        var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        piece.name = "Piece";
                        piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        piece.renderer.material.color = Color.black;
                        piece.transform.position = board[2, i].transform.position;
                        piece.AddComponent("Piece5");

                        Position5 sc = (Position5)board[2, i].GetComponent("Position5");
                        sc.gamePiece = piece;
                        break;
                    }
                case (4):
                    {
                        var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        piece.name = "Piece";
                        piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        piece.renderer.material.color = Color.white;
                        piece.transform.position = board[2, i].transform.position;
                        piece.AddComponent("Piece5");

                        Position5 sc = (Position5)board[2, i].GetComponent("Position5");
                        sc.gamePiece = piece;
                        break;
                    }
            }

        }

        for (int i = 0; i < board.GetLength(0); ++i)
        {
            var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            piece.name = "Piece";
            piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            piece.renderer.material.color = Color.white;
            piece.transform.position = board[3, i].transform.position;
            piece.AddComponent("Piece5");

            Position5 sc = (Position5)board[3, i].GetComponent("Position5");
            sc.gamePiece = piece;
        }
        for (int i = 0; i < board.GetLength(0); ++i)
        {
            var piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            piece.name = "Piece";
            piece.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            piece.renderer.material.color = Color.white;
            piece.transform.position = board[4, i].transform.position;
            piece.AddComponent("Piece5");

            Position5 sc = (Position5)board[4, i].GetComponent("Position5");
            sc.gamePiece = piece;
        }
	}

    public bool isOccupied (GameObject position)
    {
        return (((Position5)position.GetComponent("Position5")).gamePiece) ? true : false;
    }

    /// <summary>
    /// Highlights all valid move spaces with the color Magenta
    /// </summary>
    public void showValidMoves()
    {
        Pos p = new Pos(-1, -1);
        // Iterate through matrix to find selected object
        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                var sc = ((Position5)board[y, x].GetComponent("Position5"));
                if (sc.gamePiece && sc.gamePiece.renderer.material.color == Color.cyan)
                {
                    p.x = x;
                    p.y = y;
                }
            }
        }

        Debug.Log("Selected piece is " + p.y + ", " + p.x);

        // START HIGHLIGHT VALID SPACES
        bool captureHighlight = false;
        if (p.y > 0 && !isOccupied(board[p.y - 1, p.x]))
        {
            if (willCapture(board[p.y - 1, p.x]))
            {
                board[p.y - 1, p.x].renderer.material.color = Color.magenta;
                captureHighlight = true;
            }
        }
        if (p.y < 2 && !isOccupied(board[p.y + 1, p.x]))
        {
            if (willCapture(board[p.y + 1, p.x]))
            {
                board[p.y + 1, p.x].renderer.material.color = Color.magenta;
                captureHighlight = true;
            }
        }
        if (p.x > 0 && !isOccupied(board[p.y, p.x - 1]))
        {
            if (willCapture(board[p.y, p.x - 1]))
            {
                board[p.y, p.x - 1].renderer.material.color = Color.magenta;
                captureHighlight = true;
            }
        }
        if (p.x < 2 && !isOccupied(board[p.y, p.x + 1]))
        {
            if (willCapture(board[p.y, p.x + 1]))
            {
                board[p.y, p.x + 1].renderer.material.color = Color.magenta;
                captureHighlight = true;
            }
        }
        if ((p.x > 0 && p.y > 0) && !isOccupied(board[p.y - 1, p.x - 1]))
        {
            if (willCapture(board[p.y - 1, p.x - 1]))
            {
                board[p.y - 1, p.x - 1].renderer.material.color = Color.magenta;
                captureHighlight = true;
            }
        }
        if ((p.x > 0 && p.y < 2) && !isOccupied(board[p.y + 1, p.x - 1]))
        {
            if (willCapture(board[p.y + 1, p.x - 1]))
            {
                board[p.y + 1, p.x - 1].renderer.material.color = Color.magenta;
                captureHighlight = true;
            }
        }
        if ((p.x < 2 && p.y > 0) && !isOccupied(board[p.y - 1, p.x + 1]))
        {
            if (willCapture(board[p.y - 1, p.x + 1]))
            {
                board[p.y - 1, p.x + 1].renderer.material.color = Color.magenta;
                captureHighlight = true;
            }
        }
        if ((p.x < 2 && p.y < 2) && !isOccupied(board[p.y + 1, p.x + 1]))
        {
            if (willCapture(board[p.y + 1, p.x + 1]))
            {
                board[p.y + 1, p.x + 1].renderer.material.color = Color.magenta;
                captureHighlight = true;
            }
        }

        if (!captureHighlight)
        {
            Debug.Log("No capture found for " + p.y + ", " + p.x);
            // Check to see if other pieces can capture
            for (int y = 0; y < 5; ++y)
            {
                for (int x=0; x<5; ++x)
                {
                    var poscr = (Position5)board[y, x].GetComponent("Position5");
                    if (poscr.gamePiece)
                    {
                        var piscr = (Piece5)poscr.gamePiece.GetComponent("Piece5");
                        var selPiece = (Piece5)selectedPiece.GetComponent("Piece5");
                        if (piscr.pieceType == selPiece.pieceType)
                        {
                            Pos op = new Pos(x, y);

                            // START HIGHLIGHT VALID SPACES
                            bool otherCapture = false;
                            if (op.y > 0 && !isOccupied(board[op.y - 1, op.x]))
                            {
                                if (willCapture(board[op.y - 1, op.x], board[y,x]))
                                {
                                    otherCapture = true;
                                }
                            }
                            if (op.y < 4 && !isOccupied(board[op.y + 1, op.x]))
                            {
                                if (willCapture(board[op.y + 1, op.x], board[y, x]))
                                {
                                    otherCapture = true;
                                }
                            }
                            if (op.x > 0 && !isOccupied(board[op.y, op.x - 1]))
                            {
                                if (willCapture(board[op.y, op.x - 1], board[y, x]))
                                {
                                    otherCapture = true;
                                }
                            }
                            if (op.x < 4 && !isOccupied(board[op.y, op.x + 1]))
                            {
                                if (willCapture(board[op.y, op.x + 1], board[y, x]))
                                {
                                    otherCapture = true;
                                }
                            }
                            if ((op.x > 0 && op.y > 0) && !isOccupied(board[op.y - 1, op.x - 1]))
                            {
                                if (willCapture(board[op.y - 1, op.x - 1], board[y, x]))
                                {
                                    otherCapture = true;
                                }
                            }
                            if ((op.x > 0 && op.y < 4) && !isOccupied(board[op.y + 1, op.x - 1]))
                            {
                                if (willCapture(board[op.y + 1, op.x - 1], board[y, x]))
                                {
                                    otherCapture = true;
                                }
                            }
                            if ((op.x < 4 && op.y > 0) && !isOccupied(board[op.y - 1, op.x + 1]))
                            {
                                if (willCapture(board[op.y - 1, op.x + 1], board[y, x]))
                                {
                                    otherCapture = true;
                                }
                            }
                            if ((op.x < 4 && op.y < 4) && !isOccupied(board[op.y + 1, op.x + 1]))
                            {
                                if (willCapture(board[op.y + 1, op.x + 1], board[y, x]))
                                {
                                    otherCapture = true;
                                }
                            }
                            if (otherCapture) return;
                        }
                    }
                }
            }
            if (p.y > 0 && !isOccupied(board[p.y - 1, p.x]))
            {
                board[p.y - 1, p.x].renderer.material.color = Color.magenta;
            }
            if (p.y < 4 && !isOccupied(board[p.y + 1, p.x]))
            {
                board[p.y + 1, p.x].renderer.material.color = Color.magenta;
            }
            if (p.x > 0 && !isOccupied(board[p.y, p.x - 1]))
            {
                board[p.y, p.x - 1].renderer.material.color = Color.magenta;
            }
            if (p.x < 4 && !isOccupied(board[p.y, p.x + 1]))
            {
                board[p.y, p.x + 1].renderer.material.color = Color.magenta;
            }
            if ((p.x > 0 && p.y > 0) && !isOccupied(board[p.y - 1, p.x - 1]))
            {
                board[p.y - 1, p.x - 1].renderer.material.color = Color.magenta;
            }
            if ((p.x > 0 && p.y < 4) && !isOccupied(board[p.y + 1, p.x - 1]))
            {
                board[p.y + 1, p.x - 1].renderer.material.color = Color.magenta;
            }
            if ((p.x < 4 && p.y > 0) && !isOccupied(board[p.y - 1, p.x + 1]))
            {
                board[p.y - 1, p.x + 1].renderer.material.color = Color.magenta;
            }
            if ((p.x < 4 && p.y < 4) && !isOccupied(board[p.y + 1, p.x + 1]))
            {
                board[p.y + 1, p.x + 1].renderer.material.color = Color.magenta;
            }
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
        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                board[y, x].renderer.material.color = Color.black;
                var sc = (Position5)board[y, x].GetComponent("Position5");
                if (sc.gamePiece)
                {
                    sc.gamePiece.renderer.material.color = ((Piece5)sc.gamePiece.GetComponent("Piece5")).pieceType == 0 ? Color.white : Color.black;
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
        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                if (board[y,x].transform.position == selectedPiece.transform.position)
                {
                    var fromsc = (Position5)board[y, x].GetComponent("Position5");
                    fromsc.gamePiece = null;
                    fromp.x = x;
                    fromp.y = y;
                }
            }
        }

        selectedPiece.transform.position = to.transform.position;
        var tosc = (Position5)to.GetComponent("Position5");
        tosc.gamePiece = selectedPiece;

        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
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

        bool didCapture = false;

        if (fromp.y - top.y == 0 && Mathf.Abs(fromp.x - top.x) == 1)
        {
            for (int i = 0; i < 5; ++i)
            {
                var csc = (Position5)board[top.y,i].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                        didCapture = true;

                        if (cscGPC.pieceType == Piece5.PT.WHITE)
                        {
                            --noWhite;
                        }
                        else { --noBlack; }
                    }
                }
            }
        }
        if (fromp.x - top.x == 0 && Mathf.Abs(fromp.y - top.y) == 1)
        {
            for (int i = 0; i < 5; ++i)
            {
                var csc = (Position5)board[i, top.x].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                        didCapture = true;

                        if (cscGPC.pieceType == Piece5.PT.WHITE)
                        {
                            --noWhite;
                        }
                        else { --noBlack; }
                    }
                }
            }
        }
        if ((fromp.x - top.x == 1) && (fromp.y - top.y == 1) || (fromp.x - top.x == -1) && (fromp.y - top.y == -1))
        {
            int ty = top.y, tx = top.x;
            int ry = top.y, rx = top.x;
            while (ty >= 0 && tx >= 0)
            {
                var csc = (Position5)board[ty, tx].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                        didCapture = true;

                        if (cscGPC.pieceType == Piece5.PT.WHITE)
                        {
                            --noWhite;
                        }
                        else { --noBlack; }
                    }
                }
                --ty;
                --tx;
            }
            while (rx <= 4 && ry <= 4)
            {
                var csc = (Position5)board[ry, rx].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                        didCapture = true;

                        if (cscGPC.pieceType == Piece5.PT.WHITE)
                        {
                            --noWhite;
                        }
                        else { --noBlack; }
                    }
                }
                ++ry;
                ++rx;
            }
        }
        if ((fromp.x - top.x == -1) && (fromp.y - top.y == 1) || (fromp.x - top.x == 1) && (fromp.y - top.y == -1))
        {
            int ty = top.y, tx = top.x;
            int ry = top.y, rx = top.x;
            while (ty >= 0 && tx <= 4)
            {
                var csc = (Position5)board[ty, tx].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                        didCapture = true;

                        if (cscGPC.pieceType == Piece5.PT.WHITE)
                        {
                            --noWhite;
                        }
                        else { --noBlack; }
                    }
                }
                --ty;
                ++tx;
            }
            while (ry <= 4 && rx >= 0)
            {
                var csc = (Position5)board[ry, rx].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        Destroy(csc.gamePiece);
                        csc.gamePiece = null;
                        didCapture = true;

                        if (cscGPC.pieceType == Piece5.PT.WHITE)
                        {
                            --noWhite;
                        }
                        else { --noBlack; }
                    }
                }
                ++ry;
                --rx;
            }
        }
        //// END CAPTURE

        clearSelect();

        turnInProgress = false;

        if (!didCapture)
        {
            currentTurn = currentTurn == PT.BLACK ? PT.WHITE : PT.BLACK;
            turnInProgress = false;
            ++turnCount;
        }
    }

    bool willCapture(GameObject to, GameObject from = null)
    {
        Pos fromp = new Pos();
        Pos top = new Pos();

        //// BEGIN DETECT
        if (!from)
        {
            for (int y = 0; y < 5; ++y)
            {
                for (int x = 0; x < 5; ++x)
                {
                    if (board[y, x].transform.position == selectedPiece.transform.position)
                    {
                        var fromsc = (Position5)board[y, x].GetComponent("Position5");
                        fromp.x = x;
                        fromp.y = y;
                    }
                }
            }
        }

        if (from)
        {
            for (int y = 0; y < 5; ++y)
            {
                for (int x = 0; x < 5; ++x)
                {
                    if (board[y, x].transform.position == from.transform.position)
                    {
                        var fromsc = (Position5)board[y, x].GetComponent("Position5");
                        fromp.x = x;
                        fromp.y = y;
                    }
                }
            }
        }

        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                if (board[y, x].transform.position == to.transform.position)
                {
                    top.x = x;
                    top.y = y;
                }
            }
        }
        //// END DETECT

        //// BEGIN CAPTURE

        bool didCapture = false;

        if (fromp.y - top.y == 0 && Mathf.Abs(fromp.x - top.x) == 1)
        {
            for (int i = 0; i < 5; ++i)
            {
                var csc = (Position5)board[top.y, i].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        didCapture = true;
                    }
                }
            }
        }
        if (fromp.x - top.x == 0 && Mathf.Abs(fromp.y - top.y) == 1)
        {
            for (int i = 0; i < 5; ++i)
            {
                var csc = (Position5)board[i, top.x].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        didCapture = true;
                    }
                }
            }
        }
        if ((fromp.x - top.x == 1) && (fromp.y - top.y == 1) || (fromp.x - top.x == -1) && (fromp.y - top.y == - 1))
        {
            int ty = top.y, tx = top.x;
            int ry = top.y, rx = top.x;
            while (ty >= 0 && tx >= 0)
            {
                var csc = (Position5)board[ty, tx].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        didCapture = true;
                    }
                }
                --ty;
                --tx;
            }
            while (rx <= 4 && ry <= 4)
            {
                var csc = (Position5)board[ry, rx].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        didCapture = true;
                    }
                }
                ++ry;
                ++rx;
            }
        }
        if ((fromp.x - top.x == -1) && (fromp.y - top.y == 1) || (fromp.x - top.x == 1) && (fromp.y - top.y == -1))
        {
            int ty = top.y, tx = top.x;
            int ry = top.y, rx = top.x;
            while (ty >= 0 && tx <= 2)
            {
                var csc = (Position5)board[ty, tx].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        didCapture = true;
                    }
                }
                --ty;
                ++tx;
            }
            while (ry <= 4 && rx >= 0)
            {
                var csc = (Position5)board[ry, rx].GetComponent("Position5");
                if (csc.gamePiece)
                {
                    var cscGPC = (Piece5)csc.gamePiece.GetComponent("Piece5");
                    var gpGPC = (Piece5)selectedPiece.GetComponent("Piece5");
                    if (cscGPC.pieceType != gpGPC.pieceType)
                    {
                        didCapture = true;
                    }
                }
                ++ry;
                --rx;
            }
        }
        return didCapture;
    }

    void playAI()
    {
        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                var poscr = (Position5)board[y, x].GetComponent("Position5");
                if (poscr.gamePiece)
                {
                    var gpscr = (Piece5)poscr.gamePiece.GetComponent("Piece5");
                    if (gpscr.pieceType == Piece5.PT.BLACK && humanPlayer == PT.WHITE)
                    {
                        Debug.Log("AI selected piece, calculating moves.");
                        gpscr.select();
                        for (int yp = 0; yp < 5; ++yp)
                        {
                            for (int xp = 0; xp < 5; ++xp)
                            {
                                if (board[yp, xp].renderer.material.color == Color.magenta)
                                {
                                    Debug.Log("AI Played.");
                                    doMove(board[yp, xp]);
                                    return;
                                }
                            }
                        }
                        clearSelect();
                    }
                    else if (gpscr.pieceType == Piece5.PT.WHITE && humanPlayer == PT.BLACK)
                    {
                        Debug.Log("AI selected piece, calculating moves.");
                        gpscr.select();
                        for (int yp = 0; yp < 5; ++yp)
                        {
                            for (int xp = 0; xp < 5; ++xp)
                            {
                                if (board[yp, xp].renderer.material.color == Color.magenta)
                                {
                                    Debug.Log("AI Played.");
                                    doMove(board[yp, xp]);
                                    return;
                                }
                            }
                        }
                        clearSelect();
                    }
                }
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

        if (noWhite == 0)
        {
            clearSelect();
            turnInProgress = true;

            GUI.Box(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 100, 200, 125), "BLACK WINS");
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) - 65, 150, 25), "Restart?"))
            {
                Application.LoadLevel("5x5");
            }
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) - 25, 150, 25), "Main Menu"))
            {
                Application.LoadLevel("StartGUI");
            }
        }
        if (noBlack == 0)
        {
            clearSelect();
            turnInProgress = true;

            GUI.Box(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 100, 200, 125), "WHITE WINS");
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) - 65, 150, 25), "Restart?"))
            {
                Application.LoadLevel("5x5");
            }
            if (GUI.Button(new Rect((Screen.width / 2) - 75, (Screen.height / 2) - 25, 150, 25), "Main Menu"))
            {
                Application.LoadLevel("StartGUI");
            }
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

        if ((currentTurn != humanPlayer) && !turnInProgress && humanPlayer != PT.UNCHOSEN)
        {
            turnInProgress = true;
            playAI();
        }	
	}
}
