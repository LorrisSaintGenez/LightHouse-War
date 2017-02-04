using UnityEngine;
using System.Collections;

public class RotateTower : MonoBehaviour {

	bool rotatingRight;
	float speed;
	int count;
	bool plain;

	// Use this for initialization
	void Start () {
		if (Random.Range (0, 100) > 50) {
			rotatingRight = true;
		} else {
			rotatingRight = false;
		}
		plain = false;
		speed = Random.Range (0.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		print (transform.rotation);
		if (rotatingRight) {
			transform.Rotate (0, speed, 0, Space.World);
			count++;
		} else {
			transform.Rotate (0, -speed, 0, Space.World);
			count++;
		}
		if (count > 500 && !plain || count > 1000 && plain) {
			plain = true;
			count = 0;
			print ("Change");
			rotatingRight = !rotatingRight;
		}

	}
}