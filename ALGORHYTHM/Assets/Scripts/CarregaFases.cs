using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CarregaFases : MonoBehaviour {

	public GameObject gameManager;
	public Text capituloTitulo;
	public AudioSource musicaMenu;
	//public int capitulo;

	public List<GameObject> listaBotoesFases;
	public List<Sprite> listaImagensFases;

	private float intervaloEspera = 0.2f;
	private float tempo;
	public int capituloMostrando;

	void Awake () 
	{

		if (ControladorGeral.referencia == null)
		{
			Instantiate(gameManager);
			ControladorGeral.referencia.musicaRolando = musicaMenu;
			ControladorGeral.referencia.musicaRolando.Play ();
			DontDestroyOnLoad(musicaMenu.gameObject);
		}
		else
		{
			if(ControladorGeral.referencia.musicaRolando.clip != musicaMenu.clip)
			{
				Destroy(ControladorGeral.referencia.musicaRolando.gameObject);
				ControladorGeral.referencia.musicaRolando = musicaMenu;
				ControladorGeral.referencia.musicaRolando.Play ();
				ControladorGeral.referencia.musicaRolando.volume = ControladorGeral.referencia.volumeAtual;
				DontDestroyOnLoad(musicaMenu.gameObject);
			}
			else
			{
				Destroy (musicaMenu.gameObject);
			}
		}

		tempo = Time.time;
	}
	
	void Start()
	{
		CarregarFases ();
	}

	void Update()
	{
		if(tempo < Time.time)
		{
			
			if(Input.GetKey(KeyCode.Escape))
			{
				tempo = Time.time + intervaloEspera;
				Application.LoadLevel(2);
			}
		}
	}


	public void CarregarFases()
	{
		capituloTitulo.text = "Capítulo " + ControladorGeral.referencia.jogoAtual.capituloAtual;
		capituloMostrando = ControladorGeral.referencia.jogoAtual.capituloAtual;

		int numeroFase = ControladorGeral.referencia.jogoAtual.numeroFaseLiberada;
		numeroFase = numeroFase - (ControladorGeral.referencia.jogoAtual.capituloAtual-1) * 10;
		Debug.Log ("NumeroFase: "+numeroFase);

		foreach (GameObject obj in listaBotoesFases) 
		{
			if(obj.GetComponent<BotaoFase>().numero > numeroFase)
			{
				obj.SetActive(false);
			}
			Button btn = obj.GetComponent<Button>();
			btn.onClick.RemoveAllListeners();
			int n = (ControladorGeral.referencia.jogoAtual.capituloAtual-1)*10+obj.GetComponent<BotaoFase>().numero;
			int pontuacao = ProcuraPontuacaoFase(n);
			Debug.Log ("Jogo "+ControladorGeral.referencia.jogoAtual.idJogo+" pontuacao Fase "+n+" es:"+pontuacao);
			Transform painelImagens = obj.transform.FindChild("Panel");
			switch(pontuacao)
			{
			case 1:
				painelImagens.FindChild("Image").gameObject.GetComponent<Image>().color = Color.white;
				break;
			case 2:
				painelImagens.FindChild("Image").gameObject.GetComponent<Image>().color = Color.white;
				painelImagens.FindChild("Image 2").gameObject.GetComponent<Image>().color = Color.white;
				break;
			case 3:
				Debug.Log ("tentou 3");
				painelImagens.FindChild("Image").gameObject.GetComponent<Image>().color = Color.white;
				painelImagens.FindChild("Image 2").gameObject.GetComponent<Image>().color = Color.white;
				painelImagens.FindChild("Image 3").gameObject.GetComponent<Image>().color = Color.white;
				break;
			}
			obj.transform.FindChild("ImagemFase").gameObject.GetComponent<Image>().sprite = listaImagensFases[n];

			btn.onClick.AddListener(() => CarregaFase("Fase "+n.ToString()));
			//Debug.Log (n);
		}

		//Debug.Log ("O Capitulo e: " + ControladorGeral.referencia.jogoAtual.capituloAtual + " e a Fase e: " + numeroFase);
	}

	int ProcuraPontuacaoFase (int n)
	{
		int pontuacao = 0;

		ConexaoBanco banco = new ConexaoBanco();
		banco.AbrirBanco("URI=file:" + Application.dataPath + "/MeuJogoSalvo.sqdb");

		List<Fase> fasesSelect = new List<Fase> ();
		
		ArrayList minhasFasesSelecionadas = banco.LerTabelaToda ("Fase");//banco.SelecionaUnicoWhere ("Fase", "*", "idJogo", "=", novaFase.idJogo.ToString ());
		if (minhasFasesSelecionadas != null || minhasFasesSelecionadas.Count > 0) 
		{
			foreach(ArrayList faseSelecioanda in minhasFasesSelecionadas)
			{
				Fase abcFase = new Fase();
				abcFase.numeroFase = (int)faseSelecioanda[0];
				abcFase.capitulo = (int)faseSelecioanda[1];
				abcFase.pontuacaoCubinhoDigital = (int)faseSelecioanda[2];
				abcFase.idJogo = (int)faseSelecioanda[3];
				
				fasesSelect.Add (abcFase);
				//Debug.Log ("\n"+abcFase.numeroFase+abcFase.capitulo+abcFase.pontuacaoCubinhoDigital+abcFase.idJogo);
			}
		}

		//Debug.Log (fasesSelect.Count);
		foreach(Fase fs in fasesSelect)
		{
			if(fs.numeroFase == n && fs.idJogo == ControladorGeral.referencia.jogoAtual.idJogo)
			{
				Debug.Log (pontuacao+" Achou!");
				pontuacao = fs.pontuacaoCubinhoDigital;
			}
		}

		banco.FecharBanco();
		return pontuacao;
	}

	public void CarregaFase(string fase)
	{
		Application.LoadLevel(fase);
	}

	public void ProximoCapitulo()
	{
		if(ControladorGeral.referencia.jogoAtual.capituloAtual > capituloMostrando)
		{
			CarregaCapitulo(capituloMostrando+1);
		}
	}

	public void AnteriorCapitulo()
	{
		if(capituloMostrando >= 2)
		{
			CarregaCapitulo(capituloMostrando-1);
		}
	}

	public void CarregaCapitulo(int numeroCapitulo)
	{
		capituloTitulo.text = "Capítulo " + numeroCapitulo.ToString ();
		capituloMostrando = numeroCapitulo;
		int numeroFase = ControladorGeral.referencia.jogoAtual.numeroFaseLiberada;
		numeroFase = numeroFase - (numeroCapitulo-1) * 10;
		Debug.Log ("NumeroFase: "+numeroFase);
		
		foreach (GameObject obj in listaBotoesFases) 
		{
			if(obj.GetComponent<BotaoFase>().numero > numeroFase)
			{
				obj.SetActive(false);
			}
			else
			{
				obj.SetActive(true);
			}
			Button btn = obj.GetComponent<Button>();
			btn.onClick.RemoveAllListeners();
			int n = (numeroCapitulo-1)*10+obj.GetComponent<BotaoFase>().numero;
			int pontuacao = ProcuraPontuacaoFase(n);
			Debug.Log ("Jogo "+ControladorGeral.referencia.jogoAtual.idJogo+" pontuacao Fase "+n+" es:"+pontuacao);
			Transform painelImagens = obj.transform.FindChild("Panel");
			switch(pontuacao)
			{
			case 1:
				painelImagens.FindChild("Image").gameObject.GetComponent<Image>().color = Color.white;
				break;
			case 2:
				painelImagens.FindChild("Image").gameObject.GetComponent<Image>().color = Color.white;
				painelImagens.FindChild("Image 2").gameObject.GetComponent<Image>().color = Color.white;
				break;
			case 3:
				Debug.Log ("tentou 3");
				painelImagens.FindChild("Image").gameObject.GetComponent<Image>().color = Color.white;
				painelImagens.FindChild("Image 2").gameObject.GetComponent<Image>().color = Color.white;
				painelImagens.FindChild("Image 3").gameObject.GetComponent<Image>().color = Color.white;
				break;
			}
			obj.transform.FindChild("ImagemFase").gameObject.GetComponent<Image>().sprite = listaImagensFases[n];
			
			btn.onClick.AddListener(() => CarregaFase("Fase "+n.ToString()));
		}
		Debug.Log ("Capitulo Carregado: "+capituloMostrando.ToString());
	}//Fim Carrega Capitulo


}
