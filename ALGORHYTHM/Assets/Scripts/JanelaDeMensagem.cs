using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JanelaDeMensagem : MonoBehaviour {

	public Text mensagem;
	public Button btnOk;

	public void btnEntendi_OnClick()
	{
		this.gameObject.SetActive (false);
	}

}
