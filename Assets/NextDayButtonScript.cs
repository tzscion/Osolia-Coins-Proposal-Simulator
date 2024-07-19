using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextDayButtonScript : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private CoinsHandler ch;
    [SerializeField] private TextMeshProUGUI dayText;
    private List<PlayerHandler> players;
    public float interval = 0.1f;
    private float timeSinceLastExecution = 0f;
    // Start is called before the first frame update
    void Start()
    {
        players = ch.players;
        dayText.text = "Day " + ch.days.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastExecution += Time.deltaTime;

        if (timeSinceLastExecution >= interval && Input.GetKey("d"))
        {
            Debug.Log("=============================================================================================================================================================");
            Debug.Log("Day " + ch.days.ToString());

            for (int i = 0; i < players.Count; i++)
            {
                players[i].NextDay();
            }

            ch.days++;
            dayText.text = "Day " + ch.days.ToString();

            timeSinceLastExecution = 0f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("=============================================================================================================================================================");
        Debug.Log("Day " + ch.days.ToString());
        for(int i = 0; i < players.Count; i++)
        {
            players[i].NextDay();
        }
        ch.days ++;
        dayText.text = "Day " + ch.days.ToString();
    }
}
