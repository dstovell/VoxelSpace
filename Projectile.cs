using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public enum ProjectileType
	{
		PDC,
		Railgun,
		Torpedo,
		Laser
	}

	public class Projectile : Moveable
	{
		public ProjectileType Type;

		public float Speed = 10f;
		public float MaxLifetimeSeconds = 10f;
		public int MaxHitCount = 1;

		public Vector3 TargetPosition;
		public Collider Collision;

		public float Age = 0;
		public int HitCount = 0;

		public void Awake()
		{
			this.Collision = this.GetComponent<Collider>();
		}

		public bool IsSpawnable()
		{
			return !this.gameObject.activeSelf;
		}

		public void Spawn(Vector3 position, Vector3 target)
		{
			this.transform.position = position;
			this.gameObject.SetActive(true);

			this.TargetPosition = target;
			this.Age = 0;
			this.HitCount = 0;
		}

		public void UnSpawn()
		{
			this.gameObject.SetActive(false);
			this.transform.position = Vector3.zero;
			this.transform.localRotation = Quaternion.identity;
		}

		public bool IsComplete()
		{
			return (this.Age >= this.MaxLifetimeSeconds) || (this.HitCount >= this.MaxHitCount);
		}

		public void Update()
		{
			if (this.IsComplete())
			{
				return;
			}

			Vector3 lookDir = Vector3.Normalize( this.TargetPosition - this.transform.position );
			this.transform.rotation = Quaternion.LookRotation( lookDir );

			this.transform.position = Vector3.MoveTowards(this.transform.position, this.TargetPosition, this.Speed*Time.deltaTime);

			this.Age += Time.deltaTime;
		}

		void LateUpdate()
	    {
			if (this.IsComplete())
			{
				this.UnSpawn();
			}
	    }

		public virtual void OnProjectileHit(Damagable d)
		{
			this.HitCount++;
		}

		void OnTriggerEnter(Collider other)
	    {
			Damagable d = other.gameObject.GetComponent<Damagable>();
			if (d != null)
			{
				this.OnProjectileHit(d);
				return;
			}
	    }
	}
}

	