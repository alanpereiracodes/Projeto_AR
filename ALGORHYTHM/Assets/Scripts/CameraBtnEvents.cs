using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraBtnEvents : MonoBehaviour {
	
	public GameObject myCameraSuporte;
	public float rotateSpeed = 0.5f;
	public float rotationTime = 1f;

	public Button botaoRL; //referencia ao botao para poder desabilita-lo no processo de girar a camera
	public Button botaoRR;

	private Player jogador;

	private int cameraYAxisRotation;
	//private float waitTime = 0.5f;

	void Awake()
	{
		cameraYAxisRotation = 0;
	}

	void Start()
	{
		if(ControladorGeral.referencia != null)
		{
			ControladorGeral.referencia.cameraEventos = this;
		}
	}

	public void SetCameraToDefault()
	{
		if (myCameraSuporte != null) 
		{
			cameraYAxisRotation = 0;
//			myCamera.transform.position = new Vector3(12,10,-12);
//			Debug.Log ("ok");
//			myCamera.transform.rotation = Quaternion.Euler(30, 315, 0);
//			Debug.Log ("ok2");

			myCameraSuporte.transform.position = new Vector3(2.5f,0,0);
			myCameraSuporte.transform.rotation = Quaternion.identity;
			ControladorGeral.referencia.myPlayer.GetComponent<Player>().direcaoGlobal = ControladorGeral.referencia.direcaoInicial;
			ControladorGeral.referencia.myPlayer.GetComponent<Player>().direcaoCamera = ControladorGeral.referencia.direcaoInicial;
			//ControladorGeral.referencia.myPlayer.GetComponentInChildren<Animator>().SetInteger("direcao",1);

			switch( ControladorGeral.referencia.direcaoInicial)
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

		}
	}

	public void RotateCameraToRight()
	{
		jogador = ControladorGeral.referencia.myPlayer.GetComponent<Player> ();

		int temp = 0;

		switch(cameraYAxisRotation)
		{
			case 0:
				temp = 90;
				
				break;

			case 90:
				temp = 180;
				break;

			case 180:
				temp = 270;
				break;

			case 270:
				temp = 0;
				break;
		
		}

		cameraYAxisRotation = temp;
		Quaternion novaRotation = Quaternion.Euler (0, cameraYAxisRotation, 0);

		//myCameraSuporte.transform.rotation = novaRotation;
		StartCoroutine(rotateObject (myCameraSuporte.transform.rotation, novaRotation, rotationTime));
		//toLeft
		switch(jogador.direcaoCamera)
		{
		case Player.Direction.Frente:
			jogador.direcaoCamera = Player.Direction.Esquerda;
			break;
		case Player.Direction.Esquerda:
			jogador.direcaoCamera = Player.Direction.Tras;
			break;
		case Player.Direction.Tras:
			jogador.direcaoCamera = Player.Direction.Direita;
			break;
		case Player.Direction.Direita:
			jogador.direcaoCamera = Player.Direction.Frente;
			break;
		}
		//
		switch(jogador.direcaoCamera)
		{
		case Player.Direction.Frente:
			jogador.GetComponentInChildren<Animator>().SetInteger("direcao",1);
			break;
		case Player.Direction.Esquerda:
			jogador.GetComponentInChildren<Animator>().SetInteger("direcao",2);
			break;
		case Player.Direction.Tras:
			jogador.GetComponentInChildren<Animator>().SetInteger("direcao",3);
			break;
		case Player.Direction.Direita:
			jogador.GetComponentInChildren<Animator>().SetInteger("direcao",4);
			break;
		}
	}

	public void RotateCameraToLeft()
	{
		jogador = ControladorGeral.referencia.myPlayer.GetComponent<Player> ();

		int temp = 0;
		
		switch(cameraYAxisRotation)
		{
		case 0:
			temp = 270;
			break;
			
		case 90:
			temp = 0;
			break;
			
		case 180:
			temp = 90;
			break;
			
		case 270:
			temp = 180;
			break;
			
		}
		
		cameraYAxisRotation = temp;

		Quaternion novaRotation = Quaternion.Euler (0, cameraYAxisRotation, 0);

		StartCoroutine(rotateObject (myCameraSuporte.transform.rotation, novaRotation, rotationTime));
		//myCameraSuporte.transform.rotation = novaRotation;
		//myCameraSuporte.transform.rotation = Quaternion.Lerp (myCameraSuporte.transform.rotation, novaRotation, Time.deltaTime);
		//toRight
		switch(jogador.direcaoCamera)
		{
		case Player.Direction.Frente:
			jogador.direcaoCamera = Player.Direction.Direita;
			break;
		case Player.Direction.Direita:
			jogador.direcaoCamera = Player.Direction.Tras;
			break;
		case Player.Direction.Tras:
			jogador.direcaoCamera = Player.Direction.Esquerda;
			break;
		case Player.Direction.Esquerda:
			jogador.direcaoCamera = Player.Direction.Frente;
			break;
		}
		//
		switch(jogador.direcaoCamera)
		{
		case Player.Direction.Frente:
			jogador.GetComponentInChildren<Animator>().SetInteger("direcao",1);
			break;
		case Player.Direction.Esquerda:
			jogador.GetComponentInChildren<Animator>().SetInteger("direcao",2);
			break;
		case Player.Direction.Tras:
			jogador.GetComponentInChildren<Animator>().SetInteger("direcao",3);
			break;
		case Player.Direction.Direita:
			jogador.GetComponentInChildren<Animator>().SetInteger("direcao",4);
			break;
		}	
	}

	//Coroutine
	IEnumerator /*moveObject*/ rotateObject(Quaternion source, Quaternion target, float overTime)
	{
		botaoRL.GetComponent<Button>().enabled = false;
		botaoRR.GetComponent<Button>().enabled = false;
//		Button botao1 = botaoRL.GetComponent<Button> ();
//		Button botao2 =	botaoRR.GetComponent<Button>();



		
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			//transform.position = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			myCameraSuporte.transform.rotation = Quaternion.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		//transform.position = target;
		myCameraSuporte.transform.rotation = target;
		botaoRL.GetComponent<Button>().enabled = true;
		botaoRR.GetComponent<Button>().enabled = true;
		//Debug.Log ("coroutine REALLY WORKS!!");
	}

	/*
	 * public void GirarPersonagem(Player.Direction direcaoGiro)
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
		}
		*/

}
