using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public sealed class ToggleCheck : MonoBehaviour
{

	public Toggle toggleCheck;

	private void Start()
	{
		this.toggleCheck.isOn = false;
	}

	private void ToggleButtonCheck() => this.toggleCheck.isOn = !this.toggleCheck.isOn;
}
