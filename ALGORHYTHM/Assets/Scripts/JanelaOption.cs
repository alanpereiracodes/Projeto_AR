using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JanelaOption : MonoBehaviour 
{
	public Button btnSalvar;
	public Text txtResolucao;
	public Slider volume;
	public Toggle togSimples;
	public Toggle togAvancado;

	//Logs do Sistema
	public GameObject logSimples;
	public GameObject logAvancado;


	public void AumentaResolucao()
	{
		switch(txtResolucao.text)
		{

			case "1280x720":
			txtResolucao.text = "1366x728";
			break;

			case "1366x728":
			txtResolucao.text = "1600x900";
			break;

		}
	}

	public void DiminuirResolucao()
	{
		switch(txtResolucao.text)
		{
			
		case "1366x728":
			txtResolucao.text = "1280x720";
			break;
			
		case "1600x900":
			txtResolucao.text = "1366x728";
			break;
			
		}
	}

	public void SalvarAlteracoes()
	{
		switch(txtResolucao.text)
		{
		case "1280x720":
			Screen.SetResolution(1280,720, true);
			break;

		case "1366x728":
			Screen.SetResolution(1366,728, true);
			break;
			
		case "1600x900":
			Screen.SetResolution(1600,900, true);
			break;	
		}

		if(togSimples.isOn)
		{
			if(logAvancado.activeInHierarchy)
			{
				logAvancado.SetActive(false);
				logSimples.SetActive(true);
				ControladorGeral.referencia.logAvanc = false;
            }
		}
		else
		{
			if(logSimples.activeInHierarchy)
			{
				logAvancado.SetActive(true);
				logSimples.SetActive(false);
				ControladorGeral.referencia.logAvanc = true;
            }
		}

		ControladorGeral.referencia.resolucaoAtual = txtResolucao.text;
		ControladorGeral.referencia.musicaRolando.volume = volume.value;
		ControladorGeral.referencia.volumeAtual = volume.value;
            
        this.gameObject.SetActive (false);
	}

}
