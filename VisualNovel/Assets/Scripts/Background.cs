using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
	[System.Serializable]
	public struct BackgroundImage
	{
		public string backgroundName;
		public GameObject backgroundImage;
		//public int textLineNumber;
	}
	public BackgroundImage[] background;

	TextSystem text;

	// Start is called before the first frame update
	void Start()
    {
		text = GetComponent<TextSystem>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void checkBackground()
	{
		if (text != null)
		{
			for (int i = 0; i < background.Length; i++)
			{
				if (background[i].backgroundImage != null)
				{


					if (text.backgroundName == background[i].backgroundName)
					{
						background[i].backgroundImage.SetActive(true);
					}
					else if (text.backgroundName != background[i].backgroundName)
					{
						background[i].backgroundImage.SetActive(false);
					}
				}
			}
		}
	}

}
