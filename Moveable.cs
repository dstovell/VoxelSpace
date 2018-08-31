using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public abstract class Moveable : Damager
	{
		public float MaxG = 1.0f;

		public Vector3 Velocity = Vector3.zero;

		public void InitMoveable()
		{
		}
	}
}

