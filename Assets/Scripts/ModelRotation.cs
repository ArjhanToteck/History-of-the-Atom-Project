using UnityEngine;

public class ModelRotation : MonoBehaviour
{
	public Transform target;
	public float rotationSpeed = 150f;

	Vector3 lastMousePosition;

	void Start()
	{
		lastMousePosition = Input.mousePosition;
	}

	void Update()
	{
		Vector2 rotation = new Vector2();

		// get mouse input
		if (Input.GetMouseButton(0))
		{
			Vector3 currentMousePosition = Input.mousePosition;
			Vector3 mouseDelta = currentMousePosition - lastMousePosition;

			rotation.x = -mouseDelta.x * rotationSpeed * Time.deltaTime;
			rotation.y = mouseDelta.y * rotationSpeed * Time.deltaTime;
		}
		else
		{
			// get keyboard input
			rotation.x = -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
			rotation.y = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
		}

		// handle lack of target
		if (!target)
		{
			return;
		}

		// rotate target (I used RotateAround because originally, the camera rotated around it, but for some weird fucking reason, Rotate had issues, so whatever)
		target.RotateAround(target.position, transform.up, rotation.x);
		target.RotateAround(target.position, transform.right, rotation.y);

		// update last mouse position
		lastMousePosition = Input.mousePosition;
	}
}
