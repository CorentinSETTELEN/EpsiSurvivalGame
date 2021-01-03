using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private GameObject player;
	public GameStats stats;
	public float damage;
	public float speed;
	public GameObject weapon;
	public float health;
	public LayerMask layer;
	public int distance;
	private RaycastHit2D hit;
	private bool inView = false;
	private bool dodging = false;
	private bool canAttack = true;

	private Vector3 lastSeen;
	//private Collider2D col;
	private Rigidbody2D enemybody;

	public float moveDelay;
	private float mdCounter;
	public float movingIn;
	private float miCounter;
	private bool moving;
	private Vector3 moveDirection;
	private Vector3 dodgeDirection;

	public float dodgeProb;
	public float range;
	public GameObject weaponPivot;
	public Transform firePoint;
	private bool canShoot = true;
	public float coolDown;

	// Use this for initialization
	void Start () {
		
		player = GameObject.FindWithTag ("Player");
		stats = player.GetComponent<GameStats> ();

		//col = GetComponent<Collider2D> ();
		enemybody = GetComponent<Rigidbody2D> ();

		mdCounter = Random.Range(moveDelay * .5f , moveDelay * 1.5f);
		miCounter = Random.Range(movingIn * .5f, movingIn * 1.5f);
	
	}

	// Update is called once per frame
	void Update () {

		Vector3 rayDirection = player.transform.position - transform.position;
		dodgeDirection = new Vector3(rayDirection.y,-rayDirection.x, 0);
		hit = Physics2D.Raycast (transform.position, rayDirection, distance, layer);
		if (!stats.dead && hit.transform != null && hit.transform.tag == "Player") { 
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
			lastSeen = player.transform.position;
			inView = true;

			if (/*rayDirection.magnitude <= range && */weapon != null) {
				float angle = Mathf.Atan2 (rayDirection.y, rayDirection.x);
				weaponPivot.transform.rotation = Quaternion.Slerp (weaponPivot.transform.rotation, Quaternion.AngleAxis ((angle * Mathf.Rad2Deg) - 90.0f, Vector3.forward), 2.0f);
				if (canShoot) {
					ShootPlayer ();
				}
			}
		} else {
			// there is something obstructing the view
			if (lastSeen != Vector3.zero && inView) {
				transform.position = Vector2.MoveTowards (transform.position, lastSeen, speed * Time.deltaTime);
				inView = false;
			} else {
				if (moving) {
					miCounter -= Time.deltaTime;
					if (!dodging) {
						enemybody.velocity = moveDirection;
					}
					if (miCounter < 0f) {
						moving = false;
						miCounter = Random.Range (movingIn * .5f, movingIn * 1.5f);
					}
				} else {
					mdCounter -= Time.deltaTime;
					if (!dodging) {
						enemybody.velocity = Vector2.zero;
					}
					if (mdCounter < 0f) {
						moving = true;
						mdCounter = Random.Range (moveDelay * .5f, moveDelay * 1.5f);

						moveDirection = new Vector3 (Random.Range (-1f, 1f) * speed / 2, Random.Range (-1f, 1f) * speed / 2, 0f);
					}
				}
			}
		}
		if (health <= 0) {
            stats.health += 10;
			stats.points += 10;
			Destroy (gameObject);
		}
	}
	public void TakeDamage(float damage){
        health -= damage;
	}

	void OnCollisionEnter2D (Collision2D obj){
		if (obj.gameObject.tag == "Player" && canAttack) { 
			stats.TakeDamage (damage);
			canAttack = false;
			StartCoroutine (ResetFire (coolDown, false));
		}
	}

	void OnTriggerEnter2D (Collider2D obj){

		if (obj.gameObject.tag == "Projectile" || obj.gameObject.tag == "Bomb"){
			dodging = true;
			float p = Random.Range (0.0f, 1.0f);
			if (p <= dodgeProb) {
				StartCoroutine(Dodge());
			}
		}
	}

	IEnumerator Dodge(){
		float d = (float)Random.Range (0, 1);
		float m;
		if (dodgeDirection.magnitude <= 2) {
			m = 4;
		} else if (dodgeDirection.magnitude <= 4) {
			m = 2.5f;
		} else if (dodgeDirection.magnitude <= 10) {
			m = 1.25f;
		} else {
			m = 2/3;
		}
		if (d <= 0.5f) { 
			enemybody.velocity = - m * dodgeDirection;
		} else {
			enemybody.velocity = m * dodgeDirection;
		}
		yield return new WaitForSeconds (0.1f);
		dodging = false;
		enemybody.velocity = Vector2.zero;

	}

	void ShootPlayer (){
		
		canShoot = false;
		Instantiate (weapon, firePoint.position, firePoint.rotation);
		StartCoroutine(ResetFire (coolDown, true));

	}

	IEnumerator ResetFire(float t, bool g){
		yield return new WaitForSeconds (t);
		if (g) {
			canShoot = true;
		} else {
			canAttack = true;
		}
	}
}
