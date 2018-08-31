using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class ProjectileManager : MonoBehaviour
	{
		public static ProjectileManager Instance;

		void Awake()
		{
			Instance = this;

			this.Spawners = this.GetComponents<ProjectileSpawner>();
		}

		public ProjectileSpawner [] Spawners;

		public void SpawnProjectile(ProjectileType type, Vector3 position, Vector3 target)
		{
			for (int i=0; i<this.Spawners.Length; i++)
			{
				ProjectileSpawner spawner = this.Spawners[i];
				if (spawner.Type == type)
				{
					spawner.Spawn(position, target);
					return;
				}
			}

			Debug.LogError("SpawnProjectile no spawner for " + type.ToString());
		}
	}
}

