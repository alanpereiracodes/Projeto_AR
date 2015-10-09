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
		if (!ControladorGeral.referencia.retry) 
		{
			if (!ControladorGeral.referencia.listaEmExecucao) {
				foreach (Comando cmd in programListScript.listaPrograma) {
					//if(btn.listNumber > esseBotaoCMD.listNumber)
					if (cmd.numeroLista > esseCMD.numeroLista) {
						//btn.listNumber--;
						cmd.numeroLista--;
						//btn.numberLabel.text = '#'+btn.listNumber.ToString();
					}
				}
				//programListScript.programList.Remove (esseBotaoCMD);
				programListScript.listaPrograma.Remove (esseCMD);
				Destroy (esseBotao);
			} 
			else 
			{
				EnviaMensagem ("\nNão foi possível remover o Comando porque a lista esta em execução.");
			}
		}
		else 
		{
			EnviaMensagem ("\nReinicie a Fase antes de remover o Comando.");
		}
	}

	public void EnviaMensagem(string mensagem)
	{
		ControladorGeral.referencia.myLog.text += mensagem;
		if (ControladorGeral.referencia.myScroll != null)
		{
			//Debug.Log (ControladorGeral.referencia.myScroll.value.ToString ());
			ControladorGeral.referencia.myScroll.value = 0;
		}
	}
}
