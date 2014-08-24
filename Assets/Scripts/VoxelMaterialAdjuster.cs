using UnityEngine;
using System.Collections;

public class VoxelMaterialAdjuster : MonoBehaviour
{
	void OnDestroy()
	{
		DestroyImmediate(renderer.material);
	}

	public void SetType(VoxelType type)
	{
		renderer.materials[0].color = TypeToColor(type);
	}

	Color TypeToColor(VoxelType type)
	{
		switch (type)
		{
			case VoxelType.Empty: return Color.clear;
			case VoxelType.Red: return Color.red;
			case VoxelType.Green: return Color.green;
			case VoxelType.Blue: return Color.blue;
			case VoxelType.Cyan: return Color.cyan;
			case VoxelType.Yellow: return Color.yellow;
			case VoxelType.Magenta: return Color.magenta;
			case VoxelType.Black: return Color.black;
			case VoxelType.White: return Color.white;
		}

		throw new System.Exception("Invalid code path.");
	}
}
