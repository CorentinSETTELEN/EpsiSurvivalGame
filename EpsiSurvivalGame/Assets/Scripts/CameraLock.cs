using UnityEngine;
using System.Collections;

public class CameraLock : MonoBehaviour {

	//public GameObject player;
	public Transform player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {

		//player = GameObject.FindGameObjectWithTag("Player");
		offset = transform.position;
		//offset.y = 3.26f;
	}
	
	// Update is called once per frame
	void Update () {
	
	//	transform.position = player.transform.position + offset;
		transform.position = new Vector3 (player.position.x, player.position.y, offset.z);

	}
}
