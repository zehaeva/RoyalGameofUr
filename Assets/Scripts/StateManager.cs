using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public int NumberOfPlayers = 2;
    public int CurrentPlayerID = 0;

    public int? DiceTotal = null;
    public bool IsDoneRolling = false;
    public bool IsDoneClicking = false;
    public bool IsDoneAnimating = false; //TODO: Animation
    public bool GameOver = false;

    public enum TurnPhase {WAITING_FOR_ROLL, WAITING_FOR_MOVE, WAITING_FOR_ANIMATION, GAME_OVER};
    public TurnPhase CurrentPhase;

    private PlayerStoneStorage[] playerStoneStorages;

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentPlayerID = 0;
        this.playerStoneStorages = GameObject.FindObjectsOfType<PlayerStoneStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsDoneClicking && this.IsDoneRolling)
        { 
            this.NewTurn();
        } 
        else
        {
            this.checkForWinner();
        }
    }

    public void NewTurn()
    {
        this.IsDoneAnimating = false;
        this.IsDoneClicking = false;
        this.IsDoneRolling = false;
        this.DiceTotal = null;

        this.CurrentPhase = TurnPhase.WAITING_FOR_ROLL;

        this.CurrentPlayerID = (this.CurrentPlayerID + 1) % this.NumberOfPlayers;
    }

    private void checkForWinner()
    {
        foreach (PlayerStoneStorage stoneStorage in this.playerStoneStorages)
        {
            if(stoneStorage.transform.childCount == 0)
            {
                this.GameOver = true;
                this.CurrentPhase = TurnPhase.GAME_OVER;
                return;
            }
        }
        return;
    }
}
