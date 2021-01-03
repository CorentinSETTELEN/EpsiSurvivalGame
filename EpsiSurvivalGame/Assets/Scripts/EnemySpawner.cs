using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameStats player;
	public GameObject[] Enemies;
	public Transform[] spawnPoints;

	private bool spawned = false;
	private float spawnDelay = 2.3f;
	//private int i = 0;

	void OnTriggerEnter2D(Collider2D obj){
	

		if (obj.tag == "Player" && !spawned) {
			spawned = true;
			InvokeRepeating ("Spawner", spawnDelay, spawnDelay);
		}
	}

	void Spawner (){

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (player.health <= 0.0f || enemies.Length > 250){ //|| i >= 2){//|| i >= player.level) {
			//i=0;
			return;
		}

		int spawnpoint = Random.Range (0, spawnPoints.Length);
		int enemytype = Random.Range (0, Enemies.Length);

		Instantiate (Enemies [enemytype], spawnPoints [spawnpoint].position, spawnPoints [spawnpoint].rotation);
		//i++; 
	}

}
