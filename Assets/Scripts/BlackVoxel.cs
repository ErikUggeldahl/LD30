using UnityEngine;
using System.Collections;

public class BlackVoxel : MonoBehaviour
{
	VoxelGrid grid;
	GridCoord coord;

	public void Initialize(VoxelGrid grid, GridCoord coord)
	{
		this.grid = grid;
		this.coord = coord;
	}

	void Start()
	{
		StartCoroutine(Move());
	}

	IEnumerator Move()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);

			var moveTo = grid.RandomAdjacent(coord);

			grid.Move(coord, moveTo);

			coord = moveTo;
		}
	}
}
