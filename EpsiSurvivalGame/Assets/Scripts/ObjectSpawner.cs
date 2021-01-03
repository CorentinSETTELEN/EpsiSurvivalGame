using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour {

	public GameObject[] objects;
	public GameObject[] goals;
	public GameStats stats;
	public GameObject controls;
	public float coinProb, healthProb, sheildProb;
    public Text Objectif;



    private bool triggered = false;

	void OnTriggerEnter2D(Collider2D obj) {

		if (obj.tag == "Player" && !triggered) {
            Objectif.text = "Objectif :\n Survivre et atteindre les 200 points  !";
            controls.SetActive (false);

			stats = obj.GetComponent<GameStats> ();
			stats.toggle = false;

			triggered = true;
		
			int n = Random.Range (40,80);

			for (int x = 0; x < n; x++){ // Apparition des potions
				/*int index = Random.Range (0, objects.Length);
				Vector3 location;
				do {
					location = transform.position + new Vector3 (Random.Range (-9.5f, 9.5f), Random.Range (-9.5f, 9.5f), 0);
				} while (Physics2D.OverlapCircle (location, 0.25f) != null);
				Instantiate (objects [index], location, Quaternion.identity);
				if (x < 25) {*/
					Vector3 coinLocation;
					do {
						coinLocation = transform.position + new Vector3 (Random.Range (-9.5f, 9.5f), Random.Range (-9.5f, 9.5f), 0);
					} while (Physics2D.OverlapCircle (coinLocation, 0.25f) != null);
					Instantiate (goals[0], coinLocation, Quaternion.identity);
				//}
			}
			InvokeRepeating ("moreGoals", 4, 3);
		}
	}

	void moreGoals(){

		GameObject[] qCoins = GameObject.FindGameObjectsWithTag("Coins");
		GameObject[] qHealth = GameObject.FindGameObjectsWithTag("Health");
		GameObject[] qShield = GameObject.FindGameObjectsWithTag("Shield");
		if (stats.health <= 0.0f || qCoins.Length > 100 || qHealth.Length > 100|| qShield.Length > 100){
			return;
		}

		Vector3 coinLocation;
		do {
			coinLocation = transform.position + new Vector3 (Random.Range (-9.5f, 9.5f), Random.Range (-9.5f, 9.5f), 0);
		} while (Physics2D.OverlapCircle (coinLocation, 0.25f) != null);
		int p = Random.Range (0,10);

		if (p < 6) { 
			Instantiate (goals [0], coinLocation, Quaternion.identity);
		} else if (p < 8) {
			Instantiate (goals [1], coinLocation, Quaternion.identity);
		} else {
			Instantiate (goals [2], coinLocation, Quaternion.identity);
		}
	}
}
