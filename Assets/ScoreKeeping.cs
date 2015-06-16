using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Basically the scorekeeper is the whole of the UI and scorekeeping
public class ScoreKeeping : MonoBehaviour {

	// A reference to the target
	public Target t;

	// Text to show the best reaction time for the current trial
	private Text reactionText;

	// Text to show whether it is currently showing the target
	private Text targetText;

	// Text to show how many trials you have selected
	private Text trialsText;

	// Text to show what trial it is currently
	private Text currentTrialText;

	// Text to show the number missed
	private Text missedText;

	// Title text
	private Text title;

	// Buttons to adjust the number of trials selected
	private Button increaseTrials;
	private Button decreaseTrials;

	// Button to start the game
	private Button startButton;

	// Reaction time and misses per trial
	private float[] reaction;
	private float[] missed;

	// Total trials and current trial
	private int trials = 1;
	private int currentTrial = 1;

	// Whether we are at the score screen currently
	bool atEndScreen = false;

	// A reference to the score screen's Canvas
	private Canvas endScreen;

	// Use this for initialization
	void Start () {
		// Instantiate missed/reaction to avoid null reference exeptions in update
		missed = new float[trials];
		reaction = new float[trials];

		// Set each UI element variable
		endScreen = transform.FindChild ("End Score").GetComponent<Canvas> ();
		reactionText = transform.FindChild("Score").GetComponent<Text> ();
		targetText = transform.FindChild("Target Indicator").GetComponent<Text> ();
		increaseTrials = transform.FindChild ("Increase Trials").GetComponent<Button> ();
		decreaseTrials = transform.FindChild ("Decrease Trials").GetComponent<Button> ();
		startButton = transform.FindChild ("Start").GetComponent<Button> ();
		trialsText = transform.FindChild ("Number of Trials").GetComponent<Text> ();
		currentTrialText = transform.FindChild ("Current Trial").GetComponent<Text> ();
		missedText = transform.FindChild ("Missed 1").GetComponent<Text> ();
		title = transform.FindChild ("Title").GetComponent<Text> ();

		// Disable the UI not used on the title screen
		reactionText.enabled = false;
		targetText.enabled = false;
		currentTrialText.enabled = false;
		missedText.enabled = false;

		// Set the buttons' onClick
		increaseTrials.onClick.AddListener(() => {
			trials++;
		});
		decreaseTrials.onClick.AddListener(() => {
			if(trials > 1)
				trials--;
		});
		startButton.onClick.AddListener(() => {
			Begin ();
		});
	}
	
	// Update is called once per frame
	void Update () {
		// Keep the text updated
		reactionText.text = "Best Reaction Time: " + reaction[currentTrial-1];
		missedText.text = "Number Missed " + missed [currentTrial-1];
		targetText.text = "Target";
		trialsText.text = "Trials: " + trials;
		currentTrialText.text = "Trial #" + currentTrial;

		// Check for space on the score screen to return to the title screen
		if (atEndScreen) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				endScreen.enabled = false;
				this.GetComponent<Canvas>().enabled = true;
				EndTrials ();
				atEndScreen = false;
			}
		}

	}

	// Getters/Setters for the current trial's misses and reactions
	public float Missed{get{return missed[currentTrial-1];} set{missed[currentTrial-1] = value;}}
	public float Reaction{get{return reaction[currentTrial-1];} set{reaction[currentTrial-1] = value;}}

	// Other getters/setters
	public int CurrentTrial{ get{return currentTrial;}set{currentTrial = value;}}
	public Canvas EndScreen{ get { return endScreen; } }

	// Toggle the target text
	public void ToggleTargetText()
	{
		if (targetText.enabled)
			targetText.enabled = false;
		else
			targetText.enabled = true;
	}

	// Start the game
	void Begin()
	{
		// Set the missed/reaction arrays to the correct capacity
		missed = new float[trials];
		reaction = new float[trials];

		// Remove the UI from the main menu
		increaseTrials.enabled = false;
		increaseTrials.GetComponent<Image> ().enabled = false;
		increaseTrials.transform.GetChild (0).GetComponent<Text> ().enabled = false;
		decreaseTrials.enabled = false;
		decreaseTrials.GetComponent<Image> ().enabled = false;
		decreaseTrials.transform.GetChild (0).GetComponent<Text> ().enabled = false;
		startButton.enabled = false;
		startButton.GetComponent<Image> ().enabled = false;
		startButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
		trialsText.enabled = false;
		reactionText.enabled = true;
		targetText.enabled = false;
		title.enabled = false;
		currentTrialText.enabled = true;
		missedText.enabled = true;

		// Start the game
		t.Begin (trials);
	}

	public void EndTrials()
	{
		// Replace the main menu UI
		increaseTrials.enabled = true;
		increaseTrials.GetComponent<Image> ().enabled = true;
		increaseTrials.transform.GetChild (0).GetComponent<Text> ().enabled = true;
		decreaseTrials.enabled = true;
		decreaseTrials.GetComponent<Image> ().enabled = true;
		decreaseTrials.transform.GetChild (0).GetComponent<Text> ().enabled = true;
		startButton.enabled = true;
		startButton.GetComponent<Image> ().enabled = true;
		startButton.transform.GetChild (0).GetComponent<Text> ().enabled = true;
		trialsText.enabled = true;
		reactionText.enabled = true;
		currentTrialText.enabled = true;
		title.enabled = true;
		currentTrialText.enabled = false;
	}

	public void SwapToEndUI()
	{
		// Score screen UI
		atEndScreen = true;
		GetComponent<Canvas> ().enabled = false;
		endScreen.enabled = true;
		Text accuracy = endScreen.transform.FindChild ("Missed").GetComponent<Text> ();
		Text reactionScore = endScreen.transform.FindChild ("Reaction").GetComponent<Text> ();
		Text finalScore = endScreen.transform.FindChild ("Final Score").GetComponent<Text> ();
		accuracy.text = "Accuracy: \n";
		reactionScore.text = "Best Reaction: \n";
		finalScore.text = "Final Score: \n";

		// Calculate accuracy
		for (int i = 0; i<missed.Length; i++)
			accuracy.text += "Trial " + (i + 1) + ": " + (1-missed [i])*100 + "%   ";

		// Get best reaction times
		for (int j = 0; j<reaction.Length; j++) {
			reactionScore.text += "Trial " + (j + 1) + ": " + reaction [j] + "  ";
			if(reaction[j] == 0)
				reaction[j] = 1;
		}
		// The final score is calculated by multiplying 1/ReactionTime by the accuracy percentage (100% = 100)
		for(int k = 0; k<reaction.Length; k++)
			finalScore.text += "Trial " + (k + 1) + ": " + (int)(1/reaction[k] * (1-missed [k])*100) + "  ";
		
	}
}
