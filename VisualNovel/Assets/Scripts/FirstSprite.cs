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
		public Image Character;
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
                    charactersSprites[i].Character.enabled = true;
                }
                else if (dialogue.speakerNameHold != charactersSprites[i].characterName)
                {
                    charactersSprites[i].Character.enabled = false;
                }
            }

		}

	}

}
