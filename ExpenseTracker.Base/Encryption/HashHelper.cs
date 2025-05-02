using System.Security.Cryptography;
using System.Text;

namespace ExpenseTracker.Base;
 public static class HashHelper
 {
    // Şifrelenmiş ve hashlenmiş kullanıcı şifresi oluşturma işlemi
    public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using (var hashMac = new HMACSHA512())
        {
            passwordSalt = Convert.ToBase64String(hashMac.Key);
            passwordHash = Convert.ToBase64String(hashMac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
    
     // Şifre Hash' inin, requestten gelen şifre ile doğruluğunu kontrol etme
    public static bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
    {
        byte[] salt = Convert.FromBase64String(passwordSalt);
        byte[] expectedPassHash = Convert.FromBase64String(passwordHash);
        using (var hmac = new HMACSHA512(salt))
        {
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != expectedPassHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
