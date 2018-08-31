using UnityEngine;
using System;
using System.Collections;

namespace VoxelSpace
{
	public abstract class ShipComponent : Showable
	{
		public Damagable [] Damagables;

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

