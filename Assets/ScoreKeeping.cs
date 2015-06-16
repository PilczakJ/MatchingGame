using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreKeeping : MonoBehaviour {

	public Target t;
	private Text reactionText;
	private Text targetText;
	private Text trialsText;
	private Text currentTrialText;
	private Text missedText;
	private Text title;
	private Button increaseTrials;
	private Button decreaseTrials;
	private Button startButton;
	public float[] score;
	public float[] missed;
	private int trials = 1;
	private int currentTrial = 1;
	bool atEndScreen = false;
	private Canvas endScreen;

	// Use this for initialization
	void Start () {
		missed = new float[trials];
		score = new float[trials];
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
		reactionText.enabled = false;
		targetText.enabled = false;
		currentTrialText.enabled = false;
		missedText.enabled = false;

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
		reactionText.text = "Best Reaction Time: " + score[currentTrial-1];
		missedText.text = "Number Missed " + missed [currentTrial-1];
		targetText.text = "Target";
		trialsText.text = "Trials: " + trials;
		currentTrialText.text = "Trial #" + currentTrial;
		if (atEndScreen) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				endScreen.enabled = false;
				this.GetComponent<Canvas>().enabled = true;
				EndTrials ();
				atEndScreen = false;
			}
		}

	}

	public void AddScore(float val) {
		score[currentTrial-1] += val;
	}
	
	public int CurrentTrial{ get{return currentTrial;}set{currentTrial = value;}}
	public Canvas EndScreen{ get { return endScreen; } }

	public void ToggleTargetText()
	{
		if (targetText.enabled)
			targetText.enabled = false;
		else
			targetText.enabled = true;
	}

	void Begin()
	{
		missed = new float[trials];
		score = new float[trials];
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
		t.Begin (trials);
	}

	public void EndTrials()
	{
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
		//targetText.enabled = true;
	}

	public void SwapToEndUI()
	{
		atEndScreen = true;
		GetComponent<Canvas> ().enabled = false;
		endScreen.enabled = true;
		Text accuracy = endScreen.transform.FindChild ("Missed").GetComponent<Text> ();
		Text reaction = endScreen.transform.FindChild ("Reaction").GetComponent<Text> ();
		Text finalScore = endScreen.transform.FindChild ("Final Score").GetComponent<Text> ();
		accuracy.text = "Accuracy: \n";
		reaction.text = "Best Reaction: \n";
		finalScore.text = "Final Score: \n";
		for (int i = 0; i<missed.Length; i++)
			accuracy.text += "Trial " + (i + 1) + ": " + (1-missed [i])*100 + "%   ";
		for (int j = 0; j<score.Length; j++) {
			reaction.text += "Trial " + (j + 1) + ": " + score [j] + "  ";
			if(score[j] == 0)
				score[j] = 1;
		}
		for(int k = 0; k<score.Length; k++)
			finalScore.text += "Trial " + (k + 1) + ": " + (int)(1/score[k] * (1-missed [k])*100) + "  ";
		
	}
}
