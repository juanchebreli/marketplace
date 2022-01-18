namespace marketplace.Helpers.Interfaces
{
	public interface ICryptoEngine
	{
		public string Encrypt(string input, string key);
		public string Decrypt(string input, string key);

	}
}
