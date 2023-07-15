using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMaterialsController : MonoBehaviour
{
	[SerializeField] private List<Renderer> primaryColorRenderers;
	[SerializeField] private List<Renderer> secondaryColorRenderers;

	public void SetupMaterials(PlatformMaterials materials)
	{
		foreach (Renderer r in primaryColorRenderers)
			r.material = materials.Primary;
		foreach (Renderer r in secondaryColorRenderers)
			r.material = materials.Secondary;
	}
}
