using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerText : MonoBehaviour
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
        if(this.theStateManager.GameOver)
        {
            GetComponent<Text>().text = "GAME OVER: Player " + (this.theStateManager.CurrentPlayerID + 1) + " Won!";
        }
        else
        {
            GetComponent<Text>().text = "Current Player: " + (this.theStateManager.CurrentPlayerID + 1);
        }
    }
}
