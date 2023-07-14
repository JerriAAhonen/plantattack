using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	private class GridObject 
	{
		public GameObject platform;
		public Renderer renderer;
		public int owner = -1;
	}

	[SerializeField] private Vector2Int gridSize;
	[SerializeField] private float cellSize;
	[SerializeField] private GameObject platformPrefab;
	[SerializeField] private PlayerController playerControllerPrefab;
	[Space]
	[SerializeField] private Material neutralMaterial;
	[SerializeField] private Material player1Material;
	[SerializeField] private Material player2Material;

	private Plane inputPlane;
	private Grid<GridObject> grid;
	private ScoreController scoreController;

	private void Start()
	{
		scoreController = new ScoreController(2);
		inputPlane = new Plane(Vector3.up, Vector3.zero);

		var origin = Vector3.zero - (new Vector3(gridSize.x, 0, gridSize.y) / 2f);
		grid = new Grid<GridObject>(gridSize, cellSize, origin, true, false, (Vector2Int gridPos, Vector3 worldPos, int index) => 
		{
			var gridObject = new GridObject();
			gridObject.platform = Instantiate(platformPrefab, transform);
			gridObject.platform.transform.position = worldPos;
			gridObject.renderer = gridObject.platform.GetComponent<Renderer>();
			gridObject.renderer.material = neutralMaterial;
			return gridObject;
		});

		var pController = Instantiate(playerControllerPrefab);
		pController.Init(this, 0);
		var p1StartingTile = GetStartingTile(0);
		grid.GetGridAlignedPosition(p1StartingTile, out var p1StartingPos);
		pController.transform.position = p1StartingPos;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ClaimTile(0);
		}
		if (Input.GetMouseButtonDown(1))
		{
			ClaimTile(1);
		}
	}

	private void OnDrawGizmos()
	{
		grid?.OnDrawGizmos_DrawDebugData();
	}

	public bool CanMoveTo(Vector3 nextPos)
	{
		if (grid.GetGridPosition(nextPos, out var gridPos))
		{
			grid.GetValue(gridPos, out var gridObject);
			return true;
		}
		else
			return false;
	}

	private void ClaimTile(int playerIndex)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (inputPlane.Raycast(ray, out var enter))
		{
			var clickPos = ray.GetPoint(enter);
			ClaimTile(playerIndex, clickPos);
		}
	}

	public void ClaimTile(int playerIndex, Vector3 worldPos)
	{
		if (!grid.GetValue(worldPos, out var gridObject))
			return;

		gridObject.renderer.material = playerIndex == 0 ? player1Material : player2Material;
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

	private Vector2Int GetStartingTile(int playerIndex)
	{
		return playerIndex switch
		{
			0 => new Vector2Int(0, 0),
			1 => new Vector2Int(gridSize.x - 1, gridSize.y - 1),
			2 => new Vector2Int(0, gridSize.y - 1),
			_ => new Vector2Int(gridSize.x - 1, 0)
		};
		
	}
}
