using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public ScoreKeeping scoreKeeper;
	private float time;
	private bool shown;
	// Use this for initialization
	void Start () {
		shown = true;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if(!shown && (int)time == 2)
			ShowPossibleTarget ();
		if ((int)time == 2 && shown)
			RemovePossibleTarget ();
		scoreKeeper.SetScore ((int)time);
	}

	void ShowPossibleTarget()
	{
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		shown = true;
		time = 0;
	}

	void RemovePossibleTarget()
	{
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		time = 0;
		shown = false;
	}
}
