using UnityEngine;
using System.Collections;




public class Objeto : MonoBehaviour {

	public enum Nome
	{
		Portal,
		Alavanca,
		Cubo,
		Cristal,
		Altar
	};
	
	public enum Tipo
	{
		Interativo,
		ItemPegavel,
		Altar,
		Estatico
	};
	
	public bool ativado;
	public bool bloqueiaCaminho;
	public Vector2 posicaoTabuleiro;
	public Objeto.Nome nome;
	public Objeto.Tipo tipo;

}
