using UnityEngine;
using System.Collections;

public enum Nome
{
	Portal,
	Alavanca,
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
	public Vector2 posicaoTabuleiro;

}
