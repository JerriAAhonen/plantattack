using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlatformMaterials : ScriptableObject
{
	[SerializeField] private Material primaryColorMaterial;
	[SerializeField] private Material secondaryColorMaterial;

	public Material Primary => primaryColorMaterial;
	public Material Secondary => secondaryColorMaterial;
}
