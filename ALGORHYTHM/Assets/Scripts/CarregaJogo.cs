using UnityEngine;
using System.Collections;

public class CarregaJogo : MonoBehaviour {

	
	public GameObject gameManager;          //GameManager prefab to instantiate.
	public GameObject myPlayer;
	private CriaTabuleiro tabuleiroScript;

	void Awake ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (ControladorGeral.referencia == null)			
			//Instantiate gameManager prefab
			Instantiate(gameManager);

		//Get a component reference to the attached BoardManager script
		tabuleiroScript = GetComponent<CriaTabuleiro>();
		tabuleiroScript.GeraMapa();

		ControladorGeral.referencia.myPlayer = (GameObject)Instantiate(myPlayer, new Vector3 (-5.5f, 1.3f, 4.5f), Quaternion.identity);

	}
}
