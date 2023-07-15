using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoreUI : Singleton<CoreUI>
{
	[SerializeField] private float scoreGainScale;
	[SerializeField] private float scoreGainDuration;
	[SerializeField] private Transform scoreLabelsParent;
	[SerializeField] private GameObject scoreLabelPrefab;
	[SerializeField] private float tooltipVerticalOffset;
	[SerializeField] private List<RectTransform> tooltips;
	
	private readonly List<TextMeshProUGUI> playerScoreLabels = new();

	public void InitPlayerScores(int playerCount)
	{
		for (int i = 0; i < playerCount; i++)
		{
			var scoreLabel = Instantiate(scoreLabelPrefab, scoreLabelsParent).GetComponent<TextMeshProUGUI>();
			playerScoreLabels.Add(scoreLabel);
		}
	}

	public void SetPlayerScore(int playerIndex, int score)
	{
		if (playerIndex < 0 || playerIndex >= playerScoreLabels.Count)
			return;

		var label = playerScoreLabels[playerIndex];

		label.text = score.ToString();
		label.transform.localScale = Vector3.one * scoreGainScale;

		LeanTween.cancel(label.gameObject);
		LeanTween.scale(label.gameObject, Vector3.one, scoreGainDuration).setEase(LeanTweenType.easeOutBack);
	}

	public void UpdateTooltip(int playerIndex, Vector3 playerWorldPos)
	{
		tooltips[playerIndex].anchoredPosition = 
			Camera.main.WorldToScreenPoint(playerWorldPos) + Vector3.up * tooltipVerticalOffset;
	}
}
