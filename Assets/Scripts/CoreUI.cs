using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoreUI : Singleton<CoreUI>
{
	[SerializeField] private float scoreGainScale;
	[SerializeField] private float scoreGainDuration;
	[SerializeField] private List<TextMeshProUGUI> playerScoreLabels;

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
}
