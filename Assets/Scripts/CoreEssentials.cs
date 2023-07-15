using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEssentials : MonoBehaviour
{
	[SerializeField] private LevelController levelController;
	[SerializeField] private CoreUI coreUI;

	public LevelController LevelController => levelController;
	public CoreUI CoreUI => coreUI;
}
