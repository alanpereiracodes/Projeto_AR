using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CarregaFases : MonoBehaviour {

	public GameObject gameManager;
	public Text capituloTitulo;
	//public int capitulo;

	public List<GameObject> listaBotoesFases;

	void Awake () 
	{
		if (ControladorGeral.referencia == null)			
			Instantiate(gameManager);
	}

	void Start()
	{
		CarregarFases ();
	}

	public void CarregarFases()
	{
		capituloTitulo.text = "Capítulo " + ControladorGeral.referencia.jogoAtual.capituloAtual;

		int numeroFase = ControladorGeral.referencia.jogoAtual.numeroFaseLiberada;
		numeroFase = numeroFase - (ControladorGeral.referencia.jogoAtual.capituloAtual-1) * 10;

		foreach (GameObject obj in listaBotoesFases) 
		{
			if(obj.GetComponent<BotaoFase>().numero > numeroFase)
			{
				obj.SetActive(false);
			}
			Button btn = obj.GetComponent<Button>();
			btn.onClick.RemoveAllListeners();
			int n = (ControladorGeral.referencia.jogoAtual.capituloAtual-1)*10+numeroFase;
			btn.onClick.AddListener(() => CarregaFase("Fase "+n.ToString()));
			Debug.Log (n);
		}

		//Debug.Log ("O Capitulo e: " + ControladorGeral.referencia.jogoAtual.capituloAtual + " e a Fase e: " + numeroFase);
	}

	public void CarregaFase(string fase)
	{
		Application.LoadLevel(fase);
	}


}
