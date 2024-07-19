using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionHandlerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transaction(PlayerHandler buyer, List<PlayerHandler> players)
    {
        if (players.Count < 2)
        {
            Debug.Log("Not enough players for a transaction.");
            return;
        }

        PlayerHandler seller;
        do
        {
            seller = players[Random.Range(0, players.Count)];
        } while (seller == buyer);
        int quantity = Random.Range(1,1000);
        if(seller.price * quantity < buyer.coins)
        {
            buyer.coins -= seller.price * quantity;
            seller.coins += seller.price * quantity;
            buyer.moneySpent += seller.price * quantity;
            buyer.purchases ++;
            Debug.Log(buyer.name + " purchased " + quantity.ToString() + " items from " + seller.name + " for " + seller.price * quantity + " coins");
        }
        else
        {
            Debug.Log("Transaction too expensive");
        }
    }
}
