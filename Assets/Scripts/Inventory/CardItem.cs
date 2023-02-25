using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItem : ScriptableObject
{
	public string itemName;
	public int ItemIndex;
	public CardItem(string Name, int Index)
	{
		this.itemName = Name;
		this.ItemIndex = Index;
	}
}
