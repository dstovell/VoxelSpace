using UnityEngine;
using System;
using System.Collections;

namespace VoxelSpace
{
	public class ShipTurret : ShipComponent
	{
		const float MaxDistance = 999999.0f;

		public Damagable CurrentTarget;

		public ProjectileType Ammo;
		public float FireCooldown;
		public float TurnSpeed = 5f;
		public float MaxRange = 100f;

		public Transform [] ProjectileExit;
		public int nextProjectileExit = 0;

		public float GetRangeTo(Damagable d)
		{
			if (d == null)
			{
				return MaxDistance;
			}

			return this.GetRangeTo(d.transform.position);
		}

		public float GetRangeTo(Vector3 pos)
		{
			return Vector3.Distance(this.transform.position, pos);
		}

		public void PickTarget()
		{
			if ((this.ParentShip != null) && (this.ParentShip.PriorityTarget != null))
			{
				this.CurrentTarget = this.ParentShip.PriorityTarget;
				return;
			}

			float closestDistance = this.MaxRange;
			Damagable closest = null;

			for (int i=0; i<Damagable.All.Count; i++)
			{
				Damagable d = Damagable.All[i];
				float range = this.GetRangeTo(d);
				if (!d.IsDestroyed() && (range < closestDistance))
				{
					closest = d;
					closestDistance = range;
				}
			}

			this.CurrentTarget = closest;
		}

		public void FireAt(Vector3 target)
		{
			Vector3 lookDir = Vector3.Normalize( target - this.transform.position );
			Quaternion targetRotation = Quaternion.LookRotation(lookDir);

			if (Quaternion.Angle(this.transform.rotation, targetRotation) > 1f)
			{
				this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, Time.deltaTime*this.TurnSpeed);
			}

			float range = this.GetRangeTo(target);
			if (range < this.MaxRange)
			{
				return;
			}

			Vector3 spawnPos = this.transform.position;
			if (this.ProjectileExit.Length > 0)
			{
				Transform t = this.ProjectileExit[this.nextProjectileExit];
				if (t != null)
				{
					spawnPos = t.position;
				}

				this.nextProjectileExit++;
				if (this.nextProjectileExit >= this.ProjectileExit.Length)
				{
					this.nextProjectileExit = 0;
				}
			}

			ProjectileManager.Instance.SpawnProjectile(this.Ammo, spawnPos, target);
		}

		public void FireAt(Damagable target)
		{
			if (target == null)
			{
				return;
			}

			this.FireAt(target.transform.position);
		}

		public void Awake()
		{
			this.InitComponent();
		}

		public void Update()
		{
			if (this.CurrentTarget == null)
			{
				this.PickTarget();
			}

			if (this.CurrentTarget)
			{
				this.FireAt(this.CurrentTarget.transform.position);
			}
		}
	}
}

