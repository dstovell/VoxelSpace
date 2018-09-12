using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class Spawner : MonoBehaviour
	{
		public GameObject SpawnPrefab;

		public int MaxSpawnables = 50;

		public Spawnable [] Spawnables;

		public void InitSpawner()
		{
			if (this.SpawnPrefab != null)
			{
				this.Spawnables = new Projectile[this.MaxSpawnables];
				for (int i=0; i<this.Spawnables.Length; i++)
				{
					GameObject go = GameObject.Instantiate(this.SpawnPrefab, this.transform);
					Projectile p = go.GetComponent<Projectile>();
					if (p == null)
					{
						break;
					}

					p.UnSpawn();
					this.Spawnables[i] = p;
				}
			}
		}

		public void Spawn(Vector3 position, Vector3 target)
		{
			for (int i=0; i<this.Spawnables.Length; i++)
			{
				Spawnable sp = this.Spawnables[i];
				if (sp.IsSpawnable())
				{
					sp.Spawn(position, target);
					break;
				}
			}
		}
	}
}

	