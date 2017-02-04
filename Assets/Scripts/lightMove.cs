using UnityEngine;
using System.Collections;

public class lightMove : MonoBehaviour {

	public float angle;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(0, angle * Time.deltaTime, 0, Space.World);
	}
}
