using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 public class Mapa{

	public List<int> linha = new List<int> ();

}
 */


public class Controlador : MonoBehaviour { //public class GameManager : MonoBehaviour {


	//Variaveis
	public List<GameObject> tiles;
	//public int mapSizeX = 10;
	//public int mapSizeY = 5;

	//public List<List<int>> mapaTeste = new List<List<int>>();
	//public List<Mapa> maaaapa = new List<Mapa> (); 

	//
	private List<List<Tile>> map = new List<List<Tile>>();

	private int mapSizeX = 12;
	private int mapSizeY = 12;

	private float valorCorrecaoX = 0.5f;
	private float valorCorrecaoY = -0.5f;
	
	private int[,] mapa = new int[12, 12] //y,x
	{ 
//				{5,5,5,4,5,0,2,2,0,0,0,0},
//				{5,5,4,2,2,5,2,5,5,5,5,0},
//				{5,2,4,2,2,5,2,2,5,5,5,0},
//				{2,2,4,2,5,5,5,5,0,0,4,0},
//				{5,4,5,5,5,2,4,4,0,0,4,0},
//				{5,2,2,2,2,5,0,2,0,0,4,0},
//				{2,5,5,2,5,5,0,0,0,0,2,0},
//				{5,2,5,5,2,2,0,0,0,2,2,2},
//				{5,5,5,2,2,2,0,0,5,5,5,2},
//				{5,5,0,0,0,0,0,0,5,5,5,5},
//				{0,0,0,0,0,0,2,2,2,2,2,2},
//				{0,0,0,0,0,0,2,2,2,4,2,2}

		//Training Room
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1}
	};
#region mapas

		//		{1,1,0,1,1,1,1,0,1,1,1}, 
		//		{0,1,1,0,1,1,0,1,1,0,1},
		//		{0,1,0,0,0,0,0,0,1,0,1},
		//		{0,0,0,1,0,0,1,0,0,0,1},
		//		{0,0,0,0,0,0,0,0,0,0,1},
		//		{1,0,0,0,0,0,0,0,0,1,1},
		//		{1,1,0,1,1,1,1,0,1,1,1},
		//		{1,0,1,1,1,1,1,1,0,1,1},
		//		{1,1,1,0,0,0,1,1,1,1,1},
		//		{1,1,1,0,1,0,0,1,1,1,1},
		//		{1,1,1,0,0,0,0,1,1,1,1}
		
		//		{1,1,1,1,1,1,1,1,1,1,1},
		//		{0,1,1,1,1,1,1,1,0,0,0},
		//		{0,1,1,1,1,1,1,0,0,0,0},
		//		{0,1,1,1,1,1,1,0,0,0,0},
		//		{0,2,2,0,0,2,2,0,0,0,0},
		//		{0,2,2,0,0,2,2,0,0,0,0},
		//		{0,2,2,0,0,2,2,0,0,0,0},
		//		{1,1,1,1,1,1,1,1,1,1,1},
		//		{1,1,1,1,1,1,1,1,1,1,1},
		//		{1,1,1,1,1,1,1,1,1,1,1},
		//		{1,1,1,1,1,0,0,1,0,0,0}
		
//		{1,1,1,1,0,1,0,1,0,1,1,1},
//		{1,1,1,1,1,1,1,1,1,1,1,1},
//		{1,1,1,1,0,0,0,0,1,1,1,1},
//		{1,1,1,1,1,0,0,1,1,1,1,1},
//		{1,1,0,1,1,0,0,1,1,0,1,1},
//		{1,1,0,0,0,0,0,0,0,0,1,1},
//		{1,1,0,0,0,0,0,0,0,0,1,1},
//		{1,1,0,1,1,0,0,1,1,0,1,1},
//		{1,1,1,1,1,0,0,1,1,1,1,1},
//		{1,1,1,1,0,0,0,0,1,1,1,1},
//		{1,1,1,1,1,1,1,1,1,1,1,1},
//		{1,1,1,1,1,1,0,1,1,1,1,1}

		//Zerada
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0},
//		{0,0,0,0,0,0,0,0,0,0,0,0}

		//Mario new int[16, 12] 
//		{0,0,0,3,3,3,3,3,0,0,0,0},
//		{0,0,3,3,3,3,3,3,3,3,3,0},
//		{0,0,2,2,2,4,4,2,4,0,0,0},
//		{0,2,4,2,4,4,4,2,4,4,4,0},
//		{0,2,4,2,2,4,4,4,2,4,4,4},
//		{0,2,2,4,4,4,4,2,2,2,2,0},
//		{0,0,0,4,4,4,4,4,4,4,0,0},
//		{0,0,2,2,3,2,2,2,0,0,0,0},
//		{0,2,2,2,3,2,2,3,2,2,2,0},
//		{2,2,2,2,3,3,3,3,2,2,2,2},
//		{4,4,2,3,2,3,3,2,3,2,4,4},
//		{4,4,4,3,3,3,3,3,3,4,4,4},
//		{4,4,3,3,3,3,3,3,3,3,4,4},
//		{0,0,3,3,3,0,0,3,3,3,0,0},
//		{0,2,2,2,0,0,0,0,2,2,2,0},
//		{2,2,2,2,0,0,0,0,2,2,2,2}
	
#endregion mapas
	
