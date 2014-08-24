using UnityEngine;
using System.Collections;

public enum VoxelType
{
	Empty,
	Red,
	Green,
	Blue,
	Cyan,
	Yellow,
	Magenta,
	Black,
	Grey,
	White
}

public class VoxelGrid : MonoBehaviour
{
	public const int WIDTH = 19;
	public const int HEIGHT = 19;
	public const int DEPTH = 19;
	public const int SIZE = WIDTH * HEIGHT * DEPTH;

	public GridCoord RandomEmpty
	{
		get
		{
			if (filledCount > SIZE)
				throw new System.Exception("Cannot find random as there are no empty spaces left.");

			while (true)
			{
				int x = Random.Range(0, WIDTH);
				int y = Random.Range(0, HEIGHT);
				int z = Random.Range(0, DEPTH);

				if (grid[x, y, z] == VoxelType.Empty)
					return new GridCoord(x, y, z); 
			}
		}
	}

	[SerializeField]
	GameObject voxelGO;

	VoxelType[,,] grid = new VoxelType[WIDTH, HEIGHT, DEPTH];
	GameObject[,,] gridGOs = new GameObject[WIDTH, HEIGHT, DEPTH];

	int filledCount = 0;

	void Start()
	{
		InitializeWithColors();
	}

	public VoxelType Get(GridCoord coord)
	{
		return grid[coord.x, coord.y, coord.z];
	}

	public void Set(GridCoord coord, VoxelType type)
	{
		var previousType = grid[coord.x, coord.y, coord.z];
		AdjustFillCount(previousType, type);

		grid[coord.x, coord.y, coord.z] = type;

		CreateVoxel(coord, type);
	}

	public void Move(GridCoord start, GridCoord end)
	{
		if (start.Equals(end))
			return;

		var occupying = gridGOs[end.x, end.y, end.z];
		if (occupying != null)
			Destroy(occupying);

		gridGOs[start.x, start.y, start.z].transform.position = end.WorldPosition;
		gridGOs[end.x, end.y, end.z] = gridGOs[start.x, start.y, start.z];
		gridGOs[start.x, start.y, start.z] = null;

		grid[end.x, end.y, end.z] = grid[start.x, start.y, start.z];
		grid[start.x, start.y, start.z] = VoxelType.Empty;
	}

	public GridCoord RandomAdjacent(GridCoord coord)
	{
		int dirX = 0;
		int dirY = 0;
		int dirZ = 0;

		do
		{
			dirX = Random.Range(-1, 2);
			dirY = Random.Range(-1, 2);
			dirZ = Random.Range(-1, 2);
		} while (dirX == 0 && dirY == 0 && dirZ == 0);

		return new GridCoord(coord.x + dirX, coord.y + dirY, coord.z + dirZ);
	}

	void AdjustFillCount(VoxelType from, VoxelType to)
	{
		if (from != to)
		{
			if (to == VoxelType.Empty)
				filledCount--;
			else if (from == VoxelType.Empty)
				filledCount++;
		}
	}

	void CreateVoxel(GridCoord coord, VoxelType type)
	{
		var voxel = gridGOs[coord.x, coord.y, coord.z];

		if (voxel == null)
		{
			voxel = (GameObject)Instantiate(voxelGO, coord.WorldPosition, Quaternion.identity);
			gridGOs[coord.x, coord.y, coord.z] = voxel;
		}

		voxel.GetComponent<VoxelMaterialAdjuster>().SetType(type);

		if (type == VoxelType.Black)
		{
			var black = voxel.AddComponent<BlackVoxel>();
			black.Initialize(this, coord);
		}
	}

	void InitializeWithColors()
	{
		Set(RandomEmpty, VoxelType.Red);
		Set(RandomEmpty, VoxelType.Green);
		Set(RandomEmpty, VoxelType.Blue);

		Set(RandomEmpty, VoxelType.Black);
	}
}
