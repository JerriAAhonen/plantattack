using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMaterialsController : MonoBehaviour
{
	[SerializeField] private Renderer potRenderer;
	[SerializeField] private List<Renderer> primaryColorRenderers;
	[SerializeField] private List<Renderer> secondaryColorRenderers;
	[SerializeField] private List<Renderer> tetriaryColorRenderers;

	public void SetupMaterials(PlayerMaterials materials)
	{
		var potMaterials = potRenderer.sharedMaterials;
		potMaterials[0] = materials.Primary;
		potRenderer.SetMaterials(potMaterials.ToList());

		foreach (Renderer r in primaryColorRenderers)
			r.material = materials.Primary;
		foreach (Renderer r in secondaryColorRenderers)
			r.material = materials.Secondary;
		foreach (Renderer r in tetriaryColorRenderers)
			r.material = materials.Tetriary;
	}
}
