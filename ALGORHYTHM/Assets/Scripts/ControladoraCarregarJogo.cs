using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControladoraCarregarJogo : MonoBehaviour
{
	public GameObject gameManager; //GameManager prefab para instantiate.
	public GameObject btnJogoSalvo; //Prefab do Botao para instanciar.
	public Transform scrollConteudo; //Referencia ao objeto na qual o prefab sera filho.

	public List<Jogo> listaJogosSalvos;
	public List<Perfil> listaPerfis;

	//Banco
	private ConexaoBanco banco;
	private float intervaloEspera = 0.2f;
	private float tempo;

	// Use this for initialization
	void Awake ()
	{
		banco = null;
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (ControladorGeral.referencia == null)			
			//Instantiate gameManager prefab
			Instantiate(gameManager);
		CarregaJogosSalvos();
		tempo = Time.time;
	}

	void Update()
	{
		if(tempo < Time.time)
		{
			
			if(Input.GetKey(KeyCode.Escape))
			{
				tempo = Time.time + intervaloEspera;
				Application.LoadLevel(0);
			}
		}
	}

	void CarregaJogosSalvos()
	{
		listaJogosSalvos = new List<Jogo>();
		listaPerfis = new List<Perfil>();

		banco = new ConexaoBanco();
		banco.AbrirBanco("URI=file:" + Application.dataPath + "/MeuJogoSalvo.sqdb");

		ArrayList arrayTemp = new ArrayList();
		ArrayList arrayJogosTemp = new ArrayList();
		ArrayList arrayPerfisTemp = new ArrayList();

		arrayTemp = banco.LerTabelaToda("Jogo");	
		foreach(object obj in arrayTemp)
		{
			arrayJogosTemp.Add((ArrayList)obj); //Cada Objeto aqui e um Jogo em forma de Array
		}
		foreach(ArrayList obj in arrayJogosTemp) //Aqui e onde quebramos a Array e colocamos como um objeto Jogo
		{
			Jogo jg = new Jogo();
			jg.idJogo = (int)obj[0];
			jg.pontuacaoTotal = (int)obj[1];
			jg.dataJogoCriado = (string)obj[2];
			jg.dataJogoSalvo = (string)obj[3];
			jg.numeroFaseLiberada = (int)obj[4];
			jg.capituloAtual = (int)obj[5];
			jg.idPerfilJogador = (int)obj[6];
			
			listaJogosSalvos.Add(jg);
		}
		//

		arrayTemp = banco.LerTabelaToda("Perfil");
		foreach(object obj in arrayTemp)
		{
			arrayPerfisTemp.Add((ArrayList)obj); //Cada Objeto aqui e um Perfil em forma de Array
		}
		foreach(ArrayList obj in arrayPerfisTemp) //Aqui e onde quebramos a Array e colocamos como um objeto Perfil
		{
			Perfil oPe = new Perfil();
			oPe.idPerfil = (int)obj[0];
			oPe.nomeAluno = (string)obj[1];
			oPe.idadeAluno = (int)obj[2];
			oPe.serieAluno = (string)obj[3];
			oPe.generoAluno = (string)obj[4];
			
			listaPerfis.Add(oPe);
		}
		//


		foreach(Jogo jg in listaJogosSalvos)
		{
			GameObject novoBotao = Instantiate(btnJogoSalvo) as GameObject;
			novoBotao.transform.SetParent(scrollConteudo, false);
			BotaoJogoSalvo btnJg = novoBotao.GetComponent<BotaoJogoSalvo>();

			Perfil oPerf = new Perfil();
			Jogo oJogo = new Jogo();

			btnJg.txtIdJogo.text = jg.idJogo.ToString();
			btnJg.txtNumeroFase.text = jg.numeroFaseLiberada.ToString();
			btnJg.txtPontuacao.text = jg.pontuacaoTotal.ToString ();
			btnJg.txtDataSalvo.text = jg.dataJogoSalvo;
			Debug.Log ("Capitulo: "+jg.capituloAtual);

			foreach(Perfil oPe in listaPerfis)
			{
				if(oPe.idPerfil == jg.idPerfilJogador)
				{
					btnJg.txtNomeJogador.text = oPe.nomeAluno;
					oPerf = oPe;
				}
			}

			oJogo = jg;

			Button oButton = btnJg.gameObject.GetComponent<Button>();
			oButton.onClick.RemoveAllListeners();
			oButton.onClick.AddListener(() => Carregar(oJogo, oPerf));
		}

		banco.FecharBanco();
	}

	public void Carregar(Jogo jg, Perfil perf)
	{
		ControladorGeral.referencia.jogoAtual = jg;
		ControladorGeral.referencia.perfilAtual = perf;
		Debug.Log ("Capitulo Carregado: "+jg.capituloAtual);
		Application.LoadLevel (1);
	}

}

