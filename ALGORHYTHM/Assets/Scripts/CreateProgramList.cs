using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


//[System.Serializable]
//public class Item
//{
//	public string name;
//	public Sprite icon;
//	public string method;
//	/*espaço ocupados
//	 *se o metodo precisa de algum argumento
//	 */
//}


public class CreateProgramList : MonoBehaviour {

	public static CreateProgramList referencia;

	public Transform contentPanel;
	public int limiteLista;

	//public ScrollRect scroller;

	//public static GameObject stContentPanel;
	public List<CommandButton> programList = new List<CommandButton>();

	void Awake() //for static use
	{
		//stContentPanel = contentPanel;
		if (referencia == null)		
			//if not, set instance to this
			referencia = this;		
		//If instance already exists and it's not this:
		else if (referencia != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);  
	}


	
	public void PopulateList(GameObject sampleButton)
	{
		if (programList.Count < limiteLista) {
		GameObject newButton = Instantiate (sampleButton) as GameObject;
		CommandButton button = newButton.GetComponent <CommandButton>();
		if (contentPanel != null) 
		{
			newButton.transform.SetParent(contentPanel);
		}

			button.listNumber = programList.Count + 1;
			button.numberLabel.text = '#'+button.listNumber.ToString();
			programList.Add (button);

		} else
			Debug.Log ("A Lista esta cheia!");
	}
	
}
