using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ControladorSelecaoFase : MonoBehaviour {

	//
	public int idJogoAtual = 0;
	public int ultimaFaseLiberada = 1;
	public int capituloAtual;

	//Lista com os Botoes das fases
	public List<GameObject> listaBotoesFases;
	public List<Fase> listaFases;

	//UI
	public Text capituloTexto;
	public Image cubinhoVazio;
	public Image cubinhoPreenchido;

	// Use this for initialization
	void Start () {
		//Carrega os dados do Jogo no banco de Dados e Verifica a ultima fase que foi liberada.
		//Cria uma Lista de Fases com os dados de todas fases no Banco (Suas pontuaçoes e estrelas);
		AtualizaFases(/*ultimoCapitulo*/);

//		foreach(GameObject gObj in listaBotoesFases)
//		{
//			Fase oFase = gObj.GetComponent<Fase>();
//			//Agora e onde serao liberadas as fases com numero igual ou menor ao "ultimaFaseLiberada".
//			if(oFase.numero <= ultimaFaseLiberada)
//			{
//				oFase.liberada = true;
//			}
//		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}


	public void AtualizaFases()
	{

	}

	public void AtualizaFases(int capitulo)
	{
		
	}
}
