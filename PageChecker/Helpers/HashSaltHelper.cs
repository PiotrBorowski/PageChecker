using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PageCheckerAPI.Helpers
{
    public static class HashSaltHelper
    {
        public static void GeneratePasswordHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //using (HMACSHA512 hmac = new HMACSHA512())
            //{
            //    passwordSalt = hmac.Key;
            //    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            //}

            using (var provder = new RNGCryptoServiceProvider())
            {
                passwordSalt = new byte[24];
                provder.GetBytes(passwordSalt);

                var pbkdf2 = new Rfc2898DeriveBytes(password, passwordSalt, 100000);
                passwordHash = pbkdf2.GetBytes(24);
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
            //{
            //    byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            //    return passwordHash.SequenceEqual(computedHash);
            //}


            using (var provder = new RNGCryptoServiceProvider())
            {
                var pbkdf2 = new Rfc2898DeriveBytes(password, passwordSalt, 100000);
                var computedHash = pbkdf2.GetBytes(24);

                return passwordHash.SequenceEqual(computedHash);
            }
        }
    }
}
