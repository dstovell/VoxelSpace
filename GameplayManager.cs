using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	namespace Gameplay
	{
		public enum Team
		{
			None,
			Blue,
			Pink
		}

		public class GameplayManager : MonoBehaviour
		{
			public static GameplayManager Instance;

			void Awake()
			{
				Instance = this;
			}

			public bool AreHostile(Gameplay.Team a, Gameplay.Team b)
			{
				return (!this.AreNeutral(a, b) && (a != b));
			}

			public bool AreFriendly(Gameplay.Team a, Gameplay.Team b)
			{
				return (!this.AreNeutral(a, b) && (a == b));
			}

			public bool AreNeutral(Gameplay.Team a, Gameplay.Team b)
			{
				return ((a == Team.None) || (b == Team.None));
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
}

