using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStoneStorage : MonoBehaviour
{
    public int PlayerID;
    public GameObject StonePrefab;
    public int TotalPieces;
    public Tile StartTile;
    private GameObject[] playerPieces;

    // Start is called before the first frame update
    void Start()
    {
        this.playerPieces = new GameObject[this.TotalPieces];

        GameObject stone;
        int row = 1;
        int z = 0;

        for (int i = 0; i < this.TotalPieces; i++)
        {
            if (i % 2 == 0)
            {
                row = 2;
                z++;
            }
            else
            {
                row = 1;
            }

            stone = (GameObject)Instantiate(StonePrefab);

            stone.GetComponent<PlayerPiece>().PlayerID = this.PlayerID;
            stone.GetComponent<PlayerPiece>().StartTile = this.StartTile;
            stone.GetComponent<PlayerPiece>().Storage = this;
            stone.GetComponent<PlayerPiece>().StartPosition = new Vector3(z, 0, row);
            stone.transform.SetParent(this.transform);
            stone.transform.localPosition = new Vector3(z, 0, row);

            this.playerPieces[i] = stone;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
