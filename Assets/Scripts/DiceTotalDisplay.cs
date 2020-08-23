using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceTotalDisplay : MonoBehaviour
{
    StateManager theStateManager;
    // Start is called before the first frame update
    void Start()
    {
        this.theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.theStateManager.DiceTotal == null)
        {
            GetComponent<Text>().text = "Roll The Dice";
        }
        else
        {
            GetComponent<Text>().text = "= " + this.theStateManager.DiceTotal;
        }
    }
}
