using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public float speed;

	private Rigidbody rb;

    public Rigidbody barrel;
    public Transform barrelSpawn;

    public float fireRate = 1F;
    private float nextFire = 0.0F;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
    void Update () {
        Fire();
    }

	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveVertical, 0.0f, -moveHorizontal);

		rb.AddForce (movement * speed);
	}

    void Fire () {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Rigidbody newBarrel = Instantiate(barrel, barrelSpawn.position, barrelSpawn.rotation) as Rigidbody;
            newBarrel.velocity = -50f * barrelSpawn.right;
        }
    }
}
