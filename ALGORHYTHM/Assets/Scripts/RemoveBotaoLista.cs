using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemoveBotaoLista : MonoBehaviour {

	GameObject esseBotao;
	CreateProgramList programListScript;
	//CommandButton esseBotaoCMD;
	Comando esseCMD;

	void Awake()
	{
		esseBotao = this.gameObject;
		//esseBotaoCMD = esseBotao.GetComponent<CommandButton> ();
		esseCMD = esseBotao.GetComponent<Comando>();
		buttonSetup(esseBotao.GetComponent<Button>());
		programListScript = CreateProgramList.referencia;
	}

	void buttonSetup(Button button) {
		button.onClick.RemoveAllListeners();
		//Add your new event
		button.onClick.AddListener(() => DestroiBotao(button));
	}
	
	void DestroiBotao(Button b) {
		//Debug.Log("O Comando '" + esseBotaoCMD.nameLabel.text + "' - #" + esseBotaoCMD.listNumber.ToString() + " foi removido da Program List!");
	//	foreach (CommandButton btn in programListScript.programList) 
		foreach (Comando cmd in programListScript.listaPrograma)
		{
			//if(btn.listNumber > esseBotaoCMD.listNumber)
			if(cmd.numeroLista > esseCMD.numeroLista)
			{
				//btn.listNumber--;
				cmd.numeroLista--;
				//btn.numberLabel.text = '#'+btn.listNumber.ToString();
			}
		}
		//programListScript.programList.Remove (esseBotaoCMD);
		programListScript.listaPrograma.Remove(esseCMD);
		Destroy (esseBotao);
	}
}
