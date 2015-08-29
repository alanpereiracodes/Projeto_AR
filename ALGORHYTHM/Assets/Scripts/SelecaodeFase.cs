using UnityEngine;
using System.Collections;

public class SelecaodeFase : MonoBehaviour {

	public void CarregarFase(int FaseSelecionada)
	{
		//Chamar Fase
		Application.LoadLevel (FaseSelecionada);
	}
}
