using System;

namespace Challenger.Email
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Please enter a key to use:");
            string key = "";
            //Console.WriteLine("Please enter a string to encrypt:");
            string plaintext = "";
            Console.WriteLine("");

            Console.WriteLine("Your encrypted string is:");
            string encryptedstring = StringCipher.Encrypt(plaintext, key);
            Console.WriteLine(encryptedstring);
            Console.WriteLine("");

            Console.WriteLine("Your decrypted string is:");
            string decryptedstring = StringCipher.Decrypt(encryptedstring, key);
            Console.WriteLine(decryptedstring);
            Console.WriteLine("");

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
