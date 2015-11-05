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

	void buttonSetup(Button button) 
	{
		button.onClick.RemoveAllListeners();
		//Add your new event
		button.onClick.AddListener(() => DestroiBotao(button));
	}
	
	void DestroiBotao(Button b) {
		//Debug.Log("O Comando '" + esseBotaoCMD.nameLabel.text + "' - #" + esseBotaoCMD.listNumber.ToString() + " foi removido da Program List!");
		//	foreach (CommandButton btn in programListScript.programList) 
		if (!ControladorGeral.referencia.retry) 
		{
			if (!ControladorGeral.referencia.listaEmExecucao)
			{
				if(!ControladorGeral.referencia.capituloDois)
				{
					foreach (Comando cmd in programListScript.listaPrograma) 
					{
						//if(btn.listNumber > esseBotaoCMD.listNumber)
						if (cmd.numeroLista > esseCMD.numeroLista) 
						{
							//btn.listNumber--;
							cmd.numeroLista--;
							//btn.numberLabel.text = '#'+btn.listNumber.ToString();
						}
					}
				}
				else
				{
					Debug.Log (b.transform.parent.name);
					if(b.transform.parent == programListScript.contentPanel2)
					{
						foreach (Comando cmd in programListScript.listaFuncao) 
						{
							//if(btn.listNumber > esseBotaoCMD.listNumber)
							if (cmd.numeroLista > esseCMD.numeroLista) 
							{
								//btn.listNumber--;
								cmd.numeroLista--;
								//btn.numberLabel.text = '#'+btn.listNumber.ToString();
							}
						}
					}
					else
					{
						foreach (Comando cmd in programListScript.listaPrograma) 
						{
							//if(btn.listNumber > esseBotaoCMD.listNumber)
							if (cmd.numeroLista > esseCMD.numeroLista) 
							{
								//btn.listNumber--;
								cmd.numeroLista--;
								//btn.numberLabel.text = '#'+btn.listNumber.ToString();
							}
						}
					}
				}
				//programListScript.programList.Remove (esseBotaoCMD);
				if(!ControladorGeral.referencia.capituloDois)
					programListScript.listaPrograma.Remove (esseCMD);
				else
				{
					Debug.Log (b.transform.parent.name);
					if(b.transform.parent == programListScript.contentPanel2)
					{
						programListScript.listaFuncao.Remove (esseCMD);
					}
					else
					{
						programListScript.listaPrograma.Remove (esseCMD);
					}
				}
				Destroy (esseBotao);
				Debug.Log(programListScript.listaPrograma.Count.ToString()+" E "+programListScript.listaFuncao.Count.ToString());
			} 
			else 
			{
				EnviaMensagem ("\nNão foi possível remover o Comando porque a lista esta em execução.");
				EnviaCodigo ("\nErro: if(lista.execucao){ retorno false;}");
			}
		}
		else 
		{
			EnviaMensagem ("\nReinicie a Fase antes de remover o Comando.");
			EnviaCodigo ("\nErro: if(!fase.reiniciada){ retorno false;}");
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

	public void EnviaCodigo(string mensagem)
	{
		ControladorGeral.referencia.myLogAvanc.text += mensagem;
		if (ControladorGeral.referencia.myScrollAvanc != null)
		{
			//Debug.Log (ControladorGeral.referencia.myScroll.value.ToString ());
			ControladorGeral.referencia.myScrollAvanc.value = 0;
		}
	}
}
