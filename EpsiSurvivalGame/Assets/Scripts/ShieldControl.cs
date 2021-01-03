using UnityEngine;
using System.Collections;

public class ShieldControl : MonoBehaviour {

	public GameStats stats;
	private float value;


	// Use this for initialization
	void Start () {

		stats = GetComponentInParent<GameStats> ();
	}
	
	// Update is called once per frame
	void Update () {

		value = (2 * stats.shield) / 300;
		if (value >= 0.75f) {
			value = 0.75f;
		}
		GetComponent<SpriteRenderer>().color = new Color (0f, 234f, 255, value ); //is about 50% transparent
	}
}
