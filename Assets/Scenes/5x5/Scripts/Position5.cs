using UnityEngine;
using System.Collections;

public class Position5 : MonoBehaviour {

	public GameObject gamePiece;

    Game5Controller cont;

	/// <summary>
	/// Establishes reference to GameController, sets
    /// collider to accept mouse events
	/// </summary>
	void Start () {
        cont = (Game5Controller)GameObject.Find("Main Camera").GetComponent("Game5Controller");
        collider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //public void selectForMove() ??

    /// <summary>
    /// On select, iff the piece is of magenta color (valid),
    /// it will call the game controller and prompt it to execute a move
    /// </summary>
    void OnMouseDown()
    {
        if (cont.isPieceSelected && renderer.material.color == Color.magenta)
        {
            cont.doMove(this.gameObject);
        }
    }
}
