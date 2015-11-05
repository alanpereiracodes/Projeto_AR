using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

//Esse script se encarrega de concentrar toda a informaçao a ser manipulad apor outros scripts em um so lugar.
//Pois esse script e criado asism que o jogo e criado/carregado e ao inves de ser destruido a cada Cena carregado este se mantem.

public class ControladorGeral : MonoBehaviour {

	//Referencia ao Script Controlador Geral Principal---------------------------------------
	public static ControladorGeral referencia = null;

	//Referencias aos objetosna excuçao das Fases.-----------------------------------
	public CriaTabuleiro tabuleiroAtual;
	public CameraBtnEvents cameraEventos;

	public bool listaEmExecucao = false;
	public bool aSalvar = false;
	public bool retry = false;
	public bool logAvanc = false; //Guarda informaçoes sobre qual Log esta a ser mostrado. Simples (false) ou Avançado (true)

	public int faseAtual;
	public int capituloAtual;
	public int numeroComandos;
	public int numeroRetries;
	public int pontuacao;
	public int numeroComandosIdeal; //para pontuaçao

	public float volumeAtual = 0.8f;

	public string resolucaoAtual = "1366x728"; //UI guarda informaçoes sobrea  resoluçao atual - Janela de Opçoes
    
	public Text myTituloFase; //UI Texto da Fase na tela de Fase
	public Text myLog; //UI Log do Sistema Simples
	public Text myLogAvanc; //UI Log do Sistema Avançado
	public Text capituloTexto; //UI Texto do Capitulo na tela de Seleçao de Fase
	public Scrollbar myScroll; //UI Scrollbar do Log de Sistema Simples
	public Scrollbar myScrollAvanc; //UI Scrollbar do Log do Sistema Avançado
	public Image myBtnExecutarImage; //Referencia ao componente de Imagem do Botao Executar na Tela de Fase
	public Sprite myBtnPlay;
	public Sprite myBtnRetry;
	public Sprite cubinhoVazio;
	public Sprite cubinhoPreenchido;
	public AudioSource musicaRolando; //Controle da Musica que esta tocando no momento, volume e etc

	public GameObject myPlayer;
	public GameObject janelaFaseConcluida;

	public Player.Direction direcaoInicial;

	//Fase Atual
	public Vector2 posicaoInicial;
	//Direçao do personagem Inicial - a fazer
	public Vector2 posicaoObjetivo = new Vector2(99f,99f);

	//Lista com os Botoes das fases
	public List<GameObject> listaBotoesFases;
	public List<Fase> listaFases;

	//Referencia para Tela de Carregar jogo
	public List<Jogo> listaJogosSalvos;

	//Dados concentrados no Script
	public Perfil perfilAtual; //Por enquanto so sera informaçoes que poderiam estar na mesma classe, futuramente um perfil podera ter mais de um jogo vinculado a ele
	public Jogo jogoAtual; //Armazena o Jogo Atual;

	//Variavel para ter acesso ao banco
	private ConexaoBanco banco;
	//Variaveis para manipulaçao das tabelas
	private string tableName;
	private ArrayList columnNames;
	private ArrayList columnValues;

	//Capitulo 2
	public bool capituloDois;
	public int limiteListaPrincipal;
	public int limiteListaFuncao;
	
	//===================== INICIO ===============================
	void Awake () 
	{
		musicaRolando = null;
		if (referencia == null)	
			referencia = this;		
		else if (referencia != this)
			Destroy(gameObject);  

		DontDestroyOnLoad(gameObject);
        jogoAtual = null;
		perfilAtual = null;
		listaJogosSalvos = new List<Jogo>(); //Lista para armazenar todos os jogos salvos de um dado perfil;
		//Option
		resolucaoAtual = "1366x728";
		//Inicializa o banco como null;
        banco = null;
    }

