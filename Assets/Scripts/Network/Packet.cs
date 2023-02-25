using System;

[Serializable]
public sealed class PacketWrapper
{
	public string @event = "DiceGame";
	public WebPacket data;
}

[Serializable]
public sealed class WebMessage
{
	public EPacketId id;
	public string msg;
}

[Serializable]
public class WebPacket
{
	public EPacketId id;
}

[Serializable]
public class Request : WebPacket
{
	public string sender;
}

[Serializable]
public class Response : WebPacket
{
	public EError result;
	public long tick;
}

[Serializable]
public sealed class RollDiceReq : Request
{
	
}

[Serializable]
public sealed class RollDiceRes : Response
{
	public int diceResult;
}

[Serializable]
public sealed class ChangeTurnNoti : Response
{
	public int turn;
}

[Serializable]
public sealed class Build : WebPacket
{
	public string Country;
	public int Land;
	public int Building;
	public int Villa;
	public int Hotel;
}