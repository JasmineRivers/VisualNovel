using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstSprite : MonoBehaviour
{
    //public int numberOfCharacters;

    //public Image sprite1;


    [System.Serializable]
    public struct Characters
    {
        public string characterName;
        public Image Happy;
        public Image Sad;
        public Image Nutral;
        public Image Angry;
    }

    //public List<Characters> characters = new List<Characters>();
    public Characters[] charactersSprites;

    TextSystem text;
    DialogueSystem dialogue;



    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextSystem>();
        dialogue = DialogueSystem.instance;

    }

    // Update is called once per frame
    void Update()
    {


        if (text != null)
        {

            for (int i = 0; i < charactersSprites.Length; i++)
            {

                if (dialogue.speakerNameHold == charactersSprites[i].characterName)
                {
                    if (text.index == i)
                    {
                        switch (text.text[i].emotion)
                        {
                            case TextSystem.Text.Emotion.Happy:
                                {

                                    charactersSprites[i].Happy.enabled = true;
                                    // charactersSprites[i].Nutral.enabled = false;
                                    // charactersSprites[i].Angry.enabled = false;
                                    charactersSprites[i].Sad.enabled = false;

                                    break;
                                }
                            case TextSystem.Text.Emotion.Sad:
                                {
                                    charactersSprites[i].Sad.enabled = true;
                                    //  charactersSprites[i].Nutral.enabled = false;
                                    //  charactersSprites[i].Angry.enabled = false;

                                    charactersSprites[i].Happy.enabled = false;
                                    break;
                                }
                            case TextSystem.Text.Emotion.Angry:
                                {

                                    charactersSprites[i].Nutral.enabled = false;
                                    charactersSprites[i].Angry.enabled = false;
                                    charactersSprites[i].Sad.enabled = false;
                                    charactersSprites[i].Happy.enabled = false;
                                    break;
                                }
                            case TextSystem.Text.Emotion.Nutrual:
                                {
                                    charactersSprites[i].Nutral.enabled = true;

                                    charactersSprites[i].Angry.enabled = false;
                                    charactersSprites[i].Sad.enabled = false;
                                    charactersSprites[i].Happy.enabled = false;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }

                        }
                    }
                    // charactersSprites[i].Character.enabled = true;
                }
                else if (dialogue.speakerNameHold != charactersSprites[i].characterName)
                {
                    //charactersSprites[i].Character.enabled = false;
                      //charactersSprites[i].Nutral.enabled = false;
                      // charactersSprites[i].Angry.enabled = false;
                    charactersSprites[i].Sad.enabled = false;
                    charactersSprites[i].Happy.enabled = false;
                }
            }

        }

    }

    void checkIfNull()
    {

    }
}
