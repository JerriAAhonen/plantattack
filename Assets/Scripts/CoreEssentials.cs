using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEssentials : MonoBehaviour
{
	[SerializeField] private LevelController levelController;
	[SerializeField] private CoreUI coreUI;
	[SerializeField] private CameraController cameraController;
	[SerializeField] private CountdownController countdownController;

	public LevelController LevelController => levelController;
	public CoreUI CoreUI => coreUI;
	public CameraController CameraController => cameraController;
	public CountdownController CountdownController => countdownController;
}
