﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iaStupid : MonoBehaviour {
	public float fpsTargetDistanceOne;
	public float fpsTargetDistanceTwo;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemymovementspeed;
	public float damping;

	public Transform[] targets;
	private int destPoint;

	public int count;

	Rigidbody rb;
	Renderer myRender;

	bool isPause;
	bool dead;

	public GameObject ExplosionBig;

	private Vector3 lastAgentVelocity;
	private Vector3 lastAgentDestination;

	public Rigidbody barrel;
	public GameObject trainerDeFeu;
	public Transform barrelSpawn;
	public float speed;

	// Use this for initialization
	void Start () {
		myRender = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
		print ("ok");
		count = 0;
		isPause = false;

	}


	// Update is called once per frame
	void FixedUpdate () {
		if (dead) {
			return;
		}
		//rb.transform = agent.transform;
		fpsTargetDistanceOne = Vector3.Distance(targets[0].position, transform.position);
		fpsTargetDistanceTwo = Vector3.Distance(targets[1].position, transform.position);
		if (fpsTargetDistanceOne < attackDistance) {
			myRender.material.color = Color.red;
			lookAtPlayer (false, 0);
			Attack ();
			print ("Attack");
		} else if (fpsTargetDistanceTwo < attackDistance) {
			myRender.material.color = Color.red;
			lookAtPlayer (false, 1);
			Attack ();
			print ("Attack");
		} else if (fpsTargetDistanceOne < enemyLookDistance) {
			myRender.material.color = Color.yellow;
			lookAtPlayer (true, 0);
			print ("look at target ");
			if (count > 200) {
				count = 0;
				float force = 20;
				Vector3 up = new Vector3 (0, Random.Range (5, 25), 0);
				StartCoroutine (FireProjectile (force, barrelSpawn, up));
			}
			count++;
		} else if (fpsTargetDistanceTwo < enemyLookDistance) {
			myRender.material.color = Color.yellow;
			lookAtPlayer (true, 1);
			print ("look at target ");
			if (count > 200) {
				count = 0;
				float force = 20;
				Vector3 up = new Vector3 (0, Random.Range (5, 25), 0);
				StartCoroutine (FireProjectile (force, barrelSpawn, up));
			}
			count++;
		} else {
			myRender.material.color = Color.blue;
		}
		/*rb.velocity = agent.velocity;
		agent.nextPosition = rb.position;*/
	}

	void lookAtPlayer(bool stop, int index){
		if (stop) {
			print ("case vitesse");
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
		Quaternion rotation = Quaternion.LookRotation (targets[index].position - transform.position);
		rotation *= Quaternion.Euler (0, 90, 0);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
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
		yield return new WaitForSeconds (0.1f);
		print("cgi");
		if (newBarrel3 != null) {
			GameObject tmp = Instantiate (trainerDeFeu, new Vector3 (Spawn.position.x, Spawn.position.y, Spawn.position.z), Spawn.rotation) as GameObject;
			tmp.transform.parent = newBarrel3.transform;
			Object.Destroy (tmp, 3.0f);
			StartCoroutine (trackParticules (newBarrel3, 0));
		}
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
