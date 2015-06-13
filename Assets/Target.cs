using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public ScoreKeeping scoreKeeper;
	public int trials;
	private int currentTrial = 1;
	private float time;
	private bool imageShown;
	private bool started;
	private bool targetShown;
	private int imageSeriesNum = 0;
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
			scoreKeeper.CurrentTrial = currentTrial;
		}
		//If the last trial is done, end the game


		// If the trial has started and the initial target hasn't been shown, show the target for two seconds
		if (started && currentTrial<=trials && !targetShown) {
			if (!imageShown && (int)time == 1)
			{
				ShowPossibleTarget ();
			}
			else if ((int)time == 2 && imageShown)
			{
				RemovePossibleTarget ();
				targetShown = true;
				scoreKeeper.ToggleTargetText();
			}
		}

		// If the trial has started and the initial target has been shown, go through the series of images
		else if (targetShown && started && currentTrial<= trials) {
			if (!imageShown && (int)time == 1)
				ShowPossibleTarget ();
			if ((int)time == 2 && imageShown)
				RemovePossibleTarget ();

			if(imageShown)
			{
				if(Input.GetKeyDown(KeyCode.Space))
				{
					if(scoreKeeper.Score > (time) || scoreKeeper.Score == 0)
						scoreKeeper.Score = (time);
					RemovePossibleTarget();
				}
			}
		}


	}

	void ShowPossibleTarget()
	{
		transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		imageShown = true;
		time = 0;
	}

	void RemovePossibleTarget()
	{
		transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		time = 0;
		imageShown = false;
		if (targetShown) {
			if(imageSeriesNum == 5)
				NewTrial();
			else
				imageSeriesNum++;
		}

	}

	void NewTrial()
	{
		if (currentTrial == trials && started) {
			EndTrials ();
		} else {
			targetShown = false;
			imageSeriesNum = 0;
			ShowPossibleTarget();
			scoreKeeper.ToggleTargetText ();
			currentTrial++;
		}
	}

	void EndTrials()
	{
		started = false;
		scoreKeeper.EndTrials();
	}

	public void Begin(int trials)
	{
		started = true;
		currentTrial = 1;
		targetShown = false;
		scoreKeeper.ToggleTargetText ();
		transform.GetChild(0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		this.trials = trials;

	}
}
