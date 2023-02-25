using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BoardManager : MonoBehaviour
{
  [SerializeField] private List<BoardCell> cellList;
	[SerializeField] private List<GameObject> estateObj;

  public List<BoardCell> CellList => this.cellList;

	private void Awake()
	{
		EstateRotation();
	}

	private void EstateRotation()
	{
		for(int i=0; i<estateObj.Count;i++)
		{
			if (i < 10) { estateObj[i].transform.rotation = Quaternion.Euler(0, 0, 0); }
			else if (i < 20) { estateObj[i].transform.rotation = Quaternion.Euler(0, 90, 0); }
			else if (i<30) { estateObj[i].transform.rotation = Quaternion.Euler(0, 180, 0); }
			else { estateObj[i].transform.rotation = Quaternion.Euler(0, 270, 0); }
		}
	}
} 
