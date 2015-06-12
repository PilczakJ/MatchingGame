using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreKeeping : MonoBehaviour {

	private Text txt;
	private int score = 0;
	// Use this for initialization
	void Start () {
		txt = GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {
		txt.text = "Score: " + score;
		if (Input.GetKeyDown (KeyCode.Space))
			score = 1;
		if (Input.GetKeyUp (KeyCode.Space))
			score = 0;
	}

	public void SetScore(int val) {
		score = val;
	}
}
