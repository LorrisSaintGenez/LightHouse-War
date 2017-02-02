using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public float speed;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate  () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (-moveVertical, 0.0f, moveHorizontal);

		rb.AddForce (movement * speed);
	}
}
