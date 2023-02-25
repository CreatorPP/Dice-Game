using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			OnClickRay(true);
		}
	}

	public void OnClickRay(bool start)
	{
		var RayStart = start;
		if(RayStart)
		{
			RaycastHit hit = new RaycastHit();

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray.origin, ray.direction, out hit))
			{
				Debug.Log(hit.transform.gameObject.name);
			}
		}

		RayStart = false;
	}
}
