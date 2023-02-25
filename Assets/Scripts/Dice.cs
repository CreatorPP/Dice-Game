using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
	[HideInInspector]
	public bool isRolling;

	public float minRollForce;
	public float maxRollForce;
	private Rigidbody rb;
	public int value;
	public UnityEvent rollEvent;

	private void Awake()
	{
		this.rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (this.isRolling == true)
		{
			if (this.rb.IsSleeping())
			{
				this.isRolling = false;
				BlueMarbleManager.Instance.CurrentStep = ESteps.MOVING;
				CalculateValue();
				this.rollEvent?.Invoke();
			}
		}
	}

	private void CalculateValue()
	{
		var upPosDot = Vector3.Dot(Vector3.up, this.transform.up);
		var rightPosDot = Vector3.Dot(Vector3.up, this.transform.right);
		var forwardPosDot = Vector3.Dot(Vector3.up, this.transform.forward);

    if (System.Math.Abs(upPosDot - 1) < 0.1f) this.value = 2;
    if (System.Math.Abs(upPosDot - -1) < 0.1f) this.value = 4;
    if (System.Math.Abs(rightPosDot - 1) < 0.1f) this.value = 3;
    if (System.Math.Abs(rightPosDot - -1) < 0.1f) this.value = 1;
    if (System.Math.Abs(forwardPosDot - 1) < 0.1f) this.value = 5;
    if (System.Math.Abs(forwardPosDot - -1) < 0.1f) this.value = 6;
	}

	public void AddForceToDice()
	{
		if (this.isRolling) return;
		this.isRolling = true;
		var randomPosition = new Vector3(Random.Range(-0.2f, 0.8f), -1, Random.Range(-0.2f, 0.8f));
		this.rb.AddExplosionForce(Random.Range(this.minRollForce, this.maxRollForce), randomPosition, -0.5f, 2f);
	}
}                                                                                             