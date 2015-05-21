using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExecuteProgramList : MonoBehaviour {
	
	public float tempoMover = 0.6f;
	public List<CommandButton> programList = new List<CommandButton>();

	private GameObject myPlayer;//a
	private Player myPlayerStat;

	//Acontece depois do Awake, dando tempo de a referencia do Player ser atribuida!
	void Start()
	{
		myPlayer = ControladorGeral.referencia.myPlayer;
		myPlayerStat = myPlayer.GetComponent<Player> ();
	}

	public void ExecuteList(/*GameObject myPlayer, List<CommandButton> programList*/)
	{
		//atualiza a lista;
		programList = new List<CommandButton> ();
		programList = CreateProgramList.referencia.programList;

		StartCoroutine (ExecutarLista (programList));
		
		//Debug.Log (myPlayerStat.direcao); //teste
	}

	public void MoverPersonagem()
	{
		Vector3 newPos;
		switch(myPlayerStat.direcao)
		{
			case Player.Direction.Frente:
				//verifica se o proximo bloco e andavel
				if(VerificaBlocoQueVaiAndar(Player.Direction.Frente, myPlayerStat.posicaoTabuleiro))
				{
					newPos = new Vector3(myPlayer.transform.position.x,myPlayerStat.transform.position.y,myPlayer.transform.position.z-1);
					StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
				}
				break;

			case Player.Direction.Tras:
				newPos = new Vector3(myPlayer.transform.position.x,myPlayerStat.transform.position.y,myPlayer.transform.position.z+1);
				StartCoroutine(MoverObjeto(myPlayer,myPlayer.transform.position, newPos, tempoMover));
				break;
		}
	}

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
		return false;
	}

	//Chamando uma Coroutine
	//StartCoroutine(rotateObject (myCameraSuporte.transform.rotation, novaRotation, 1f));

//Coroutine para Movimentar Smooth, sem teletransportar o personagem e sem depender do Update/Fixed Update para fazer isso.
	IEnumerator MoverObjeto(GameObject player, Vector3 source, Vector3 target, float overTime)
	{	
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			player.transform.position = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		player.transform.position = target;

	}

	//Coroutine para Executar a Lista


	IEnumerator ExecutarLista(List<CommandButton> lista)
	{
		if (lista != null) {
			foreach (CommandButton comando in lista) {
				switch (comando.nameLabel.text) {
				case "Andar":
					//MoveGo(myPlayer, myPlayerStat);
					MoverPersonagem ();
					yield return new WaitForSeconds (0.8f);
					Debug.Log ("Andou");
					break;
				case "Falar":
					//ActTalk(string comando.parametro1.text, myPlayer, myPlayerStat);
					Debug.Log ("Falou " + comando.parametro1.text);
					//Debug.Log ("Falou");
					break;
				case "Interagir":
					//ActInteract(myPlayer, myPlayerStat);
					Debug.Log ("Interagiu");
					break;
				}
			}
		} else
			Debug.Log ("A Program Lista esta nula!");
		yield return null;
	}

	//Coroutine para esperar
//	IEnumerator Wait(){
//		yield return new WaitForSeconds (3.0f);
//	}
//

}
