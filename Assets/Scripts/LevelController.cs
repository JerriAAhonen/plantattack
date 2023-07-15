using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	private class GridObject 
	{
		public GameObject platform;
		public PlatformMaterialsController platformMaterialsController;
		public int owner = -1;
		public bool occupied;
	}

	[SerializeField] private Vector2Int gridSize;
	[SerializeField] private float cellSize;
	[SerializeField] private GameObject platformPrefab;
	[SerializeField] private PlayerController playerControllerPrefab;
	[Space]
	[SerializeField] private PlatformMaterials neutralMaterial;
	[SerializeField] private List<PlatformMaterials> playerPlatformMaterials;

	private Plane inputPlane;
	private Grid<GridObject> grid;
	private ScoreController scoreController;

	private void Awake()
	{
		scoreController = new ScoreController(2);
		inputPlane = new Plane(Vector3.up, Vector3.zero);

		var origin = Vector3.zero - (new Vector3(gridSize.x, 0, gridSize.y) / 2f);
		grid = new Grid<GridObject>(gridSize, cellSize, origin, true, false, (Vector2Int gridPos, Vector3 worldPos, int index) => 
		{
			var gridObject = new GridObject();
			gridObject.platform = Instantiate(platformPrefab, transform);
			gridObject.platform.transform.position = worldPos;
			gridObject.platformMaterialsController = gridObject.platform.GetComponent<PlatformMaterialsController>();
			gridObject.platformMaterialsController.SetupMaterials(neutralMaterial);
			return gridObject;
		});
	}

	private void OnDrawGizmos()
	{
		grid?.OnDrawGizmos_DrawDebugData();

		if (Application.isPlaying)
			return;

		var bottomLeft = Vector3.zero - (new Vector3(gridSize.x, 0, gridSize.y) / 2f);
		var topLeft = bottomLeft + cellSize * gridSize.y * Vector3.forward;
		var topRight = topLeft + cellSize * gridSize.x * Vector3.right;
		var bottomRight = bottomLeft + cellSize * gridSize.x * Vector3.right;

		Gizmos.DrawLine(bottomLeft, topLeft);
		Gizmos.DrawLine(topLeft, topRight);
		Gizmos.DrawLine(topRight, bottomRight);
		Gizmos.DrawLine(bottomRight, bottomLeft);
	}

	public bool CanMoveTo(Vector3 nextPos)
	{
		if (grid.GetGridPosition(nextPos, out var gridPos, true))
		{
			grid.GetValue(gridPos, out var gridObject);
			return !gridObject.occupied;
		}
		else
			return false;
	}

	public void ClaimTile(int playerIndex, Vector2Int gridPos)
	{
		if (!grid.GetValue(gridPos, out var gridObject))
			return;

		gridObject.platformMaterialsController.SetupMaterials(playerPlatformMaterials[playerIndex]);
		var prevOwner = gridObject.owner;
		var newOwner = playerIndex;
		gridObject.owner = newOwner;

		if (prevOwner == newOwner)
			return;

		if (prevOwner >= 0 && prevOwner != playerIndex)
		{
			scoreController.SubstractScore(prevOwner);
			CoreUI.Instance.SetPlayerScore(prevOwner, scoreController.GetScore(prevOwner));
		}

		scoreController.AddScore(newOwner);
		CoreUI.Instance.SetPlayerScore(newOwner, scoreController.GetScore(newOwner));
	}

	public void SetTileOccupied(Vector2Int previousGridPos, Vector2Int newGridPos)
	{
		if (grid.GetValue(previousGridPos, out var prevGridObject, true))
			prevGridObject.occupied = false;

		if (grid.GetValue(newGridPos, out var gridObject, true))
			gridObject.occupied = true;
	}

	public void SetTileOccupied(Vector3 previousWorldPos, Vector3 newWorldPos)
	{
		grid.GetValue(previousWorldPos, out var prevGridObject);
		prevGridObject.occupied = false;

		grid.GetValue(newWorldPos, out var gridObject);
		gridObject.occupied = true;
	}

	public Vector3 GetStartingPosition(int playerIndex)
	{
		var startingTile =  playerIndex switch
		{
			0 => new Vector2Int(0, 0),
			1 => new Vector2Int(gridSize.x - 1, gridSize.y - 1),
			2 => new Vector2Int(0, gridSize.y - 1),
			_ => new Vector2Int(gridSize.x - 1, 0)
		};
		grid.GetGridAlignedPosition(startingTile, out var startingPos);
		return startingPos;
	}

	public Vector3 GetStartingRotation(int playerIndex)
	{
		return playerIndex switch
		{
			0 => Vector3.zero,
			1 => new Vector3(0, 180, 0),
			2 => new Vector3(0, 90, 0),
			_ => new Vector3(0, 270, 0)
		};
	}

	public Vector2Int GetGridPos(Vector3 worldPos)
	{
		grid.GetGridPosition(worldPos, out var gridPosition);
		return gridPosition;
	}

	/*private void ClaimTile(int playerIndex)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (inputPlane.Raycast(ray, out var enter))
		{
			var clickPos = ray.GetPoint(enter);
			ClaimTile(playerIndex, clickPos);
		}
	}*/
}
