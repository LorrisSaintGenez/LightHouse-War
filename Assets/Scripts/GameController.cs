using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Vector3 position;
    public float radius;
    public float timeCapture;

    public Text timer_player_1;
    public Text timer_player_2;

    public GameObject boat1;
    public GameObject boat2;

    public Canvas CanvasEndPlayer1;
    public Canvas CanvasEndPlayer2;

    public RawImage ImageEndPlayer1;
    public RawImage ImageEndPlayer2;

    public Texture victory;
    public Texture defeat;

    public GameObject waterFlood;

    public GameObject collectableChest;
    public Vector3[] chestSpawnPoints;

    private int count = 0;
    private float timeLeft;

    private Text ownerText;
    private string owner;

    private int buttonWidth = 200;
    private int buttonHeight = 50;
    private int groupWidth = 200;
    private int groupHeight = 170;

    private bool paused = false;
    private bool gameEnd = false;
    private bool waterOverflow = false;

    void Start()
    {
        timeLeft = timeCapture;
		GetComponent<AudioSource> ().Play ();
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

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        if (gameEnd)
        {
            Time.timeScale = 0;
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (waterFlood.transform.position.y == 0)
        {
            if (!waterOverflow)
            {
                waterOverflow = true;
                InvokeRepeating("spawnChest", 5, 15);
            }
        }
    }

    void spawnChest()
    {
        int randomSpawnPoint = Random.Range(0, chestSpawnPoints.Length);
        Instantiate(collectableChest, chestSpawnPoints[randomSpawnPoint], new Quaternion(0f, 0f, 0f, 0f));
    }

    void OnGUI()
    {
        if (paused)
        {
            GUI.BeginGroup(new Rect(((Screen.width / 2) - (groupWidth / 2)), ((Screen.height / 2) - (groupHeight / 2)), groupWidth, groupHeight));
            if (GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "Main Menu"))
            {
                SceneManager.LoadScene(0);
            }
            if (GUI.Button(new Rect(0, 60, buttonWidth, buttonHeight), "Resume"))
            {
                paused = false;
            }

            if (GUI.Button(new Rect(0, 120, buttonWidth, buttonHeight), "Quit Game"))
            {
                Application.Quit();
            }
            GUI.EndGroup();
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
        if (boat1.name == winner)
        {
            ImageEndPlayer1.texture = victory;
            ImageEndPlayer2.texture = defeat;

            CanvasEndPlayer1.planeDistance = 1;
            CanvasEndPlayer2.planeDistance = 1;
        }
        else
        {
            ImageEndPlayer1.texture = defeat;
            ImageEndPlayer2.texture = victory;

            CanvasEndPlayer1.planeDistance = 1;
            CanvasEndPlayer2.planeDistance = 1;
        }

        gameEnd = true;
    }
}