//	private int[,] mapHeight = new int[12, 12]
//	{ 
//		//		{1,1,1,1,1,1,1,1,1,1,1}, 
//		//		{2,2,1,9,1,1,0,1,1,0,1},
//		//		{0,1,4,3,9,4,9,2,1,0,1},
//		//		{0,0,0,1,12,9,1,0,0,0,1},
//		//		{0,0,0,0,0,0,0,0,0,0,1},
//		//		{1,0,0,0,0,0,0,0,0,1,1},
//		//		{1,1,0,1,1,1,1,0,1,1,1},
//		//		{1,0,1,1,1,1,1,1,0,1,1},
//		//		{1,1,1,3,2,1,1,1,1,1,1},
//		//		{1,1,1,4,1,1,9,1,1,1,1},
//		//		{1,1,1,5,6,7,8,1,1,1,1}
//		
//		{3,2,1,1,1,1,1,2,3,2,2,1},
//		{0,1,1,1,1,1,1,1,0,0,0,1},
//		{0,1,1,2,2,0,0,0,0,0,0,1},
//		{0,0,0,0,0,0,0,0,0,0,0,1},
//		{0,0,0,0,0,0,0,0,0,0,0,1},
//		{0,0,0,0,0,0,0,0,0,0,0,1},
//		{0,0,0,0,0,0,0,0,0,0,0,1},
//		{2,0,0,0,0,0,0,0,0,0,2,1},
//		{1,0,0,0,1,0,0,0,0,1,1,1},
//		{1,1,1,1,1,1,1,1,1,1,1,1},
//		{1,1,1,1,1,0,0,1,0,0,0,1},
//		{1,1,1,1,1,0,0,1,0,0,0,1}
//		
//	};
	


	// Use this for initialization
	void Start () {	
		generateMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void generateMap ()
	{
		map = new List<List<Tile>>();
		for(int j = 0; j < mapSizeY; j++) 
		{
			List<Tile> row = new List<Tile>();
			for(int i = 0; i < mapSizeX; i++)
			{
//				Tile tile = ((GameObject)Instantiate(tilePrefab, new Vector3(i - Mathf.Floor(mapSizeX/2) , 0 , j - Mathf.Floor(mapSizeY/2) ), Quaternion.identity)).GetComponent<Tile>();
//				             tile.gridPosition = new Vector2(i,j);
//				             row.Add(tile);

				Tile tile = /*null;
				//GameObject.Instantiate(tiles[0],new Vector3((mapSizeX-j)-1, 0 ,(mapSizeY-i)-1),Quaternion.identity);
				tile = */((GameObject)Instantiate(tiles[mapa[j,i]], new Vector3(i - Mathf.Floor(mapSizeX/2)+valorCorrecaoX , tiles[mapa[j,i]].transform.position.y , j - Mathf.Floor(mapSizeY/2)+valorCorrecaoY  ), Quaternion.identity)).GetComponent<Tile>();

//				switch(mapa[j,i])
//				{
//				case 0: //GameObject.Instantiate(tiles[0],new Vector3((mapSizeX-j)-1, 0 ,(mapSizeY-i)-1),Quaternion.identity);
//					tile = ((GameObject)Instantiate(tiles[0], new Vector3(i - Mathf.Floor(mapSizeX/2)+0.5f , -0.5f , -j + Mathf.Floor(mapSizeY/2)-0.5f  ), Quaternion.identity)).GetComponent<Tile>();
//					break;
//				case 1: //GameObject.Instantiate(tiles[0],new Vector3((mapSizeX-j)-1, 0 ,(mapSizeY-i)-1),Quaternion.identity);
//					tile = ((GameObject)Instantiate(tiles[1], new Vector3(i - Mathf.Floor(mapSizeX/2)+0.5f  , 0 , -j + Mathf.Floor(mapSizeY/2)-0.5f  ), Quaternion.identity)).GetComponent<Tile>();
//					break;
//
//				case 2: //GameObject.Instantiate(tiles[2],new Vector3(i,0,j),Quaternion.identity);
//					tile = ((GameObject)Instantiate(tiles[2], new Vector3(i - Mathf.Floor(mapSizeX/2)+0.5f  , 0 , -j + Mathf.Floor(mapSizeY/2)-0.5f  ), Quaternion.identity)).GetComponent<Tile>();
//					break;
//
//				case 3:
//					tile = ((GameObject)Instantiate(tiles[3], new Vector3(i - Mathf.Floor(mapSizeX/2)+0.5f  , 0 , -j + Mathf.Floor(mapSizeY/2)-0.5f  ), Quaternion.identity)).GetComponent<Tile>();			
//					break;
//
//				case 4: 
//					tile = ((GameObject)Instantiate(tiles[4], new Vector3(i - Mathf.Floor(mapSizeX/2)+0.5f  , 0 , -j + Mathf.Floor(mapSizeY/2)-0.5f  ), Quaternion.identity)).GetComponent<Tile>();				
//					break;
//
//				case 5: 
//					tile = ((GameObject)Instantiate(tiles[5], new Vector3(i - Mathf.Floor(mapSizeX/2)+0.5f , 0 , -j + Mathf.Floor(mapSizeY/2)-0.5f  ), Quaternion.identity)).GetComponent<Tile>();				
//					break;
//				
//				}
			
				if(mapa[j,i]!= 0)
				{
					tile.posicaoTabuleiro = new Vector2(i,/*mapSizeY - */j);
					row.Add(tile);
				}

			}

			map.Add(row);

		}

	}
}
