using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class CardHand : MonoBehaviour
	{
		public UltimateJoystick.Anchor anchor = UltimateJoystick.Anchor.Right;

		public CardWidget CardWidgetPrefab;

		public const int MaxCards = 5;
		public const float CardHeight = 18;
		public const float CardWidth = 10;

		public CardWidget [] Widgets;
		public List<Card> Cards = new List<Card>();

		void Awake()
		{
			this.Cards = new List<Card>();
			this.Widgets = new CardWidget[CardHand.MaxCards];

			for (int i=0; i<this.Widgets.Length; i++)
			{
				this.Widgets[i] = GameObject.Instantiate(this.CardWidgetPrefab, this.transform);
			}

			this.UpdateCardPositions();
		}

		public void AddCard()
		{
			
		}

		public void UpdateCardPositions()
		{
			int requiredCards = 1;
			float fullHeight = 100f;
			float requiredHeight = (float)requiredCards * CardHand.CardHeight;
			float startHeight = (fullHeight - requiredHeight)/2f + CardHand.CardHeight/2f;
			//Debug.LogError("requiredHeight: " + requiredHeight + " fullHeight: " + fullHeight + " startHeight: " + startHeight);

			float xRowOne = 4;
			float xRowTwo = xRowOne + CardHand.CardWidth/2;

			float x = xRowOne;
			float y = startHeight;
			for (int i=0; i<this.Widgets.Length; i++)
			{
				CardWidget cw = this.Widgets[i];
				if (i < requiredCards)
				{
					//cw.SetCard(this.Cards[i]);
					cw.SetCard(null);
					cw.SetPosition(anchor, x, y);

					x = (x == xRowOne) ? xRowTwo : xRowOne;
					y += CardHand.CardHeight;
				}
				else 
				{
					cw.ClearCard();
				}
			}
		}
	}
}

