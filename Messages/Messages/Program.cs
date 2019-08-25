using LeroyMerlinClient;
using VkNet;
namespace Messages
{
	public static class Program
	{
		public static VkApi vk = new VkApi();
		public static long chatID = 22409448;
		public static string phone, pass;
		public static ulong appIP;
		public static long cSid;
		public static string name = "Коллега";
		public static MessageSer settings;
	}
}