using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour {

	public float health = 100f;
	public float shield = 0.0f;
	public Slider healthLevel;
	public Slider shieldLevel;
	public Text score;
	public Text highScore;
	public Text Health; 
	public Text Shield;
	public GameObject controls;
	public GameObject gameOver;
	public GameObject win;
    public bool dead = false;
	public bool activeShield = false;
	public bool toggle = true;
    public Text Objectif;

	public float points = 0;
	public float bestScore; 

    public int timerHungry = 0;

	// Initialization
	void Start () {

		if (PlayerPrefs.HasKey ("HighScore")) {
			bestScore = PlayerPrefs.GetFloat ("HighScore");
		}

	}
	
	// Appeller à chaque frame
	void Update () {

        timerHungry += 1;
        if (timerHungry % 15 == 1 && health != 0 && dead != true && points != 0)
        {
            health -= 1;
        }

        if(points == 200)
        {
            dead = true;
            win.SetActive(true);
            DestroyAllObjects();
        }
        

        score.text = "Score: " + points;
		Health.text = "Health: " + health + "/100";
		highScore.text = "High Score: " + bestScore;

		shieldLevel.value = shield;
		Shield.text = "Shield: " + Mathf.RoundToInt(100 * (shield/100f))+"%";

		if (points > bestScore) {
			bestScore = points;
			PlayerPrefs.SetFloat ("HighScore", bestScore);
		}


		if (dead && Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene ("LvL1");
		}
	
		if (Input.GetKeyUp (KeyCode.C)){
				toggle = !toggle;
				controls.SetActive (toggle);
				
		}
	}

	public void TakeDamage(float damage){

		if (shield > 0) {
			activeShield = true;
		} else {
			activeShield = false;
		}

		if (!dead && !activeShield) {
			if (health - damage <= 0) {
				health = 0;
			} else {
				health -= damage;
			}
			healthLevel.value = health;
		}

		if (activeShield) {
			if (shield - damage <= 0) {
				shield = 0;
			} else {
				shield -= damage;
			}
		}

		if (health <= 0) { // Fin de la partie
			dead = true;
			gameOver.SetActive (true);
			DestroyAllObjects ();
		}
	}

	void DestroyAllObjects() {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Bomb");
		for(var i = 0 ; i < gameObjects.Length ; i ++)
		{
			Destroy(gameObjects[i]);
		}
	} 
}

