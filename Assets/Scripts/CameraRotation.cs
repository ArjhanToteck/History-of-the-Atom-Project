using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	public Transform target;
	public float rotationSpeed = 100f;

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

			rotation.x = mouseDelta.x * rotationSpeed * Time.deltaTime;
			rotation.y = -mouseDelta.y * rotationSpeed * Time.deltaTime;
		}
		else
		{
			// get keyboard input
			rotation.x = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
			rotation.y = -Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
		}

		// handle lack of target
		if (!target)
		{
			return;
		}

		// rotate camera around target
		transform.RotateAround(target.position, transform.up, rotation.x);
		transform.RotateAround(target.position, transform.right, rotation.y);

		// update last mouse position
		lastMousePosition = Input.mousePosition;
	}
}
