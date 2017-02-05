using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player2 : MonoBehaviour
{

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
    void FixedUpdate()
    {
        float translation = 0;
        float rotation = 0;

        if (Input.GetKey(KeyCode.Z))
        {
            //rb.AddForce(-transform.right * Time.deltaTime * speed, ForceMode.Impulse);
            //transform.position -= transform.right * Time.deltaTime * speed;
            translation = -speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            translation = speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rotation = -speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotation = speed * Time.deltaTime;
        }

        transform.Translate(translation, 0, 0);
        transform.Rotate(0, rotation, 0);

        if (healthPoint <= 0)
            Destroy(boat);
    }

    void Fire()
    {
        if (barrelCount > 0)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Rigidbody newBarrel = Instantiate(barrel, barrelSpawn.position, barrelSpawn.rotation) as Rigidbody;
                newBarrel.velocity = 25f * speed * barrelSpawn.right;
                RemoveBarrel();
            }

            if (Input.GetKey(KeyCode.LeftControl) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Rigidbody newBarrel = Instantiate(barrel, barrelSpawn.position, new Quaternion(barrelSpawn.rotation.x, barrelSpawn.rotation.y, 45, barrelSpawn.rotation.w)) as Rigidbody;
                newBarrel.velocity = 50f * barrelSpawn.right;
                RemoveBarrel();
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Barrel")
        {
            col.gameObject.SetActive(false);
            LowerHealthPoint(10);
            Destroy(col.gameObject);
        }
    }
}
