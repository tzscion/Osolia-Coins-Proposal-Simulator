using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class PlayerHandler : MonoBehaviour
{
    public int coins;
    public float chanceToLogIn;
    public float chanceToLogInLonger;
    public float chanceToSpend;
    public int price;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CoinsHandler ch;
    [SerializeField] private TransactionHandlerScript ths;
    [SerializeField] private int index;
    public int daysEarned; 
    public int purchases;
    public int moneySpent;
    private int lastInterval = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Player " + index.ToString() + "'s Balance = " + coins.ToString();
    }

    public void NextDay()
    {
        float chance = Random.Range(0f, 1f);
        float spendChance = Random.Range(0f, 1f);
        //Debug.Log("chance rolled = " + chance.ToString());
        if(ch.days % 30 == 0)
        {
            daysEarned = 0;
        }
        if (coins < ch.coinsCap && (!ch.hardCapEnabled || ch.coinsInCirculation < ch.hardCap))
        {
            if(daysEarned < ch.monthlyCap)
            {
                if (chance < chanceToLogIn)
                {
                    coins += ch.dailyCoins;
                    daysEarned ++;
                    if(chance < chanceToLogInLonger)
                    {
                        coins += ch.dailyCoinsLonger;
                        Debug.Log(name + " got the log in bonus today as well as the additional log in bonus");
                    }
                    else
                    {
                        Debug.Log(name + " got the log in bonus today");
                    }
                }
                else
                {
                    Debug.Log(name + " did not log in today");
                }
            }
        }
        if (chance < chanceToLogIn)
        {
            if(spendChance < chanceToSpend)
            {
                ths.Transaction(this, ch.players);
            }
            else
            {
                Debug.Log(name + " did not purchase anything today");
            }
        }
        /*
        Code is being weird, but this is to try and simulate price changes in the market as the coins in other players' balances change

        var sortedPlayers = ch.players.OrderBy(p => p.coins).ToList();
        int currentPlayerIndex = sortedPlayers.FindIndex(p => p == this);
        if (currentPlayerIndex >= 0 && sortedPlayers.Count > 1)
        {
            PlayerHandler nextLowestPlayer = sortedPlayers.ElementAtOrDefault(currentPlayerIndex + 1);
            PlayerHandler leastCoinsPlayer = sortedPlayers.First();

            bool priceIncreased = false;
            bool priceDecreased = false;

            if (coins <= 0.75f * nextLowestPlayer.coins)
            {
                priceIncreased = true;
            }

            if (coins >= 3f * leastCoinsPlayer.coins)
            {
                if (price > 1)
                {
                    priceDecreased = true;
                }
            }
            if (priceIncreased && priceDecreased)
            {
                priceIncreased = false;
                priceDecreased = false;
                Debug.Log($"{name} did not change their price");
            }
            else
            {
                if (priceIncreased)
                {
                    price++;
                    Debug.Log($"{name} increased their price");
                }

                if (priceDecreased)
                {
                    price--;
                    Debug.Log($"{name} decreased their price");
                }

                if (!priceIncreased && !priceDecreased)
                {
                    Debug.Log($"{name} did not change their price");
                }
            }
        }
        */

        //This code attempts to do that with a more simplified implementation
        int currentInterval = (ch.coinsInCirculation / ch.coinsCap) * ch.coinsCap;
        if (currentInterval > lastInterval) 
        {
            price++;
            lastInterval = currentInterval;
            Debug.Log($"{name} increased their prices");
        }

    }
}
