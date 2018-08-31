using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class ProjectileSpawner : MonoBehaviour
	{
		public ProjectileType Type;

		public GameObject Prefab;

		public int MaxSpawnables = 50;

		public Projectile [] Projectiles;

		public void Awake()
		{
			if (this.Prefab != null)
			{
				this.Projectiles = new Projectile[this.MaxSpawnables];
				for (int i=0; i<this.Projectiles.Length; i++)
				{
					GameObject go = GameObject.Instantiate(this.Prefab, this.transform);
					Projectile p = go.GetComponent<Projectile>();
					if (p == null)
					{
						break;
					}

					p.UnSpawn();
					this.Projectiles[i] = p;
				}
			}
		}

		public void Spawn(Vector3 position, Vector3 target)
		{
			for (int i=0; i<this.Projectiles.Length; i++)
			{
				Projectile p = this.Projectiles[i];
				if (p.IsSpawnable())
				{
					p.Spawn(position, target);
					break;
				}
			}
		}
	}
}

	