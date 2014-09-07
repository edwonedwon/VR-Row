using UnityEngine;
using System.Collections;

public class BoatMove : MonoBehaviour {

	public float speed;

	void Start () 
	{
	
	}
	
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		transform.position += new Vector3(0,0,-speed);
	}
}
