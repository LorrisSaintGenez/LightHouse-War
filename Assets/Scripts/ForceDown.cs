using UnityEngine;
using System.Collections;

public class ForceDown : MonoBehaviour {

	Rigidbody barrel;
	bool touchGround;
	// Use this for initialization
	void Start () {
		barrel = GetComponent<Rigidbody> ();
		touchGround = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y < -5 && touchGround == false) {
			touchGround = true;
			barrel.velocity = Vector3.zero;
			barrel.angularVelocity = Vector3.zero;
		} else if (touchGround == false){
			Vector3 down = new Vector3 (0, -3000, 0);
			barrel.AddForce (down);
		}
	}
}
