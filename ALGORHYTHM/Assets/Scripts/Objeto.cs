using UnityEngine;
using System.Collections;

public enum Nome
{
	Portal,
	Alavanca,
	Cubo,
	Cristal
};

public enum Tipo
{
	Interativo,
	ItemPegavel,
	Altar,
	Estatico
};


public class Objeto : MonoBehaviour {
	
	public bool ativado;
	public bool bloqueiaCaminho;
	public Vector2 posicaoTabuleiro;

}
