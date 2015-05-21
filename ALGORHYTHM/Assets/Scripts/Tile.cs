using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour{

	public Vector2 posicaoTabuleiro = new Vector2(); //Armazena a posicao do tile em um sistema de coordenadas X e Y
	public GameObject objetoEmCima; //variavel para definir se esta ocupado e qual objeto esta ocupando nosso Tile no momento, 
									//pode ser um objeto interativo, um inimigo ou o proprio jogador
	public bool andavel; //determina se o personagem pode andar por esse Tile se nao estiver ocupado

}

