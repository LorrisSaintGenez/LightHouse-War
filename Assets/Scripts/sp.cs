using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sp : MonoBehaviour {

    public Canvas CanvasEndPlayer1;
    public RawImage ImageEndPlayer1;

    public Texture victory;
    public Texture defeat;

    public Slider healthBar;

    public GameObject[] listOfIa;

    private bool gameOver;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		bool win = true;
		foreach (GameObject go in listOfIa) {
			if (go != null) {
				win = false;
				break;
			}
		}

        if (healthBar.value >= 100)
        {
            PlayerHaveLost();
            gameOver = true;
        }

		if (win) {
            Time.timeScale = 0;
			PlayerHaveWin ();
            gameOver = true;
        }

        if (gameOver)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene(0);
            }
        }
	}

    void PlayerHaveLost()
    {
        ImageEndPlayer1.texture = defeat;
        CanvasEndPlayer1.planeDistance = 1;
    }


    void PlayerHaveWin()
	{
        ImageEndPlayer1.texture = victory;
        CanvasEndPlayer1.planeDistance = 1;
    }
}
