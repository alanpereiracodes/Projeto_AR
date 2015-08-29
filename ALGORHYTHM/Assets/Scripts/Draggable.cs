using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;

	GameObject placeholder = null;
	
	public void OnBeginDrag(PointerEventData eventData) {
		if (!ControladorGeral.referencia.listaEmExecucao) 
		{
			Debug.Log ("OnBeginDrag");
		
			placeholder = new GameObject ();
			placeholder.transform.SetParent (this.transform.parent);
			LayoutElement le = placeholder.AddComponent<LayoutElement> ();
			le.preferredWidth = this.GetComponent<LayoutElement> ().preferredWidth;
			le.preferredHeight = this.GetComponent<LayoutElement> ().preferredHeight;
			le.flexibleWidth = 0;
			le.flexibleHeight = 0;

			placeholder.transform.SetSiblingIndex (this.transform.GetSiblingIndex ());
		
			parentToReturnTo = this.transform.parent;
			placeholderParent = parentToReturnTo;
			this.transform.SetParent (this.transform.parent.parent);
		
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}
	
	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("OnDrag");
		if (!ControladorGeral.referencia.listaEmExecucao) {
			this.transform.position = eventData.position;

			if (placeholder.transform.parent != placeholderParent)
				placeholder.transform.SetParent (placeholderParent);

			int newSiblingIndex = placeholderParent.childCount;

			for (int i=0; i < placeholderParent.childCount; i++) {
				if (this.transform.position.x < placeholderParent.GetChild (i).position.x && Mathf.Abs (this.transform.position.y - placeholderParent.GetChild (i).position.y) <= 5) {

					newSiblingIndex = i;

					if (placeholder.transform.GetSiblingIndex () < newSiblingIndex)
						newSiblingIndex--;

					break;
				}
			//
			else if (this.transform.position.y < placeholderParent.GetChild (i).position.y && Mathf.Abs (this.transform.position.x - placeholderParent.GetChild (i).position.x) <= 5) {
				
					newSiblingIndex = i;
				
					if (placeholder.transform.GetSiblingIndex () < newSiblingIndex)
						newSiblingIndex--;
                
					break;
				}
			}

			placeholder.transform.SetSiblingIndex (newSiblingIndex);
		}
	}
	
	public void OnEndDrag(PointerEventData eventData) {
		if (!ControladorGeral.referencia.listaEmExecucao) {
			Debug.Log ("OnEndDrag");
			this.transform.SetParent (parentToReturnTo);
			this.transform.SetSiblingIndex (placeholder.transform.GetSiblingIndex ());
			GetComponent<CanvasGroup> ().blocksRaycasts = true;

			Destroy (placeholder);

			//Soltou	
			CreateProgramList.referencia.listaPrograma.Clear ();
		
			for (int i=0; i<this.transform.parent.childCount; i++) {
				if (this.transform.parent.GetChild (i).gameObject.GetComponent<Comando> () != null) {
					CreateProgramList.referencia.listaPrograma.Add (this.transform.parent.GetChild (i).gameObject.GetComponent<Comando> ());
					//	Debug.Log ("Comando: "+this.transform.parent.GetChild(i).gameObject.GetComponent<Comando>().nome + " adicionado!");
				}

				//foreach(Comando cmd in CreateProgramList.referencia.listaPrograma)
				//Debug.Log("Comando: "+cmd.nome+", ");
			}
		}
	}
	
	
	
}
