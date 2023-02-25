using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public sealed class PlayerInfo : MonoBehaviour
{
	private int currentIndex;

	public UnityEvent buyStart;

	public int dest;

	public static PlayerInfo Instance { get; private set; }

	public bool CurrentMoving { get; private set; }

	private void Awake()
	{
		Instance ??= this;
		BlueMarbleManager.Instance.rollDiceEvent.AddListener(MoveTo); // 부루마블매니저에 있는 UnityEvent 안에 MoveTo 넣어줌
	}

	public void MoveTo(int idx)
	{
		this.dest = this.currentIndex + idx;
		this.dest = this.dest >= BlueMarbleManager.Instance.GetBoardSize ? this.dest - BlueMarbleManager.Instance.GetBoardSize : this.dest;
		StartCoroutine(CMoveAnimation(this.dest, () => this.buyStart.Invoke()));

		Debug.Log(this.dest);
	}

	private IEnumerator CMoveAnimation(int destination, Action temp) // 0번쨰칸 수정해야할곳
	{
		this.CurrentMoving = true;
		while (this.currentIndex != destination)
		{
			this.currentIndex = this.currentIndex >= BlueMarbleManager.Instance.GetBoardSize - 1 ? 0 : this.currentIndex + 1;
			var next = BlueMarbleManager.Instance.GetCell(this.currentIndex);
			var y = this.transform.localPosition.y;
			this.transform.parent = next.transform;
			this.transform.localPosition = new Vector3(0, y, 0);
			yield return new WaitForSeconds(0.5f);
		}
		
		temp.Invoke();
		BlueMarbleManager.Instance.CurrentStep = ESteps.BUYING;
		this.CurrentMoving = false;
	}
}
