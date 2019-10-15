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

    public float WaitBeforeSkip;
    public int textSize;
    public float lineSpacing;

    public bool userInput = false;

    bool GameStart = true;

    [Range(0f, 1f)] public float wait;

    //public AudioSource blink;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dialogue = DialogueSystem.instance;
        speechText = GetComponent<SpeechText>();

        dialogue.waitfor = wait;
        //blink = GetComponent<AudioSource>();
    }

    [TextArea]
    public string[] speakerText = new string[]
        {
            "Write Text in this box:AddNameHere"
        };

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
                    if (index >= speakerText.Length)
                    {
                        Debug.Log("Text,Done");

                        return;
                    }
                    //blink.Play();
                    Say(speakerText[index]);
                    //index++;
                }

            }


            if (index < speakerText.Length)
            {
                stopSay(speakerText[index]);
            }
        }

        Delay();


        // ShowArrayProperty(speakerText);

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
            Say(speakerText[index]);
            GameStart = false;
        }
    }

}


