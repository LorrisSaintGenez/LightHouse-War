using UnityEngine;
using System.Collections;

public class cameraFollowUser : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		//offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		/*Vector3 v3T = transform.position;
		v3T.z = player.transform.position.z + offset.z;
		v3T.x = player.transform.position.x + offset.x;
		transform.position = v3T;*/
	}
}
