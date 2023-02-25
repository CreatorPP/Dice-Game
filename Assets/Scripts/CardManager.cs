using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class CardManager : MonoBehaviour
{
	public static CardManager Instance { get; private set; }

	private List<Card> cardPool;	
	private List<Card> usedCards;

	[SerializeField] private UnityEvent<int/*player Index*/, ECardEffect/*selected Card Effect*/> pickRandomCard;
	private void OnValidate() => this.transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
	private void Awake() => Instance ??= this;

	public void InitializeCardPool(List<CardTemplate> cards)
	{
		this.cardPool = new List<Card>();

		foreach (var card in cards)
		{
			this.cardPool.Add(new Card
			{
				id = card.id,
				name = card.name,
				desc = card.desc,
				effect = card.effect,
				used = false
			});
		}
	}
	
	public void GetRandomCard(int playerIndex)
	{
		if (this.cardPool.Count == 0)
		{
			this.cardPool = this.usedCards;
			this.usedCards.Clear();
		}

		// no need to access id
		int selected = Random.Range(0, this.cardPool.Count);
		var card = this.cardPool[selected];

		this.pickRandomCard.Invoke(playerIndex, card.effect);

		this.usedCards.Add(card);
		this.cardPool.RemoveAt(selected);
	}
}

public sealed class Card : CardTemplate
{
	public bool used;
}