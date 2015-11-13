using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControladorHistoria : MonoBehaviour {

	public int slideAtual = 0;
	public List<Animation> listaAnimacoes;
	public List<GameObject> slides;


	// Use this for initialization
	void Start () 
	{
		if(slideAtual == 0)
		{
			slideAtual = 1;
			slides[1].SetActive(true);
			listaAnimacoes[1].Play ();
		}
	}

	public void TrocaSlide(int soma)
	{
		slideAtual += soma;
		if(slideAtual <= 0)
		{
			slideAtual = 1;
		}
		if(slideAtual >= 5)
		{
			Application.LoadLevel("Fase 1");
		}
		switch(slideAtual)
		{
		case 2:
			slides[1].SetActive(false);
			slides[2].SetActive(true);
			slides[3].SetActive(false);
			slides[4].SetActive(false);
			listaAnimacoes[2].Play ();
			break;
		case 3:
			slides[1].SetActive(false);
			slides[2].SetActive(false);
			slides[3].SetActive(true);
			slides[4].SetActive(false);
			listaAnimacoes[3].Play ();
			break;
		case 4:
			slides[1].SetActive(false);
			slides[2].SetActive(false);
			slides[3].SetActive(false);
			slides[4].SetActive(true);
			listaAnimacoes[4].Play ();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
