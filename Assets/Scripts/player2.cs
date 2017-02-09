using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class player2 : MonoBehaviour {

	public GameObject boat;

	public float speed;
	public float angle;

	private Rigidbody rb;

	public GameObject ComplexFire;
	public GameObject ExplosionBig;
	public GameObject trainerDeFeu;

	public Rigidbody barrel;
	public Transform barrelSpawn;

	public float fireRate = 1F;
	private float nextFire = 0.0F;

	public Text barrelText;
	public int barrelCount;

	public Slider healthBar;
	public int healthPoint;


	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		Fire();
	}

	void addFire()
	{
		GameObject fire = Instantiate (ComplexFire,
			new Vector3 (transform.position.x, transform.position.y + 1 + (100 - healthPoint) / 50, transform.position.z), barrelSpawn.rotation) as GameObject;
		fire.transform.parent = transform;

		Object.Destroy (fire, 5.0f);
		Invoke ("addFire", 5);
	}

	void LowerHealthPoint(int damages)
	{
		healthPoint -= damages;

		addFire ();

		UpdateHealthPointBar();
	}

	void UpdateHealthPointBar()
	{
		healthBar.value = 100 - healthPoint;
	}

	void RemoveBarrel()
	{
		barrelCount--;
		UpdateBarrelCount();
	}

	void AddBarrel()
	{
		barrelCount += 5;
		UpdateBarrelCount();
	}

	void UpdateBarrelCount()
	{
		barrelText.text = "x" + barrelCount;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.C)) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero; 
		}

		/*if (Input.GetKey(KeyCode.UpArrow))
		{
            //rb.velocity += transform.right * Time.deltaTime * speed;
			rb.AddForce(-transform.right * Time.deltaTime * speed, ForceMode.Impulse);
			//transform.position -= transform.right * Time.deltaTime * speed;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			rb.AddForce(transform.right * Time.deltaTime * speed * 2, ForceMode.Impulse);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate(0, Time.deltaTime * -angle, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate(0, Time.deltaTime * angle, 0);
		}*/

		float translation = 0;
		float rotation = 0;

		if (Input.GetKey(KeyCode.Z))
		{
			rb.AddForce (-rb.transform.right * speed * 10);
			//rb.AddForce(-transform.right * Time.deltaTime * speed, ForceMode.Impulse);
			//transform.position -= transform.right * Time.deltaTime * speed;
			//translation = -speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			rb.AddForce (rb.transform.right * speed * 10);
			//translation = speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.Q))
		{
			rb.transform.Rotate (new Vector3 (0f, -speed, 0f) * Time.deltaTime);
			//rotation = -speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			rb.transform.Rotate (new Vector3 (0f, speed, 0f) * Time.deltaTime);
			//rotation = speed * Time.deltaTime;
		}

		//transform.Translate(translation, 0, 0);
		transform.Rotate(0, rotation, 0);

		if (healthPoint <= 0)
			Destroy(boat);
	}

	void Fire ()
	{
		if (barrelCount > 0)
		{
			if (Input.GetKey(KeyCode.LeftControl) && Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;

				float force = 50;
				Vector3 up = new Vector3 (0, 50, 0);
				FireProjectile (force, barrelSpawn, up);
				/*Rigidbody newBarrel = Instantiate(barrel, barrelSpawn.position, barrelSpawn.rotation) as Rigidbody;*/
				/*newBarrel.velocity = 15f * speed * barrelSpawn.right;*/
				RemoveBarrel();
			}

			if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;

				float force = 50;
				Vector3 up = new Vector3 (0, 10, 0);
				FireProjectile (force, barrelSpawn, up);
				/*Rigidbody newBarrel = Instantiate(barrel, barrelSpawn.position, new Quaternion(barrelSpawn.rotation.x, barrelSpawn.rotation.y, barrelSpawn.rotation.z + 45, barrelSpawn.rotation.w)) as Rigidbody;
                newBarrel.velocity = 50f * speed * barrelSpawn.right;*/
				RemoveBarrel();
			}

			if (Input.GetKey(KeyCode.M))
			{
				LowerHealthPoint (1);
			}
		}
	}

	void FireProjectile(float force, Transform Spawn, Vector3 up)
	{
		Rigidbody newBarrel3 = Instantiate(barrel, new Vector3(Spawn.position.x, Spawn.position.y, Spawn.position.z), Spawn.rotation) as Rigidbody;
		newBarrel3.velocity = (speed * 0.2f) * force * barrelSpawn.right + up;

		GameObject tmp = Instantiate(trainerDeFeu, new Vector3(Spawn.position.x, Spawn.position.y, Spawn.position.z), Spawn.rotation) as GameObject;
		tmp.transform.parent = newBarrel3.transform;
		Object.Destroy (tmp, 3.0f);

	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Barrel")
		{
			LowerHealthPoint(10);
			Explosion ();
			Destroy(col.gameObject);
		}

		if (col.gameObject.name.StartsWith("treasure_box"))
		{
			Destroy(col.gameObject);
			AddBarrel();
		}
	}

	void Explosion()
	{
		GameObject explosion = Instantiate (ExplosionBig,
			new Vector3 (transform.position.x, transform.position.y, transform.position.z), barrelSpawn.rotation) as GameObject;
		explosion.transform.parent = transform;

		Object.Destroy (explosion, 5.0f);
	}

}
