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
	private int targetIndex;
	private int[] trialImages;
	// Use this for initialization
	void Start () {
		trialImages = new int[6];
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
			if ((int)time == 2 && imageShown)
			{
				RemovePossibleTarget ();
				targetShown = true;
				scoreKeeper.ToggleTargetText();
			}
		}

		// If the trial has started and the initial target has been shown, go through the series of images
		else if (targetShown && started && currentTrial<= trials) {
			if (!imageShown && (int)time == 1 && imageSeriesNum < 6)
				ShowPossibleTarget ();
			if ((int)time == 2 && imageShown && imageSeriesNum < 6)
				RemovePossibleTarget ();
			else if(imageSeriesNum == 6 && (int)time == 2)
				NewTrial();

			if(imageShown)
			{
				if(Input.GetKeyDown(KeyCode.Space) && trialImages[imageSeriesNum] == targetIndex)
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
		transform.GetChild(trialImages[imageSeriesNum]).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		imageShown = true;
		time = 0;
	}

	void RemovePossibleTarget()
	{
		transform.GetChild(trialImages[imageSeriesNum]).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		time = 0;
		imageShown = false;
		if (targetShown) {
			imageSeriesNum++;
		} else
			transform.GetChild(targetIndex).gameObject.GetComponent<SpriteRenderer> ().enabled = false;

	}

	void NewTrial()
	{
		targetShown = false;
		imageSeriesNum = 0;
		if (currentTrial == trials && started) {
			EndTrials ();
		} else {
			NewTarget();
			scoreKeeper.ToggleTargetText ();
			transform.GetChild(targetIndex).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			imageShown = true;
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
		NewTarget ();
		currentTrial = 1;
		targetShown = false;
		scoreKeeper.ToggleTargetText ();
		transform.GetChild(targetIndex).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		imageShown = true;
		this.trials = trials;

	}

	void NewTarget()
	{
		targetIndex = Random.Range (1, 6);
		for(int i = 0;i<5;i++)
		{
			//while(trialImages[i] == targetIndex || trialImages[i] == 0)
			trialImages[i] = Random.Range(1,6);
		}
	}
}
