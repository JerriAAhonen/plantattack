using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
	[SerializeField] private float duration;
	[SerializeField] private float startScale;
	[SerializeField] private float endScale;
	[SerializeField] private List<GameObject> images;

	public float StartCountdown()
	{
		foreach (var go in images)
			go.SetActive(false);

		StartCoroutine(Routine());

		return duration;
	}

	private IEnumerator Routine()
	{
		var oneStepDur = duration / images.Count;

		for (var i = 0; i < images.Count; i++)
		{
			var go = images[i];

			go.transform.localScale = Vector3.one * startScale;
			go.SetActive(true);

			LeanTween.scale(go, Vector3.one * endScale, oneStepDur)
				.setOnComplete(() => go.SetActive(false));

			yield return new WaitForSeconds(oneStepDur);
		}
	}
}
