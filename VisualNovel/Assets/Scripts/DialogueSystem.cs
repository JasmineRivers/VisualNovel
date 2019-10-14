using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;



public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    TextSystem textSystem;
    public ELEMENTS elements;
    // public Text test;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        textSystem = TextSystem.instance;
    }

    // Update is called once per frame
    void Update()
    {
        checkifTextIsFinished();
    }

    public void Say(string speech, string speaker = "")
    {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(speech, speaker));
    }

    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
            
        }
        speaking = null;
        
    }

    public bool isSpeaking { get { return speaking != null; } }
    public bool isWatingForUserInput = false;


    Coroutine speaking = null;
    IEnumerator Speaking(string targetSpeech, string speaker = "")
    {
        speechPanel.SetActive(true);
        speechText.text = "";
        speakerNameText.text = DetermineSpeaker(speaker);
        isWatingForUserInput = false;

      while (speechText.text != targetSpeech)
        {
            speechText.text += targetSpeech[speechText.text.Length];
            yield return new WaitForSeconds(0.1f);
          
        }

        isWatingForUserInput = true;
        while (isWatingForUserInput)
        yield return new WaitForEndOfFrame();
        textSystem.index++;
        textSystem.userInput = false;
        StopSpeaking();
    }

    public void SkipTextScroll(string targetSpeech, string speaker = "")
    {
        if (speaking != null)
        {
            speechText.text = targetSpeech;
            StopCoroutine(speaking);
            isWatingForUserInput = true;
        }
    }

    string DetermineSpeaker(string speakerName)
    {
        string retVal = speakerNameText.text;
        if (speakerName != speakerNameText.text && speakerName != "")
            retVal = (speakerName.ToLower().Contains("narrator")) ? "" : speakerName;

        return retVal;
    }

    void checkifTextIsFinished()
    {
        if (speaking == null)
        {
            textSystem.userInput = false;
        }
    }

    [System.Serializable]
    public class ELEMENTS
    {
        public GameObject speechPanel;
        public Text speakerNameText;
        public Text speechText;
    }
    public GameObject speechPanel { get { return elements.speechPanel; } }
    public Text speakerNameText { get { return elements.speakerNameText; } }
    public Text speechText { get { return elements.speechText; } }
}
