using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CommandButton : MonoBehaviour {
	
	public Button button;
	public Text nameLabel;
	public Image icon;
	public Text methodLabel;
	public Text numberLabel;
	public InputField parametro1; //Strings
	public InputField parametro2; //no caso de X e Y
	public int listNumber; //teste - atributo para reorganizar a lista caso um elemento seja removido 
						   //e tambem para ajudar na ordem de leitura dos comandos na hora de execuçao.

}
