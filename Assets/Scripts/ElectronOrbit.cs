using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronOrbit : MonoBehaviour
{
	public Transform nucleus;
	public float rotationSpeed = 100.0f;
	public Vector3 axis;

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

		// get orbital path points
		Vector3[] orbitalPath = CalculateOrbitalPath();
		lineRenderer.SetPositions(orbitalPath);
	}

	void Update()
	{
		// calculate rotation step
		float rotationStep = rotationSpeed * Time.deltaTime;

		// rotate electron around nucleus
		transform.RotateAround(nucleus.position, axis, rotationStep);
	}

	Vector3[] CalculateOrbitalPath()
	{
		Vector3[] path = new Vector3[numberOfPoints];

		Vector3 startVector = transform.position - nucleus.position;
		Vector3 perpendicularAxis = Vector3.Cross(axis, startVector).normalized;
		Quaternion rotation = Quaternion.AngleAxis(361.0f / numberOfPoints, axis);

		for (int i = 0; i < numberOfPoints; i++)
		{
			path[i] = nucleus.position + startVector;
			startVector = rotation * startVector;
		}

		return path;
	}
}