	void Update()
	{

		if (tabuleiroAtual != null && myPlayer != null && posicaoObjetivo != new Vector2(99f,99f) && !janelaFaseConcluida.activeInHierarchy) 
		{
				if (myPlayer.GetComponent<Player> ().posicaoTabuleiro == posicaoObjetivo)
				{
					if (numeroRetries > 0)
						pontuacao = 1;
					else 
					{
						if (numeroComandos > numeroComandosIdeal)
							pontuacao = 2;
						else
						{
							pontuacao = 3;
						}
					}
					Debug.Log (pontuacao);
					//PassouDeFase
					retry = false;
					listaEmExecucao = false;

					JanelaPassouFase jan = janelaFaseConcluida.GetComponent<JanelaPassouFase> ();
					jan.PreencheCubos (pontuacao, capituloAtual.ToString () + " - " + faseAtual.ToString ()); //Apagado 303030FF Ligado FFFFFFFF
					jan.btnOk.onClick.RemoveAllListeners ();
					jan.btnAgain.onClick.RemoveAllListeners ();

					jan.btnOk.onClick.AddListener (() => PassaFase (capituloAtual, faseAtual + 1));
					jan.btnAgain.onClick.AddListener (() => RecarregaFase ());

					if(!aSalvar)
					{
						aSalvar = !aSalvar;
						SalvarJogo (capituloAtual, faseAtual); //Salva o Jogo Atual se nao existir um registro dessa Fase ainda ou se a pontuaçao for maior
					}

					janelaFaseConcluida.SetActive (true);
					janelaFaseConcluida.GetComponentInChildren<Animation>().Play();
			}
		} 
	}


	#region BancoDados
	public void CriarTabelas ()
	{
		//Criar/Verifica Existencia da tabela Perfil
		tableName = "Perfil";
		columnNames = new ArrayList();
		columnNames.Add("idPerfil");
		columnNames.Add("nomeAluno");
		columnNames.Add("idadeAluno");
		columnNames.Add("serieAluno");
		columnNames.Add("generoAluno");
		
		columnValues = new ArrayList();
		columnValues.Add("INTEGER DEFAULT 1 PRIMARY KEY AUTOINCREMENT NOT NULL");
		columnValues.Add("TEXT NOT NULL");
		columnValues.Add("INTEGER NOT NULL");
		columnValues.Add("TEXT NOT NULL");
		columnValues.Add("TEXT NOT NULL");		
		try 
		{
			Debug.Log(banco.CriarTabelaRetornaQuery(tableName, columnNames, columnValues));
		}
		catch(UnityException e)
		{
			Debug.Log ("Nao foi possivel criar a tabela "+tableName+", devido a: "+e.ToString());
		}

		//Criar/Verifica Existencia da tabela Jogo
		tableName = "Jogo";
		columnNames = null;
		columnNames = new ArrayList();
		columnNames.Add("idJogo");
		columnNames.Add("pontuacaoTotal");
		columnNames.Add("dataJogoCriado");
		columnNames.Add("dataJogoSalvo");
		columnNames.Add("numeroFaseLiberada");
		columnNames.Add("capituloAtual");
		columnNames.Add("idPerfilJogador");
		
		columnValues = null;
		columnValues = new ArrayList();
		columnValues.Add("INTEGER DEFAULT 1 PRIMARY KEY AUTOINCREMENT NOT NULL");
		columnValues.Add("INTEGER NOT NULL");
		columnValues.Add("TEXT NOT NULL");
		columnValues.Add("TEXT NOT NULL");
		columnValues.Add("INTEGER NOT NULL");
		columnValues.Add("INTEGER NOT NULL");
		columnValues.Add("INTEGER NOT NULL, FOREIGN KEY(`idPerfilJogador`) REFERENCES Perfil");
		
		try {
			banco.CriarTabela(tableName, columnNames, columnValues);
		}
		catch(UnityException e){
			Debug.Log ("Nao foi possivel criar a tabela "+tableName+", devido a: "+e.ToString());
		}
		
		//Criar/Verifica Existencia da tabela Fase
		tableName = "Fase";
		columnNames = null;
		columnNames = new ArrayList();
		columnNames.Add("numeroFase");
		columnNames.Add("capitulo");
		columnNames.Add("pontuacaoCubinhoDigital");
		columnNames.Add("idJogo");
		
		columnValues = null;
		columnValues = new ArrayList();
		columnValues.Add("INTEGER NOT NULL");
		columnValues.Add("INTEGER NOT NULL");
		columnValues.Add("INTEGER NOT NULL");
		columnValues.Add("INTEGER NOT NULL, FOREIGN KEY(`idJogo`) REFERENCES Jogo, PRIMARY KEY (numeroFase, idJogo)");
		
		try {
			banco.CriarTabela(tableName, columnNames, columnValues);
		}
		catch(UnityException e){
			Debug.Log ("Nao foi possivel criar a tabela "+tableName+", devido a: "+e.ToString());
		}
	}

