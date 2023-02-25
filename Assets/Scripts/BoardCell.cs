using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class BoardCell : MonoBehaviour
{
	[SerializeField] private BlueMarbleManager bmm;
	[SerializeField] private ECellType cellType;
	[SerializeField] private int owner;
	[SerializeField] private GameObject villa;
	[SerializeField] private GameObject hotel;
	[SerializeField] private GameObject building;
	[SerializeField] private Transform estateTransform;
	[SerializeField] private GameObject estate;
	
	/// <summary>
	/// [0] : ?μ  ?¬λ?
	/// [1] : λΉλ”©
	/// [2] : λΉλΌ
	/// [3] : ?Έν…”
	/// </summary>
	[SerializeField] private bool[] buildArray = new bool[4];
	private float moveSpeed;
  
  public ECellType GetCellType => this.cellType;
  public bool[] GetBuildStatus => this.buildArray;

  private void Start()
  {
    this.moveSpeed = Random.Range(0.5f, 1.5f);
		this.villa.SetActive(false);
		this.hotel.SetActive(false);
		this.building.SetActive(false);
  }

  private void Update()
  {
    this.transform.position = new Vector3(this.transform.position.x, 
			(Mathf.Sin(Time.time * this.moveSpeed) + 1) / 8, this.transform.position.z);
  }

	public void BuildingOn(bool[] bo)
	{
		for(int i=0; i<bo.Length; i++)
		{
			if( i==0&& bo[0]) { Debug.Log("?‰λ„£?”κ³³"); }
			else if (i == 1 && bo[1]) { this.building.SetActive(true); }
			else if (i == 2 && bo[2]) { this.villa.SetActive(true); }
			else if (i == 3 && bo[3]) { this.hotel.SetActive(true); }
			else { Debug.Log("BuildingOn false"); }
		}
	}
}