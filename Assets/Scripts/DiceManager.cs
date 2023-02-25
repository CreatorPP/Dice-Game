using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DiceManager : MonoBehaviour
{
	[SerializeField] private Image image;
	public List<Dice> diceList;
	public int totalValue;
	public UnityEvent endRollEvent;

	private void Start() => AddHandlers();

	public void RollAllDice()
	{
		this.totalValue = 0;

		foreach (var t in this.diceList)
		{
			t.AddForceToDice();
		}
	}

	public void CountAllDiceValues()
	{
		for (int i = 0; i < this.diceList.Count; i++)
		{
			if (this.diceList[i].isRolling == true)
			{
				this.totalValue = 0;
				return;
			}
			else
			{
				this.totalValue += this.diceList[i].value;
			}
		}

		this.endRollEvent.Invoke();
	}

	public void OnReceivePacket(RollDiceRes packet)
	{
		D.Log("received packet : " + packet.result + ", " + packet.tick);
		if (packet.result == EError.SUCCESS)
		{
			RollAllDice();
		}
	}

	public void OnReceivePacket(ChangeTurnNoti packet)
	{
		
	}

	private void AddHandlers()
	{
		WebSocketManager.AddHandler(EPacketId.ROLL_DICE_RESPONSE, new MessageHandler<RollDiceRes>(OnReceivePacket));
		WebSocketManager.AddHandler(EPacketId.CHANGE_TURN_NOTI, new MessageHandler<ChangeTurnNoti>(OnReceivePacket));
	}
}
