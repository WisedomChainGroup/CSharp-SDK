using System;
using Isopoh.Cryptography.Argon2;
using Nethereum.Hex.HexConvertors.Extensions;

namespace C__SDK
{
    public class Argon2Manager
    {
        public static Argon2Manager Current { get; } = new Argon2Manager();

        public static int memoryCost = 20480;
        public static int timeCost = 4;
        public static int parallelism = 2;

        public byte[] hash(byte[] associatedData, byte[] salt)
        {
            var config = new Argon2Config
            {
                Type = Argon2Type.HybridAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = timeCost,
                MemoryCost = memoryCost,
                Lanes = parallelism,
                Threads = Environment.ProcessorCount,
                Password = Utils.Combine(System.Text.Encoding.ASCII.GetBytes(salt.ToHex()), associatedData),
                Salt = System.Text.Encoding.ASCII.GetBytes(salt.ToHex()), // >= 8 bytes if not null
                HashLength = 32 // >= 4
            };
            var argon2A = new Argon2(config);
            Isopoh.Cryptography.SecureArray.SecureArray<byte> hashA = argon2A.Hash();
            return hashA.Buffer;
        }

        public String kdf()
        {
            return "ARGON2id".ToLower();
        }

    }
}
