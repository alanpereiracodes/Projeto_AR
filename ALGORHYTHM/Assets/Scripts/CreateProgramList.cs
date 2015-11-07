using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CreateProgramList : MonoBehaviour {

	public static CreateProgramList referencia;

	public Transform contentPanel;
	public Transform contentPanel2; //Comandos Funçao

	//Capitulo 1 UI
	public Text refLog;
	public Text refLogAvanc;
	public Scrollbar refScroll;
	public Scrollbar refScrollAvanc;
	public Text tituloFase;
	public Image btnExecutarImage;
	public Sprite btnPlay;
	public Sprite btnRetry;
	public Sprite cubinhoVazio;
	public Sprite cubinhoPreenchido;
	public GameObject janelaPontuacao;
	public GameObject janelaOpcao;
    public GameObject janelaAjuda;
	public AudioSource musicaFase;
	public Image imagemFase;
	public Image imagemFase2;
	public Text textoObjetivo;

	//Capitulo 2 UI
	public Text limitePrincipal;
	public Text limiteFuncao;
	public int numLimitePrincipal;
	public int numLimiteFuncao;
	public Toggle togPrincipal;
	public Toggle togFuncao;
	public GameObject destaqueComando;
	public GameObject destaqueComandoDois;
	public Material materialCristal;
	
	public List<Comando> listaPrograma = new List<Comando>();
	public List<Comando> listaFuncao = new List<Comando>();

	void Awake()
	{
		if (referencia == null)
			referencia = this;	
		else if (referencia != this)
			Destroy(gameObject);
	}

	public void RecarregaUI()
	{
		ControladorGeral.referencia.myLog = refLog;
		ControladorGeral.referencia.myLogAvanc = refLogAvanc;
		ControladorGeral.referencia.myScroll = refScroll;
		ControladorGeral.referencia.myScrollAvanc = refScrollAvanc;
		ControladorGeral.referencia.myTituloFase = tituloFase;
		ControladorGeral.referencia.myBtnExecutarImage = btnExecutarImage;
		ControladorGeral.referencia.myBtnPlay = btnPlay;
		ControladorGeral.referencia.myBtnRetry = btnRetry;
		ControladorGeral.referencia.cubinhoVazio = cubinhoVazio;
		ControladorGeral.referencia.cubinhoPreenchido = cubinhoPreenchido;
		ControladorGeral.referencia.janelaFaseConcluida = janelaPontuacao;
		if(ControladorGeral.referencia.musicaRolando != null)
		{
			if(ControladorGeral.referencia.musicaRolando.clip != musicaFase.clip)
			{
				Destroy(ControladorGeral.referencia.musicaRolando.gameObject);
				GameObject MusicaHolder = new GameObject();
				MusicaHolder.name = "Musica Fase";
				MusicaHolder.AddComponent<AudioSource>();
				MusicaHolder.GetComponent<AudioSource>().clip = musicaFase.clip;
				ControladorGeral.referencia.musicaRolando = MusicaHolder.GetComponent<AudioSource>();
				ControladorGeral.referencia.musicaRolando.loop = true;
				ControladorGeral.referencia.musicaRolando.Play();
				Debug.Log ("Nao destroi vei2");
				DontDestroyOnLoad(ControladorGeral.referencia.musicaRolando);
			}
			else
			{
				Debug.Log ("Nao destruiu");
				Destroy (musicaFase.gameObject);
			}
		}
		else
		{

			GameObject MusicaHolder = new GameObject();
			MusicaHolder.name = "Musica Fase";
			MusicaHolder.AddComponent<AudioSource>();
			MusicaHolder.GetComponent<AudioSource>().clip = musicaFase.clip;
			ControladorGeral.referencia.musicaRolando = MusicaHolder.GetComponent<AudioSource>();
			ControladorGeral.referencia.musicaRolando.loop = true;
			ControladorGeral.referencia.musicaRolando.Play();
			Debug.Log ("Nao destroi vei1");
			DontDestroyOnLoad(ControladorGeral.referencia.musicaRolando);
		}
		ControladorGeral.referencia.musicaRolando.volume = ControladorGeral.referencia.volumeAtual;
		if(ControladorGeral.referencia.logAvanc)
		{
			ControladorGeral.referencia.myLogAvanc.rectTransform.parent.transform.parent.gameObject.SetActive(true);
			ControladorGeral.referencia.myLog.rectTransform.parent.transform.parent.gameObject.SetActive(false);
		}
		else
		{
			ControladorGeral.referencia.myLog.rectTransform.parent.transform.parent.gameObject.SetActive(true);
			ControladorGeral.referencia.myLogAvanc.rectTransform.parent.transform.parent.gameObject.SetActive(false);
		}
	}

	
	public void PopulateList(GameObject sampleButton)
	{
		if (!ControladorGeral.referencia.listaEmExecucao)
		{
			if(!ControladorGeral.referencia.capituloDois)
			{
				GameObject newButton = Instantiate (sampleButton) as GameObject;
				Comando meuComando = newButton.GetComponent<Comando> ();
				if (contentPanel != null) 
				{
					newButton.transform.SetParent (contentPanel);
				}
				meuComando.numeroLista = listaPrograma.Count + 1;
				listaPrograma.Add (meuComando);
			}
			else
			{
				if(togPrincipal.isOn) //Vai popular os comandos na lista de Comandos Principal
				{
					if(listaPrograma.Count < numLimitePrincipal)
					{
						GameObject newButton = Instantiate (sampleButton) as GameObject;
						Comando meuComando = newButton.GetComponent<Comando> ();
						if (contentPanel != null) 
						{
							newButton.transform.SetParent (contentPanel);
						}
						meuComando.numeroLista = listaPrograma.Count + 1;
						listaPrograma.Add (meuComando);
					}
				}
				else //Vai popular os comandos na lista de Comandos de Funçao
				{
					if(listaFuncao.Count < numLimiteFuncao)
					{
						GameObject newButton = Instantiate (sampleButton) as GameObject;
						Comando meuComando = newButton.GetComponent<Comando> ();
						if (contentPanel != null) 
						{
							newButton.transform.SetParent (contentPanel2);
						}
						meuComando.numeroLista = listaFuncao.Count + 1;
						listaFuncao.Add (meuComando);
					}
				}
			}
		}
		else 
		{
			Debug.Log ("A Lista de Programa ja esta em execuçao!!");
		}
	}

	public void LimpaLista()
	{
		if (!ControladorGeral.referencia.retry) {
			if (!ControladorGeral.referencia.listaEmExecucao) 
			{
				foreach (GameObject objeto in GameObject.FindGameObjectsWithTag("Comando")) 
				{
					Destroy (objeto);
				}
				listaPrograma.Clear ();
				if(ControladorGeral.referencia.capituloDois)
				{	
					listaFuncao.Clear ();
				}
				Debug.Log ("Lista de Programa apagada!");
			} 
			else
				Debug.Log ("Lista de Programa esta em execuçao!");
		} 
		else 
		{
			EnviaMensagem("\nReinicie a Fase antes de Limpar a Lista!");
			EnviaCodigo ("\nErro: if(!fase.reiniciada){ retorno false;}");
		}
	}

	public void EnviaMensagem(string mensagem)
	{
		ControladorGeral.referencia.myLog.text += mensagem;
		if (ControladorGeral.referencia.myScroll != null)
		{
			ControladorGeral.referencia.myScroll.value = 0;
		}
	}

	public void EnviaCodigo(string mensagem)
	{
		ControladorGeral.referencia.myLogAvanc.text += mensagem;
		if (ControladorGeral.referencia.myScrollAvanc != null)
		{
			ControladorGeral.referencia.myScrollAvanc.value = 0;
		}
	}

	public void AbreOption()
	{
		if (!janelaOpcao.activeInHierarchy) 
		{
			JanelaOption jaOpt = janelaOpcao.GetComponent<JanelaOption>();
			jaOpt.txtResolucao.text = ControladorGeral.referencia.resolucaoAtual;
			jaOpt.volume.value = ControladorGeral.referencia.volumeAtual;
			if(ControladorGeral.referencia.logAvanc)
			{
				jaOpt.togAvancado.isOn = true;
				jaOpt.togSimples.isOn = false;
			}
			else
			{
				jaOpt.togSimples.isOn = true;
				jaOpt.togAvancado.isOn = false;
			}
			janelaOpcao.SetActive (true);
			janelaOpcao.GetComponentInChildren<Animation>().Play();

		} 
		else
		{
			janelaOpcao.SetActive (false);
		}
	}

}
