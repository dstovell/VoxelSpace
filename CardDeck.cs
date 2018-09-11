using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace VoxelSpace
{
	public class CardDeck : MonoBehaviour
	{
		public UltimateJoystick.Anchor anchor = UltimateJoystick.Anchor.Right;

		public CardWidget CardWidgetPrefab;

		public const int MaxHandSize = 5;
		public const float CardHeight = 18;
		public const float CardWidth = 10;

		public CardWidget [] Widgets;

		public List<Card> Hand = new List<Card>();
		public List<Card> Deck = new List<Card>();

		void Awake()
		{
			this.Hand = new List<Card>();
			this.Deck = new List<Card>();
			this.Widgets = new CardWidget[CardDeck.MaxHandSize];

			if (this.CardWidgetPrefab != null)
			{
				for (int i=0; i<this.Widgets.Length; i++)
				{
					this.Widgets[i] = GameObject.Instantiate(this.CardWidgetPrefab, this.transform);
				}
			}
			this.UpdateCardPositions();

			Card [] allCards = this.GetComponentsInChildren<Card>();
			for (int i=0; i<allCards.Length; i++)
			{
				this.Deck.Add(allCards[i]);
			}

			this.DrawCard(5);
		}

		public void DrawCard(int cardCount = 1)
		{
			bool cardedAdded = false;
			for (int i=0; i<cardCount; i++)
			{
				if ((this.Deck.Count == 0) || (this.Hand.Count >= CardDeck.MaxHandSize))
				{
					break;
				}

				Card nextCard = this.Deck[0];
				this.Deck.RemoveAt(0);

				this.Hand.Add(nextCard);
				cardedAdded = true;
			}

			if (cardedAdded)
			{
				this.UpdateCardPositions();
			}
		}

		public void UpdateCardPositions()
		{
			int requiredCards = this.Hand.Count;
			float fullHeight = 100f;
			float requiredHeight = (float)requiredCards * CardDeck.CardHeight;
			float startHeight = (fullHeight - requiredHeight)/2f + CardDeck.CardHeight/2f;
			//Debug.LogError("requiredHeight: " + requiredHeight + " fullHeight: " + fullHeight + " startHeight: " + startHeight);

			float xRowOne = 4;
			float xRowTwo = xRowOne + CardDeck.CardWidth/2;

			float x = xRowOne;
			float y = startHeight;
			for (int i=0; i<this.Widgets.Length; i++)
			{
				CardWidget cw = this.Widgets[i];
				if (cw == null)
				{
					continue;
				}

				if (i < requiredCards)
				{
					Card c = (i < this.Hand.Count) ? this.Hand[i] : null;
					cw.SetCard(c);
					cw.SetPosition(anchor, x, y);

					x = (x == xRowOne) ? xRowTwo : xRowOne;
					y += CardDeck.CardHeight;
				}
				else 
				{
					cw.ClearCard();
				}
			}
		}
	}
}

