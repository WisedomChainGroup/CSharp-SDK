using Nethereum.Hex.HexConvertors.Extensions;
using Newtonsoft.Json;

namespace CSharp_SDK
{
    public class WalletUtility
    {

        private static int saltLength = 32;
        private static int ivLength = 16;

        private static string newVersion = "2";

        public static string FromPassword(string password)
        {
            if (password.Length > 20 || password.Length < 8)
            {
                return JsonConvert.SerializeObject("");
            }
            else
            {
                KeyPair keyPair = KeyPair.generateEd25519KeyPair();
                PublicKey publicKey = keyPair.GetPublicKey();
                byte[] salt = new byte[saltLength];
                byte[] iv = new byte[ivLength];
                Org.BouncyCastle.Security.SecureRandom random = new Org.BouncyCastle.Security.SecureRandom();
                random.NextBytes(iv);
                Org.BouncyCastle.Security.SecureRandom sr = new Org.BouncyCastle.Security.SecureRandom();
                sr.NextBytes(salt);
                Argon2Manager argon2Manager = Argon2Manager.Current;
                byte[] derivedKey = argon2Manager.hash(System.Text.Encoding.ASCII.GetBytes(password), salt);
                AesManager aes = AesManager.Current;
                byte[] cipherPrivKey = aes.Encryptdata(derivedKey, keyPair.GetPrivateKey().getBytes(), iv);
                Sha3Keccack sha3Keccack = Sha3Keccack.Current;
                byte[] mac = sha3Keccack.CalculateHash(Utils.Combine(derivedKey, cipherPrivKey));
                Crypto crypto = new Crypto(
                       AesManager.cipher, new string(cipherPrivKey.ToHex()),
                       new Cipherparams(
                               new string(iv.ToHex())
                       )
               );
                Kdfparams kdfparams = new Kdfparams(Argon2Manager.memoryCost, Argon2Manager.timeCost, Argon2Manager.parallelism, new string(salt.ToHex()));
                Address address = new Address(publicKey);
                Keystore ks = new Keystore(address.getAddress(), crypto, Utils.generateUUID(),
                      newVersion, new string(mac.ToHex()), argon2Manager.kdf(), kdfparams
              );
                return JsonConvert.SerializeObject(ks);
            }
        }

    }
}

