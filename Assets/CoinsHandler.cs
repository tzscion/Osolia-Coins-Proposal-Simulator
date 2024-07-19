using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsHandler : MonoBehaviour
{
    public int coinsInCirculation;
    public int dailyCoins;
    public int dailyCoinsLonger;
    public int coinsCap;
    public int monthlyCap;
    public int hardCap;
    public List<PlayerHandler> players;
    public int days;
    [SerializeField] private TextMeshProUGUI circulationText;
    public bool newPlayerAdded = false;
    public bool hardCapEnabled;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        coinsInCirculation = 0;
        int temp = 0;
        for(int i = 0; i < players.Count; i++)
        {
            temp += players[i].coins;
        }
        coinsInCirculation = temp;
        circulationText.text = coinsInCirculation.ToString() + " coins in circulation.";
        if(hardCapEnabled)
        {
            hardCap = coinsCap * players.Count;
        }
    }

    public void AddNewPlayer()
    {
        if(!newPlayerAdded)
        {
            GameObject newPlayer = GameObject.Find("NewPlayer");
            players.Add(newPlayer.GetComponent<PlayerHandler>());
        }
    }
}
