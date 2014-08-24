using UnityEngine;
using System.Collections;

public class VoxelPicker : MonoBehaviour
{
	[SerializeField]
	VoxelGrid grid;

	float distanceFromOrigin;

	void Start()
	{
		distanceFromOrigin = Mathf.Abs(transform.position.z);
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Pick();
		}
	}

	public void Pick()
	{
		var point = camera.ScreenPointToRay(Input.mousePosition).GetPoint(distanceFromOrigin);
		var coord = new GridCoord(point);

		if (grid.Get(coord) == VoxelType.Empty)
			grid.Set(coord, VoxelType.White);
	}
}
