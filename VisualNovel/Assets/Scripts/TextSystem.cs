using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TextSystem : MonoBehaviour
{
    public static TextSystem instance;
    SpeechText speechText;
    DialogueSystem dialogue;

  
    [Header("Text Settings")]
    public Font font;
    [Range(0.2f, 1f)] public float WaitBeforeSkip;
    [Range(1f, 30f)] public int textSize;
    [Range(1f, 10f)] public float lineSpacing;
    [Range(0.01f, 1f)] public float textSpeed;
    public AudioSource textSound;
    public Color textColour;


    [Header("Name Text Settings")]
    public Font nameFont;
    [Range(1f, 30f)] public int nameTextSize;
    public Color nameTextColour;

	[Header("Scene Management")]
	[Space(10)]
	public bool GoToNextScene;
	public bool UseName;
	public string sceneName;
	public int sceneNumber;

	[System.Serializable]
	public struct Text
        {
        public string CharacterName;
		public string BackgroundName;
        [System.Serializable]
        public enum Emotion
        {
            Happy,
            Sad,
            Nutrual,
            Angry
        };
		public Emotion emotion;
		[Space(10)]
        [TextArea(10, 20)]
        public string text;
        }

	//[Header("Script Settings")]
	[Space(10)]
	public Text[] text;


	[System.Serializable]
	public struct Characters
	{
		public string characterName;
		public GameObject Happy;
		public GameObject Sad;
		public GameObject Nutral;
		public GameObject Angry;
	}
	//[Header("Characters")]
	[Space(10)]
	public Characters[] characters;

	[System.Serializable]
	public struct BackgroundImage
	{
		public string backgroundName;
		public GameObject backgroundImage;
		//public int textLineNumber;
	}
	//[Header("Background")]
	[Space(10)]
	public BackgroundImage[] background;

	bool GameStart = true;
	float waitTime = 0.0f;
	[HideInInspector] public string backgroundName;
	[HideInInspector] public int index = 0;
	[HideInInspector] public bool userInput = false;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		dialogue = DialogueSystem.instance;
		speechText = GetComponent<SpeechText>();
		dialogue.waitfor = textSpeed;
	}

	void Update()
    {
		
		gameStart();
		

        if (dialogue != null)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0))) && userInput == false)
            {

                if (!dialogue.isSpeaking || dialogue.isWatingForUserInput)
                {
                    if (index >= text.Length)
                    {
                        Debug.Log("Text,Done");
						LoadScene(sceneName,sceneNumber);
                        return;
                    }
                   
                    textSound.Play();

                    Say(text[index].text);
				
					checkIfNull();
					getBackGroundName();
					checkBackground();

                }

            }

            if (index < text.Length)
            {
                stopSay(text[index].text);
            }
        }
        Delay();
    }


    void Say(string s)
    {
        string[] parts = s.Split(':');
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";
        dialogue.Say(speech, speaker);
    }

    void stopSay(string s)
    {
        string[] parts = s.Split(':');
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";
        SkipText(speech, speaker);
    }

    void SkipText(string speech, string speaker)
    {
        if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0))) && !dialogue.isWatingForUserInput)
        {
            if (userInput == true)
            {
                dialogue.SkipTextScroll(speech, speaker);
                index++;
                userInput = false;
                waitTime = 0;
                textSound.Play();
            }

        }
    }

    void Delay()
    {
        if (dialogue.isWatingForUserInput == false)
        {
            if (waitTime != WaitBeforeSkip)
            {
                waitTime += Time.deltaTime;
            }

            if (waitTime >= WaitBeforeSkip)
            {
                userInput = true;
            }
        }
        else
        {
            userInput = false;
        }
    }

    void gameStart()
    {
        if (GameStart == true)
        {
			getBackGroundName();
			Say(text[index].text);
			checkIfNull();
			checkBackground();
			GameStart = false;
        }
    }

	string getBackGroundName()
	{

	    backgroundName = text[index].BackgroundName;
		return backgroundName;
	}

	void checkIfNull()
	{
		if (text != null)
		{

			for (int i = 0; i < characters.Length; i++)
			{

				if (dialogue.speakerNameHold == characters[i].characterName)
				{
					switch (text[index].emotion)
					{
						case Text.Emotion.Happy:
							{
								nullCheck(i);
								characters[i].Happy.SetActive(true);
								break;
							}
						case Text.Emotion.Sad:
							{
								nullCheck(i);
								characters[i].Sad.SetActive(true);
								break;
							}
						case Text.Emotion.Angry:
							{
								nullCheck(i);
								characters[i].Angry.SetActive(true);
								break;
							}
						case Text.Emotion.Nutrual:
							{
								nullCheck(i);
								characters[i].Nutral.SetActive(true);
								break;
							}
						default:
							{
								break;
							}
					}

				}
				else if (dialogue.speakerNameHold != characters[i].characterName)
				{
					nullCheck(i);
				}
			}

		}


	}

	void nullCheck(int i)
	{
		if (characters[i].Sad != null)
		{
			characters[i].Sad.SetActive(false);
		}

		if (characters[i].Happy != null)
		{
			characters[i].Happy.SetActive(false);
		}

		if (characters[i].Angry != null)
		{
			characters[i].Angry.SetActive(false);
		}

		if (characters[i].Nutral != null)
		{
			characters[i].Nutral.SetActive(false);
		}
	}

	void checkBackground()
	{
		if (text != null)
		{
			for (int i = 0; i < background.Length; i++)
			{
				if (background[i].backgroundImage != null)
				{


					if (backgroundName == background[i].backgroundName)
					{
						background[i].backgroundImage.SetActive(true);
					}
					else if (backgroundName != background[i].backgroundName)
					{
						background[i].backgroundImage.SetActive(false);
					}
				}
			}
		}
	}

	void LoadScene(string scenename, int sceneNumber)
	{
		if (GoToNextScene == true)
		{
			if (UseName == true)
			{
				SceneManager.LoadScene(scenename);
			}
			else if (UseName == false)
			{
				SceneManager.LoadScene(sceneNumber);
			}
		}
	}
}


