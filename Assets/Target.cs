using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public ScoreKeeping scoreKeeper;
	public int trials;
	private float time;
	private bool shown;
	private bool started;
	// Use this for initialization
	void Start () {
		for(int i = 0;i<transform.childCount;i++)
			transform.GetChild(i).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		started = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			time += Time.deltaTime;
			if (!shown && (int)time == 2)
				ShowPossibleTarget ();
			if ((int)time == 2 && shown)
				RemovePossibleTarget ();

			if(shown)
			{
				if(Input.GetKeyDown(KeyCode.Space))
				{
					//scoreKeeper.AddScore(2 - time);
					if(scoreKeeper.Score > (time) || scoreKeeper.Score == 0)
						scoreKeeper.Score = (time);
					RemovePossibleTarget();
				}
			}
		} else {

		}

	}

	void ShowPossibleTarget()
	{
		transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		shown = true;
		time = 0;
	}

	void RemovePossibleTarget()
	{
		transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		time = 0;
		shown = false;
	}

	public void Begin(int trials)
	{
		started = true;
		shown = true;
		transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	}
}
