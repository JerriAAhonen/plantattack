using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Vector3 offset;
	[SerializeField] private float movementSpeed = 3f;

	private readonly List<Transform> players = new();
	private Bounds bounds = new();


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
	}
}
