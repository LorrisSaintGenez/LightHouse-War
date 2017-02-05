using UnityEngine;
using System.Collections;

public class Alexandrie : MonoBehaviour {

	public Light spotlightOne;
	public Light spotlightTwo;
	public Light spotlightThree;
	public Light spotlightFour;
	public Light point;

	public Light BaseOne;
	public Light BaseTwo;
	public Light BaseThree;
	public Light BaseFour;

	public GameObject socle;

	public Rigidbody Barrel;
	public GameObject explosion;
	public Transform SpawnTowerOne;

	public Transform SpawnTowerTwo;

	public Transform SpawnTowerThree;

	public Transform SpawnTowerFour;

	public float SpawnTime;

    public GameController sphereOwner;

	// 0 no one, 1 player One, 2 player Two
	private int winner;

	// Use this for initialization
	void Start () {
		winner = 0;
		changeUser (winner);
		InvokeRepeating("TowerFire", 5, 5);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        string owner = sphereOwner.getOwner();
		if (owner == null)
			changeUser (0);
        else if (owner == "Boat_Player_One")
            changeUser (1);
		else
			changeUser (2);

	}

	void TowerFire(){
		float force = Random.Range (20f, 50f);
		Vector3 up = new Vector3 (0, Random.Range(20f, 40f), 0);

		Vector3 vect = new Vector3 (0, 1, -1);
		Rigidbody newBarrel = Instantiate(Barrel, new Vector3(SpawnTowerOne.position.x, SpawnTowerOne.position.y, SpawnTowerOne.position.z), SpawnTowerOne.rotation) as Rigidbody;
		Instantiate(explosion, new Vector3(SpawnTowerOne.position.x, SpawnTowerOne.position.y, SpawnTowerOne.position.z), SpawnTowerOne.rotation);
		newBarrel.velocity = force * -SpawnTowerOne.forward + up;

		Vector3 vect1 = new Vector3 (1, 1, 0);
		Rigidbody newBarrel1 = Instantiate(Barrel, new Vector3(SpawnTowerTwo.position.x, SpawnTowerTwo.position.y, SpawnTowerTwo.position.z), SpawnTowerTwo.rotation) as Rigidbody;
		Instantiate (explosion, new Vector3 (SpawnTowerTwo.position.x, SpawnTowerTwo.position.y, SpawnTowerTwo.position.z), SpawnTowerTwo.rotation);
		newBarrel1.velocity = force * SpawnTowerTwo.right + up;

		Vector3 vect2 = new Vector3 (-1, 1, 0);
		Rigidbody newBarrel2 = Instantiate(Barrel, new Vector3(SpawnTowerThree.position.x, SpawnTowerThree.position.y, SpawnTowerThree.position.z), SpawnTowerThree.rotation) as Rigidbody;
		Instantiate(explosion, new Vector3(SpawnTowerThree.position.x, SpawnTowerThree.position.y, SpawnTowerThree.position.z), SpawnTowerThree.rotation);
		newBarrel2.velocity = force * -SpawnTowerThree.right + up;

		Vector3 vect3 = new Vector3 (0 ,1, 1);
		Rigidbody newBarrel3 = Instantiate(Barrel, new Vector3(SpawnTowerFour.position.x, SpawnTowerFour.position.y, SpawnTowerFour.position.z), SpawnTowerFour.rotation) as Rigidbody;
		Instantiate(explosion, new Vector3(SpawnTowerFour.position.x, SpawnTowerFour.position.y, SpawnTowerFour.position.z), SpawnTowerFour.rotation);
		print (SpawnTowerFour.forward);
		newBarrel3.velocity = force * SpawnTowerFour.forward + up;
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
		spotlightOne.color = color;
		spotlightTwo.color = color;
		spotlightThree.color = color;
		spotlightFour.color = color;
		point.color = color;
		BaseOne.color = color;
		BaseTwo.color = color;
		BaseThree.color = color;
		BaseFour.color = color;

		socle.GetComponent<MeshRenderer> ().material.color = color;
	}
}
