using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
#if UNITY_EDITOR
using System.Threading;
#endif
using UnityEngine;
using UnityEngine.UI;

public sealed class WebSocketManager : MonoBehaviour
{
	private static Dictionary<EPacketId, IMessageHandler> messageHandler = new Dictionary<EPacketId, IMessageHandler>();
	[SerializeField] private Text uiText;

	private void Start()
	{
#if UNITY_EDITOR
		Init();
#endif
	}

	public void Test(string str)
	{
		this.uiText.text = str;
	}

	public static void AddHandler(EPacketId packetId, IMessageHandler handler)
	{
		if (messageHandler.ContainsKey(packetId))
		{
			D.Error("Try to add duplicated key in message handler");
		}
		else
		{
			messageHandler.Add(packetId, handler);
		}
	}

	public static void SendPacket(WebPacket packet) => SendPacket(new PacketWrapper() {data = packet});

	public void ReceiveWebMessage(string str)
	{
		var message = JsonConvert.DeserializeObject<WebMessage>(str);

		if (messageHandler.TryGetValue(message.id, out var handler))
		{
			var action = handler.MakeAction(message.msg);
			action.Invoke();
		}
		else
		{
			D.Error("Try to access not registered handler. packet Id : " + message.id);
		}
	}

	/*
	 *
Module['WebGLTest'].OnTest2 = function() {
    // Module.cwarp("name", null, [...args]);
}
// Module.cwrap("name", return, [...args]
	 * 
	 */

	#region in unity editor

#if UNITY_EDITOR
	private string host = "localhost";
	private int port = 8080;
	private static ClientWebSocket socket = new ClientWebSocket();
	private Thread thread;
	private bool listening;

	private void Init()
	{
		var uri = new Uri("ws://localhost:8080");
		socket.ConnectAsync(uri, CancellationToken.None).Wait();
		this.listening = true;
		this.thread = new Thread(ListenLoop) {IsBackground = true};
		this.thread.Start();
	}

	private async void ListenLoop()
	{
		while (this.listening)
		{
			using var ms = new MemoryStream();
			while (socket.State == WebSocketState.Open)
			{
				WebSocketReceiveResult result;
				do
				{
					if (!this.listening) return;
					var buffer = WebSocket.CreateClientBuffer(4096, 4096);
					result = await socket.ReceiveAsync(buffer, CancellationToken.None);
					ms.Write(buffer.Array, buffer.Offset, result.Count);
				} while (!result.EndOfMessage);

				if (result.MessageType == WebSocketMessageType.Text)
				{
					var str = Encoding.Default.GetString(ms.GetBuffer());
					ReceiveWebMessage(str);
				} 
				else if (result.MessageType == WebSocketMessageType.Close)
				{
					this.listening = false;
					await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, CancellationToken.None);
					socket.Dispose();
				}
				
				ms.SetLength(0);
			}
		}
	}

	public void OnApplicationQuit()
	{
		this.listening = false;
		this.thread.Join();
		socket?.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).Wait();
		socket?.Dispose();
	}

#endif
	
#if UNITY_WEBGL && !UNITY_EDITOR
	private static void SendPacket(PacketWrapper packet) => SendPacket(packet.data.id, JsonConvert.SerializeObject(packet.data));	
	
	[DllImport("__Internal")]
	private static extern void SendPacket(EPacketId id, string str);
#else 
	private static void SendPacket(PacketWrapper packet)
	{
		var str = JsonConvert.SerializeObject(packet);
		var arrSeg = new ArraySegment<byte>(Encoding.Default.GetBytes(str));
		
		socket.SendAsync(arrSeg, WebSocketMessageType.Text, true, CancellationToken.None).Wait();
	}
#endif
	#endregion

}
