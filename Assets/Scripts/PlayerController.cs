using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float travelDuration;
	[SerializeField] Transform model;

	private Vector3 prevPos;				// Where the player was before moving
	private Vector3 targetPos;				// Where the player is currently moving to
	private Vector3 offset;					// Direction of the input
	private Vector3 prevOffset;             // Offset on previous InputUpdate
	private Vector2 input;                  // User input
	private Vector3 nextPos;				// Where the input would lead the player

	private bool traveling;                 // Currently moving to next pos
	private float elapsedMovementTime;		// Used to calculate player movement in UpdateMovement()
	private Vector2Int prevTileGridPos;		// Previous tile the player was on

	private LevelController levelController;
	private int playerIndex;
	private bool isInitialised;

	public Transform Model => model;

	public void Init(LevelController levelController, int playerIndex) 
	{
		this.levelController = levelController;
		this.playerIndex = playerIndex;

		transform.position = levelController.GetStartingPosition(playerIndex);
		transform.eulerAngles = levelController.GetStartingRotation(playerIndex);

		offset = transform.forward;
		prevOffset = transform.forward;
		prevPos = transform.position;
		targetPos = transform.position + offset;
		traveling = true;

		prevTileGridPos = Vector2Int.one * 1000;

		isInitialised = true;
	}

	private void Update()
	{
		if (!isInitialised)
			return;

		input = InputController.Instance.GetInput(playerIndex);
		UpdateInput();
		UpdateNextPos();
		UpdateMovement();

		var gridPos = levelController.GetGridPos(transform.position);
		if (gridPos.x != prevTileGridPos.x || gridPos.y != prevTileGridPos.y)
		{
			levelController.SetTileOccupied(prevTileGridPos, gridPos);
			levelController.ClaimTile(playerIndex, gridPos);
			prevTileGridPos = gridPos;
		}
	}

	private void UpdateInput()
	{
		if (input.y > 0) // Turn upwards
			offset = Vector3.forward;
		if (input.x < 0) // Turn left
			offset = Vector3.left;
		if (input.y < 0) // Turn downwards
			offset = Vector3.back;
		if (input.x > 0) // Turn right
			offset = Vector3.right;
	}

	private void UpdateNextPos()
	{
		nextPos = targetPos + offset;
		if (!traveling && levelController.CanMoveTo(nextPos))
		{
			targetPos = nextPos;
			prevOffset = offset;
			traveling = true;
		}
		else
		{
			// Reset offset if new selected one is invalid
			offset = prevOffset;
		}
	}

	private void UpdateMovement()
	{
		if (traveling)
		{
			float t = elapsedMovementTime / travelDuration;
			// Makes the transition smooth at start and end
			// https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
			t = t * t * (3f - 2f * t);
			transform.position = Vector3.Lerp(prevPos, targetPos, t);
			elapsedMovementTime += Time.deltaTime;

			if (elapsedMovementTime >= travelDuration)
			{
				transform.position = targetPos;
				prevPos = targetPos;
				elapsedMovementTime = 0;
				traveling = false;
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(targetPos, 0.3f);

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(nextPos, 0.2f);
	}
}
