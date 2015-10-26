using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JanelaPassouFase : MonoBehaviour 
{

	public Text mensagemFase;
	public Image cubo1;
	public Image cubo2;
	public Image cubo3;
	public Button btnOk; //Colocar Evento para Carregar a Proxima Fase
	//O botao de seleçao de Fase nao precisa mudar o evento :D
	public Button btnAgain; //Colocar Evento para Recarregar a fase

	public void PreencheCubos(int pontuacao, string mensagem)
	{
		ControladorGeral cGeral = ControladorGeral.referencia;

		mensagemFase.text = "Fase Concluida: "+mensagem;

		switch (pontuacao) 
		{
			case 1:
			cubo1.sprite = cGeral.cubinhoPreenchido; cubo1.color = Color.white;
			cubo1.rectTransform.rotation = Quaternion.identity;
			cubo2.sprite = cGeral.cubinhoVazio; cubo2.color = new Color32 (50,50,50,255);
			cubo3.sprite = cGeral.cubinhoVazio; cubo3.color = new Color32 (50,50,50,255);
			break;

			case 2:
			cubo1.sprite = cGeral.cubinhoPreenchido; cubo1.color = Color.white;
			cubo1.rectTransform.rotation = Quaternion.identity;
			cubo3.sprite = cGeral.cubinhoPreenchido; cubo3.color = Color.white;
			cubo3.rectTransform.rotation = Quaternion.identity;
			cubo2.sprite = cGeral.cubinhoVazio; cubo2.color = new Color32 (50,50,50,255);
			break;

			case 3:
			cubo1.sprite = cGeral.cubinhoPreenchido; cubo1.color = Color.white;
			cubo1.rectTransform.rotation = Quaternion.identity;
			cubo2.sprite = cGeral.cubinhoPreenchido; cubo2.color = Color.white;
			cubo2.rectTransform.rotation = Quaternion.identity;
			cubo3.sprite = cGeral.cubinhoPreenchido; cubo3.color = Color.white;
			cubo3.rectTransform.rotation = Quaternion.identity;
			break;
		}
	}

}
