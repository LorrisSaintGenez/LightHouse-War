using UnityEngine;
using System.Collections;

public class Alexandrie : MonoBehaviour {

    public Light[] lights;
	/*public Light spotlightOne;
	public Light spotlightTwo;
	public Light spotlightThree;
	public Light spotlightFour;
	public Light point;

	public Light BaseOne;
	public Light BaseTwo;
	public Light BaseThree;
	public Light BaseFour;*/

	public AudioSource explosionSound;

	public Rigidbody Barrel;
	public GameObject explosion;
	public GameObject FireComplex;

    public Transform[] spawnTowers;
	/*public Transform SpawnTowerOne;
	public Transform SpawnTowerTwo;
	public Transform SpawnTowerThree;
	public Transform SpawnTowerFour;*/

	public float SpawnTime;

    public GameController sphereOwner;

    public GameObject waterFlood;

	// 0 no One, 1 player One, 2 player Two
	private int winner;

    private bool waterOverflow;

	// Use this for initialization
	void Start () {
		winner = 0;
		changeUser (winner);
        waterOverflow = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (sphereOwner.getOwner() == null)
		{
			changeUser (0);
		}
        else if (sphereOwner.getOwner() == "Boat_Player_One")
        {
            changeUser (1);
		}
		else
		{
			changeUser (2);
		}

        if (waterFlood.transform.position.y == 0)
        {
            if (!waterOverflow)
            {
                waterOverflow = true;
                InvokeRepeating("TowerFire", 5, SpawnTime);
            }
        }
    }

    IEnumerator fireRoutine(int delay, Transform spawnTower)
    {
        yield return new WaitForSeconds(delay);

        float force = Random.Range(200, 1000);
        Vector3 up = new Vector3(0, Random.Range(35, 80), 0);
        FireProjectile(force, spawnTower, up);
    }
	
    // COROUTINE	

	void TowerFire()
    {

        foreach (Transform spawnTower in spawnTowers)
        {
            int delay = Random.Range(2, 5);
            StartCoroutine(fireRoutine(delay, spawnTower));
        }

		/*float force = Random.Range (200, 1000);
		Vector3 up = new Vector3 (0, Random.Range(35, 80), 0);
		FireProjectile (force, SpawnTowerOne, up);

		force = Random.Range (200, 1000);
		up = new Vector3 (0, Random.Range(35, 80), 0);
		FireProjectile (force, SpawnTowerTwo, up);

		force = Random.Range (200, 1000);
		up = new Vector3 (0, Random.Range(35, 80), 0);
		FireProjectile (force, SpawnTowerThree, up);

		force = Random.Range (200, 1000);
		up = new Vector3 (0, Random.Range(35, 80), 0);
		FireProjectile (force, SpawnTowerFour, up);*/


	}

	void FireProjectile(float force, Transform Spawn, Vector3 up)
	{
		Rigidbody newBarrel3 = Instantiate(Barrel, new Vector3(Spawn.position.x, Spawn.position.y, Spawn.position.z), Spawn.rotation) as Rigidbody;
		newBarrel3.velocity = force * Spawn.forward + up;

		GameObject tmp = Instantiate(explosion, new Vector3(Spawn.position.x, Spawn.position.y, Spawn.position.z), Spawn.rotation) as GameObject;
		Object.Destroy (tmp, 3.0f);

		GameObject fireAttach = Instantiate(FireComplex, new Vector3(Spawn.position.x, Spawn.position.y, Spawn.position.z), Spawn.rotation) as GameObject;
		fireAttach.transform.parent = newBarrel3.transform;
		Object.Destroy (fireAttach, 5.0f);

		GetComponent<AudioSource> ().Play ();
	}

	void changeUser(int val){
		winner = val;
		Color color;
		if (val == 0) {
			color = Color.white;
		} else if (val == 1) {
			color = Color.red;
		} else {
			color = Color.green;
		}
		ApplyColor (color);
	}

	void ApplyColor(Color color)
	{
        foreach(Light l in lights)
        {
            l.color = color;
        }
		/*spotlightOne.color = color;
		spotlightTwo.color = color;
		spotlightThree.color = color;
		spotlightFour.color = color;
		point.color = color;
		BaseOne.color = color;
		BaseTwo.color = color;
		BaseThree.color = color;
		BaseFour.color = color;*/
	}
}
