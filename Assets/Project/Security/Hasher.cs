using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Project.Security
{
    public sealed class Hasher
    {
        private readonly int degreesOfParallelism;
        private readonly int iterations;
        private readonly int memorySize;

        public Hasher(int degreesOfParallelism, int iterations, int memorySize)
        {
            this.degreesOfParallelism = degreesOfParallelism;
            this.iterations = iterations;
            this.memorySize = memorySize;
        }

        public static byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        public byte[] HashPassword(string password, byte[] salt) =>
            new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = degreesOfParallelism,
                Iterations = iterations,
                MemorySize = memorySize,
                
            }.GetBytes(16);

        public bool VerifyHash(string password, byte[] salt, byte[] hash) =>
            hash.SequenceEqual(HashPassword(password, salt));   
    }
}