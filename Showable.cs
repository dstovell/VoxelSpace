using UnityEngine;
using System;
using System.Collections;

namespace VoxelSpace
{
	public abstract class Showable : MonoBehaviour
	{
		public bool Showing { get; private set; }

		public void ChangeShow(bool show)
		{
			if (this.Showing == show)
			{
				return;
			}

			this.Showing = show;
			if (this.Showing)
			{
				this.OnShow();
			}
			else
			{
				this.OnHide();
			}
		}

		protected abstract void OnShow();
		protected abstract void OnHide();
	}
}

