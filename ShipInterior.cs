using UnityEngine;
using System;
using System.Collections;

namespace VoxelSpace
{
	public class ShipInterior : Showable
	{
		protected override void OnShow()
		{
			this.gameObject.SetActive(true);
		}

		protected override void OnHide()
		{
			this.gameObject.SetActive(false);
		}

		public void Awake()
		{
		}

		public void Update()
		{
		}
	}
}

