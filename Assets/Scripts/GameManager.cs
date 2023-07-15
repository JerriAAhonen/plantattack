using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : PersistentSingleton<GameManager>
{
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private List<Material> playerMaterials;
	
	private LevelController levelController;
	private CoreUI ui;

	private List<PlayerController> players = new();

	private int playerCount = 2;

	private void Start()
	{
		// TEMP
		StartCoroutine(OnCoreSceneLoaded());
	}

	private void LateUpdate()
	{
		for (int i = 0; i < players.Count; ++i)
		{
			ui.UpdateTooltip(i, players[i].transform.position);
		}
	}

	/// <summary>
	/// Call this when a new core scene has fully loaded and is ready to receive players
	/// </summary>
	private IEnumerator OnCoreSceneLoaded()
	{
		yield return new WaitForEndOfFrame();

		// Fetch all references from scene
		GetCoreEssentials();

		ui.InitPlayerScores(playerCount);
		SpawnPlayers();
	}

	private void GetCoreEssentials()
	{
		var coreEssentials = FindObjectOfType<CoreEssentials>();
		levelController = coreEssentials.LevelController;
		ui = coreEssentials.CoreUI;
	}

	private void SpawnPlayers()
	{
		for (int i = 0; i < playerCount; i++)
		{
			var playerController = Instantiate(playerPrefab).GetComponent<PlayerController>();
			playerController.Init(levelController, i);

			playerController.Model.GetComponent<Renderer>().material = playerMaterials[i];

			players.Add(playerController);
		}
	}
}
