using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
	public event Action OnClick;

	public void OnPointerEnter()
	{
		LeanTween.cancel(gameObject);
		LeanTween.scale(gameObject, Vector3.one * 1.3f, 0.5f).setEase(LeanTweenType.easeOutBack);
	}

	public void OnPointerExit()
	{
		LeanTween.cancel(gameObject);
		LeanTween.scale(gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutBack);
	}

	public void OnClicked()
	{
		LeanTween.cancel(gameObject);
		LeanTween.scale(gameObject, Vector3.one * 0.7f, 0.1f)
			.setEase(LeanTweenType.easeOutCubic)
			.setOnComplete(() =>
			{
				LeanTween.scale(gameObject, Vector3.one, 0.2f)
					.setEase(LeanTweenType.easeOutExpo)
					.setOnComplete(() =>
					{
						Debug.Log("Clicked!");
						OnClick?.Invoke();
					});
			});
	}
}
