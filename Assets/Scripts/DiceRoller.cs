using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public int[] Dice;

    public Sprite[] DieImageOne;
    public Sprite[] DieImageZero;

    private StateManager theStateManager;

    // Start is called before the first frame update
    void Start()
    {
        this.Dice = new int[4];
        this.theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollDice()
    {
        if (this.theStateManager.GameOver)
        {
            return;
        }
        if (this.theStateManager.IsDoneRolling == true)
        {
            Debug.Log("We're Done Rolling!");
            return;
        }

        this.theStateManager.DiceTotal = 0;
        for (int i = 0; i < this.Dice.Length; i++)
        {
            // roll the die
            this.Dice[i] = Random.Range(0, 2);
            this.theStateManager.DiceTotal += this.Dice[i];

            //display a die
            if (this.Dice[i] == 0)
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = this.DieImageZero[Random.Range(0, this.DieImageZero.Length)];
            }
            else
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = this.DieImageOne[Random.Range(0, this.DieImageOne.Length)];
            }
        }

        this.theStateManager.IsDoneRolling = true;
        if(this.theStateManager.CurrentPlayerID == 1)
        {
            this.theStateManager.DiceTotal = 8;
        }

        if (this.theStateManager.DiceTotal == 0)
        {
            this.theStateManager.IsDoneClicking = true;
        }
    }
}
