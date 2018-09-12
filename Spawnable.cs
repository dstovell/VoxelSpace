using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class Spawnable : MonoBehaviour
	{
		public bool IsSpawnable()
		{
			return !this.gameObject.activeSelf;
		}

		public void Spawn(Vector3 position, Vector3 target)
		{
			this.transform.position = position;
			this.gameObject.SetActive(true);
			this.OnSpawn(position, target);
		}

		public virtual void OnSpawn(Vector3 position, Vector3 target)
		{
		}

		public void UnSpawn()
		{
			this.OnUnSpawn();
			this.gameObject.SetActive(false);
			this.transform.position = Vector3.zero;
			this.transform.localRotation = Quaternion.identity;
		}

		public virtual void OnUnSpawn()
		{
		}
	}
}

	