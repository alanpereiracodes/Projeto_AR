using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControladoraCadastroPerfil : MonoBehaviour {

	public GameObject gameManager; //GameManager prefab to instantiate.

	//UI
	public InputField inputNomeAluno;
	public InputField inputIdadeAluno;
	public InputField inputSerieAluno;
	public Toggle toggleMasc;
	public Toggle toggleFemi;

	// Use this for initialization
	void Awake () 
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (ControladorGeral.referencia == null)			
			//Instantiate gameManager prefab
			Instantiate(gameManager);
	}

	public void SalvarJogar_OnClick()
	{
		Perfil novoPerfil = new Perfil();
		if(toggleMasc.isOn)
			novoPerfil.generoAluno = "Masculino";
		else 
		{
			if(toggleFemi.isOn)
				novoPerfil.generoAluno = "Feminino";
		}

		novoPerfil.nomeAluno = inputNomeAluno.text;
		novoPerfil.idadeAluno = int.Parse(inputIdadeAluno.text);
		novoPerfil.serieAluno = inputSerieAluno.text;

		ControladorGeral.referencia.CriarJogoNovo(novoPerfil);
		Debug.Log ("Jogo Criado e Salvo com Sucesso!");
	}
}