	//=========================================
	//Funçoes do Banco
	public void CriarJogoNovo(Perfil perfilJogador)
	{
		Jogo novoJogo = new Jogo();
		novoJogo.dataJogoCriado = DateTime.Now.ToString("dd/MM/yyyy");
		novoJogo.dataJogoSalvo = DateTime.Now.ToString("dd/MM/yyyy");
		novoJogo.pontuacaoTotal = 0;
		novoJogo.capituloAtual = 1;
		novoJogo.numeroFaseLiberada = 1; //Fase 1 e a primeira fase;

		//Inicia a COnexao com o Banco
		banco = new ConexaoBanco();
		banco.AbrirBanco("URI=file:" + Application.dataPath + "/MeuJogoSalvo.sqdb");
		CriarTabelas();
		//TUDO CRIADO!!

		//Incluir Perfil e Jogo
		tableName = "Perfil";

		columnNames.Clear();
		columnNames.Add("nomeAluno");
		columnNames.Add("idadeAluno");
		columnNames.Add("serieAluno");
		columnNames.Add("generoAluno");

		columnValues.Clear();
		columnValues.Add("'"+perfilJogador.nomeAluno+"'");
		columnValues.Add(perfilJogador.idadeAluno);
		columnValues.Add("'"+perfilJogador.serieAluno+"'");
		columnValues.Add("'"+perfilJogador.generoAluno+"'");

		//Inclui o Perfil e retorna o ID
		ArrayList ultimoPerfil = new ArrayList();
		ultimoPerfil = banco.IncluirEspecificoRetornaId(tableName,"idPerfil", columnNames, columnValues);
		ArrayList teste = (ArrayList)ultimoPerfil[0]; //lista dos Objetos com somente um Objeto que contem os registros do nosso ultimo registro
		int t = (int)teste[0];//(int)teste[0]; //o id e o primeiro registro de nosso Objeto, pois trata-se da primeira Coluna;
		perfilJogador.idPerfil = t;
		novoJogo.idPerfilJogador = t;

		tableName = "Jogo";

		columnNames.Clear();
		columnNames.Add("pontuacaoTotal");
		columnNames.Add("dataJogoCriado");
		columnNames.Add("dataJogoSalvo");
		columnNames.Add("numeroFaseLiberada");
		columnNames.Add("capituloAtual");
		columnNames.Add("idPerfilJogador");

		columnValues.Clear();
		columnValues.Add(novoJogo.pontuacaoTotal.ToString());
		columnValues.Add("'"+novoJogo.dataJogoCriado+"'");
		columnValues.Add("'"+novoJogo.dataJogoSalvo+"'");
		columnValues.Add(novoJogo.numeroFaseLiberada.ToString());
		columnValues.Add(novoJogo.capituloAtual.ToString());
		columnValues.Add(novoJogo.idPerfilJogador.ToString());

		//Inclui o Jogo
		ultimoPerfil = null;
		ultimoPerfil = new ArrayList();
		ultimoPerfil = banco.IncluirEspecificoRetornaId(tableName,"idJogo", columnNames, columnValues);
		teste = (ArrayList)ultimoPerfil[0];
		t = (int)teste[0];
		novoJogo.idJogo = t;

		Debug.Log ("Perfil cadastrado: "+perfilJogador.idPerfil.ToString()+", "+perfilJogador.nomeAluno);
		Debug.Log ("\nJogo cadastrado: "+novoJogo.idJogo.ToString());

		referencia.perfilAtual = perfilJogador;
		referencia.jogoAtual = novoJogo;

		banco.FecharBanco();

	}
	#endregion

