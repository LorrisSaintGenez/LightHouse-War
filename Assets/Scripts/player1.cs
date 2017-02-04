using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player1 : MonoBehaviour {

    public GameObject boat;

    public float speed;
    public float angle;

	private Rigidbody rb;

    public Rigidbody barrel;
    public Transform barrelSpawn;

    public float fireRate = 1F;
    private float nextFire = 0.0F;

    public Text barrelText;
    public int barrelCount;

    public Text hpText;
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

        if (healthPoint <= 0)
            Destroy(boat);
    }

    void Fire ()
    {
        if (barrelCount > 0)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Rigidbody newBarrel = Instantiate(barrel, new Vector3(barrelSpawn.position.x, barrelSpawn.position.y, barrelSpawn.position.z), barrelSpawn.rotation) as Rigidbody;
                newBarrel.velocity = 15f * speed * barrelSpawn.right;
                RemoveBarrel();
            }

            if (Input.GetKey(KeyCode.Mouse1) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Rigidbody newBarrel = Instantiate(barrel, new Vector3(barrelSpawn.position.x, barrelSpawn.position.y, barrelSpawn.position.z), new Quaternion(barrelSpawn.rotation.x, barrelSpawn.rotation.y, barrelSpawn.rotation.z + 45, barrelSpawn.rotation.w)) as Rigidbody;
                newBarrel.velocity = 50f * barrelSpawn.right;
                RemoveBarrel();
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Barrel")
        {
            LowerHealthPoint(10);
            Destroy(col.gameObject);
        }
    }
}
