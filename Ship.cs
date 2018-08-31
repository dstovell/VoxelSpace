using UnityEngine;
using System;
using System.Collections;

namespace VoxelSpace
{
	public class Ship : MonoBehaviour
	{
		public enum RenderMode
		{
			None,
			Exterior,
			Interior,
			Hybrid
		}
		public RenderMode Mode = RenderMode.Exterior;

		public ShipInterior Interior;
		public ShipExterior Exterior;

		public ShipTurret [] Turrets;

		public Damagable [] Damagables;

		public Damagable PriorityTarget;

		public bool MoveToTarget = false;
		public Vector3 MoveTarget;

		public float Speed = 10f;
		public float TurnSpeed = 5f;

		public void Awake()
		{
			this.Interior = this.GetComponentInChildren<ShipInterior>();
			this.Exterior = this.GetComponentInChildren<ShipExterior>();

			this.Turrets = this.GetComponentsInChildren<ShipTurret>();

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
				else if (d.IsShipCritical)
				{
					return false;
				}
			}

			return (damagablesNotDestroyed > 0);
		}

		public void MoveTo(Vector3 target)
		{
			this.MoveTarget = target;
			this.MoveToTarget = true;
		}

		public void UpdateShowing()
		{
			bool showExterior = (this.Mode == RenderMode.Exterior) || (this.Mode == RenderMode.Hybrid);
			bool showInterior = (this.Mode == RenderMode.Interior) || (this.Mode == RenderMode.Hybrid);

			this.Exterior.ChangeShow(showExterior);
			this.Interior.ChangeShow(showInterior);

			if (this.Turrets != null)
			{
				for (int i=0; i<this.Turrets.Length; i++)
				{
					if (this.Turrets[i] != null)
					{
						this.Turrets[i].ChangeShow(showExterior);
					}
				}
			}
		}

		public void Update()
		{
			this.UpdateShowing();

			if (this.MoveToTarget)
			{
				Vector3 lookDir = Vector3.Normalize( this.MoveTarget - this.transform.position );
				Quaternion targetRotation = Quaternion.LookRotation(lookDir);

				//Turn towards target
				this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, Time.deltaTime*this.TurnSpeed);

				//Move in forward direction
				this.transform.position += Time.deltaTime * this.Speed * this.transform.forward;
			}
		}
	}
}

