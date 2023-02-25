using System;
using Newtonsoft.Json;

public interface IMessageHandler
{
	Action MakeAction(string message);
}

public class MessageHandler<T> : IMessageHandler where T : WebPacket
{
	private Action<T> handler;

	public MessageHandler(Action<T> handler)
	{
		this.handler = handler;
	}

	public Action MakeAction(string message)
	{
		var msg = JsonConvert.DeserializeObject<T>(message);
		return () => this.handler(msg);
	}
}