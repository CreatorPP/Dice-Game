using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CsvReader : MonoBehaviour
{

	[SerializeField] private Text earthPrice;
	[SerializeField] private Text building;
	[SerializeField] private Text villa;
	[SerializeField] private Text hotel;

	[SerializeField] private Text specialName;
	[SerializeField] private Text specialPrice;

	public string[,] array = new string[40, 5];
	public static CsvReader Instance { get; private set; }
	void Start()
	{
		Instance ??= this;
	}

	public void CsvTest(int x)
	{
		StreamReader sr = new StreamReader(Application.dataPath + "/" + "BlueMarbleCSV1.csv");
		var number = x;
		int a = 0;
		bool endOfFile = false;
		while (!endOfFile)
		{
			string data_String = sr.ReadLine();
			if (data_String == null)
			{
				endOfFile = true;
				break;
			}

			var data_values = data_String.Split(',');
			for(int i=0; i<5; i++)
			{
				array[a,i] = data_values[i];
			}

			a++;
			
		}

		if(number == 5 || number==15 || number==25)
		{
			SpecialPrice(number);
		}

		else
		{
			Price(number);
		}
	}

	void Price(int a)
	{
		earthPrice.text = array[a, 1];
		building.text = array[a, 2];
		villa.text = array[a, 3];
		hotel.text = array[a, 4];
	}

	void SpecialPrice(int a)
	{
		specialName.text = array[a, 0];
		specialPrice.text = array[a, 1];
	}
}