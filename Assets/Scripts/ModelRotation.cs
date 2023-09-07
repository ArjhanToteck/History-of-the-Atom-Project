using UnityEngine;

public class ModelRotation : MonoBehaviour
{
	public Transform model;
	public float rotationSpeed = 150f;

	Vector3 lastMousePosition;

	void Start()
	{
		lastMousePosition = Input.mousePosition;
	}

	void Update()
	{
		// handle lack of model
		if (!model)
		{
			return;
		}

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

		// rotate model (I used RotateAround because originally, the camera rotated around it, but for some weird fucking reason, Rotate had issues, so whatever)
		model.RotateAround(model.position, transform.up, rotation.x);
		model.RotateAround(model.position, transform.right, rotation.y);

		// update last mouse position
		lastMousePosition = Input.mousePosition;
	}
}
