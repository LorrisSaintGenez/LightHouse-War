using UnityEngine;
using System.Collections;

public class cameraFollowUser : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
        GameObject.Find("player1Camera").SetActive(true);
        GameObject.Find("player2Camera").SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
