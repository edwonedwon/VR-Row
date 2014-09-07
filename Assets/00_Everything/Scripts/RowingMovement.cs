using UnityEngine;
using System.Collections;

public class RowingMovement : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	Vector3 direction = Vector3.forward;

	float force_scale = 1.0f;
	float drag_scale = 0.33f;
	
	// Update is called once per frame
	void Update () {
		float velocity_magnitude = velocity.magnitude;

		// add to velocity based on displaced sin wave
		if (velocity_magnitude < 5.0f) {
				velocity += direction * (Mathf.Sin(Time.time * 2.0f) + 0.5f) * force_scale * Time.deltaTime;
				transform.position += velocity * Time.deltaTime;
		}

		// crap drag simulation
		if (velocity_magnitude > 0.1f) {
			velocity -= direction * drag_scale * Time.deltaTime;
		}
	}
}
