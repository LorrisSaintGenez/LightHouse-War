using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    public GameController gameController;

	public float speed;
    public float angle;

	private Rigidbody rb;

    public Rigidbody barrel;
    public Transform barrelSpawn;

    public float fireRate = 1F;
    private float nextFire = 0.0F;

    public GUIText barrelText;
    public int barrelCount;

    public GUIText hpText;
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

    void LowerHealthPoint(int damages)
    {
        healthPoint -= damages;
        UpdateHealthPointText();
    }

    void UpdateHealthPointText()
    {
        hpText.text = "HP: " + healthPoint;
    }

    void RemoveBarrel()
    {
        barrelCount--;
        UpdateBarrelCount();
    }

    void UpdateBarrelCount()
    {
        barrelText.text = "Barrels: " + barrelCount;
    }

    // Update is called once per frame
    void FixedUpdate () {
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

    void Fire ()
    {
        if (barrelCount > 0)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Rigidbody newBarrel = Instantiate(barrel, new Vector3(barrelSpawn.position.x, barrelSpawn.position.y + 5, barrelSpawn.position.z), new Quaternion(barrelSpawn.rotation.x, barrelSpawn.rotation.y, barrelSpawn.rotation.z, barrelSpawn.rotation.w)) as Rigidbody;
                newBarrel.velocity = -15f * speed * barrelSpawn.right;
                RemoveBarrel();
                LowerHealthPoint(10);
            }

            if (Input.GetKey(KeyCode.Mouse1) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Rigidbody newBarrel = Instantiate(barrel, new Vector3(barrelSpawn.position.x, barrelSpawn.position.y + 5, barrelSpawn.position.z), new Quaternion(barrelSpawn.rotation.x, barrelSpawn.rotation.y, barrelSpawn.rotation.z + 30, barrelSpawn.rotation.w)) as Rigidbody;
                newBarrel.velocity = -50f * barrelSpawn.right;
                RemoveBarrel();
            }
        }
    }
}
