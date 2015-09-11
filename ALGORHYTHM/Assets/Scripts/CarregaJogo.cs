using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarregaJogo : MonoBehaviour {

	
	public GameObject gameManager;          //GameManager prefab to instantiate.
	public GameObject myPlayer;
	public Vector2 posicaoPlayer;
	public Vector2 posicaoObjetivo;

	private CriaTabuleiro tabuleiroScript;
	private bool passouFase = false;
	private int score = 150;

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

//		foreach(List<Tile> tileCols in tabuleiroScript.mapaGerado)
//		{
//			foreach(Tile tile in tileCols)
//			{
//				if(tile.posicaoTabuleiro == posicaoPlayer)
//				{
//					tempPosInicial = tile.transform.position;
//					tempPosInicial.y = 1.3f;
//				}
//			}
//		}

		Tile oTile = tabuleiroScript.ProcuraTile(posicaoPlayer);
		if(oTile != null)
		{
			tempPosInicial = oTile.transform.position;
			tempPosInicial.y = 1.5f;
		}

		ControladorGeral.referencia.myPlayer = (GameObject)Instantiate(myPlayer, tempPosInicial/*new Vector3 (-5.5f, 1.3f, 4.5f)*/, Quaternion.identity);
		ControladorGeral.referencia.myPlayer.GetComponent<Player>().posicaoTabuleiro = posicaoPlayer;
		if(oTile != null)
			oTile.objetosEmCima.Add(ControladorGeral.referencia.myPlayer);
		ControladorGeral.referencia.tabuleiroAtual = tabuleiroScript;

	}

	//Verifica Objetivo
	void Update()
	{
		if(ControladorGeral.referencia.myPlayer.GetComponent<Player>().posicaoTabuleiro == posicaoObjetivo && !passouFase)
		{
			//score -= 15*CreateProgramList.referencia.programList.Count;
			score -= 15*CreateProgramList.referencia.listaPrograma.Count;


			Debug.Log ("VOCE PASSOU DE FASE!!!");
			Debug.Log ("SEU SCORE FOI: " + score.ToString());
			passouFase = true;
		}
	}

}
