using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

	public GameObject weapon;
	public Transform firePoint;
	private Vector3 fireDirection;
	private RaycastHit2D hit;
	private float distance;
	public LayerMask layer;
	public float time;
	private bool canShoot = true;
	public float fireRate;

	// Use this for initialization
	void Start () {

		StartCoroutine (SelfDestruct (time));
		distance = GetComponent<CircleCollider2D> ().radius;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D (Collider2D obj){
		if (obj.gameObject.tag == "Enemy") {
			Vector3 rayDirection = obj.gameObject.transform.position - transform.position;
			hit = Physics2D.Raycast (transform.position, rayDirection, distance, layer);
			if (hit.transform != null && hit.transform.tag == "Enemy") { 
					float angle = Mathf.Atan2 (rayDirection.y, rayDirection.x);
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis ((angle * Mathf.Rad2Deg) - 90.0f, Vector3.forward), 1.0f);
					if (canShoot) {
						ShootEnemy();
					}
			}

		}
	}

	void ShootEnemy(){

		canShoot = false;
		Instantiate (weapon, firePoint.position, firePoint.rotation);
		StartCoroutine(ResetFire (fireRate));

	}

	IEnumerator ResetFire(float t){
		yield return new WaitForSeconds (t);
		canShoot = true;
	}

	IEnumerator SelfDestruct(float t){
		yield return new WaitForSeconds (t);
		Destroy (transform.parent.gameObject);
	}
}
