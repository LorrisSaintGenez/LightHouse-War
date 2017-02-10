using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minesaI : MonoBehaviour {
	
		public float fpsTargetDistance;
		public float enemyLookDistance;
		public float attackDistance;
		public float enemymovementspeed;
		public float damping;
		public Transform fpsTarget;

		public Transform[] points;
		private int destPoint;

		public int count;

		Rigidbody rb;
		Renderer myRender;

		bool isPause;

		private Vector3 lastAgentVelocity;
		private Vector3 lastAgentDestination;

		public Rigidbody barrel;
		public GameObject trainerDeFeu;
		public GameObject feu;
		public Transform barrelSpawn;
		public float speed;

		// Use this for initialization
		void Start () {
			myRender = GetComponent<Renderer>();
			rb = GetComponent<Rigidbody>();
			count = 0;
			isPause = false;

		}


		// Update is called once per frame
		void FixedUpdate () {
			fpsTargetDistance = Vector3.Distance(fpsTarget.position, transform.position);
			if (fpsTargetDistance < attackDistance){
				myRender.material.color = Color.red;
				lookAtPlayer (false);
				Attack ();
			}
			else if (fpsTargetDistance < enemyLookDistance) {
				myRender.material.color = Color.yellow;
				lookAtPlayer (true);
				if (count > 200) {
					count = 0;
					float force = 20;
					Vector3 up = new Vector3 (0, Random.Range (5, 25), 0);
					FireProjectile (force, barrelSpawn, up);
				}
				count++;
			} else {
				myRender.material.color = Color.blue;
			}
		}

		void lookAtPlayer(bool stop){
			if (stop) {
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
			}
			Quaternion rotation = Quaternion.LookRotation (fpsTarget.position - transform.position);
			rotation *= Quaternion.Euler (0, 90, 0);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
		}

		void Attack(){
			if (rb.velocity.y > 50.0f) {
				rb.AddForce (transform.right*250);
			} else {
				rb.AddForce (-transform.right*250*10);
			}
		}

		void FireProjectile(float force, Transform Spawn, Vector3 up)
		{
		    GameObject tmp = Instantiate(feu, new Vector3(Spawn.position.x, Spawn.position.y, Spawn.position.z), Spawn.rotation) as GameObject;
		    tmp.transform.parent = this.transform;
		    Object.Destroy (tmp, 3.0f);
		}
}
