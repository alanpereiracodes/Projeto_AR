using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

//Esse script se encarrega de concentrar toda a informaçao a ser manipulad apor outros scripts em um so lugar.
//Pois esse scirpt e criado asism que o jogo e criado/carregado e ao inves de ser destruido a cada Cena carregado este se mantem.

public class ControladorGeral : MonoBehaviour {

	//Referencia ao Script Controlador Geral Principal---------------------------------------
	public static ControladorGeral referencia = null;

	//Referencias aos objetosna excuçao das Fases.-----------------------------------
	public GameObject myPlayer;
	public CriaTabuleiro tabuleiroAtual;
	public CameraBtnEvents cameraEventos;
	public bool listaEmExecucao = false;
	public Text myLog;
	public Scrollbar myScroll;
	public Text myTituloFase;
	public int faseAtual;
	public bool retry = false;
	public Image myBtnExecutarImage;
	public Sprite myBtnPlay;
	public Sprite myBtnRetry;

	//Fase Atual
	public Vector2 posicaoInicial;
	public Vector2 posicaoObjetivo;

	//Referencias aos objetos da Selecao de fases--------------------------
	//Esses atributos podem ser encontrados na variavel Jogo Atual!! 
//	public int idJogoAtual = 0;
//	public int ultimaFaseLiberada = 1;
//	public int capituloAtual;
	
	//Lista com os Botoes das fases
	public List<GameObject> listaBotoesFases;
	public List<Fase> listaFases;
	
	//UI
	public Text capituloTexto;
	public Sprite cubinhoVazio;
	public Sprite cubinhoPreenchido;

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
	
	//===================== INICIO ===============================

	// Use this for initialization
	void Awake () {
		//Check if instance already exists
		if (referencia == null)	
			//if not, set instance to this
			referencia = this;		
		//If instance already exists and it's not this:
		else if (referencia != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);  
		
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
		//Call the InitGame function to initialize the first level 
		//IniciaJogo();
        jogoAtual = null;
		perfilAtual = null;
		listaJogosSalvos = new List<Jogo>(); //Lista para armazenar todos os jogos salvos de um dado perfil;
        
		//Inicializa o banco como null;
        banco = null;
    }

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
		
		try {
			Debug.Log(banco.CriarTabelaRetornaQuery(tableName, columnNames, columnValues));
		}
		catch(UnityException e){
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
		columnValues.Add("INTEGER DEFAULT 1 PRIMARY KEY AUTOINCREMENT NOT NULL");
		columnValues.Add("INTEGER NOT NULL");
		columnValues.Add("INTEGER NOT NULL");
		columnValues.Add("INTEGER NOT NULL, FOREIGN KEY(`idJogo`) REFERENCES Jogo");
		
		try {
			banco.CriarTabela(tableName, columnNames, columnValues);
		}
		catch(UnityException e){
			Debug.Log ("Nao foi possivel criar a tabela "+tableName+", devido a: "+e.ToString());
		}
	}

	//Temporario
	public void PassouFase()
	{
		myLog.text +="\n<b>VOCE PASSOU DE FASE!!!</b>";
		//Aqui que ia aparecer a janela de pontuaçao!
	}

	//=========================================
	//Funçoes do Banco
	public void CriarJogoNovo(Perfil perfilJogador)
	{
		Jogo novoJogo = new Jogo();
		novoJogo.dataJogoCriado = DateTime.Now.ToString("dd/MM/yyyy");
		novoJogo.dataJogoSalvo = DateTime.Now.ToString("dd/MM/yyyy");
		//novoJogo.idPerfilJogador = perfilJogador.idPerfil;
		novoJogo.pontuacaoTotal = 0;
		novoJogo.capituloAtual = 0; //Capitulo 0 e o Capitulo Tutorial, composto de 4 fases.
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
		teste = (ArrayList)ultimoPerfil[0]; //lista dos Objetos com somente um Objeto que contem os registros do nosso ultimo registro
		t = (int)teste[0];//(int)teste[0]; //o id e o primeiro registro de nosso Objeto, pois trata-se da primeira Coluna;
		novoJogo.idJogo = t;

		Debug.Log ("Perfil cadastrado: "+perfilJogador.idPerfil.ToString()+", "+perfilJogador.nomeAluno);
		Debug.Log ("\nJogo cadastrado: "+novoJogo.idJogo.ToString());

		referencia.perfilAtual = perfilJogador;
		referencia.jogoAtual = novoJogo;

		banco.FecharBanco();

	}


	//=========================================

//	void IniciaJogo ()
//	{
//		//?
//	}


}
