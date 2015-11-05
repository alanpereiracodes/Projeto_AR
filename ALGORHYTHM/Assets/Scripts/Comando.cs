using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comando : MonoBehaviour {

	public enum botaoNome
	{
		Andar,
		Falar,
		GirarDireita,
		GirarEsquerda,
		Interagir,
		Pular,
		Funcao
	};

		public Button btn;
		public botaoNome nome;
		public Image icone;
		public int numeroLista; 
	//teste - atributo para reorganizar a lista caso um elemento seja removido 
	//e tambem para ajudar na ordem de leitura dos comandos na hora de execuçao.
		//public Text methodLabel;
		//public Text numberLabel;
		//public InputField parametro1; //Strings
		//public InputField parametro2; //no caso de X e Y
		
		
}
