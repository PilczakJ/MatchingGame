using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	// The scorekeeper to report score changes to
	public ScoreKeeping scoreKeeper;

	// The total number of trials
	public int trials;

	// The current trial
	private int currentTrial = 1;

	// The amount of time that has passed since the last action (show/remove image)
	private float time;

	// If an image is currently being shown
	private bool imageShown;

	// Whether the game has started
	private bool started;

	// Whether the initial target has been shown this trial
	private bool targetShown;

	// The number in the image series that it is on
	private int imageSeriesNum = 0;

	// Which image is the target
	private int targetIndex;

	// The array of indexes which tell which image to show in the series
	private int[] trialImages;

	// How many times space has been hit this trial (used for accuracy)
	private int spaceCount = 0;

	// Use this for initialization
	void Start () {
		// Instantiate the trialImage array and make all the images disabled to start
		trialImages = new int[6];
		for(int i = 0;i<transform.childCount;i++)
			transform.GetChild(i).gameObject.GetComponent<SpriteRenderer> ().enabled = false;

		// The game hasn't started yet
		started = false;
	}
	
	// Update is called once per frame
	void Update () {

		// Keep track of the time since the last action and the current trial always
		if (started) {
			time += Time.deltaTime;
			scoreKeeper.CurrentTrial = currentTrial;
		}

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
			//If the last image in the series has been shown, move on to the next trial
			else if(imageSeriesNum == 6 && (int)time == 2)
				NewTrial();

			// If an image is shown...
			if(imageShown)
			{
				// If space is detected...
				if(Input.GetKeyDown(KeyCode.Space))
				{
					// increment the space count
					spaceCount++;

					// If it is the target, get the reaction time and move on to the next image
					if(trialImages[imageSeriesNum] == targetIndex)
					{
						if(scoreKeeper.Reaction > (time) || scoreKeeper.Reaction == 0)
							scoreKeeper.Reaction = (time);
						RemovePossibleTarget();
					}

					// If not, it is a miss
					else
						scoreKeeper.Missed++;
				}
			}
		}
	}

	// Show an image
	void ShowPossibleTarget()
	{
		transform.GetChild(trialImages[imageSeriesNum]).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		imageShown = true;

		// Reset the time since the last action
		time = 0;
	}

	// Remove an image
	void RemovePossibleTarget()
	{
		// If the image was the target and space wasn't hit, it is a miss
		if (trialImages [imageSeriesNum] == targetIndex && targetShown) {
			if(time > 1.1)
				scoreKeeper.Missed++;
		}

		// Reset the time since the last action
		time = 0;
		imageShown = false;

		// If it is the target, just remove it, if not, remove and move on to the next image
		if (targetShown) {
			transform.GetChild(trialImages[imageSeriesNum]).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			imageSeriesNum++;
		} else
			transform.GetChild(targetIndex).gameObject.GetComponent<SpriteRenderer> ().enabled = false;

	}

	// Start a new trial
	void NewTrial()
	{
		// The new trial's target has not been shown
		targetShown = false;

		// Reset the images
		imageSeriesNum = 0;

		// Calculate the percent missed
		if (spaceCount == 0 && scoreKeeper.Missed == 0)
			scoreKeeper.Missed = 0;
		else if (spaceCount == 0 && scoreKeeper.Missed > 0)
			scoreKeeper.Missed = 1;
		else
			scoreKeeper.Missed = (scoreKeeper.Missed/spaceCount);
		spaceCount = 0;

		// If it is the last trial, end the game. If not, move on to the next trial
		if (currentTrial == trials && started) {
			EndTrials ();
		} else {
			// Make a new target and image series
			NewTarget();

			// Show the target text and the target
			scoreKeeper.ToggleTargetText ();
			time = 0;
			transform.GetChild(targetIndex).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			imageShown = true;

			// Increment the trial
			currentTrial++;
		}
	}

	// End the game and move to the score screen
	void EndTrials()
	{
		started = false;
		scoreKeeper.SwapToEndUI ();
	}

	// Start the game
	public void Begin(int trials)
	{
		started = true;

		// Make a random target and image series
		NewTarget ();

		// It is the first trial
		currentTrial = 1;

		// Set the scores to 0
		scoreKeeper.Reaction = 0;
		scoreKeeper.Missed = 0;

		// The target has not been shown
		targetShown = false;

		// Show the target text
		scoreKeeper.ToggleTargetText ();

		// Show the target image
		transform.GetChild(targetIndex).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		imageShown = true;
		time = 0;

		// Set the total number of trials
		this.trials = trials;

	}

	// Create a new randomized target and array of images
	void NewTarget()
	{
		// The target is one of the six possible images
		targetIndex = Random.Range (1, 6);

		// Set a random image for each image in the series
		// This means there can be one, multiple, or no targets in a trial
		for(int i = 0;i<6;i++)
		{
			trialImages[i] = Random.Range(1,6);
		}
	}
}
