using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlinhaCaretStick : MonoBehaviour {

	private bool ok;

	void Awake()
	{
		ok = false;
	}
	
	void Update () 
	{
		//Tente ate conseguir
		if(!ok)
		{
			if(transform.childCount > 2)
			{
				if(transform.FindChild("InputField Input Caret").gameObject != null)
				{
					GameObject caretStick = transform.FindChild("InputField Input Caret").gameObject;
					caretStick.GetComponent<RectTransform>().pivot = new Vector2(0, 0.45f);
					ok = true;
				}
			}
		}
	}//FIM Update
	
}//FIM Script
