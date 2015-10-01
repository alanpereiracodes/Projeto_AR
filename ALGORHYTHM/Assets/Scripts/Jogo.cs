using UnityEngine;
using System.Collections;

public class Jogo {

	public int idJogo;
	public int pontuacaoTotal; //total de cubos
	public string dataJogoCriado;
	public string dataJogoSalvo;
	public int numeroFaseLiberada; //Ultima fase que foi liberada;
	public int capituloAtual; //Ultimo capitulo em que salvou o jogo e saiu. Para quando retornar, retornar ja na tela de seleçao desse capitulo
	public int idPerfilJogador; //Foreign Key do Perfil

}
