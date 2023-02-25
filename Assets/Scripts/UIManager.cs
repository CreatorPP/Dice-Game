using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
	[SerializeField] private BlueMarbleManager bmm;
	[SerializeField] private TextMeshProUGUI txtDiceResult;
	[SerializeField] private PlayerInfo playerinfo;
	[SerializeField] private Text myMoney;
	[SerializeField] private Text specialBuyMoney;

	[SerializeField] public List<Toggle> toggleCheck;
	[SerializeField] private List<Text> textPrice;

	[SerializeField] private DiceManager diceManager;
	[SerializeField] private TextMeshProUGUI valueText;
	[SerializeField] private Image buyImage;
	[SerializeField] private Image specialbuyImage;
	[SerializeField] private Image goldKeyImage;
	public UnityEvent rollDicePlay;
	private BoardCell cell;

	public bool[] buildBool = new bool[4];

	private int money;
	private int dest;
	private int sum;
	private int diceSum;

	private void Awake()
	{
		this.bmm.rollDiceEvent.AddListener(BuildingBuyOn);
		var num = Random.Range(0, 8);
		D.Log(num.ToString());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			BuildingBuyOff();
		}
	}

	public void PlayerMovePoint()
	{
		int diceToMove = this.diceManager.totalValue;
		this.valueText.text = diceToMove.ToString();
	}

	/// <summary>
	/// ??Î∞?Í±¥Î¨º Íµ¨Îß§ UI ÏºúÍ∏∞
	/// </summary>
	/// <param name="dest">int, destination</param>

	// ?¥Ïá† 2,7,12,17,22,28,35,38
	// ?πÏàòÏßÄ??5,15,25 ,35, 32

	public void BuildingBuyOn(int dest)        
	{
		this.diceSum += dest;
		this.dest = dest;

		////?πÏàòÏπ?Î¶¨Ïä§??
		int[] specialDest = { 5, 15, 25, 32, 35 };
		var specialList = new List<int>();
		specialList.AddRange(specialDest);
		///
		///?¥Ïá†Ïπ?Î¶¨Ïä§??
		int[] keyDest = { 2, 7, 12, 17, 22, 28, 35, 38 };
		var keyList = new List<int>();
		keyList.AddRange(keyDest);
		///

		if (diceSum >= 40) { 
			Debug.Log("40?òÍ?");
			diceSum -= 40; 
		}

		if (keyList.Contains(diceSum)) 
		{
			GoldCardKeyPopup();
			CardJson.Instance.CardLoad();
		}

		else if (specialList.Contains(diceSum))
		{
			SpecialBuyPopup();
			//CsvReader.Instance.CsvTest(diceSum);
			JsonManagers.Instance.Load3(diceSum);
		}

		else if (this.diceSum == 10)
		{
			var moneys = int.Parse(myMoney.text) / 10;
			money = int.Parse(myMoney.text) - moneys;
			this.myMoney.text = this.money.ToString();
		}
		else
		{
			BuildingBuyPopup();
			//CsvReader.Instance.CsvTest(diceSum);
			JsonManagers.Instance.Load3(diceSum);
		}
	}

	public void BuildingBuyOff()
	{
		BlueMarbleManager.Instance.CurrentStep = ESteps.NONE; //?ÑÏû¨?ÅÌÉú
		Debug.Log(BlueMarbleManager.Instance.CurrentStep);
		this.buyImage.gameObject.SetActive(false);
	}

	private void SpecialBuyPopup()
	{
		this.cell = this.bmm.GetCell(this.diceSum);
		this.specialbuyImage.gameObject.SetActive(true);
	}

	private void GoldCardKeyPopup()
	{
		this.cell = this.bmm.GetCell(this.diceSum);
		this.goldKeyImage.gameObject.SetActive(true);
	}

	private void BuildingBuyPopup()
	{
		this.cell = this.bmm.GetCell(this.diceSum);  // Î∂ÄÎ£®ÎßàÎ∏îÏóê ?àÎäî getCell??dest(Ï£ºÏÇ¨?ÑÍ∞í?????£Ïñ¥Ï§òÏÑú ?åÎ†à?¥Ïñ¥ ?ÑÏπòÍ∞Ä Î≥¥Îìú ?¥Îäê?ÑÏπò?∏Ï? ?òÏò§Í≤åÌï® (dest?êÏÑú diceSum?ºÎ°ú Î≥ÄÍ≤?
		var buildStatus = this.cell.GetBuildStatus; // BoardCell???àÎäî GetBuildStatus ??bool?Ä??Í∞Ä?∏Ïò¥
		this.buyImage.gameObject.SetActive(true);
		for (int i = 0; i < buildStatus.Length; ++i)
		{
			this.toggleCheck[i].interactable = buildStatus[i] == false;
			this.toggleCheck[i].isOn = buildStatus[i] == false;
		}
	}
	public void OnClickBuyButton() // ?†Í? Ï≤¥ÌÅ¨?????êÏÇ∞?êÏÑú Í±¥Î¨ºÍ∞?ÏßÄÎ∂?
	{
		var buildStatus = this.cell.GetBuildStatus;
		for (int i = 0; i < this.toggleCheck.Count; i++)
		{

			if (this.toggleCheck[i].isOn)
			{
				//this.sum += int.Parse(this.textPrice[i].text);
				buildStatus[i] = true;
			}
			else
			{
				buildStatus[i] = false;
			}
		}

		cell.BuildingOn(buildStatus);
		Buy();
		BuildingBuyOff();
	}

	public void GoldCardESCOff()
	{
		this.goldKeyImage.gameObject.SetActive(false);
	}

	public void SpecialBuildingBuyOff()
	{
		BlueMarbleManager.Instance.CurrentStep = ESteps.NONE; //?ÑÏû¨?ÅÌÉú
		Debug.Log(BlueMarbleManager.Instance.CurrentStep);
		this.specialbuyImage.gameObject.SetActive(false);
	}

	public void OnClickSpecialBuyButton()
	{
		var sub = int.Parse(this.myMoney.text);
		var specialmoney = int.Parse(this.specialBuyMoney.text);
		this.myMoney.text = (sub - specialmoney).ToString();

		SpecialBuildingBuyOff();
	}

	public void Buy()
	{
		for (int i = 0; i < this.toggleCheck.Count; i++)
		{

			if (this.toggleCheck[i].isOn)
			{
				this.sum += int.Parse(this.textPrice[i].text);
			}
			else
			{
			}
		}

		int sub = int.Parse(this.myMoney.text);
		this.myMoney.text = (sub - this.sum).ToString();
		sum = 0;
	}
}
