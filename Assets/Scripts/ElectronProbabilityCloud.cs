using UnityEngine;

public class ElectronProbabilityCloud : MonoBehaviour
{
	[Header("Mesh")]
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;

	[Header("Sphere")]
	public float diameter = 1.0f;
	public int segments = 32;
	public int rings = 16;
	Mesh sphereMesh;

	[Header("Noise")]
	public float noiseScale = 0.1f;
	Vector2 noiseSeed;
	Mesh noisedMesh;

	void Start()
	{
		// create sphere mesh
		sphereMesh = GenerateSphere();
		noisedMesh = new Mesh();

		// generate random seed for noise
		noiseSeed = new Vector2(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f));
	}

	void Update()
	{
		meshFilter.mesh = AddNoise(sphereMesh);
	}

	Mesh GenerateSphere()
	{
		Mesh mesh = new Mesh();

		Vector3[] vertices = new Vector3[(segments + 1) * (rings + 1)];
		Vector3[] normals = new Vector3[vertices.Length];
		int[] triangles = new int[segments * rings * 6];

		// sphere vertices and normals
		float phiStep = Mathf.PI / rings;
		float thetaStep = 2.0f * Mathf.PI / segments;

		int vertexIndex = 0;
		int triangleIndex = 0;

		for (int ring = 0; ring <= rings; ring++)
		{
			float phi = ring * phiStep;
			for (int segment = 0; segment <= segments; segment++)
			{
				float theta = segment * thetaStep;

				float x = Mathf.Sin(phi) * Mathf.Cos(theta);
				float y = Mathf.Cos(phi);
				float z = Mathf.Sin(phi) * Mathf.Sin(theta);

				vertices[vertexIndex] = new Vector3(x, y, z) * (diameter / 2f);

				// negate the normal (there was a bug where the meshes were inside out for some reason, idfk)
				normals[vertexIndex] = -vertices[vertexIndex].normalized;

				if (ring != rings && segment != segments)
				{
					int nextRing = (ring + 1) % (rings + 1);
					int nextSegment = (segment + 1) % (segments + 1);

					triangles[triangleIndex] = vertexIndex;
					triangles[triangleIndex + 1] = vertexIndex + 1;
					triangles[triangleIndex + 2] = vertexIndex + segments + 1;

					triangles[triangleIndex + 3] = vertexIndex + 1;
					triangles[triangleIndex + 4] = vertexIndex + segments + 2;
					triangles[triangleIndex + 5] = vertexIndex + segments + 1;

					triangleIndex += 6;
				}

				vertexIndex++;
			}
		}


		// set mesh
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.triangles = triangles;

		return mesh;
	}

	Mesh AddNoise(Mesh inputMesh)
	{
		Vector3[] vertices = inputMesh.vertices;
		Vector3[] normals = inputMesh.normals;
		int[] triangles = inputMesh.triangles;

		// loops through each vertex
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 vertex = vertices[i];

			// generate noise values for each axis

			float noiseValueX = Mathf.PerlinNoise(
				(vertex.x + noiseSeed.x + Time.time) * noiseScale,
				(vertex.z + noiseSeed.y + Time.time) * noiseScale
			);

			float noiseValueY = Mathf.PerlinNoise(
				(vertex.x + noiseSeed.x + Time.time) * noiseScale,
				(vertex.y + noiseSeed.y + Time.time) * noiseScale
			);

			float noiseValueZ = Mathf.PerlinNoise(
				(vertex.z + noiseSeed.x + Time.time) * noiseScale,
				(vertex.y + noiseSeed.y + Time.time) * noiseScale
			);

			// apply noise to vertex
			vertex.x += noiseValueX;
			vertex.y += noiseValueY;
			vertex.z += noiseValueZ;

			vertices[i] = vertex;
		}

		// set mesh properties
		noisedMesh.vertices = vertices;
		noisedMesh.normals = normals;
		noisedMesh.triangles = triangles;

		return noisedMesh;
	}

}
