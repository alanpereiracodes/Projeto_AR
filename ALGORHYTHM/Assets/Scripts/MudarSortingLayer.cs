using UnityEngine;
using System.Collections;

public class MudarSortingLayer : MonoBehaviour {

	public string sortingLayerName;
	public int sortingLayerOrder;


	// Use this for initialization
	void Awake () 
	{
		this.gameObject.GetComponent<MeshRenderer>().sortingLayerName = sortingLayerName;
		this.gameObject.GetComponent<MeshRenderer>().sortingOrder = sortingLayerOrder;
		this.gameObject.GetComponent<Renderer>().sortingLayerName = sortingLayerName;
		this.gameObject.GetComponent<Renderer>().sortingOrder = sortingLayerOrder;
	}
}
