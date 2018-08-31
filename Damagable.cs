using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class Damagable : MonoBehaviour
	{
		public static List<Damagable> All = new List<Damagable>();

		public bool IsShipCritical = false;
		public bool IsComponentCritical = false;
		public bool IsDirectlyTargetable = false;

		public float MaxHealth = 100f;
		public float Health;

		public void Damage(float amount)
		{
			this.Health -= amount;
			this.Health = Mathf.Clamp(this.Health, 0, this.MaxHealth);
		}

		public void Damage(Damager d)
		{
			if (d == null)
			{
				return;
			}

			this.Damage(d.Damage);
		}

		public void Heal(float amount)
		{
			this.Health += amount;
			this.Health = Mathf.Clamp(this.Health, 0, this.MaxHealth);
		}

		public bool IsDestroyed()
		{
			return (this.Health <= 0f);
		}

		void Awake()
		{
			All.Add(this);
			this.Health = this.MaxHealth;
		}

		void OnDestroy()
	    {
	    	All.Remove(this);
	    }

		void OnTriggerEnter(Collider other)
	    {
			Damager d = other.gameObject.GetComponent<Damager>();
			this.Damage(d);
	    }
	}
}

