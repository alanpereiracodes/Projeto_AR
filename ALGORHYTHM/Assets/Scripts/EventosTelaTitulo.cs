using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EventosTelaTitulo : MonoBehaviour {

	public RectTransform meuCursor;

	public enum OpcoesTitulo
	{
		Novojogo,
		Continuar,
		Sair
	};
	
	float intervaloEspera = 0.2f;
	float tempo;

	OpcoesTitulo opcaoAtual = OpcoesTitulo.Novojogo;

	void Awake()
	{
		tempo = Time.time;
	}

	// Update is called once per frame
	void Update () 
	{
		if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
		{
			if(tempo < Time.time)
			{
				MudaOpcao(false);
				//Debug.Log ("Desce menu"); //Teste somente para avaliar o 'firing rate' da tecla, estava repetindo muito com apenas um toque
				tempo = Time.time + intervaloEspera;
			}
		}
		if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
		{
			if(tempo < Time.time)
			{
				MudaOpcao(true);
				tempo = Time.time + intervaloEspera;
			}
		}
		if(Input.GetKey(KeyCode.Return)) //Return = Enterzao huh3u3hu3
		{
			if(tempo < Time.time)
			{
			switch(opcaoAtual)
				{
				case OpcoesTitulo.Novojogo:
					chamaNovoJogo();
					break;
				case OpcoesTitulo.Continuar:
					chamaTelaContinue();
					break;
				case OpcoesTitulo.Sair:
					Application.Quit();
					break;
				}
				tempo = Time.time + intervaloEspera;
			}
		}
		
	}

	void MudaOpcao (bool opcao)
	{
		if(!opcao)
		{
			switch(opcaoAtual)
			{
			case OpcoesTitulo.Novojogo:
				opcaoAtual=OpcoesTitulo.Continuar;
				meuCursor.position = new Vector3(meuCursor.position.x,meuCursor.position.y - 25,meuCursor.position.z);
				break;
			case OpcoesTitulo.Continuar:
				opcaoAtual=OpcoesTitulo.Sair;
				meuCursor.position = new Vector3(meuCursor.position.x,meuCursor.position.y - 25,meuCursor.position.z);
				break;
			case OpcoesTitulo.Sair:
				opcaoAtual=OpcoesTitulo.Novojogo;
				meuCursor.position = new Vector3(meuCursor.position.x,meuCursor.position.y + 50,meuCursor.position.z);
				break;
			}
		}
		else
		{
			switch(opcaoAtual)
			{
			case OpcoesTitulo.Novojogo:
				opcaoAtual=OpcoesTitulo.Sair;
				meuCursor.position = new Vector3(meuCursor.position.x,meuCursor.position.y - 50,meuCursor.position.z);
				break;
			case OpcoesTitulo.Continuar:
				opcaoAtual=OpcoesTitulo.Novojogo;
				meuCursor.position = new Vector3(meuCursor.position.x,meuCursor.position.y + 25,meuCursor.position.z);
				break;
			case OpcoesTitulo.Sair:
				opcaoAtual=OpcoesTitulo.Continuar;
				meuCursor.position = new Vector3(meuCursor.position.x,meuCursor.position.y + 25,meuCursor.position.z);
				break;
			}
		}
	}

	void chamaNovoJogo ()
	{
		Application.LoadLevel (1);
	}

	void chamaTelaContinue ()
	{
		//A FAZER
	}
}
