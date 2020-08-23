using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public Tile StartTile;
    public int PlayerID;
    public PlayerStoneStorage Storage;
    public Vector3 StartPosition;

    private Tile currentTile = null;
    private StateManager theStateManager;

    private bool isAnimating = false;

    // Start is called before the first frame update
    void Start()
    {
        this.theStateManager = GameObject.FindObjectOfType<StateManager>();
        this.currentTile = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        if (this.theStateManager.GameOver)
        {
            return;
        }

        // Is it our turn?
        if (this.PlayerID != this.theStateManager.CurrentPlayerID)
        {
            return;
        }
        // have we rolled the dice?
        if(theStateManager.IsDoneRolling == false)
        {
            // not ready, leave
            return;
        }
        if (this.theStateManager.IsDoneClicking == true)
        {
            // we've already moved!
            return;
        }

        int? spacesToMove = theStateManager.DiceTotal;

        if(this.StartTile == null)
        {
            return;
        }

        Tile destinationTile = this.currentTile;

        // where are we going to?
        for (int i = 0; i < spacesToMove; i++)
        {
            if (destinationTile == null)
            {
                destinationTile = this.StartTile;
            }
            else
            {
                if (destinationTile.NextTiles == null || destinationTile.NextTiles.Length == 0)
                {
                    Destroy(gameObject);
                    this.theStateManager.IsDoneClicking = true;
                    return;
                }
                else if (destinationTile.NextTiles.Length > 1)
                {
                    destinationTile = destinationTile.NextTiles[this.PlayerID];
                }
                else
                {
                    destinationTile = destinationTile.NextTiles[0];
                }
            }
        }

        // can we even go there?
        if(destinationTile == null)
        {
            // We've finished!
            return;
        }

        // Is there a stone here?
        if (destinationTile.PlayerPiece != null)
        {
            // is it ours?
            if (destinationTile.PlayerPiece.PlayerID == this.PlayerID)
            {
                // we can't go there, so return!
                return;
            }
            else
            {
                destinationTile.PlayerPiece.ReturnToStorage();
            }
        }

        // where are we going?
        Vector3 MoveToPoint = new Vector3(destinationTile.transform.position.x, 1, destinationTile.transform.position.z);


        // TODO: Animate
        Debug.Log("Animate");

        // go there
        if (this.currentTile != null)
        {
            this.currentTile.PlayerPiece = null;
        }
        this.currentTile = destinationTile;
        this.currentTile.PlayerPiece = this;
        this.transform.position = MoveToPoint;
        this.theStateManager.IsDoneClicking = true;
        this.isAnimating = true;
    }

    // Reset Back to Storage
    public void ReturnToStorage()
    {
        this.transform.localPosition = this.StartPosition;
        this.currentTile.PlayerPiece = null;
        this.currentTile = null;
    }
}
