using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
========================================================================================
	    ___    __            ____  __          __  __            
	   /   |  / /___ _____  / __ \/ /_  __  __/ /_/ /_  ____ ___ 
	  / /| | / / __ `/ __ \/ /_/ / __ \/ / / / __/ __ \/ __ `__ \
	 / ___ |/ / /_/ / /_/ / _, _/ / / / /_/ / /_/ / / / / / / / /
	/_/  |_/_/\__, /\____/_/ |_/_/ /_/\__, /\__/_/ /_/_/ /_/ /_/ 
	         /____/                  /____/                      

========================================================================================

20-05-2015 21:57

PROGRAMADORES:
ALAN PEREIRA
WILSON OLIVEIRA

CriaTabuleiro:
Script responsavel pela parte de criaçao do mapa atraves de uma Lista dinamica personalizada para cada Fase do Jogo.
Essa lista sera recebida pelo Script de Carrega Jogo de cada Scene.

*/


public class CriaTabuleiro : MonoBehaviour {

	[System.Serializable] //Faz a classe aparecer no Inspector do Unity Editor
	public class Colunas //Classe para armazenar uma lista de int, correspondente as Linhas de nosso mapa 2D
	{
		public int[] Linha;
	}
	//Cada Coluna armazena uma lista de linha, logo criando uma lista de colunas temos um ambiente 2D
	public Colunas[] terrenoMapa;
	//Vetor 2D para armazenar o tamanho do mapa em coordenadas X e Y.
	public Vector2 tamanhoMapa;
	//Valores para centralizar os tiles na tela;
	private float valorCorrecaoX = 0.5f;
	private float valorCorrecaoY = -1.5f;
	//Lista para armazenar todos os tiles que iremos utilizar na criaçao do Mapa
	public List<GameObject> tilesConjunto;
	//Lista para armazenar a informaçao de cada tile gerado, exemplo:
	//Suas coordenadas, se esta ocupado, se da para passar, se e interativo, etc
	public List<List<Tile>> mapaGerado;
	//Uma variavel para armazenar o nosso tabuleiro
	private Transform meuTabuleiro;                                  
	//O metodo Start eh chamado assim que o objeto a qual o Script esta vinculado eh inicializado.
	void Start () {	
		//Gera o Mapa
		//GeraMapa();
	}

	public void GeraMapa()
	{
		//cria o objeto para receber o Transform que servira de referencia para os Tiles e o nomeia Tabuleiro, para assim jogar os Tiles como "Child" dele.
		meuTabuleiro = new GameObject ("Tabuleiro").transform;
		//inicializa a lista
		mapaGerado = new List<List<Tile>>();
		//Para cada Coluna do Mapa
		for(int j = 0; j < terrenoMapa.Length; j++) 
		{
			//Cria uma lista para armazenar os Tiles que serao gerados correspondentes a Coluna que esta no "for".
			List<Tile> linhaTemp = new List<Tile>();
			//Para cada Item na Linha que esta no "for".
			for(int i = 0; i < terrenoMapa[j].Linha.Length; i++)
			{
				//Gera um novo Objeto baseado na informaçao lida do Mapa
				GameObject novoTile = (GameObject)Instantiate //Instantiate e o metodo que gera um novo objeto no jogo
				                       (
					//O primeiro parametro que ele pede eh qual objeto vai ser gerado
					tilesConjunto[terrenoMapa[j].Linha[i]], //Em nosso caso eh o tile na lista tilesConjunto que corresponde ao valor recebido no terrenoMapa
					//O segundo parametro eh a posicao no espaco 3D em que iremos criar esse Objeto, entao inserimos as coordenadas X,Y,Z
					new Vector3
					(
					i - Mathf.Floor(tamanhoMapa.x/2)+valorCorrecaoX , 
					tilesConjunto[terrenoMapa[j].Linha[i]].transform.position.y , 
					-j + Mathf.Floor(tamanhoMapa.y/2)+valorCorrecaoY  
					), 
					//O terceiro e ultimo parametro que ele pede eh sua rotacao, no caso o Quaternion.identity nos traz uma rotacao de zero graus em todos os eixos 3D (0º,0º,0º).
					Quaternion.identity
						);
				//Joga o novo tile gerado como filho do meuTabuleiro
				novoTile.transform.SetParent(meuTabuleiro);
				//Recebe a classe Tile do novo objeto criado
				Tile tile = novoTile.GetComponent<Tile>();
				//Tile criado, hora de definir suas coordenadas e outros atributos
				tile.posicaoTabuleiro = new Vector2(i,/*mapSizeY - */j);
				//Se o tile que geramos for diferente do Tile Zero
				if(terrenoMapa[j].Linha[i]!= 0) 
				{
					//Diz que o tile eh andavel
					tile.andavel = true;
				}				
				//Adiciona o tile gerado na linha temporaria
				linhaTemp.Add(tile);
			}
			//Adiciona a linha com todos os tiles gerados ao mapa Final
			mapaGerado.Add(linhaTemp);
		} //Mapa Gerado: OK!
	} //fim GeraMapa

} //fim CriaTabuleiro
