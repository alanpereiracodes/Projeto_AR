using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemoveBotaoLista : MonoBehaviour {

	GameObject esseBotao;
	CreateProgramList programListScript;
	CommandButton esseBotaoCMD;

	void Awake()
	{
		esseBotao = this.gameObject;
		esseBotaoCMD = esseBotao.GetComponent<CommandButton> ();
		buttonSetup(esseBotao.GetComponent<Button>());
		programListScript = CreateProgramList.referencia;
	}

	void buttonSetup(Button button) {
		button.onClick.RemoveAllListeners();
		//Add your new event
		button.onClick.AddListener(() => DestroiBotao(button));
	}
	
	void DestroiBotao(Button b) {
		Debug.Log("O Comando '" + esseBotaoCMD.nameLabel.text + "' - #" + esseBotaoCMD.listNumber.ToString() + " foi removido da Program List!");
		foreach (CommandButton btn in programListScript.programList) 
		{
			if(btn.listNumber > esseBotaoCMD.listNumber)
			{
				btn.listNumber--;
				btn.numberLabel.text = '#'+btn.listNumber.ToString();
			}
		}
		programListScript.programList.Remove (esseBotaoCMD);
		Destroy (esseBotao);
	}
}
