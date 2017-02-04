using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Vector3 position;
    public float radius;
    public float timeCapture;

    public Text timer_player_1;
    public Text timer_player_2;

    private int count = 0;
    private float timeLeft;

    private Text ownerText;
    private string owner;

    void Start()
    {
        timeLeft = timeCapture;
        owner = null;
        ownerText = timer_player_1;
    }

    void Update()
    {
        if (timeLeft < 0)
        {
            GameOver(owner);
        }
        else if (count == 1 && timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
            ownerText.text = "Time: " + Mathf.Round(timeLeft);
        }
        else if (count != 1)
        {
            timeLeft = timeCapture;
            ownerText.text = "Time: " + Mathf.Round(timeLeft);
            owner = null;
        }
    }

    public string getOwner()
    {
        return owner;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            count++;
            if (count == 1)
            {
                if (c.gameObject.name == "Boat_Player_One")
                {
                    ownerText = timer_player_1;
                    owner = c.gameObject.name;
                }
                else
                {
                    ownerText = timer_player_2;
                    owner = c.gameObject.name;
                }
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            count--;
            if (count == 1)
            {
                if (c.gameObject.name == "Boat_Player_One")
                {
                    ownerText = timer_player_2;
                    owner = "Boat_Player_Two";
                }
                else
                {
                    ownerText = timer_player_1;
                    owner = "Boat_Player_One";
                }
            }
        }
    }

    public void GameOver(string winner)
    {
        Debug.Log("The winner is: " + winner);
    }
}
