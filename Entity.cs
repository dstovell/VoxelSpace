using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public abstract class ShipEntity : MonoBehaviour
	{
		public Ship ParentShip;

		public Gameplay.Team GetTeam()
		{
			return (this.ParentShip != null) ? this.ParentShip.Team : Gameplay.Team.None;
		}

		public virtual bool IsAlive()
		{
			return (this.ParentShip != null) ? this.ParentShip.IsAlive() : false;
		}

		public bool Showing { get; private set; }

		public void InitEntity()
		{
			this.ParentShip = this.GetComponentInParent<Ship>();
		}

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

		protected virtual void OnShow()
		{
			this.gameObject.SetActive(true);
		}

		protected virtual void OnHide()
		{
			this.gameObject.SetActive(false);
		}
	}
}

