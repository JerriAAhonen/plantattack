using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Vector3 offset;
	[SerializeField] private float movementSpeed = 3f;
	[SerializeField] private float zoomSpeed;
	[SerializeField] private float minFov;
	[SerializeField] private float maxFov;
	[SerializeField] private float zoomLimiter = 50f;

	private readonly List<Transform> players = new();
	private Camera cam;
	private Bounds bounds = new();
	private float zoom;

	private void Awake()
	{
		cam = GetComponent<Camera>();
	}

	public void AddPlayer(Transform player) => players.Add(player);

	private void LateUpdate()
	{
		if (players.Count == 0) 
			return;

		bounds.min = Vector3.zero; 
		bounds.max = Vector3.zero;

		foreach (Transform t in players)
			bounds.Encapsulate(t.position);

		transform.position = Vector3.Lerp(transform.position, bounds.center + offset, movementSpeed * Time.deltaTime);
		float newZoom = Mathf.Lerp(minFov, maxFov, bounds.extents.z / zoomLimiter);
		zoom = Mathf.Lerp(zoom, newZoom, zoomSpeed * Time.deltaTime);
		cam.fieldOfView = zoom;
	}
}
