using System.Security.Cryptography;
using System.Text;

namespace ExpenseTracker.Base;
public static class PasswordEncryption
{
    /// <summary>
    /// Creates a MD5 hash from the given string input.
    /// </summary>
    /// <param name="input">The string to create the hash from.</param>
    /// <returns>The MD5 hash as a hexadecimal string.</returns>
    public static string CreateMD5(string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }


    /// <summary>
    /// Creates a MD5 hash from the concatenation of the given salt and a secret password.
    /// </summary>
    /// <param name="input">The string to create the hash from. (Currently unused)</param>
    /// <param name="salt">The salt to prepend to the secret password before hashing.</param>
    /// <returns>The MD5 hash as a hexadecimal string.</returns>

    public static string CreateMD5(string input, string salt)
    {
        var provider = MD5.Create();
        byte[] bytes = provider.ComputeHash(Encoding.ASCII.GetBytes(salt + input));
        string computedHash = BitConverter.ToString(bytes);
        return computedHash.Replace("-", "");
    }


    /// <summary>
    /// Generates a random password of the given length, using a-z, A-Z and 0-9.
    /// </summary>
    /// <param name="length">The length of the password to generate.</param>
    /// <returns>The generated password.</returns>
    public static string GeneratePassword(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}