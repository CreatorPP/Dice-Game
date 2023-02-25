using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class NetworkManager : MonoBehaviour
{
	public static NetworkManager Instance { get; private set; }
	private void OnValidate() => this.transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;

	private Queue<Action> messageQueue;

	private void Awake() => Instance ??= this;

	private void Update()
	{
		lock (this.messageQueue)
		{
			while (this.messageQueue.Count > 0) this.messageQueue.Dequeue().Invoke();
		}
	}
}
