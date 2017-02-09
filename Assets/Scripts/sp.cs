using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sp : MonoBehaviour {

	public GameObject[] listOfIa;
	// Use this for initialization
	void Start () {
		print ("win script start");
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

		if (win) {
			PLayerHaveWin ();
		}
	}

	void PLayerHaveWin()
	{
		print ("WIN");
	}
}
