using EF_DiegoQuispeR.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace EF_DiegoQuispeR.Models
{
    public class LoginRequestClass
    {
        public string Username { get; set; }
        public string Clave { get; set; }
        [JsonIgnore]
        public string Salt { get; set; }
        [JsonIgnore]
        public int Iterations {  get; set; }

        public LoginRequestClass(string Username, string Clave, string Salt, int Iterations)
        {
            this.Username = Username;
            this.Salt = Salt;
            this.Iterations = Iterations;
            this.Clave = Clave;
            System.Diagnostics.Debug.WriteLine($"[CTOR] Salt recibidas: {Salt}");
            System.Diagnostics.Debug.WriteLine($"[CTOR] Iterations recibidas: {Iterations}");
            SetHashPassword(Clave);
        }

        public LoginRequestClass() { }
   
        public void SetHashPassword(string passw)
        {
            byte[] saltBytes;

            if (string.IsNullOrEmpty(Salt))
            {
                saltBytes = new byte[16];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(saltBytes);
                Salt = Convert.ToBase64String(saltBytes);
            }

            saltBytes = Convert.FromBase64String(Salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(passw, saltBytes, Iterations, HashAlgorithmName.SHA256);

            byte[] hashBytes = pbkdf2.GetBytes(32);
            Clave = Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string storedHash)
        {
            return Clave == storedHash;
        }
    }
}
