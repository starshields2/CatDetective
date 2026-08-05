using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//to do: activate with items
//activate when interacting with characters
//activate when walking into trigger
//disable player mouse input while dialogue is active (so that they can click through dialogue). 

// This is a super bare bones example of how to play and display a ink story in Unity.
public class OverheadDialogueTemplate : MonoBehaviour {
    public static event Action<Story> OnCreateStory;
	public static bool IsDialogueActive { get; private set; }
	public bool autoStart;
	public CanvasGroup thisCanvas;
	public CanvasGroup inventoryCanvas;
	public GameObject[] speakerID;
	
	void Awake () {
		// Remove the default message
		RemoveChildren();

        if (autoStart)
        {
			StartStory();
        }
        else
        {
			this.gameObject.SetActive(false);
        }
		
	}

	public void LoadStory()
    {
		Debug.Log("Loading Story...");
		currentStory = null;
		currentStory = preloadedStory;
		StartStory();
    }

	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory () {
		thisCanvas.alpha = 1;
		story = new Story (currentStory.text);
		inventoryCanvas.alpha = 0;
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}
	
	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();

		// Create a container for text
		GameObject textContainer = new GameObject("TextContainer");
		textContainer.transform.SetParent(canvas.transform, false);

		VerticalLayoutGroup textLayoutGroup = textContainer.AddComponent<VerticalLayoutGroup>();
		textLayoutGroup.childControlHeight = false;
		textLayoutGroup.childForceExpandWidth = false;
		textLayoutGroup.childForceExpandHeight = false;
		textLayoutGroup.childControlWidth = false;
		textLayoutGroup.childControlHeight = false;
		textLayoutGroup.childAlignment = TextAnchor.LowerLeft;
		textLayoutGroup.padding.left = -175;
		textLayoutGroup.padding.right = 0;
		textLayoutGroup.padding.top = -275;
		textLayoutGroup.padding.bottom = 175;
		textLayoutGroup.spacing = 15;

		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.ContinueMaximally();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text, textContainer);
		}

		// Display all the choices, if there are any!
		if(story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				//Button button = CreateChoiceView (choice.text.Trim ());
				// Tell the button what to do when we press it
				//button.onClick.AddListener (delegate {
				//	OnClickChoiceButton (choice);
				//});
			}
		}
		// If we've read all the content and there's no choices, the story is finished!
		else {

			Deactivate();

			//choice.onClick.AddListener(delegate{
			//	StartStory();
			//});
		}
	}

	void Deactivate()
    {
		thisCanvas.alpha = 0;
		thisCanvas.interactable = false;
		inventoryCanvas.alpha = 1;
		inventoryCanvas.interactable = true;
    }

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView (string text, GameObject container) {
		TextMeshProUGUI storyText = Instantiate (textPrefab) as TextMeshProUGUI;
		storyText.text = text;
		storyText.transform.SetParent (container.transform, false);


		// Check for character tag.
		if (story.currentTags.Contains("Ward"))
		{
			speakerText.text = "Ward";
			speakerID[0].SetActive(true);

			// Disable all other speakerID game objects
			for (int i = 1; i < speakerID.Length; i++)
			{
				speakerID[i].SetActive(false);
			}
		}

		if (story.currentTags.Contains("Peter"))
		{
			speakerText.text = "Peter";
			speakerID[1].SetActive(true);

			// Disable all other speakerID game objects
			for (int i = 0; i < speakerID.Length; i++)
			{
				if (i != 1) // Skip index 1 
				{
					speakerID[i].SetActive(false);
				}
			}
		}

		if (story.currentTags.Contains("Quinn"))
		{
			speakerText.text = "Quinn";
			speakerID[2].SetActive(true);

			// Disable all other speakerID game objects
			for (int i = 0; i < speakerID.Length; i++)
			{
				if (i != 2) // Skip index 2 
				{
					speakerID[i].SetActive(false);
				}
			}
		}


	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			Destroy (canvas.transform.GetChild (i).gameObject);
		}
	}

	public void AdvanceDialogue()
	{
		Debug.Log("advancing.");
		if (story.currentChoices.Count > 0)
		{
			story.ChooseChoiceIndex(0);
			RefreshView();
		}
	}

	[SerializeField]
	public TextAsset currentStory = null;
	public TextAsset preloadedStory = null;
	public Story story;

	[SerializeField]
	private Canvas canvas = null;

	// UI Prefabs
	[SerializeField]
	private TextMeshProUGUI textPrefab = null;
	[SerializeField]
	private TextMeshProUGUI speakerText = null;
	[SerializeField]
	private Button buttonPrefab = null;

	public GameObject speakerImage;
	public string speakerName;
	public string[] currentInktags; //current tags to track
}
