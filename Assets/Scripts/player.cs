using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	public float angle;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate  () {
		/*float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (-moveVertical, 0.0f, 0.0f);


		rb.AddForce (movement * speed);
		if(Input.GetKeyUp(KeyCode.RightArrow))
			rb.AddTorque (0.0f, 2f, 0.0f);
		if(Input.GetKeyUp(KeyCode.LeftArrow))
			rb.AddTorque (0.0f, -2f, 0.0f);


		transform.position += transform.forward * Time.deltaTime * speed;*/
	}

	void Update()
	{
		Vector3 movement = new Vector3 (transform., transform., 0.0f);
		if(Input.GetKey(KeyCode.UpArrow)) {
			transform.position += movement * Time.deltaTime * speed;
		}
		else if(Input.GetKey(KeyCode.DownArrow)) {
			rb.position -= movement * Time.deltaTime * speed;
		}

		if(Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(0, Time.deltaTime * 5, 0);
		}
		else if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(0, Time.deltaTime * -5, 0);
		}
	}
}
