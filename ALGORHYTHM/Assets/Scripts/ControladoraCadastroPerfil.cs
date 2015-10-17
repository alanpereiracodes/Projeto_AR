using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControladoraCadastroPerfil : MonoBehaviour {

	public GameObject gameManager; //GameManager prefab to instantiate.

	//UI
	public InputField inputNomeAluno;
	public InputField inputIdadeAluno;
	public StyledComboBox inputSerieAluno;
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
		
		string mensagem = validardados ();
		if(mensagem == "Validado com Sucesso")
		{
			if (toggleMasc.isOn)
				novoPerfil.generoAluno = "Masculino";
			else {
				if (toggleFemi.isOn)
					novoPerfil.generoAluno = "Feminino";
			}

			novoPerfil.nomeAluno = inputNomeAluno.text;
			novoPerfil.idadeAluno = int.Parse (inputIdadeAluno.text);
			novoPerfil.serieAluno = inputSerieAluno.nomebutton;

			ControladorGeral.referencia.CriarJogoNovo (novoPerfil);
			Debug.Log ("Jogo Criado e Salvo com Sucesso!");
		}
		else
		{
			Debug.Log ("Todos os campos sao obrigatorios! " + mensagem);
		}

	}

	public string validardados()
	{
		string mensagem;

		try {
			if (inputNomeAluno.text == "") {  
				return mensagem = "Digite o Nome!";
			}
			if (inputIdadeAluno.text != "") {
				if (int.Parse (inputIdadeAluno.text) <= 7) {
					return mensagem = "Idade Impropria!";
				}
			} else {	return mensagem = "Digite a Idade!";	}
			if (toggleFemi.isOn == false && toggleMasc.isOn == false) {
				return mensagem = "Selecione o Genero!";
			} 
			if (inputSerieAluno.nomebutton == "") {
				return mensagem = "Selecione a Serie!";
			}
		} catch (System.Exception ex) {
			return mensagem = "Idade permite apenas numeros!";
		}

		return mensagem = "Validado com Sucesso";
	}
}