	void SalvarJogo (int cap, int fase)
	{
		banco = new ConexaoBanco();
		banco.AbrirBanco("URI=file:" + Application.dataPath + "/MeuJogoSalvo.sqdb");
		CriarTabelas ();
		if(jogoAtual != null)
		{
		Fase novaFase = new Fase ();
		novaFase.idJogo = jogoAtual.idJogo;
		novaFase.numeroFase = fase;
		novaFase.capitulo = cap;
		novaFase.pontuacaoCubinhoDigital = pontuacao;
		
		List<Fase> fasesSelect = new List<Fase> ();

		ArrayList minhasFasesSelecionadas = banco.LerTabelaToda ("Fase");//banco.SelecionaUnicoWhere ("Fase", "*", "idJogo", "=", novaFase.idJogo.ToString ());
		if (minhasFasesSelecionadas != null || minhasFasesSelecionadas.Count > 0) 
		{
			foreach(ArrayList faseSelecioanda in minhasFasesSelecionadas)
			{
				Fase abcFase = new Fase();
				abcFase.numeroFase = (int)faseSelecioanda[0];
				abcFase.capitulo = (int)faseSelecioanda[1];
				abcFase.pontuacaoCubinhoDigital = (int)faseSelecioanda[2];
				abcFase.idJogo = (int)faseSelecioanda[3];
				
				fasesSelect.Add (abcFase);
				Debug.Log ("\n"+abcFase.numeroFase+abcFase.capitulo+abcFase.pontuacaoCubinhoDigital+abcFase.idJogo);
			}
		}

		tableName = "Fase";
		
		columnNames.Clear ();
		columnNames.Add ("numeroFase");
		columnNames.Add ("capitulo");
		columnNames.Add ("pontuacaoCubinhoDigital");
		columnNames.Add ("idJogo");
		
		columnValues.Clear ();
		columnValues.Add (novaFase.numeroFase.ToString ());
		columnValues.Add (novaFase.capitulo.ToString ());
		columnValues.Add (novaFase.pontuacaoCubinhoDigital.ToString ());
		columnValues.Add (novaFase.idJogo.ToString ());

		bool podeSalvar = true;
		bool podeAlterar = false;
		int numeroAltera = 0;
		int somaPontuacao = pontuacao;

			foreach (Fase fa in fasesSelect) 
			{
				if (fa.numeroFase == novaFase.numeroFase && fa.capitulo == novaFase.capitulo && fa.idJogo == novaFase.idJogo) 
				{
					podeSalvar = false;
					if (novaFase.pontuacaoCubinhoDigital > fa.pontuacaoCubinhoDigital) 
					{
						somaPontuacao = pontuacao - fa.pontuacaoCubinhoDigital;
						numeroAltera = fa.numeroFase;
						podeAlterar = true;
					}
					else
					{
						if(novaFase.pontuacaoCubinhoDigital <= fa.pontuacaoCubinhoDigital)
						{
							somaPontuacao = 0; //Nao soma a pontuaçao a Pontuacao Total
						}
					}
				}
			}
		if(podeSalvar)
			banco.IncluirEspecifico (tableName, columnNames, columnValues);
		if (podeAlterar)
		{
			banco.AlterarPorIDComposto(tableName, columnNames, columnValues, "numeroFase","idJogo", numeroAltera.ToString (), novaFase.idJogo.ToString());
			Debug.Log ("alterado");
		}

		//FASE SALVA
		//SALVAR  O JOGO AGORA
			Jogo salvaJogo = jogoAtual;
			if(faseAtual >= 10)
			{
				salvaJogo.capituloAtual = capituloAtual++;
				faseAtual = 0;
			}
			else
			{
				salvaJogo.capituloAtual = capituloAtual;
			}
			if(jogoAtual.numeroFaseLiberada < (((capituloAtual-1) * 10) + faseAtual + 1))
			{
				salvaJogo.numeroFaseLiberada = (((capituloAtual-1) * 10) + faseAtual + 1); 
				Debug.Log ("Fase Liberada: "+salvaJogo.numeroFaseLiberada);
			}
			salvaJogo.dataJogoSalvo = DateTime.Now.ToString("dd/MM/yyyy");
			salvaJogo.pontuacaoTotal += somaPontuacao;

			tableName = "Jogo";
			
			columnNames.Clear ();
			columnNames.Add ("pontuacaoTotal");
			columnNames.Add ("dataJogoSalvo");
			columnNames.Add ("numeroFaseLiberada");
			columnNames.Add ("capituloAtual");
			
			columnValues.Clear ();
			columnValues.Add (salvaJogo.pontuacaoTotal.ToString());
			columnValues.Add ("'"+salvaJogo.dataJogoSalvo+"'");
			columnValues.Add (salvaJogo.numeroFaseLiberada.ToString());
			columnValues.Add (salvaJogo.capituloAtual.ToString());

			banco.AlterarPorID(tableName,columnNames,columnValues,"idJogo",salvaJogo.idJogo.ToString());
		}
		else
		{
			Debug.Log("Nao foi possivel encontrar um jogo para ser salvo.");
		}
		banco.FecharBanco ();
	}

	public void PassaFase(int cap, int fase)
	{
		aSalvar = false;
		if(fase >= 11)
		{
			cap++;
			fase = 1;
		}
		int contaFase = ((cap-1) * 10) + fase;
		Debug.Log (contaFase);

		if(cap == 2 && fase == 1) //Fase 2 - 1 e a ultima
		{
			Application.LoadLevel (3);
		}
		else
		{
			Application.LoadLevel ("Fase " + contaFase);
		}

	}

	public void RecarregaFase()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}

	//=========================================

	IEnumerator Esperar(float segundos) {
		print(Time.time);
		yield return new WaitForSeconds(segundos);
		print(Time.time);
	}

}//FIM
