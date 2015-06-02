using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExecuteProgramList : MonoBehaviour {
	
	public float tempoMover = 0.6f; //Tempo que o personagem leva para ir de um Tile ao outro.
	public List<CommandButton> programList = new List<CommandButton>(); //Variavel para referenciar a Lista de Programa Gerada

	private GameObject myPlayer; //Variaveis para armazenar referencias ao Status do nosso Player
	private Player myPlayerStat;

	//Notas:
	//Awake acontece quando um GameObject e criado.
	//Start acontece quando um GameObject e iniciado.

	//Acontece depois do Awake, dando tempo de a referencia do Player ser atribuida!
	void Start()
	{
		myPlayer = ControladorGeral.referencia.myPlayer;
		myPlayerStat = myPlayer.GetComponent<Player> ();
	}
	
	//Funçao para executar a Lista de Programa! Essa funçao e chamada pelo botao "Executar Programa" na UI do jogo.
	public void ExecuteList(/*GameObject myPlayer, List<CommandButton> programList*/)
	{
		//Atualiza a Lista de Comandos gerada pelo Script CreateProgramList;
		programList = new List<CommandButton> ();
		programList = CreateProgramList.referencia.programList;
		//Inicia uma Corotina para executar os comandos na Lista!
		StartCoroutine (ExecutarLista (programList));	
		//Debug.Log (myPlayerStat.direcao); //teste
	}
	
	//-----------------------------------------------------------------------------------------------------------
	//==================================== EVENTOS DOS COMANDOS =================================================
	//-----------------------------------------------------------------------------------------------------------
	#region Eventos dos Comandos
	//O comando "Andar" chama a funçao MoverPersonagem(), que tem como objetivo verificar a direçao do personagem,
	//verificar se existe um tile a frente do personagem, se esse tile esta ocupado. 
	//Se estiver vazio, verifica se o tile eh andavel para ai sim mover o personagem uma casa a frente na direcao. 

	#region MoverPersonagem() - (Botao: Andar)
	public void MoverPersonagem()
	{
		//Variavel para guarda a nova posicao do jogador em um espaco 3D (X, Y, Z)
		Vector3 newPos;
		//Switch para agir conforme a direcao do Personagem
		switch(myPlayerStat.direcao)
		{
			case Player.Direction.Frente:
				//verifica se o proximo bloco a frente eh andavel
				if(VerificaBlocoQueVaiAndar(Player.Direction.Frente, myPlayerStat.posicaoTabuleiro))
				{
					newPos = new Vector3(myPlayer.transform.position.x,myPlayerStat.transform.position.y,myPlayer.transform.position.z-1);
					StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
				}
				break;
			case Player.Direction.Tras:
			//verifica se o proximo bloco atras eh andavel
			if(VerificaBlocoQueVaiAndar(Player.Direction.Tras, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x,myPlayerStat.transform.position.y,myPlayer.transform.position.z+1);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;
		case Player.Direction.Direita:
			//verifica se o proximo bloco a direita eh andavel
			if(VerificaBlocoQueVaiAndar(Player.Direction.Direita, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x-1,myPlayerStat.transform.position.y,myPlayer.transform.position.z);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;
		case Player.Direction.Esquerda:
			//verifica se o proximo bloco a direita eh andavel
			if(VerificaBlocoQueVaiAndar(Player.Direction.Esquerda, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x+1,myPlayerStat.transform.position.y,myPlayer.transform.position.z);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;

		}
	}

	//Metodo que faz parte do evento MoverPersonagem(), este faz a verificacao de existencia e de ocupacao do tile a frente do personagem.
	//Separei as funçoes so para fornecer um entendimento por partes
	bool VerificaBlocoQueVaiAndar (Player.Direction direcao, Vector2 coordenadas)
	{
		if(direcao == Player.Direction.Frente)
		{
			foreach(List<Tile> tileCol in ControladorGeral.referencia.tabuleiroAtual.mapaGerado)
			{
				foreach(Tile tile in tileCol)
				{
					if(tile.posicaoTabuleiro.y == myPlayerStat.posicaoTabuleiro.y+1 && tile.posicaoTabuleiro.x == myPlayerStat.posicaoTabuleiro.x)
					{
						if(tile.andavel && tile.objetoEmCima == null)
						{
							foreach(List<Tile> tileCol2 in ControladorGeral.referencia.tabuleiroAtual.mapaGerado)
							{
								foreach(Tile tile2 in tileCol2)
								{
									if(tile2.posicaoTabuleiro == myPlayerStat.posicaoTabuleiro)
										tile2.objetoEmCima = null;
								}
							}
							myPlayerStat.posicaoTabuleiro = tile.posicaoTabuleiro;
							tile.objetoEmCima = myPlayer;
							return true;
						}
					}
				}
			}
		}
		if(direcao == Player.Direction.Tras)
		{
			foreach(List<Tile> tileCol in ControladorGeral.referencia.tabuleiroAtual.mapaGerado)
			{
				foreach(Tile tile in tileCol)
				{
					if(tile.posicaoTabuleiro.y == myPlayerStat.posicaoTabuleiro.y-1 && tile.posicaoTabuleiro.x == myPlayerStat.posicaoTabuleiro.x)
					{
						if(tile.andavel && tile.objetoEmCima == null)
						{
							foreach(List<Tile> tileCol2 in ControladorGeral.referencia.tabuleiroAtual.mapaGerado)
							{
								foreach(Tile tile2 in tileCol2)
								{
									if(tile2.posicaoTabuleiro == myPlayerStat.posicaoTabuleiro)
										tile2.objetoEmCima = null;
								}
							}
							myPlayerStat.posicaoTabuleiro = tile.posicaoTabuleiro;
							tile.objetoEmCima = myPlayer;
							return true;
						}
					}
				}
			}
		}
		if(direcao == Player.Direction.Direita)
		{
			foreach(List<Tile> tileCol in ControladorGeral.referencia.tabuleiroAtual.mapaGerado)
			{
				foreach(Tile tile in tileCol)
				{
					if(tile.posicaoTabuleiro.y == myPlayerStat.posicaoTabuleiro.y && tile.posicaoTabuleiro.x+1 == myPlayerStat.posicaoTabuleiro.x)
					{
						if(tile.andavel && tile.objetoEmCima == null)
						{
							foreach(List<Tile> tileCol2 in ControladorGeral.referencia.tabuleiroAtual.mapaGerado)
							{
								foreach(Tile tile2 in tileCol2)
								{
									if(tile2.posicaoTabuleiro == myPlayerStat.posicaoTabuleiro)
										tile2.objetoEmCima = null;
								}
							}
							myPlayerStat.posicaoTabuleiro = tile.posicaoTabuleiro;
							tile.objetoEmCima = myPlayer;
							return true;
						}
					}
				}
			}
		}
		if(direcao == Player.Direction.Esquerda)
		{
			foreach(List<Tile> tileCol in ControladorGeral.referencia.tabuleiroAtual.mapaGerado)
			{
				foreach(Tile tile in tileCol)
				{
					if(tile.posicaoTabuleiro.y == myPlayerStat.posicaoTabuleiro.y && tile.posicaoTabuleiro.x-1 == myPlayerStat.posicaoTabuleiro.x)
					{
						if(tile.andavel && tile.objetoEmCima == null)
						{
							foreach(List<Tile> tileCol2 in ControladorGeral.referencia.tabuleiroAtual.mapaGerado)
							{
								foreach(Tile tile2 in tileCol2)
								{
									if(tile2.posicaoTabuleiro == myPlayerStat.posicaoTabuleiro)
										tile2.objetoEmCima = null;
								}
							}
							myPlayerStat.posicaoTabuleiro = tile.posicaoTabuleiro;
							tile.objetoEmCima = myPlayer;
							return true;
						}
					}
				}
			}
		}
		Debug.Log ("Nao andou!");
		return false;
	}
	#endregion

	//Gira o Personagem
	public void GirarPersonagem(Player.Direction direcaoGiro)
	{
		if (direcaoGiro == Player.Direction.Direita) 
		{
			switch(myPlayerStat.direcao)
			{
			case Player.Direction.Frente:
				myPlayerStat.direcao = Player.Direction.Direita;
				break;
			case Player.Direction.Direita:
				myPlayerStat.direcao = Player.Direction.Tras;
				break;
			case Player.Direction.Tras:
				myPlayerStat.direcao = Player.Direction.Esquerda;
				break;
			case Player.Direction.Esquerda:
				myPlayerStat.direcao = Player.Direction.Frente;
				break;
			}
		} 
		if (direcaoGiro == Player.Direction.Esquerda) 
		{
			switch(myPlayerStat.direcao)
			{
			case Player.Direction.Frente:
				myPlayerStat.direcao = Player.Direction.Esquerda;
				break;
			case Player.Direction.Esquerda:
				myPlayerStat.direcao = Player.Direction.Tras;
				break;
			case Player.Direction.Tras:
				myPlayerStat.direcao = Player.Direction.Direita;
				break;
			case Player.Direction.Direita:
				myPlayerStat.direcao = Player.Direction.Frente;
				break;
			}
		}
		//MudaSpriteDeAcordoComAPosicao
		Debug.Log ("Girou para "+myPlayerStat.direcao.ToString());
	}

	#endregion
	
	#region Corotinas

	//Coroutine para Executar os comandos na Lista fornecida, neste caso de inicio e a "Programa".
	//mas ao introduzir os comandos de funçao e loop ao jogador, esse codigo podera ser reaproveitado
	IEnumerator ExecutarLista(List<CommandButton> lista)
	{
		if (lista != null) {
			foreach (CommandButton comando in lista) {
				switch (comando.nameLabel.text) {
				case NomeBotoes.andar:
					//MoveGo(myPlayer, myPlayerStat);
					MoverPersonagem ();
					yield return new WaitForSeconds (0.8f);
					break;
				case NomeBotoes.falar:
					//ActTalk(string comando.parametro1.text, myPlayer, myPlayerStat);
					Debug.Log ("Falou " + comando.parametro1.text);
					//Debug.Log ("Falou");
					break;
				case NomeBotoes.interagir:
					//ActInteract(myPlayer, myPlayerStat);
					Debug.Log ("Interagiu");
					break;
				case NomeBotoes.girarDireita:
					GirarPersonagem (Player.Direction.Direita);
					break;
				case NomeBotoes.girarEsquerda:
					GirarPersonagem (Player.Direction.Esquerda);
					break;
				}
			}
		} else
			Debug.Log ("A Program Lista esta nula!");
		yield return null;
	}

	//Coroutine para dar a sensaçao de Movimento ao modificar a posiçao do Personagem, 
	//sem teletransporta-lo direto a posiçao e sem depender das funçoes Update/Fixed Update do MonoBehaviour para fazer isso.
	IEnumerator MoverObjeto(GameObject player, Vector3 source, Vector3 target, float overTime)
	{	
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			player.transform.position = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		player.transform.position = target;
		Debug.Log (player.name + " Andou!");

	}

	#endregion

	#region Anotaçoes

	//Chamando uma Coroutine
	//StartCoroutine(rotateObject (myCameraSuporte.transform.rotation, novaRotation, 1f));

	//Coroutine para esperar
//	IEnumerator Wait(){
//		yield return new WaitForSeconds (3.0f);
//	}
//
	#endregion

}
