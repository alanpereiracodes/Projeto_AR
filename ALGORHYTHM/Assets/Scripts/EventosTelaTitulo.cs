using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EventosTelaTitulo : MonoBehaviour {

	//Menu
	public GameObject janelaMenu;

	//Botoes Açoes
	public RectTransform meuCursor;
	public GameObject btnNovoJogo;
	public GameObject btnCarregar;
	public GameObject btnSair;

	//Press Start UI
	public GameObject aperteStart;
	public GameObject botaoTelaToda;

	public enum OpcoesTitulo
	{
		Novojogo,
		Continuar,
		Sair
	};
	
	float intervaloEspera = 0.2f;
	float tempo;
	private bool menuHabilitado = false;

	OpcoesTitulo opcaoAtual = OpcoesTitulo.Novojogo;

	void Awake()
	{
		tempo = Time.time;
	}

	// Update is called once per frame
	void Update () 
	{
		if(tempo < Time.time)
		{
			if(Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
			{
				tempo = Time.time + intervaloEspera;
				if(!menuHabilitado)
					HabilitarMenu();
				else
				{
					ExecutaOpcao();
				}
			}

			if(menuHabilitado)
			{
				if(Input.GetKey(KeyCode.RightArrow))
				{
					tempo = Time.time + intervaloEspera;
					MudaOpcao(false);
				}
				if(Input.GetKey(KeyCode.LeftArrow))
				{
					tempo = Time.time + intervaloEspera;
					MudaOpcao(true);
				}
			}
		}
	}

	public void MudaOpcao (bool opcao)
	{
		if(!opcao)
		{
			switch(opcaoAtual)
			{
			case OpcoesTitulo.Novojogo:
				opcaoAtual=OpcoesTitulo.Continuar;
				meuCursor.transform.SetParent(btnCarregar.transform);
				break;
			case OpcoesTitulo.Continuar:
				opcaoAtual=OpcoesTitulo.Sair;
				meuCursor.transform.SetParent(btnSair.transform);
				break;
			case OpcoesTitulo.Sair:
				opcaoAtual=OpcoesTitulo.Novojogo;
				meuCursor.transform.SetParent(btnNovoJogo.transform);
				break;
			}
		}
		else
		{
			switch(opcaoAtual)
			{
			case OpcoesTitulo.Novojogo:
				opcaoAtual=OpcoesTitulo.Sair;
				meuCursor.transform.SetParent(btnSair.transform);
				break;
			case OpcoesTitulo.Continuar:
				opcaoAtual=OpcoesTitulo.Novojogo;
				meuCursor.transform.SetParent(btnNovoJogo.transform);
				break;
			case OpcoesTitulo.Sair:
				opcaoAtual=OpcoesTitulo.Continuar;
				meuCursor.transform.SetParent(btnCarregar.transform);
				break;
			}
		}
	}

	void ExecutaOpcao ()
	{
		switch(opcaoAtual)
		{
		case OpcoesTitulo.Novojogo:
			NovoJogo();
			break;
		case OpcoesTitulo.Continuar:
			Carregar();
			break;
		case OpcoesTitulo.Sair:
			Sair();
            break;
        }
	}

	public void NovoJogo()
	{
		Application.LoadLevel (1); //Chama tela de Criar Perfil
	}

	public void Carregar()
	{
		Application.LoadLevel (2); //Chama tela de Carregar Jogo
	}

	public void Sair()
	{
		Debug.Log ("Saiu.");
		Application.Quit();
	}

	public void HabilitarMenu()
	{
		janelaMenu.GetComponent<Animation>().Play();
		aperteStart.SetActive(false);
		botaoTelaToda.SetActive(false);
		menuHabilitado = true;
	}

}//fim Script
