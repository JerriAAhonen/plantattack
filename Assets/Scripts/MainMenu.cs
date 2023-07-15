using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private ButtonAnimator playButton;

	private void Start()
	{
		playButton.OnClick += OnPlayClicked;
	}

	private void OnPlayClicked()
	{
		LevelLoader.LoadLevel(1);
	}
}
