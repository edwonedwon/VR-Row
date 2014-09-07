using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

	public float degrees_per_second = 45.0f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * Time.deltaTime * degrees_per_second);	
	}
}
