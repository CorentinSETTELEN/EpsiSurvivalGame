using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	public float speed;
	public float damage;
	private AudioSource sound;
	public AudioClip clip;


    // Use this for initialization
    void Start () {
		
		PlaySound ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		GetComponent<Rigidbody2D> ().velocity = (Vector2)transform.up * speed;
	}

	void OnCollisionEnter2D(Collision2D obj){

		if (gameObject.tag == "Projectile" && obj.gameObject.tag == "Enemy") {
			obj.gameObject.GetComponent<EnemyController> ().TakeDamage (damage);
		} else if (gameObject.tag == "Shot" && obj.gameObject.tag == "Player") {
			obj.gameObject.GetComponent<GameStats> ().TakeDamage (damage);
		}

		if (gameObject.tag == "Projectile" && obj.gameObject.tag != "Projectile") {
			Destroy (gameObject);
		} else if (gameObject.tag == "Shot" && obj.gameObject.tag != "Shot") {
			Destroy (gameObject);
		}
	}

	void PlaySound (){

		sound = gameObject.AddComponent<AudioSource> ();
		sound.clip = clip;
		sound.PlayOneShot (sound.clip);
		sound.enabled = true;
	}
}
