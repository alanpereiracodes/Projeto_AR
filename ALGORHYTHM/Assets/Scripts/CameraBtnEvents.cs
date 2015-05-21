using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraBtnEvents : MonoBehaviour {
	
	public GameObject myCameraSuporte;
	public float rotateSpeed = 0.5f;
	public float rotationTime = 1f;

	public GameObject botaoRL; //referencia ao botao para poder desabilita-lo no processo de girar a camera
	public GameObject botaoRR;
	

	private int cameraYAxisRotation;
	//private float waitTime = 0.5f;

	void Awake()
	{
		cameraYAxisRotation = 0;
	}


	public void SetCameraToDefault()
	{
		if (myCameraSuporte != null) 
		{
			cameraYAxisRotation = 0;
//			myCamera.transform.position = new Vector3(12,10,-12);
//			Debug.Log ("ok");
//			myCamera.transform.rotation = Quaternion.Euler(30, 315, 0);
//			Debug.Log ("ok2");

			myCameraSuporte.transform.position = Vector3.zero;
			myCameraSuporte.transform.rotation = Quaternion.identity;

		}
	}

	public void RotateCameraToRight()
	{
		int temp = 0;

		switch(cameraYAxisRotation)
		{
			case 0:
				temp = 90;
				break;

			case 90:
				temp = 180;
				break;

			case 180:
				temp = 270;
				break;

			case 270:
				temp = 0;
				break;
		
		}

		cameraYAxisRotation = temp;

		Quaternion novaRotation = Quaternion.Euler (0, cameraYAxisRotation, 0);

		//myCameraSuporte.transform.rotation = novaRotation;
		StartCoroutine(rotateObject (myCameraSuporte.transform.rotation, novaRotation, rotationTime));
				
	}

	public void RotateCameraToLeft()
	{
		int temp = 0;
		
		switch(cameraYAxisRotation)
		{
		case 0:
			temp = 270;
			break;
			
		case 90:
			temp = 0;
			break;
			
		case 180:
			temp = 90;
			break;
			
		case 270:
			temp = 180;
			break;
			
		}
		
		cameraYAxisRotation = temp;

		Quaternion novaRotation = Quaternion.Euler (0, cameraYAxisRotation, 0);

		StartCoroutine(rotateObject (myCameraSuporte.transform.rotation, novaRotation, rotationTime));
		//myCameraSuporte.transform.rotation = novaRotation;
		//myCameraSuporte.transform.rotation = Quaternion.Lerp (myCameraSuporte.transform.rotation, novaRotation, Time.deltaTime);
		
	}

	//Coroutine
	IEnumerator /*moveObject*/ rotateObject(Quaternion source, Quaternion target, float overTime)
	{
		Button botao1 = botaoRL.GetComponent<Button> ();
		Button botao2 =	botaoRL.GetComponent<Button>();

		botao1.interactable = false;
		botao2.interactable = false;
		
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			//transform.position = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			myCameraSuporte.transform.rotation = Quaternion.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		//transform.position = target;
		myCameraSuporte.transform.rotation = target;
		botao1.interactable = true;
		botao2.interactable = true;
		Debug.Log ("coroutine REALLY WORKS!!");
	}


}
