using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Vector3 prevPos; // Where the player was before moving
	private Vector3 nextPos; // What is the next desired tile
	private Vector3 targetPos; // Where the player is currently moving to
	private Vector3 offset = Vector3.forward;

	private bool traveling;

	private LevelController levelController;
	private int playerIndex;

	public void Init(LevelController levelController, int playerIndex) 
	{
		this.levelController = levelController;
		this.playerIndex = playerIndex;
	}

	private void Start()
	{
		targetPos = transform.position + offset;
		StartCoroutine(LerpPosition(targetPos, 1));
	}

	private void Update()
	{
		nextPos = transform.position + transform.forward;

		if (Input.GetKeyDown(KeyCode.W))
		{
			// Turn upwards
			offset = Vector3.forward;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			// Turn left
			offset = Vector3.left;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			// Turn downwards
			offset = Vector3.back;
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			// Turn right
			offset = Vector3.right;
		}

		var input = targetPos + offset;
		if (levelController.CanMoveTo(input))
			nextPos = input;
	}

	private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
	{
		float time = 0;
		Vector3 startPosition = transform.position;
		targetPos = targetPosition;
		while (time < duration)
		{
			transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		transform.position = targetPosition;

		levelController.ClaimTile(playerIndex, targetPosition);

		StartCoroutine (LerpPosition(nextPos, 1));
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, nextPos);
	}
}
