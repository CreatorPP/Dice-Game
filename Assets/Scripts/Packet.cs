using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class Serializer
{
	public static byte[] Serialize(IPacket packet) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(packet));
	public static T Deserialize<T>(byte[] packet)	=> JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(packet));
}

public interface IPacket
{

}

[Serializable]
public class CardTemplate : IPacket
{
	public int id;
	public string name;
	public string desc;
	public ECardEffect effect;
}