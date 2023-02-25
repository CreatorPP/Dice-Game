using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using Newtonsoft.Json;
using UnityEngine.UI;

public class CardJson : MonoBehaviour
{
	[SerializeField] private Text cardName;
	[SerializeField] private Text cardDesc;

	public static CardJson Instance { get; private set; }

	private void Start()
	{
		Instance ??= this;
	}

	public void CardLoad()
	{
		string json = File.ReadAllText(Application.dataPath + "/Resources/CardJson.json");
		//var CardData = JsonConvert.DeserializeObject<CardTemplates[]>(json);
		JsonData CardData = JsonMapper.ToObject(json);
		var cardRandom = Random.Range(0, 7);
		RandomCard(cardRandom, CardData);
		
	}
	void RandomCard(int idx, JsonData data)
	{
		cardName.text = data[idx]["card"].ToString();
		cardDesc.text = data[idx]["card contents"].ToString();
		int index = int.Parse(data[idx]["index"].ToString());
		InventoryManager.Instance.AddItems(data[idx]["card"].ToString(), index);
	}
}

public sealed class CardTemplates
{
	public int index;
	public string cardName;
	public string cardDesc;
}