using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerMaterials : ScriptableObject
{
	[SerializeField] private Material primaryColorMaterial;
	[SerializeField] private Material secondaryColorMaterial;
	[SerializeField] private Material tetriaryColorMaterial;

	public Material Primary => primaryColorMaterial;
	public Material Secondary => secondaryColorMaterial;
	public Material Tetriary => tetriaryColorMaterial;
}
