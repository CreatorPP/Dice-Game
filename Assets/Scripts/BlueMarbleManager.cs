using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class BlueMarbleManager : MonoBehaviour
{
	[SerializeField] private BoardManager boardManager;
	[SerializeField] private UIManager uiManager;
	[SerializeField] private PlayerInfo playerPrefab;
	[SerializeField] private DiceManager dicemanager;
	[SerializeField] public List<PlayerInfo> playerList;
	private BlueMarbleManager instance;

	private Material[] material;

	private int turn = 0;

	public int dest;

	public UnityEvent<int> rollDiceEvent;

	public static BlueMarbleManager Instance { get; private set; }
	public int GetBoardSize => this.boardManager.CellList.Count;
	public BoardCell GetCell(int index) => this.boardManager.CellList[index]; // �������� ����?����������. 2021.08.25
	public float PlayerHeightOffset { get; } = 1f;

	private void Awake() => this.instance ??= this;

	public ESteps CurrentStep { get; set; }

	private void Start()
	{
		Instance ??= this;
		this.CurrentStep = ESteps.NONE;
		var p1 = Instantiate(this.playerPrefab, this.boardManager.CellList[0].transform);
		//material = p1.GetComponent<MeshRenderer>().materials;
		//p1.GetComponent<MeshRenderer>().materials = material;
		p1.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
		p1.transform.localPosition = new Vector3(p1.transform.localPosition.x,
		p1.transform.localPosition.y + this.PlayerHeightOffset, p1.transform.localPosition.z);
		this.playerList.Add(p1);

	}

	public void RollDice()
	{

		//if (playerList[turn].CurrentMoving) return;
		//var result = dicemanager.totalValue;
		//playerList[turn].MoveTo(dicemanager.totalValue); //��Ƽ
		//dest = playerList[turn].dest;
			this.rollDiceEvent?.Invoke(this.dicemanager.totalValue); // UIâ�� �ٷ������°�
			this.CurrentStep = ESteps.THROWING;

		//if(turn == 0) { turn = 1; }
		//else if (turn == 1) { turn = 0; }      ///// ������ ���� ���?���� UImanager���� ���� ���Ǵ� dest �����͵��� ���������?
	}

	public void RollDiceNet()
	{
		WebSocketManager.SendPacket(new RollDiceReq()
		{
			id = EPacketId.ROLL_DICE_REQUEST,
			sender = "test roll dice",
		});
	}

	private void AddHandlers()
	{
	}
}
