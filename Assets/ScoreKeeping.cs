using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreKeeping : MonoBehaviour {

	public Target t;
	private Text txt;
	private Text txt2;
	private Text txt3;
	private Text txt4;
	private Button b1;
	private Button b2;
	private Button b3;
	private float score = 0;
	private int trials = 1;
	private int currentTrial = 1;
	// Use this for initialization
	void Start () {
		txt = transform.FindChild("Score").GetComponent<Text> ();
		txt2 = transform.FindChild("Target Indicator").GetComponent<Text> ();
		b1 = transform.FindChild ("Increase Trials").GetComponent<Button> ();
		b2 = transform.FindChild ("Decrease Trials").GetComponent<Button> ();
		b3 = transform.FindChild ("Start").GetComponent<Button> ();
		txt3 = transform.FindChild ("Number of Trials").GetComponent<Text> ();
		txt4 = transform.FindChild ("Current Trial").GetComponent<Text> ();
		txt.enabled = false;
		txt2.enabled = false;
		txt4.enabled = false;

		b1.onClick.AddListener(() => {
			trials++;
		});
		b2.onClick.AddListener(() => {
			if(trials > 1)
				trials--;
		});
		b3.onClick.AddListener(() => {
			Begin ();
		});
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = "Best Reaction Time: " + score;
		txt2.text = "Target";
		txt3.text = "Trials: " + trials;
		txt4.text = "Trial #" + currentTrial;
	}

	public void AddScore(float val) {
		score += val;
	}

	public float Score{ get{return score;} set{score = value;}}
	public int CurrentTrial{ get{return currentTrial;}set{currentTrial = value;}}

	public void ToggleTargetText()
	{
		if (txt2.enabled)
			txt2.enabled = false;
		else
			txt2.enabled = true;
	}

	void Begin()
	{
		b1.enabled = false;
		b1.GetComponent<Image> ().enabled = false;
		b1.transform.GetChild (0).GetComponent<Text> ().enabled = false;
		b2.enabled = false;
		b2.GetComponent<Image> ().enabled = false;
		b2.transform.GetChild (0).GetComponent<Text> ().enabled = false;
		b3.enabled = false;
		b3.GetComponent<Image> ().enabled = false;
		b3.transform.GetChild (0).GetComponent<Text> ().enabled = false;
		txt3.enabled = false;
		txt.enabled = true;
		txt2.enabled = false;
		txt4.enabled = true;
		t.Begin (trials);
	}

	public void EndTrials()
	{
		b1.enabled = true;
		b1.GetComponent<Image> ().enabled = true;
		b1.transform.GetChild (0).GetComponent<Text> ().enabled = true;
		b2.enabled = true;
		b2.GetComponent<Image> ().enabled = true;
		b2.transform.GetChild (0).GetComponent<Text> ().enabled = true;
		b3.enabled = true;
		b3.GetComponent<Image> ().enabled = true;
		b3.transform.GetChild (0).GetComponent<Text> ().enabled = true;
		txt3.enabled = true;
		txt.enabled = true;
		txt4.enabled = true;
		//txt2.enabled = true;
	}
}
