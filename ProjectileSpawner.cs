using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class ProjectileSpawner : Spawner
	{
		public ProjectileType Type;

		public void Awake()
		{
			this.InitSpawner();
		}
	}
}

	