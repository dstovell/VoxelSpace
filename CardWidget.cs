using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class CardWidget : MonoBehaviour
	{
		public UltimateJoystick Draggable;
		public Card ActiveCard;

		public bool WasDragging = false;
		public Vector3 LastDragPosition;

		public void SetPosition(UltimateJoystick.Anchor anchor, float x, float y)
		{
			if (this.Draggable != null)
			{
				this.Draggable.anchor = anchor;
				this.Draggable.customSpacing_X = x;
				this.Draggable.customSpacing_Y = y;
				this.Draggable.UpdatePositioning();
			}
		}

		public void SetCard(Card c)
		{
			this.ActiveCard = c;

			this.Show();
		}

		public void ClearCard()
		{
			this.ActiveCard = null;

			this.Hide();
		}

		private void Hide()
		{
			this.gameObject.SetActive(false);
		}

		private void Show()
		{
			this.gameObject.SetActive(true);
		}

		void SpawnAt(Vector3 pos)
		{
			Debug.LogError("SpawnAt!!! pos:" + pos);
		}

		void Update()
		{
			if (this.Draggable != null)
			{
				bool isDragging = (this.Draggable.GetDistance() > 0f);

				if (!this.WasDragging && isDragging)
				{
					//Debug.LogError("DragStarted!!! customSpacing_X:" + this.Draggable.customSpacing_X + " customSpacing_Y:" + this.Draggable.customSpacing_Y);
				}
				else if (this.WasDragging && !isDragging)
				{
					this.SpawnAt(this.LastDragPosition);
				}
				this.WasDragging = isDragging;
				this.LastDragPosition = this.Draggable.joystick.position;
			}
		}
	}
}

