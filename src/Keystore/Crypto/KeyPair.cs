using Org.BouncyCastle.Crypto.Parameters;

namespace C__SDK
{
    public class KeyPair
    {
        private PublicKey publicKey;
        private PrivateKey privateKey;
        Org.BouncyCastle.Security.SecureRandom secureRandom = new Org.BouncyCastle.Security.SecureRandom();

        public KeyPair generateEd25519KeyPair()
        {
            Org.BouncyCastle.Crypto.Generators.Ed25519KeyPairGenerator kpg = new Org.BouncyCastle.Crypto.Generators.Ed25519KeyPairGenerator();
            kpg.Init(new Ed25519KeyGenerationParameters(secureRandom));
            Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair kp = kpg.GenerateKeyPair();
            KeyPair nkp = new KeyPair();
            Ed25519PrivateKeyParameters privateKey = (Ed25519PrivateKeyParameters)kp.Private;
            Ed25519PublicKeyParameters publicKey = (Ed25519PublicKeyParameters)kp.Public;
            nkp.publicKey = new PublicKey(publicKey.GetEncoded());
            nkp.privateKey = new PrivateKey(privateKey.GetEncoded());
            if (nkp.privateKey.isValid())
            {
                return nkp;
            }
            return generateEd25519KeyPair();
        }

    }
}
