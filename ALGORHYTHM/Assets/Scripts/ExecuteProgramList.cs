using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExecuteProgramList : MonoBehaviour {
	
	public float tempoMover = 0.6f; //Tempo que o personagem leva para ir de um Tile ao outro.
	//public List<CommandButton> programList = new List<CommandButton>(); //Variavel para referenciar a Lista de Programa Gerada
	public List<Comando> listaPrograma = new List<Comando>();
	public GameObject playerPrefab;

	private GameObject myPlayer; //Variaveis para armazenar referencias ao Status do nosso Player
	private Player myPlayerStat;
	private Animator myPlayAnim;
	private float alturaPulo;

	ControladorGeral cGeral;

	//Notas:
	//Awake acontece quando um GameObject e criado.
	//Start acontece quando um GameObject e iniciado.

	//Acontece depois do Awake, dando tempo de a referencia do Player ser atribuida!
	void Start()
	{
		cGeral = ControladorGeral.referencia;
		myPlayer = cGeral.myPlayer;
		myPlayerStat = myPlayer.GetComponent<Player> ();
		myPlayAnim = myPlayer.GetComponentInChildren<Animator>();

	}
	
	//Funçao para executar a Lista de Programa! Essa funçao e chamada pelo botao "Executar Programa" na UI do jogo.
	public void ExecuteList(/*GameObject myPlayer, List<CommandButton> programList*/)
	{
		//Atualiza a Lista de Comandos gerada pelo Script CreateProgramList;
		//programList = new List<CommandButton> ();
		//programList = CreateProgramList.referencia.programList;

		listaPrograma = new List<Comando>();
		listaPrograma = CreateProgramList.referencia.listaPrograma;

		//Inicia uma Corotina para executar os comandos na Lista!
		//StartCoroutine (ExecutarLista (programList));	
		StartCoroutine (ExecutarLista (listaPrograma));	
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
		switch(myPlayerStat.direcaoGlobal)
		{
			case Player.Direction.Frente:
				//verifica se o proximo bloco a frente eh andavel
				if(VerificaBlocoQueVaiAndar(Player.Direction.Frente, myPlayerStat.posicaoTabuleiro))
				{
					newPos = new Vector3(myPlayer.transform.position.x,myPlayerStat.transform.position.y,myPlayer.transform.position.z-1);
					myPlayAnim.SetBool("andando",true);
					StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
				}
				break;
			case Player.Direction.Tras:
			//verifica se o proximo bloco atras eh andavel
			if(VerificaBlocoQueVaiAndar(Player.Direction.Tras, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x,myPlayerStat.transform.position.y,myPlayer.transform.position.z+1);
				myPlayAnim.SetBool("andando",true);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;
		case Player.Direction.Direita:
			//verifica se o proximo bloco a direita eh andavel
			if(VerificaBlocoQueVaiAndar(Player.Direction.Direita, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x-1,myPlayerStat.transform.position.y,myPlayer.transform.position.z);
				myPlayAnim.SetBool("andando",true);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;
		case Player.Direction.Esquerda:
			//verifica se o proximo bloco a direita eh andavel
			if(VerificaBlocoQueVaiAndar(Player.Direction.Esquerda, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x+1,myPlayerStat.transform.position.y,myPlayer.transform.position.z);
				myPlayAnim.SetBool("andando",true);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;

		}
	}

	//Metodo que faz parte do evento MoverPersonagem(), este faz a verificacao de existencia e de ocupacao do tile a frente do personagem.
	//Separei as funçoes so para fornecer um entendimento por partes
	bool VerificaBlocoQueVaiAndar (Player.Direction direcao, Vector2 coordenadas)
	{
		//Anotaçao das Direçoes:
		/*
		 * Frente	Y+1
		 * Tras		Y-1
		 * Esquerda	X+1
		 * Direita	X-1
		 */

		bool passavel = true;

		if(direcao == Player.Direction.Frente)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x, myPlayerStat.posicaoTabuleiro.y+1));
			if(novoTile != null)
			{
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						if(obj.bloqueiaCaminho)
							passavel = false;
						break;
					}
				}
				if(novoTile.andavel && passavel)
				{
					Tile velhoTile = cGeral.tabuleiroAtual.ProcuraTile(myPlayerStat.posicaoTabuleiro);
					if(velhoTile != null && velhoTile.altura == novoTile.altura)
					{
						velhoTile.objetosEmCima.Remove(myPlayer);
						myPlayerStat.posicaoTabuleiro = novoTile.posicaoTabuleiro;
						novoTile.objetosEmCima.Add(myPlayer);
						return true;
					}
				}
			}
		}
		if(direcao == Player.Direction.Tras)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x, myPlayerStat.posicaoTabuleiro.y-1));
			if(novoTile != null)
			{
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						if(obj.bloqueiaCaminho)
							passavel = false;
						break;
					}
				}
				if(novoTile.andavel && passavel)
				{
					Tile velhoTile = cGeral.tabuleiroAtual.ProcuraTile(myPlayerStat.posicaoTabuleiro);
					if(velhoTile != null && velhoTile.altura == novoTile.altura)
					{
						velhoTile.objetosEmCima.Remove(myPlayer);
						myPlayerStat.posicaoTabuleiro = novoTile.posicaoTabuleiro;
						novoTile.objetosEmCima.Add(myPlayer);
						return true;
					}
				}
			}
		}
		if(direcao == Player.Direction.Direita)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x-1, myPlayerStat.posicaoTabuleiro.y));
			if(novoTile != null)
			{
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}

				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						if(obj.bloqueiaCaminho)
							passavel = false;
						break;
					}
				}

				if(novoTile.andavel && passavel)
				{
					Tile velhoTile = cGeral.tabuleiroAtual.ProcuraTile(myPlayerStat.posicaoTabuleiro);
					if(velhoTile != null && velhoTile.altura == novoTile.altura)
					{
						velhoTile.objetosEmCima.Remove(myPlayer);
						myPlayerStat.posicaoTabuleiro = novoTile.posicaoTabuleiro;
						novoTile.objetosEmCima.Add(myPlayer);
						return true;
					}
				}
			}
		}
		if(direcao == Player.Direction.Esquerda)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x+1, myPlayerStat.posicaoTabuleiro.y));
			if(novoTile != null)
			{
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						if(obj.bloqueiaCaminho)
							passavel = false;
						break;
					}
				}
				if(novoTile.andavel && passavel)
				{
					Tile velhoTile = cGeral.tabuleiroAtual.ProcuraTile(myPlayerStat.posicaoTabuleiro);
					if(velhoTile != null && velhoTile.altura == novoTile.altura)
					{
						velhoTile.objetosEmCima.Remove(myPlayer);
						myPlayerStat.posicaoTabuleiro = novoTile.posicaoTabuleiro;
						novoTile.objetosEmCima.Add(myPlayer);
						return true;
					}
				}
			}
		}
		EnviaMensagem("\n<b>Algo</b> não andou!");
		EnviaCodigo("\nErro: if(casa.posicao(x,y).naoAndavel || casa.posicao(x,y) == null){ retorno false;}");
		if (cGeral.myScroll != null)
		{
			//Debug.Log (cGeral.myScroll.value.ToString ());
			cGeral.myScroll.value = 0;
		}
		Debug.Log ("Não andou!");
		return false;
	}
	#endregion

	//Gira o Personagem
	public void GirarPersonagem(Player.Direction direcaoGiro)
	{
		if (direcaoGiro == Player.Direction.Direita) 
		{
			switch(myPlayerStat.direcaoGlobal)
			{
			case Player.Direction.Frente:
				myPlayerStat.direcaoGlobal = Player.Direction.Direita;
				break;
			case Player.Direction.Direita:
				myPlayerStat.direcaoGlobal = Player.Direction.Tras;
				break;
			case Player.Direction.Tras:
				myPlayerStat.direcaoGlobal = Player.Direction.Esquerda;
				break;
			case Player.Direction.Esquerda:
				myPlayerStat.direcaoGlobal = Player.Direction.Frente;
				break;
			}
			switch(myPlayerStat.direcaoCamera)
			{
			case Player.Direction.Frente:
				myPlayerStat.direcaoCamera = Player.Direction.Direita;
				break;
			case Player.Direction.Direita:
				myPlayerStat.direcaoCamera = Player.Direction.Tras;
				break;
			case Player.Direction.Tras:
				myPlayerStat.direcaoCamera = Player.Direction.Esquerda;
				break;
			case Player.Direction.Esquerda:
				myPlayerStat.direcaoCamera = Player.Direction.Frente;
				break;
			}
		} 
		if (direcaoGiro == Player.Direction.Esquerda) 
		{
			switch(myPlayerStat.direcaoGlobal)
			{
			case Player.Direction.Frente:
				myPlayerStat.direcaoGlobal = Player.Direction.Esquerda;
				break;
			case Player.Direction.Esquerda:
				myPlayerStat.direcaoGlobal = Player.Direction.Tras;
				break;
			case Player.Direction.Tras:
				myPlayerStat.direcaoGlobal = Player.Direction.Direita;
				break;
			case Player.Direction.Direita:
				myPlayerStat.direcaoGlobal = Player.Direction.Frente;
				break;
			}
			switch(myPlayerStat.direcaoCamera)
			{
			case Player.Direction.Frente:
				myPlayerStat.direcaoCamera = Player.Direction.Esquerda;
				break;
			case Player.Direction.Esquerda:
				myPlayerStat.direcaoCamera = Player.Direction.Tras;
				break;
			case Player.Direction.Tras:
				myPlayerStat.direcaoCamera = Player.Direction.Direita;
				break;
			case Player.Direction.Direita:
				myPlayerStat.direcaoCamera = Player.Direction.Frente;
				break;
			}
		}
		//MudaSpriteDeAcordoComAPosicao
		EnviaMensagem("\n<b>Algo</b> girou para " + myPlayerStat.direcaoGlobal.ToString ());
		EnviaCodigo("\n<b>Algo</b>.direcao = direcao." + myPlayerStat.direcaoGlobal.ToString ()+";");
		Debug.Log ("Girou para "+myPlayerStat.direcaoGlobal.ToString());
		//Muda Animaçao
			//switch(myPlayerStat.direcaoGlobal)
			switch(myPlayerStat.direcaoCamera)
			{
			case Player.Direction.Frente:
				myPlayAnim.SetInteger("direcao",1);
				break;
			case Player.Direction.Esquerda:
				myPlayAnim.SetInteger("direcao",2);
				break;
			case Player.Direction.Tras:
				myPlayAnim.SetInteger("direcao",3);
				break;
			case Player.Direction.Direita:
				myPlayAnim.SetInteger("direcao",4);
				break;
			}
	}

	bool VerificaBlocoQueVaiPular (Player.Direction direcao, Vector2 coordenadas)
	{
		//Anotaçao das Direçoes:
		/*
		 * Frente	Y+1
		 * Tras		Y-1
		 * Esquerda	X+1
		 * Direita	X-1
		 */

		bool passavel = true;

		if(direcao == Player.Direction.Frente)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x, myPlayerStat.posicaoTabuleiro.y+1));
			if(novoTile != null)
			{

				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						if(obj.bloqueiaCaminho)
							passavel = false;
						break;
					}
				}

				if(novoTile.andavel && passavel)
				{
					Tile velhoTile = cGeral.tabuleiroAtual.ProcuraTile(myPlayerStat.posicaoTabuleiro);
					if(velhoTile != null && (velhoTile.altura == novoTile.altura+1 || velhoTile.altura == novoTile.altura-1))
					{
						if(velhoTile.altura < novoTile.altura)
							alturaPulo = 0.5f;
						else
							alturaPulo = -0.5f;
						velhoTile.objetosEmCima.Remove(myPlayer);
						myPlayerStat.posicaoTabuleiro = novoTile.posicaoTabuleiro;
						novoTile.objetosEmCima.Add(myPlayer);
						return true;
					}
				}
			}
		}
		if(direcao == Player.Direction.Tras)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x, myPlayerStat.posicaoTabuleiro.y-1));
			if(novoTile != null)
			{
				
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						if(obj.bloqueiaCaminho)
							passavel = false;
						break;
					}
				}
				
				if(novoTile.andavel && passavel)
				{
					Tile velhoTile = cGeral.tabuleiroAtual.ProcuraTile(myPlayerStat.posicaoTabuleiro);
					if(velhoTile != null && (velhoTile.altura == novoTile.altura+1 || velhoTile.altura == novoTile.altura-1))
					{
						if(velhoTile.altura < novoTile.altura)
							alturaPulo = 0.5f;
						else
							alturaPulo = -0.5f;
						velhoTile.objetosEmCima.Remove(myPlayer);
						myPlayerStat.posicaoTabuleiro = novoTile.posicaoTabuleiro;
						novoTile.objetosEmCima.Add(myPlayer);
						return true;
					}
				}
			}
		}
		if(direcao == Player.Direction.Direita)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x-1, myPlayerStat.posicaoTabuleiro.y));
			if(novoTile != null)
			{
				
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						if(obj.bloqueiaCaminho)
							passavel = false;
						break;
					}
				}
				
				if(novoTile.andavel && passavel)
				{
					Tile velhoTile = cGeral.tabuleiroAtual.ProcuraTile(myPlayerStat.posicaoTabuleiro);
					if(velhoTile != null && (velhoTile.altura == novoTile.altura+1 || velhoTile.altura == novoTile.altura-1))
					{
						if(velhoTile.altura < novoTile.altura)
							alturaPulo = 0.5f;
						else
							alturaPulo = -0.5f;
						velhoTile.objetosEmCima.Remove(myPlayer);
						myPlayerStat.posicaoTabuleiro = novoTile.posicaoTabuleiro;
						novoTile.objetosEmCima.Add(myPlayer);
						return true;
					}
				}
			}
		}
		if(direcao == Player.Direction.Esquerda)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x+1, myPlayerStat.posicaoTabuleiro.y));
			if(novoTile != null)
			{
				
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						if(obj.bloqueiaCaminho)
							passavel = false;
						break;
					}
				}
				
				if(novoTile.andavel && passavel)
				{
					Tile velhoTile = cGeral.tabuleiroAtual.ProcuraTile(myPlayerStat.posicaoTabuleiro);
					if(velhoTile != null && (velhoTile.altura == novoTile.altura+1 || velhoTile.altura == novoTile.altura-1))
					{
						if(velhoTile.altura < novoTile.altura)
							alturaPulo = 0.5f;
						else
							alturaPulo = -0.5f;
						velhoTile.objetosEmCima.Remove(myPlayer);
						myPlayerStat.posicaoTabuleiro = novoTile.posicaoTabuleiro;
						novoTile.objetosEmCima.Add(myPlayer);
						return true;
					}
				}
			}
		}
		EnviaMensagem("\n<b>Algo</b> não Pulou!");
		EnviaCodigo("\nErro: posicao(x,y).naoAndavel = true;");
		Debug.Log ("não Pulou!");
		return false;
	}

	public void PularPersonagem()
	{
		//verifica altura do tile que o jogar esta
		//verifica verifica a direçao do jogador
		//Variavel para guarda a nova posicao do jogador em um espaco 3D (X, Y, Z)
		// verifica altura do tile na frente do jogador
		//verifica se o tile e andavel ou se esta ocupado
		//move jogador e executa animaçao de pula
		//diz que o jogador esta ocupando o novo tile em que ele se encontra

		Vector3 newPos;
		//Switch para agir conforme a direcao do Personagem

		switch(myPlayerStat.direcaoGlobal)
		{
		case Player.Direction.Frente:
			//verifica se o proximo bloco a frente eh andavel
			if(VerificaBlocoQueVaiPular(Player.Direction.Frente, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x,myPlayerStat.transform.position.y+alturaPulo,myPlayer.transform.position.z-1);
				myPlayAnim.SetBool("pulando",true);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;
		case Player.Direction.Tras:
			//verifica se o proximo bloco atras eh andavel
			if(VerificaBlocoQueVaiPular(Player.Direction.Tras, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x,myPlayerStat.transform.position.y+alturaPulo,myPlayer.transform.position.z+1);
				myPlayAnim.SetBool("pulando",true);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;
		case Player.Direction.Direita:
			//verifica se o proximo bloco a direita eh andavel
			if(VerificaBlocoQueVaiPular(Player.Direction.Direita, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x-1,myPlayerStat.transform.position.y+alturaPulo,myPlayer.transform.position.z);
				myPlayAnim.SetBool("pulando",true);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;
		case Player.Direction.Esquerda:
			//verifica se o proximo bloco a direita eh andavel
			if(VerificaBlocoQueVaiPular(Player.Direction.Esquerda, myPlayerStat.posicaoTabuleiro))
			{
				newPos = new Vector3(myPlayer.transform.position.x+1,myPlayerStat.transform.position.y+alturaPulo,myPlayer.transform.position.z);
				myPlayAnim.SetBool("pulando",true);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
			}
			break;
			
		}

	}

	void Interagir ()
	{
		Objeto objetoFrente = null;
		objetoFrente = VerificaBlocoQueVaiInteragir(myPlayerStat.direcaoGlobal, myPlayerStat.posicaoTabuleiro);

		if (objetoFrente != null) 
		{
			switch (objetoFrente.tipo) 
			{
			case Objeto.Tipo.ItemPegavel:
				myPlayerStat.objetoEmMaos = objetoFrente.gameObject;
				EnviaMensagem("\n<b>Algo</b>, pegou um " + objetoFrente.nome.ToString());
				EnviaCodigo("\n<b>Algo</b>.objetoEmMaos = " + objetoFrente.nome.ToString()+";");
				Debug.Log ("Algo, interagiu com um " + objetoFrente.nome.ToString());
				Tile tempTile = cGeral.tabuleiroAtual.ProcuraTile(objetoFrente.posicaoTabuleiro);
				tempTile.objetosEmCima.Remove(objetoFrente.gameObject);
				objetoFrente.gameObject.SetActive (false);
				//Destroy (objetoFrente.gameObject);
				break;
			case Objeto.Tipo.Altar:
				//executafunçaoaltar
				if(myPlayerStat.objetoEmMaos != null)
				{
					EnviaMensagem("\n<b>Algo</b>, interagiu com um " + objetoFrente.nome.ToString() + " possuindo um " + myPlayerStat.objetoEmMaos.GetComponent<Objeto>().nome.ToString());
					EnviaCodigo("\n<b>Algo</b>.colocarNoAltar(" + objetoFrente.nome.ToString() + "," + myPlayerStat.objetoEmMaos.GetComponent<Objeto>().nome.ToString()+";");
					Debug.Log ("Algo, interagiu com um " + objetoFrente.nome.ToString() + " possuindo um " + myPlayerStat.objetoEmMaos.GetComponent<Objeto>().nome.ToString());
					if(myPlayerStat.objetoEmMaos.GetComponent<Objeto>().tipo == Objeto.Tipo.ItemPegavel)
					{
						objetoFrente.ativado = true;
						EnviaMensagem("\nO " + objetoFrente.nome.ToString() + " foi ativado!");
						EnviaCodigo("\n" + objetoFrente.nome.ToString() + ".ativo = true;");
						Debug.Log ("O " + objetoFrente.nome.ToString() + " foi ativado!");
						myPlayerStat.objetoEmMaos.transform.SetParent(objetoFrente.gameObject.transform);
						myPlayerStat.objetoEmMaos.transform.localPosition = new Vector3(0,0,0);

						myPlayerStat.objetoEmMaos.SetActive(true);
						myPlayerStat.objetoEmMaos = null;
						cGeral.tabuleiroAtual.DesativaBarreira();
						//cGeral.tabuleiroAtual.

					}
				}
				else
				{
					EnviaMensagem("\n<b>Algo</b>, interagiu com um " + objetoFrente.nome.ToString());
					EnviaCodigo("\n<b>Algo</b>.colocarNoAltar(" + objetoFrente.nome.ToString() + ",null);");
					Debug.Log ("Algo, interagiu com um " + objetoFrente.nome.ToString());
				}
				break;
			}
		} 
		else 
		{
			EnviaMensagem("\n<b>Algo</b> não encontrou um objeto para interagir");
			EnviaCodigo("\n<b>Algo</b>.posicaoAFrente.objeto = null;");
			Debug.Log ("Algo não encontrou um objeto para interagir.");
		}
	}

	
	#region Verifica Bloco que vai Interagir

	Objeto VerificaBlocoQueVaiInteragir (Player.Direction direcao, Vector2 coordenadas)
	{
		//Anotaçao das Direçoes:
		/*
		 * Frente	Y+1
		 * Tras		Y-1
		 * Esquerda	X+1
		 * Direita	X-1
		 */

		if(direcao == Player.Direction.Frente)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x, myPlayerStat.posicaoTabuleiro.y+1));
			if(novoTile != null)
			{
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						return obj; //retorna o primeiro
					}
				}
			}
		}
		if(direcao == Player.Direction.Tras)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x, myPlayerStat.posicaoTabuleiro.y-1));
			if(novoTile != null)
			{
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						return obj; //retorna o primeiro
					}
				}
			}
		}
		if(direcao == Player.Direction.Direita)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x-1, myPlayerStat.posicaoTabuleiro.y));
			if(novoTile != null)
			{
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						return obj; //retorna o primeiro
					}
				}
			}
		}
		if(direcao == Player.Direction.Esquerda)
		{
			Tile novoTile = cGeral.tabuleiroAtual.ProcuraTile(new Vector2(myPlayerStat.posicaoTabuleiro.x+1, myPlayerStat.posicaoTabuleiro.y));
			if(novoTile != null)
			{
				List<Objeto> novosObjetos = new List<Objeto>();
				if(novoTile.objetosEmCima.Count > 0)
				{
					foreach(GameObject objEmCima in novoTile.objetosEmCima)
					{
						if(objEmCima.GetComponent<Objeto>() != null)
							novosObjetos.Add(objEmCima.GetComponent<Objeto>());
					}
				}
				
				if(novosObjetos.Count > 0)
				{
					foreach(Objeto obj in novosObjetos)
					{
						return obj; //retorna o primeiro
					}
				}
			}
		}
		//EnviaMensagem("\n<b>Algo</b> não existe um obj!");
		//Debug.Log ("não andou!");
		return null;
	}

	#endregion

	#endregion


	public void EnviaMensagem(string mensagem)
	{
		cGeral.myLog.text += mensagem;
		if (cGeral.myScroll != null)
		{
			//Debug.Log (cGeral.myScroll.value.ToString ());
			cGeral.myScroll.value = 0;
		}
	}

	public void EnviaCodigo(string mensagem)
	{
		cGeral.myLogAvanc.text += mensagem;
		if (cGeral.myScrollAvanc != null)
		{
			//Debug.Log (cGeral.myScroll.value.ToString ());
			cGeral.myScrollAvanc.value = 0;
		}
	}

	#region Corotinas

	//Coroutine para Executar os comandos na Lista fornecida, neste caso de inicio e a "Programa".
	//mas ao introduzir os comandos de funçao e loop ao jogador, esse codigo podera ser reaproveitado
	//IEnumerator ExecutarLista(List<CommandButton> lista)
	IEnumerator ExecutarLista(List<Comando> lista)
	{

		cGeral.numeroComandos = lista.Count;
		Debug.Log ("numero comandos: "+cGeral.numeroComandos);
		Debug.Log ("numero retries: "+cGeral.numeroRetries);
		if (CreateProgramList.referencia.listaPrograma != null && CreateProgramList.referencia.listaPrograma.Count > 0) 
		{
			if (!cGeral.retry) 
			{
				if (!cGeral.listaEmExecucao) 
				{
					cGeral.listaEmExecucao = true;
						//foreach (CommandButton comando in lista) {
						foreach (Comando comando in lista)
						{
							switch (comando.nome) 
							{
							case Comando.botaoNome.Andar: //NomeBotoes.andar:
							//MoveGo(myPlayer, myPlayerStat);
								MoverPersonagem ();
								yield return new WaitForSeconds (0.8f);
								break;
							case Comando.botaoNome.Falar://NomeBotoes.falar:
							//ActTalk(string comando.parametro1.text, myPlayer, myPlayerStat);
							//Debug.Log ("Falou " + comando.parametro1.text);
							//Debug.Log ("Falou");
								break;
							case Comando.botaoNome.Interagir: //NomeBotoes.interagir:
							//ActInteract(myPlayer, myPlayerStat);
								Interagir ();
								yield return new WaitForSeconds (0.3f);
							//Debug.Log ("Interagiu");
								break;
							case Comando.botaoNome.GirarDireita: //NomeBotoes.girarDireita:
								GirarPersonagem (Player.Direction.Direita);
								yield return new WaitForSeconds (0.3f);
								break;
							case Comando.botaoNome.GirarEsquerda: //NomeBotoes.girarEsquerda:
								GirarPersonagem (Player.Direction.Esquerda);
								yield return new WaitForSeconds (0.3f);
								break;
							case Comando.botaoNome.Pular: //NomeBotoes.pular:
								PularPersonagem ();
								yield return new WaitForSeconds (0.8f);
								break;
							}
						}
						cGeral.retry = true;
						cGeral.myBtnExecutarImage.sprite = cGeral.myBtnRetry;
						cGeral.listaEmExecucao = false;
					yield return null;
				}
				else {
					EnviaMensagem ("\nA lista de programa ja esta em execuçao!");
					EnviaCodigo ("\nErro: listaComando.executando();");
					Debug.Log ("A Lista de Programa ja esta em execuçao!!");
				}
			}
			else 
			{
                //Recoloca Os Objetos
				cGeral.numeroRetries++;
				Debug.Log ("numero retries: "+cGeral.numeroRetries);
                cGeral.tabuleiroAtual.RecolocaObjetos();

				//Reposicionar Jogador
				Destroy (cGeral.myPlayer);
				Vector3 tempPosInicial = new Vector3 ();
				Tile oTile = cGeral.tabuleiroAtual.ProcuraTile (cGeral.posicaoInicial);
				if (oTile != null) 
				{
					tempPosInicial = oTile.transform.position;
					tempPosInicial.y = 1.5f + oTile.altura * 0.5f;
				}
				cGeral.myPlayer = (GameObject)Instantiate (playerPrefab, tempPosInicial/*new Vector3 (-5.5f, 1.3f, 4.5f)*/, Quaternion.identity);
				cGeral.myPlayer.GetComponent<Player> ().posicaoTabuleiro = cGeral.posicaoInicial;
				if (oTile != null)
					oTile.objetosEmCima.Add (cGeral.myPlayer);
				cGeral.retry = false;
				cGeral.myBtnExecutarImage.sprite = cGeral.myBtnPlay;

				myPlayer = cGeral.myPlayer;
				myPlayerStat = myPlayer.GetComponent<Player> ();
				myPlayAnim = myPlayer.GetComponentInChildren<Animator> ();

				//Reinicia a Camera
				cGeral.cameraEventos.SetCameraToDefault();

				//Zerar Contagem Pontuaçao da Fase
			}
		}
		else 
		{
			EnviaMensagem ("\nA lista de programa esta vazia.");
			EnviaCodigo ("\nErro: listaComando == null");
			Debug.Log ("A Lista de Programa esta nula!");
		}

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

		if(myPlayAnim.GetBool("andando"))
		{
			myPlayAnim.SetBool("andando",false);
			EnviaMensagem("\n<b>Algo</b> Andou!");
			EnviaCodigo("\n<b>Algo</b>.posicao = x,y; <b>Algo</b>.altura = 0;");
			Debug.Log (player.name + " Andou!");
		}
			

		if(myPlayAnim.GetBool("pulando"))
		{
			myPlayAnim.SetBool("pulando",false);
			EnviaMensagem("\n<b>Algo</b> Pulou!");
			EnviaCodigo("\n<b>Algo</b>.posicao = x,y; <b>Algo</b>.altura = z;");
			Debug.Log (player.name + " Pulou!");
		}
	}
	#endregion

	#region Anotaçoes

	//Chamando uma Coroutine
	//StartCoroutine(rotateObject (myCameraSuporte.transform.rotation, novaRotation, 1f));

	//Coroutine para esperar
	IEnumerator EsperaTempo(float waitSecs){
		yield return new WaitForSeconds (waitSecs);
	}
//
	#endregion

}
