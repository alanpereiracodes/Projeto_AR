using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CarregaJogo : MonoBehaviour {

	public string tituloFase;
	public GameObject gameManager; //GameManager prefab to instantiate.
	public GameObject myPlayer;
	public Player.Direction direcaoInicial;
	public Vector2 posicaoPlayer;
	public Vector2 posicaoObjetivo;
	public int numeroComandos; //numero de comandos ideal encontrado para resolver a fase, Usado para ver a Pontuaçao
	public string textoObjetivo; //Texto do Objetivo Atual
	public Sprite imagemObjetivo; //Imagem do Objetivo
	public Sprite imagemObjetivo2; //Comando introduzido na fase.
	public bool imgObjetivoHablita;
	
	public int numeroFase;
	public int capituloFase;
	//public int tipoObjetivo;

	private CriaTabuleiro tabuleiroScript;
	//private bool passouFase = false;
	//private int score = 150;

	void Awake ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (ControladorGeral.referencia == null)			
			//Instantiate gameManager prefab
			Instantiate(gameManager);

		//Get a component reference to the attached BoardManager script
		tabuleiroScript = GetComponent<CriaTabuleiro>();
		tabuleiroScript.GeraMapa();
		tabuleiroScript.ColocaObjetos();

		Vector3 tempPosInicial = new Vector3 ();
		Tile oTile = tabuleiroScript.ProcuraTile(posicaoPlayer);
		if(oTile != null)
		{
			tempPosInicial = oTile.transform.position;
			tempPosInicial.y = 1.5f+oTile.altura*0.5f;
		}

		ControladorGeral.referencia.posicaoInicial = posicaoPlayer;
		ControladorGeral.referencia.posicaoObjetivo = posicaoObjetivo;
		ControladorGeral.referencia.myPlayer = (GameObject)Instantiate(myPlayer, tempPosInicial/*new Vector3 (-5.5f, 1.3f, 4.5f)*/, Quaternion.identity);
		ControladorGeral.referencia.myPlayer.GetComponent<Player>().posicaoTabuleiro = posicaoPlayer;
		ControladorGeral.referencia.myPlayer.GetComponent<Player>().direcaoGlobal = direcaoInicial;
		ControladorGeral.referencia.myPlayer.GetComponent<Player>().direcaoCamera = direcaoInicial;
		ControladorGeral.referencia.direcaoInicial = direcaoInicial;

		switch(direcaoInicial)
		{
		case Player.Direction.Frente:
			ControladorGeral.referencia.myPlayer.GetComponentInChildren<Animator>().SetInteger("direcao",1);
			break;
		case Player.Direction.Esquerda:
			ControladorGeral.referencia.myPlayer.GetComponentInChildren<Animator>().SetInteger("direcao",2);
			break;
		case Player.Direction.Tras:
			ControladorGeral.referencia.myPlayer.GetComponentInChildren<Animator>().SetInteger("direcao",3);
			break;
		case Player.Direction.Direita:
			ControladorGeral.referencia.myPlayer.GetComponentInChildren<Animator>().SetInteger("direcao",4);
			break;
		}

		if(oTile != null)
			oTile.objetosEmCima.Add(ControladorGeral.referencia.myPlayer);
		ControladorGeral.referencia.tabuleiroAtual = tabuleiroScript;
		//ControladorGeral.referencia.myLog = log;
		//ControladorGeral.referencia.myScroll = scroll;
		//ControladorGeral.referencia.faseAtual = numeroFase;
		ControladorGeral.referencia.listaEmExecucao = false;
	}

    void Start()
    {
		Debug.Log ("vai carregar "+tituloFase);
		CreateProgramList.referencia.RecarregaUI ();
        ControladorGeral.referencia.myTituloFase.text = tituloFase;
		ControladorGeral.referencia.numeroComandosIdeal = numeroComandos;
		ControladorGeral.referencia.faseAtual = numeroFase;
		ControladorGeral.referencia.capituloAtual = capituloFase;
		ControladorGeral.referencia.numeroComandos = 0;
		ControladorGeral.referencia.numeroRetries = 0;
		ControladorGeral.referencia.retry = false;
		ControladorGeral.referencia.listaEmExecucao = false;
		ControladorGeral.referencia.aSalvar = false;
		CreateProgramList.referencia.textoObjetivo.text = textoObjetivo;
		CreateProgramList.referencia.imagemFase.sprite = imagemObjetivo;
		CreateProgramList.referencia.imagemFase2.sprite = imagemObjetivo2;
		CreateProgramList.referencia.imagemFase2.enabled = imgObjetivoHablita;
		Debug.Log ("carregou");

    }


}









