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
        public GameObject Happy;
        public GameObject Sad;
        public GameObject Nutral;
        public GameObject Angry;
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

    }

    public void checkIfNull()
    {
		if (text != null)
		{

			for (int i = 0; i < charactersSprites.Length; i++)
			{

				if (dialogue.speakerNameHold == charactersSprites[i].characterName)
				{
					switch (text.text[text.index].emotion)
					{
						case TextSystem.Text.Emotion.Happy:
							{
								nullCheck(i);
								charactersSprites[i].Happy.SetActive(true);
								break;
							}
						case TextSystem.Text.Emotion.Sad:
							{
								nullCheck(i);
								charactersSprites[i].Sad.SetActive(true);
								break;
							}
						case TextSystem.Text.Emotion.Angry:
							{
								nullCheck(i);
								charactersSprites[i].Angry.SetActive(true);
								break;
							}
						case TextSystem.Text.Emotion.Nutrual:
							{
								nullCheck(i);
								charactersSprites[i].Nutral.SetActive(true);
								break;
							}
						default:
							{
								break;
							}
					}

				}
				else if (dialogue.speakerNameHold != charactersSprites[i].characterName)
				{
					nullCheck(i);
				}
			}

		}

		
	}

	void nullCheck(int i)
	{
		if (charactersSprites[i].Sad != null)
		{
			charactersSprites[i].Sad.SetActive(false);
		}

		if (charactersSprites[i].Happy != null)
		{
			charactersSprites[i].Happy.SetActive(false);
		}

		if (charactersSprites[i].Angry != null)
		{
			charactersSprites[i].Angry.SetActive(false);
		}

		if (charactersSprites[i].Nutral != null)
		{
			charactersSprites[i].Nutral.SetActive(false);
		}
	}


}
