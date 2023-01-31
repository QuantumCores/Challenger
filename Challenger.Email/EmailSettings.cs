namespace Challenger.Email
{
    public class EmailSettings
    {
        public string Address { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string Key { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string GetPassword()
        {
            return StringCipher.Decrypt(Password, Key);
        }
    }
}
