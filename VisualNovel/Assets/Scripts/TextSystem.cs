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

    float waitTime = 0.0f;

    [Header("Text Settings")]

    public Font font;
    [Range(0.2f, 1f)] public float WaitBeforeSkip;
    [Range(1f, 20f)] public int textSize;
    [Range(1f, 10f)] public float lineSpacing;
    [Range(0f, 1f)] public float textSpeed;

    public AudioSource textSound;

    public Color textColour;
    

    [Header("Name Text Settings")]
    public Font nameFont;
    [Range(1f, 30f)] public int nameTextSize;
    public Color nameTextColour;

	[Header("Character One")]


	[HideInInspector] public bool userInput = false;

    bool GameStart = true;

	




    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dialogue = DialogueSystem.instance;
        speechText = GetComponent<SpeechText>();
        dialogue.waitfor = textSpeed;
        //blink = GetComponent<AudioSource>();
    }


 
    [System.Serializable]
    public struct Text
        {
    
        public string CharacterName;

        public Emotion emotion;
        [System.Serializable]
        public enum Emotion
        {
            Happy,
            Sad,
            Nutrual,
            Angry
        };

        [Space(10)]
        [TextArea(10, 20)]
        public string text;
    

   
        }

    public Text[] text;


   [HideInInspector] public int index = 0;
    // Update is called once per frame
    void Update()
    {
        gameStart();

        if (dialogue != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && userInput == false)
            {

                if (!dialogue.isSpeaking || dialogue.isWatingForUserInput)
                {
                    if (index >= text.Length)
                    {
                        Debug.Log("Text,Done");

                        return;
                    }
                   
                    textSound.Play();

                    Say(text[index].text);
                    //index++;
                }

            }

            if (index < text.Length)
            {
                stopSay(text[index].text);
            }
        }

        Delay();


        // ShowArrayProperty(speakerText);
        Debug.Log(text[index].emotion);
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
        if (Input.GetKeyDown(KeyCode.Space) && !dialogue.isWatingForUserInput)
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
            Say(text[index].text);
            GameStart = false;
        }
    }

}


