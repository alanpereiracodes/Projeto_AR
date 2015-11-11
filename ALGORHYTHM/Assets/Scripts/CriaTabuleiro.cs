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

	//Vetor 2D para armazenar o tamanho do mapa em coordenadas X e Y.
	public Vector2 tamanhoMapa;
	//Cada Coluna armazena uma lista de linha, logo criando uma lista de colunas temos um ambiente 2D
	public Colunas[] terrenoMapa;
	//Deposita os objetos de acordo com os numeros nas linhas de cada coluna.
	public Colunas[] objetosMapa;
	//Valores para centralizar os tiles na tela;
	private float valorCorrecaoX = 0.5f;
	private float valorCorrecaoY = -1.5f;
	//Lista para armazenar todos os tiles que iremos utilizar na criaçao do Mapa
	public List<GameObject> tilesConjunto;
	//Lista para armazenar os Objetos que serao colocados na fase
	public List<GameObject> objetosConjunto;
	//Lista para armazenar a informaçao de cada tile gerado, exemplo:
	//Suas coordenadas, se esta ocupado, se da para passar, etc
	public List<List<Tile>> mapaGerado;
	//Lista para armazenar a informaçao de cada objeto gerado, exemplo:
	//Suas coordenadas, seu tipo, se e interativo, etc
	public List<List<Objeto>> mapaObjetosGerado;

	//Uma variavel para armazenar o nosso tabuleiro
	private Transform meuTabuleiro;                      

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
//				if(terrenoMapa[j].Linha[i]!= 0) 
//				{
//					//Diz que o tile eh andavel
//					tile.andavel = true;
//				}				
				//Adiciona o tile gerado na linha temporaria
				linhaTemp.Add(tile);
			}
			//Adiciona a linha com todos os tiles gerados ao mapa Final
			mapaGerado.Add(linhaTemp);
		} //Mapa Gerado: OK!
	} //fim GeraMapa

	public void ColocaObjetos()
	{
		//cria o objeto para receber o Transform que servira de referencia para os Tiles e o nomeia Tabuleiro, para assim jogar os Tiles como "Child" dele.
		meuTabuleiro = new GameObject ("Objetos").transform;
		//inicializa a lista
		mapaObjetosGerado = new List<List<Objeto>>();
		//Para cada Coluna do Mapa
		for(int j = 0; j < objetosMapa.Length; j++) 
		{
			//Cria uma lista para armazenar os Objetos que serao gerados correspondentes a Coluna que esta no "for".
			List<Objeto> linhaTemp = new List<Objeto>();
			//Para cada Item na Linha que esta no "for".
			for(int i = 0; i < objetosMapa[j].Linha.Length; i++)
			{
				if(objetosMapa[j].Linha[i] != 0)
				{
					//Gera um novo Objeto baseado na informaçao lida do Mapa
					GameObject novoObj = (GameObject)Instantiate //Instantiate e o metodo que gera um novo objeto no jogo
						(
							//O primeiro parametro que ele pede eh qual objeto vai ser gerado
							objetosConjunto[objetosMapa[j].Linha[i]], //Em nosso caso eh o tile na lista ObjetoConjunto que corresponde ao valor recebido no terrenoMapa
							//O segundo parametro eh a posicao no espaco 3D em que iremos criar esse Objeto, entao inserimos as coordenadas X,Y,Z
							new Vector3
							(
							i - Mathf.Floor(tamanhoMapa.x/2)+valorCorrecaoX , 
							//objetosConjunto[terrenoMapa[j].Linha[i]].transform.position.y , 
							objetosConjunto[objetosMapa[j].Linha[i]].transform.position.y,
							-j + Mathf.Floor(tamanhoMapa.y/2)+valorCorrecaoY  
							), 
							//O terceiro e ultimo parametro que ele pede eh sua rotacao, no caso o Quaternion.identity nos traz uma rotacao de zero graus em todos os eixos 3D (0º,0º,0º).
							Quaternion.identity
							);
					//Joga o novo tile gerado como filho do meuTabuleiro
					novoObj.transform.SetParent(meuTabuleiro);
					//Recebe a classe Tile do novo objeto criado
					Objeto obj = novoObj.GetComponent<Objeto>();
					//Tile criado, hora de definir suas coordenadas e outros atributos
					obj.posicaoTabuleiro = new Vector2(i,/*mapSizeY - */j);
					//Se o tile que geramos for diferente do Tile Zero
					//				if(terrenoMapa[j].Linha[i]!= 0) 
					//				{
					//					//Diz que o tile eh andavel
					//					tile.andavel = true;
					//				}				
					//Adiciona o tile gerado na linha temporaria
					linhaTemp.Add(obj);
					Tile meuTempTile = ProcuraTile(obj.posicaoTabuleiro);
					if(meuTempTile != null)
					{
						meuTempTile.objetosEmCima.Add(novoObj);
						novoObj.transform.position = new Vector3(novoObj.transform.position.x, /*meuTempTile.gameObject.transform.position.y*/meuTempTile.altura*0.5f + novoObj.transform.position.y, novoObj.transform.position.z);
						if(obj.nome == Objeto.Nome.BotaoChao)
						{
							meuTempTile.altura++;
						}
					}
				}

			}
			//Adiciona a linha com todos os objetos gerados ao mapa Final
			mapaObjetosGerado.Add(linhaTemp);
		}
	} //fim ColocaObjetos

	public void RecolocaObjetos()
	{
		//Limpa a lista de "Objetos em Cima" de cada Tile. Isso inclui o Jogador, entao o Jogador tem de ser realocado posteiormente a esse codigo.
		foreach(List<Tile> listTile in mapaGerado)
		{
			foreach(Tile tile in listTile)
			{
				if(tile.objetosEmCima.Count>0)
				{
					foreach(GameObject gObj in tile.objetosEmCima)
					{
						if(gObj.tag == "Objeto")
						{
							Objeto aObj = gObj.GetComponent<Objeto>();
							if(aObj.nome == Objeto.Nome.BotaoChao && !aObj.ativado)
							{
								tile.altura--;
							}
						}
					}
				}
				tile.objetosEmCima.Clear ();
			}
		}
		Destroy(GameObject.Find("Objetos")); //Destroi todos os objetos.

		ColocaObjetos();

	}//Fim RecolocaObjetos

	public void DesativaBarreira()
	{
		foreach(List<Tile> listTile in mapaGerado)
		{
			foreach(Tile tile in listTile)
			{
				if(tile.objetosEmCima.Count > 0)
				{
					foreach(GameObject gObj in tile.objetosEmCima)
					{
						if(gObj.GetComponent<Objeto>() != null)
						{
							if(gObj.GetComponent<Objeto>().nome == Objeto.Nome.Barreira)
							{
								gObj.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Objeto";
								gObj.GetComponentInChildren<Animator>().SetBool("ativado",false);
								gObj.GetComponent<Objeto>().bloqueiaCaminho = false;
							}
						}
					}
				}
			}
		}
	}

	public bool QuebraCorrente()
	{
		foreach(List<Tile> listTile in mapaGerado)
		{
			foreach(Tile tile in listTile)
			{
				if(tile.objetosEmCima.Count > 0)
				{
					foreach(GameObject gObj in tile.objetosEmCima)
					{
						if(gObj.GetComponent<Objeto>() != null)
						{
							if(gObj.GetComponent<Objeto>().nome == Objeto.Nome.Cristal)
							{
								if(!gObj.GetComponent<Objeto>().ativado) //Se um Cristal nao foi ativado, ele nao quebra a corrente
									return false;
							}
						}
					}
				}
			}
		}
		return true;
	}

	public bool QuebraCorrenteBotao()
	{
		foreach(List<Tile> listTile in mapaGerado)
		{
			foreach(Tile tile in listTile)
			{
				if(tile.objetosEmCima.Count > 0)
				{
					foreach(GameObject gObj in tile.objetosEmCima)
					{
						if(gObj.GetComponent<Objeto>() != null)
						{
							if(gObj.GetComponent<Objeto>().nome == Objeto.Nome.BotaoChao)
							{
								if(!gObj.GetComponent<Objeto>().ativado) //Se um Cristal nao foi ativado, ele nao quebra a corrente
									return false;
							}
						}
					}
				}
			}
		}
		return true;
	}

	//Procura um Tile com a Posiçao fornecida e retorna o Tile da seguinte posiçao se existir!
	public Tile ProcuraTile(Vector2 pos)
	{
				foreach(List<Tile> tileCol in mapaGerado)
				{
					foreach(Tile tile in tileCol)
					{
							if(tile.posicaoTabuleiro == pos)
								return tile;
					}
				}
		return null;
	}

	public Objeto AtivaBotaoChao(Vector2 pos)
	{
		foreach(List<Tile> tileCol in mapaGerado)
		{
			foreach(Tile tile in tileCol)
			{
				if(tile.posicaoTabuleiro == pos)
				{
					if(tile.objetosEmCima.Count>0)
					{
						foreach(GameObject gObj in tile.objetosEmCima)
						{
							if(gObj.tag == "Objeto")
							{
								Objeto aObj = gObj.GetComponent<Objeto>();
								if(aObj.nome == Objeto.Nome.BotaoChao && !aObj.ativado)
								{
									tile.altura--; //JaAtivaDeUmaVez uaheuhae
									aObj.ativado = true;
									return aObj;
								}
							}
						}
					}
				}
			}
		}
		return null;
	}

} //fim CriaTabuleiro
