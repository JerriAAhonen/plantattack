using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnimator : MonoBehaviour
{
	[SerializeField] private Transform model;
	[SerializeField] private float travelDistance;
	[SerializeField] private float travelduration;

	public void AnimateLandign()
	{
		LeanTween.moveLocalY(model.gameObject, -travelDistance, travelduration / 2)
			.setEase(LeanTweenType.easeInOutExpo)
			.setOnComplete(() =>
			{
				LeanTween.moveLocalY(model.gameObject, 0, travelduration / 2)
				.setEase(LeanTweenType.easeInOutExpo);
			});
	}
}
