﻿using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public float speed;
    public float angle;

	private Rigidbody rb;

    public Rigidbody barrel;
    public Transform barrelSpawn;

    public float fireRate = 1F;
    private float nextFire = 0.0F;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

    void Update() {
        Fire();
        Move();
    }

    // Update is called once per frame
    void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveVertical, 0.0f, -moveHorizontal);

        Vector3 accel = movement * speed;

		rb.AddForce (accel);
    }

    void Fire () {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Rigidbody newBarrel = Instantiate(barrel, new Vector3(barrelSpawn.position.x, barrelSpawn.position.y + 5, barrelSpawn.position.z), new Quaternion(barrelSpawn.rotation.x, barrelSpawn.rotation.y, barrelSpawn.rotation.z, barrelSpawn.rotation.w)) as Rigidbody;
            newBarrel.velocity = -15f * speed * barrelSpawn.right;
        }

        if (Input.GetKey(KeyCode.Mouse1) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Rigidbody newBarrel = Instantiate(barrel, new Vector3(barrelSpawn.position.x, barrelSpawn.position.y + 5, barrelSpawn.position.z), new Quaternion(barrelSpawn.rotation.x, barrelSpawn.rotation.y, barrelSpawn.rotation.z + 30, barrelSpawn.rotation.w)) as Rigidbody;
            newBarrel.velocity = -50f * barrelSpawn.right;
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(-transform.right * Time.deltaTime * speed, ForceMode.Impulse);
            //transform.position -= transform.right * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(transform.right * Time.deltaTime * speed, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, Time.deltaTime * -angle, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, Time.deltaTime * angle, 0);
        }
    }
}
