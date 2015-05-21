using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//Enumerate para determinar todas as direçoes possiveis que o Jogador pode tomar
	public enum Direction
	{
		Frente, 
		Esquerda, 
		Tras, 
		Direita
	};

	//Variavel para armazenar e direçao atual do personagem
	public Direction direcao;
	//Armazena a posicao em que o jogador esta em um sistema de coordenadas 2D
	public Vector2 posicaoTabuleiro;
}
