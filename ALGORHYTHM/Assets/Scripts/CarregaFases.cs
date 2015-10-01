using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CarregaFases : MonoBehaviour {

	public GameObject gameManager;          //GameManager prefab to instantiate.
	public int capitulo;

	// Use this for initialization
	void Awake () 
	{
			//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
			if (ControladorGeral.referencia == null)			
				//Instantiate gameManager prefab
				Instantiate(gameManager);

		ControladorGeral.referencia.jogoAtual.capituloAtual = capitulo;
	}
}
