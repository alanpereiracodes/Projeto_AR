using UnityEngine;
using System.Collections;




public class Objeto : MonoBehaviour {

	public enum Nome
	{
		Portal,
		Alavanca,
		Cubo,
		Cristal,
		Altar,
		Barreira,
		BotaoChao
	};
	
	public enum Tipo
	{
		Interativo,
		ItemPegavel,
		Altar,
		Estatico,
		Cristal
	};
	
	public bool ativado;
	public bool bloqueiaCaminho;
	public Vector2 posicaoTabuleiro;
	public Objeto.Nome nome;
	public Objeto.Tipo tipo;

}
