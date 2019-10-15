using UnityEngine;
using UnityEngine.UI;

public class SpeechText : MonoBehaviour
{
    TextSystem textSystem;
    Text text;
    RectTransform recTransform;
    int wordSize;
    float lineSpacing;

    void Start()
    {
      
        //Fetch the Text and RectTransform components from the GameObject
        text = GetComponent<Text>();
        recTransform = GetComponent<RectTransform>();
        textSystem = TextSystem.instance;

        wordSize = textSystem.textSize;
        lineSpacing = textSystem.lineSpacing;


    }

    void Update()
    {
        changeFontSize();
    }

    void changeFontSize()
    {
        text.fontSize = wordSize;
        text.lineSpacing = lineSpacing;
        text.color = Color.blue;
        text.font = textSystem.font;

    }
}