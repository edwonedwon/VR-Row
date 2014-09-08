using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

	public float rotate_scale = 100.0f;
	Vector3 lastPosition = Vector3.zero;

	void Start() {
		lastPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		Vector3 move = transform.position - lastPosition;
		float movescale = move.magnitude * rotate_scale;
		transform.Rotate(Vector3.forward * Time.deltaTime * movescale);	
		lastPosition = transform.position;
	}
}
