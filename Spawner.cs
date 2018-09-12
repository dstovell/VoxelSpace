using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class Spawner : MonoBehaviour
	{
		public GameObject SpawnPrefab;

		public Spawnable [] Spawnables;

		public void InitSpawner(int maxSpawnables)
		{
			if (this.SpawnPrefab != null)
			{
				this.Spawnables = new Spawnable[maxSpawnables];
				for (int i=0; i<this.Spawnables.Length; i++)
				{
					GameObject go = GameObject.Instantiate(this.SpawnPrefab, this.transform);
					Spawnable sp = go.GetComponent<Spawnable>();
					if (sp == null)
					{
						break;
					}

					sp.UnSpawn();
					this.Spawnables[i] = sp;
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

	