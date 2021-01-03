using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {

	public float damage;
	private AudioSource sound;
	public AudioClip clip;

	void OnCollisionEnter2D(Collision2D obj){

		StartCoroutine(PlaySound ());
		Collider2D[] withinRange = Physics2D.OverlapCircleAll (transform.position, 0.6f); 
		for (int i = 0; i < withinRange.Length; i++) {
			if (withinRange [i].tag == "Enemy") {
				withinRange [i].GetComponent<EnemyController> ().TakeDamage (damage);
			} else if (withinRange [i].tag == "Player") {
				withinRange [i].GetComponent<GameStats> ().TakeDamage (damage);	
				} 
			}
			if (obj.gameObject.tag != "Bomb") {
				Destroy (gameObject);
			}
	}

	IEnumerator PlaySound (){

		AudioSource.PlayClipAtPoint (clip, transform.position);
		yield return null;
	}
}