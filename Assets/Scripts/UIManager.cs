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
	/// ??๋ฐ?๊ฑด๋ฌผ ๊ตฌ๋งค UI ์ผ๊ธฐ
	/// </summary>
	/// <param name="dest">int, destination</param>

	// ?ด์  2,7,12,17,22,28,35,38
	// ?น์์ง??5,15,25 ,35, 32

	public void BuildingBuyOn(int dest)        
	{
		this.diceSum += dest;
		this.dest = dest;

		////?น์์น?๋ฆฌ์ค??
		int[] specialDest = { 5, 15, 25, 32, 35 };
		var specialList = new List<int>();
		specialList.AddRange(specialDest);
		///
		///?ด์ ์น?๋ฆฌ์ค??
		int[] keyDest = { 2, 7, 12, 17, 22, 28, 35, 38 };
		var keyList = new List<int>();
		keyList.AddRange(keyDest);
		///

		if (diceSum >= 40) { 
			Debug.Log("40?๊?");
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
		BlueMarbleManager.Instance.CurrentStep = ESteps.NONE; //?์ฌ?ํ
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
		this.cell = this.bmm.GetCell(this.diceSum);  // ๋ถ๋ฃจ๋ง๋ธ์ ?๋ getCell??dest(์ฃผ์ฌ?๊ฐ?????ฃ์ด์ค์ ?๋ ?ด์ด ?์น๊ฐ ๋ณด๋ ?ด๋?์น?ธ์? ?์ค๊ฒํจ (dest?์ diceSum?ผ๋ก ๋ณ๊ฒ?
		var buildStatus = this.cell.GetBuildStatus; // BoardCell???๋ GetBuildStatus ??bool???๊ฐ?ธ์ด
		this.buyImage.gameObject.SetActive(true);
		for (int i = 0; i < buildStatus.Length; ++i)
		{
			this.toggleCheck[i].interactable = buildStatus[i] == false;
			this.toggleCheck[i].isOn = buildStatus[i] == false;
		}
	}
	public void OnClickBuyButton() // ? ๊? ์ฒดํฌ?????์ฐ?์ ๊ฑด๋ฌผ๊ฐ?์ง๋ถ?
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
		BlueMarbleManager.Instance.CurrentStep = ESteps.NONE; //?์ฌ?ํ
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
