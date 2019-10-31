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

	public List<Characters> characters = new List<Characters>();
	Characters charactersSprites;

	//public string characterName1;
	//public Image Character1;

	//public string characterName2;
	//public Image Character2;

	TextSystem text;
	DialogueSystem dialogue;

	//public int lineNumber;
	//public int numberOff;

	

	// Start is called before the first frame update
	void Start()
    {
		text = GetComponent<TextSystem>();
		dialogue = DialogueSystem.instance;
		//Character1.enabled = false;
		//Character2.enabled = false;

	}

	// Update is called once per frame
	void Update()
	{
		

		if (text != null)
		{
			//if (text.index == lineNumber)
			//	{
			//	Character1.enabled = true;
			//	}
			//	else if(text.index == numberOff)
			//	{
			//		Character1.enabled = false;
			//	}

			//Character1Images();
			//Character2Images();

			characterList();
		}

		Debug.Log(dialogue.speakerNameHold);
	
	}

	/*void Character1Images()
	{
			if (dialogue.speakerNameHold == characterName1)
			{
				Character1.enabled = true;
			}
			else if (dialogue.speakerNameHold != characterName1)
			{
				Character1.enabled = false;
			}
	}

	void Character2Images()
	{
		if (dialogue.speakerNameHold == characterName2)
		{
			Character2.enabled = true;
		}
		else if (dialogue.speakerNameHold != characterName2)
		{
			Character2.enabled = false;
		}
	}*/

	void characterList()
	{
		if (dialogue.speakerNameHold == charactersSprites.characterName)
		{
			charactersSprites.Character.enabled = true;
		}
		else if (dialogue.speakerNameHold != charactersSprites.characterName)
		{
			charactersSprites.Character.enabled = false;
		}
	}
}
