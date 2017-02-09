using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iaEasy : MonoBehaviour {

	public float fpsTargetDistance;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemymovementspeed;
	public float damping;
	public Transform fpsTarget;

	public Transform[] points;
	private int destPoint;
	private NavMeshAgent agent;

	public int count;

	Rigidbody rb;
	Renderer myRender;

	bool isPause;

	public int refreshrate;

	private Vector3 lastAgentVelocity;
	private NavMeshPath lastAgentPath;
	private Vector3 lastAgentDestination;
	public GameObject ExplosionBig;

	public Rigidbody barrel;
	public GameObject trainerDeFeu;
	public Transform barrelSpawn;
	public float speed;

	private bool dead;

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		if(!player){
			Debug.Log("Make sure your player is tagged!!");
		}

		agent = GetComponent<NavMeshAgent> ();
		agent.autoBraking = false;

		agent.updatePosition = true;
		agent.updateRotation = true;

		myRender = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
		print ("ok");

		count = 0;
		isPause = false;

		refreshrate = 500;
		GoToNextPoint ();
	}
		
	void GoToNextPoint(){
		if (points.Length == 0) {
			return;
		}

		agent.destination = points[destPoint].position;
		lookAtPoint ();
		destPoint = (destPoint + 1) % points.Length;

		print ("Moving");
	}


	// Update is called once per frame
	void FixedUpdate () {
		
		if (dead) {
			return;
		}
		//rb.transform = agent.transform;
		fpsTargetDistance = Vector3.Distance(fpsTarget.position, transform.position);
		if (fpsTargetDistance < attackDistance){
			//pause ();
			myRender.material.color = Color.red;
			//lookAtPlayer (false);
			if (refreshrate > 500) {
				resume ();
				print("Refreshing");
				GetComponent<NavMeshAgent>().destination = player.transform.position;
				print("front");
				agent.speed = agent.speed * 20;
				rb.AddForce (-transform.right*250*10);
				refreshrate = 0;
			}
			refreshrate++;
			//Attack ();
			print ("Attack");
		}
		else if (fpsTargetDistance < enemyLookDistance) {
			
			pause ();

			myRender.material.color = Color.yellow;
			agent.velocity = Vector3.zero;
			if (count % 10 == 0) {
				lookAtPlayer (true);
			}
			print ("look at target ");
			if (count > 500) {
				count = 0;
				float force = 20;
				Vector3 up = new Vector3 (0, Random.Range(5,25), 0);
				print ("MERDE");
				StartCoroutine(FireProjectile (force, barrelSpawn, up));
			}
			count++;
			print (count);
		} else {
			myRender.material.color = Color.blue;
			if (isPause)
				resume ();
			if (agent.remainingDistance < 0.5f) {
				GoToNextPoint ();
			}
		}
		/*rb.velocity = agent.velocity;
		agent.nextPosition = rb.position;*/
	}
	void pause()
	{
		print ("stop");
		isPause = true;
		agent.Stop ();
	}

	void resume(){
		print ("resume");
		agent.Resume ();
	}

	void lookAtPlayer(bool stop){
		if (stop) {
			print ("case vitesse");
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
		Quaternion rotation = Quaternion.LookRotation (fpsTarget.position - transform.position);
		rotation *= Quaternion.Euler (0, 90, 0);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
	}

	void lookAtPoint(){
		/*print ("look at point ");
		Quaternion rotation = Quaternion.LookRotation (points[destPoint].position - transform.position);
		rotation *= Quaternion.Euler (0, 90, 0);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);*/
	}

	void Attack(){
		if (rb.velocity.y > 50.0f) {
			print("back" + rb.velocity.y);
			rb.AddForce (transform.right*250);
		} else {
			print("front");
			rb.AddForce (-transform.right*250*10);
		}
	}
		
	IEnumerator FireProjectile(float force, Transform Spawn, Vector3 up)
	{
		Rigidbody newBarrel3 = Instantiate(barrel, new Vector3(Spawn.position.x, Spawn.position.y, Spawn.position.z), Spawn.rotation) as Rigidbody;
		newBarrel3.velocity = (speed * 0.02f) * force * barrelSpawn.right + up;
		print("fire");
		Object.Destroy (newBarrel3, 10.0f);
		yield return new WaitForSeconds (0.1f);
		print("cgi");
		GameObject tmp = Instantiate(trainerDeFeu, new Vector3(Spawn.position.x, Spawn.position.y, Spawn.position.z), Spawn.rotation) as GameObject;
		tmp.transform.parent = newBarrel3.transform;
		Object.Destroy (tmp, 3.0f);
		StartCoroutine (trackParticules (newBarrel3, 0));
	}

	IEnumerator trackParticules(Rigidbody newBarrel3, int val)
	{
		GameObject tmp = Instantiate(trainerDeFeu, new Vector3(newBarrel3.transform.position.x, newBarrel3.transform.position.y, newBarrel3.transform.position.z), newBarrel3.transform.rotation) as GameObject;
		tmp.transform.parent = newBarrel3.transform;
		Object.Destroy (tmp, 3.0f);
		yield return new WaitForSeconds (3.0f);
		val++;
		if (val < 20)
			trackParticules (newBarrel3, val);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Barrel")
		{
			Explosion ();
			rb.AddForce (0, -40, 0);
			pause ();
			dead = true;
			Destroy(transform.parent.gameObject, 5.0f);
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
