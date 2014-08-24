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
	White
}

public struct GridCoord
{
	public int x;
	public int y;
	public int z;

	const int WIDTH_OFF = VoxelGrid.WIDTH / 2;
	const int HEIGHT_OFF = VoxelGrid.HEIGHT / 2;
	const int DEPTH_OFF = VoxelGrid.DEPTH / 2;

	public Vector3 WorldPosition
	{
		get
		{
			float worldX = x - WIDTH_OFF;
			float worldY = y - HEIGHT_OFF;
			float worldZ = z;
			return new Vector3(worldX, worldY, worldZ);
		}
	}

	public GridCoord(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public GridCoord(Vector3 world)
	{
		x = Mathf.Clamp(Mathf.FloorToInt(world.x) + WIDTH_OFF, 0, VoxelGrid.WIDTH - 1);
		y = Mathf.Clamp(Mathf.FloorToInt(world.y) + HEIGHT_OFF, 0, VoxelGrid.HEIGHT - 1);
		z = Mathf.Clamp(Mathf.FloorToInt(world.z), 0, VoxelGrid.DEPTH - 1);
	}
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
				int x = Random.Range(0, WIDTH - 1);
				int y = Random.Range(0, HEIGHT - 1);
				int z = Random.Range(0, DEPTH - 1);

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
	}

	void InitializeWithColors()
	{
		Set(RandomEmpty, VoxelType.Red);
		Set(RandomEmpty, VoxelType.Green);
		Set(RandomEmpty, VoxelType.Blue);
	}
}
