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

	}

	public void SetScore(int val) {
		score = val;
	}
}
