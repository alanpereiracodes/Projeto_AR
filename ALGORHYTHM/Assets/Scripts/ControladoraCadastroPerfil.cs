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
	public GameObject janelaMensagem;
	public AudioSource musicaMenu;

	private string mensagem;
	private float intervaloEspera = 0.2f;
	private float tempo;

	// Use this for initialization
	void Awake () 
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (ControladorGeral.referencia == null)	
		{
			//Instantiate gameManager prefab
			Instantiate(gameManager);
			ControladorGeral.referencia.musicaRolando = musicaMenu;
			ControladorGeral.referencia.musicaRolando.Play ();
			DontDestroyOnLoad(musicaMenu);
		}
		else
		{
			if(ControladorGeral.referencia.musicaRolando.clip != musicaMenu.clip)
			{
				Destroy(ControladorGeral.referencia.musicaRolando.gameObject);
				ControladorGeral.referencia.musicaRolando = musicaMenu;
				ControladorGeral.referencia.musicaRolando.Play ();
				ControladorGeral.referencia.musicaRolando.volume = ControladorGeral.referencia.volumeAtual;
				DontDestroyOnLoad(musicaMenu);
			}
			else
			{
				Destroy (musicaMenu.gameObject);
			}
		}
		inputNomeAluno.Select();
		tempo = Time.time;
	}

	void Update()
	{
		if(tempo < Time.time)
		{
			if((Input.GetButton("Submit") && janelaMensagem.activeInHierarchy) || (Input.GetButton("Cancel") && janelaMensagem.activeInHierarchy) )
			{
				tempo = Time.time + intervaloEspera;
				janelaMensagem.SetActive (false);
			}

			else 
			{
				if(Input.GetButton("Cancel") && !janelaMensagem.activeInHierarchy)
				{
					tempo = Time.time + intervaloEspera;
					Application.LoadLevel(0);
				}
				if(Input.GetButton("Submit") && !janelaMensagem.activeInHierarchy)
				{
					tempo = Time.time + intervaloEspera;
					SalvarJogar_OnClick();
				}
				if(Input.GetKey(KeyCode.Tab) && !janelaMensagem.activeInHierarchy)
				{
					tempo = Time.time + intervaloEspera;
					Tabulacao();
				}
			}
		}
	}

	public void Tabulacao()
	{
		if(!inputNomeAluno.isFocused && !inputIdadeAluno.isFocused)
			inputNomeAluno.Select ();
		else if(inputNomeAluno.isFocused)
				inputIdadeAluno.Select ();
	}

	public void SalvarJogar_OnClick()
	{
		Perfil novoPerfil = new Perfil();
		if(ValidarCampos ())
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
			//Chama janela com a Mensagem de que foi cadastrado e redireciona para a tela da primeira Fase
			janelaMensagem.GetComponent<JanelaDeMensagem>().mensagem.text = mensagem;
			janelaMensagem.GetComponent<JanelaDeMensagem>().btnOk.onClick.RemoveAllListeners();
			janelaMensagem.GetComponent<JanelaDeMensagem>().btnOk.onClick.AddListener(() => btnEntendiPassaFase_OnClick());
			janelaMensagem.SetActive(true);
			Debug.Log ("Jogo Criado e Salvo com Sucesso!");
		}
		else
		{
			//Chama Janela com o Erro na mensagem
			janelaMensagem.GetComponent<JanelaDeMensagem>().mensagem.text = "Todos os campos são obrigatórios, ocorreu o seguinte problema:\n" + mensagem;
			janelaMensagem.SetActive(true);
			Debug.Log ("Todos os campos sao obrigatorios! " + mensagem);
		}
	}

	public bool ValidarCampos()
	{
		mensagem = "";

		try 
		{
			if (inputNomeAluno.text == "" || inputNomeAluno.text.StartsWith(" "))
			{  
				mensagem = "O campo nome precisa ser preenchido e deve começar com alguma letra!";
				return false;
			}
			foreach(char c in inputNomeAluno.text)
			{
				if(!char.IsLetter(c) && c != ' ')
				{
					mensagem = "Nome só pode conter letras ou espaço!";
					return false;
				}
			}
			if (inputIdadeAluno.text != "") 
			{
				if (int.Parse (inputIdadeAluno.text) <= 7) 
				{
					mensagem = "Idade Imprópria!";
					return false;
				}
			} 
			else 
			{	
				mensagem = "Digite a Idade!";
				return false;
			}
			if (toggleFemi.isOn == false && toggleMasc.isOn == false)
			{
				mensagem = "Selecione o Gênero!";
				return false;
			} 
			if (inputSerieAluno.nomebutton == "") 
			{
				mensagem = "Selecione a Série!";
				return false;
			}
		}
		catch
		{
			mensagem = "Idade permite apenas números!";
			return false;
		}

		mensagem = "Cadastro efetuado com Sucesso!";
		return true;
	}

	public void btnEntendiPassaFase_OnClick()
	{
		Application.LoadLevel("Historia");
		ControladorGeral.referencia.musicaRolando.Stop();
	}

}
