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

	//Variavel para armazenar a direçao atual do personagemem relaçao ao mapa
	public Direction direcaoGlobal;
	//Variavel para armazenar a direçao do Sprite em relaçao a Camera
	public Direction direcaoCamera;
	//Armazena a posicao em que o jogador esta em um sistema de coordenadas 2D
	public Vector2 posicaoTabuleiro;
}
