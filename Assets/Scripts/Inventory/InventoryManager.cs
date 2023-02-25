using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public List<CardItem> cardItems;

	[SerializeField] private Transform slotParent;
	[SerializeField] private Slot[] slots;
	[SerializeField] private Sprite keyImage;
	[SerializeField] public List<CardItem> goldcardItem;

	public static InventoryManager Instance { get; private set; }

	private void Awake()
	{
		Instance ??= this;
	}

	public void FreshSlot()
	{
		int i = 0;
		for (; i < goldcardItem.Count && i < slots.Length; i++)
		{
			slots[i].PcardItem = goldcardItem[i];
		}

		for (; i < slots.Length; i++)
		{
			slots[i].PcardItem = null;
		}
	}
	public void CardList()
	{
		CardItem abc = new CardItem("dd", 1);
		goldcardItem.Add(abc);

	}

	public void AddItem()
	{
		if (goldcardItem.Count < slots.Length)
		{
			CardList();
			FreshSlot();
		}
		else
		{
			D.Log("?�롯 ?�참");
		}
	}

	public void AddItems(string name, int index)
	{
		if (goldcardItem.Count < slots.Length)
		{
			goldcardItem.Add(new CardItem(name, index));
			FreshSlot();
		}
		else
		{
			D.Log("?�롯?�참");
		}
	}

	public void DeleteItem()
	{
		D.Log(goldcardItem[0].ItemIndex.ToString());
		D.Log(goldcardItem[0].itemName.ToString());
		goldcardItem.Remove(goldcardItem[0]); //
		FreshSlot();
	}
}
