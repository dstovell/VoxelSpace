using UnityEngine;
using System;
using System.Collections;

namespace VoxelSpace
{
	public abstract class ShipComponent : ShipEntity
	{
		public Damagable [] Damagables;

		public void InitComponent()
		{
			this.ParentShip = this.GetComponentInParent<Ship>();
			this.FindDamagables();

			this.InitEntity();
		}

		public void FindDamagables()
		{
			this.Damagables = this.GetComponentsInChildren<Damagable>();
		}

		public bool IsAlive()
		{
			int damagablesNotDestroyed = 0;

			for (int i=0; i<this.Damagables.Length; i++)
			{
				Damagable d = this.Damagables[i];
				if (!d.IsDestroyed())
				{
					damagablesNotDestroyed++;
				}
				else if (d.IsComponentCritical)
				{
					return false;
				}
			}

			return (damagablesNotDestroyed > 0);
		}
	}
}

