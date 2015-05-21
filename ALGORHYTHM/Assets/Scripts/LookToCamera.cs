using UnityEngine;
using System.Collections;

public class LookToCamera : MonoBehaviour {
	
	void FixedUpdate() 
	{
		//transform.LookAt (myCamera,Vector3.up);
		transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
	}
}
