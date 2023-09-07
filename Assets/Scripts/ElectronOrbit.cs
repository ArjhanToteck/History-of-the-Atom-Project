using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronOrbit : MonoBehaviour
{
	public Transform nucleus;
	public float rotationSpeed = 100.0f;
	public Vector3 axis;
	public Vector3 relativeAxis;

	public LineRenderer lineRenderer;
	
	int numberOfPoints = 361;
	float orbitLineWidth = 0.05f;
	float orbitRadius;


	void Start()
	{
		// draw orbits

		// initialize line renderer
		lineRenderer.positionCount = numberOfPoints;
		lineRenderer.startWidth = orbitLineWidth;
		lineRenderer.endWidth = orbitLineWidth;

		// get orbit radius (distance from nucleus)
		orbitRadius = Vector3.Distance(transform.position, nucleus.position);
	}

	void Update()
	{
		// calculate rotation step
		float rotationStep = rotationSpeed * Time.deltaTime;

		// get relative axis
		relativeAxis = Quaternion.Euler(nucleus.rotation.eulerAngles) * axis;

		// rotate electron around nucleus
		transform.RotateAround(nucleus.position, relativeAxis, rotationStep);
		
		// get orbital path points
		Vector3[] orbitalPath = CalculateOrbitalPath();
		lineRenderer.SetPositions(orbitalPath);
	}

	Vector3[] CalculateOrbitalPath()
	{
		Vector3[] path = new Vector3[numberOfPoints];

		Vector3 startVector = transform.position - nucleus.position;
		Quaternion rotation = Quaternion.AngleAxis(361.0f / numberOfPoints, relativeAxis);

		for (int i = 0; i < numberOfPoints; i++)
		{
			path[i] = nucleus.position + startVector;
			startVector = rotation * startVector;
		}

		return path;
	}
}
