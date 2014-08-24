using UnityEngine;
using System.Collections;

public struct GridCoord
{
	public int x;
	public int y;
	public int z;
	
	const float VOXEL_DIM_OFF = 0.5f;
	const int WIDTH_OFF = VoxelGrid.WIDTH / 2;
	const int HEIGHT_OFF = VoxelGrid.HEIGHT / 2;
	const int DEPTH_OFF = VoxelGrid.DEPTH / 2;
	
	public Vector3 WorldPosition
	{
		get
		{
			float worldX = x - WIDTH_OFF + VOXEL_DIM_OFF;
			float worldY = y - HEIGHT_OFF + VOXEL_DIM_OFF;
			float worldZ = z;
			return new Vector3(worldX, worldY, worldZ);
		}
	}
	
	public GridCoord(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;

		Validate();
	}
	
	public GridCoord(Vector3 world)
	{
		x = Mathf.FloorToInt(world.x) + WIDTH_OFF;
		y = Mathf.FloorToInt(world.y) + HEIGHT_OFF;
		z = Mathf.FloorToInt(world.z);

		Validate();
	}

	public override bool Equals(object obj)
	{
		if (!(obj is GridCoord))
			return false;
		
		GridCoord other = (GridCoord)obj;
		return other.x == x && other.y == y && other.z == z;
	}

	public bool Equals(GridCoord other)
	{
		return other.x == x && other.y == y && other.z == z;
	}
	
	public override int GetHashCode()
	{
		return x ^ y ^ z;
	}

	GridCoord Validate()
	{
		x = Mathf.Clamp(x, 0, VoxelGrid.WIDTH - 1);
		y = Mathf.Clamp(y, 0, VoxelGrid.HEIGHT - 1);
		z = Mathf.Clamp(z, 0, VoxelGrid.DEPTH - 1);
		return this;
	}
}