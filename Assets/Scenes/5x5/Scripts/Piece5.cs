﻿using UnityEngine;
using System.Collections;

public class Piece5 : MonoBehaviour {

	public enum PT { WHITE, BLACK }
	Game5Controller cont;

	bool isSelected = false;

	public PT pieceType;

	// Use this for initialization
	void Start () {
		cont = (Game5Controller)GameObject.Find("Main Camera").GetComponent("Game5Controller");
		collider.isTrigger = true;
		pieceType = (renderer.material.color == Color.white ? PT.WHITE : PT.BLACK);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void select()
    {
        Debug.Log("Piece selected");

        isSelected = true;
        cont.isPieceSelected = true;
        cont.selectedPiece = this.gameObject;
        renderer.material.color = Color.cyan;
        cont.showValidMoves();
    }

	void OnMouseDown() {
		if (!cont.isPieceSelected && cont.currentTurn == cont.humanPlayer) {
            if (cont.humanPlayer == Game5Controller.PT.WHITE && pieceType == PT.WHITE)
            {
                select();
            }
            if (cont.humanPlayer == Game5Controller.PT.BLACK && pieceType == PT.BLACK)
            {
                select();
            }
		}
	}
}
