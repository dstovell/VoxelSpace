using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class Card : Spawner
	{
		public Sprite CardImage;

		public int InstanceCount = 1;

		public void Awake()
		{
			this.InitSpawner(this.InstanceCount);
		}
	}
}
	