using System;
using Org.BouncyCastle.Crypto.Parameters;
namespace C__SDK
{
    public class Ed25519PrivateKey
    {
        private Ed25519PrivateKeyParameters privateKey;
        public Ed25519PrivateKey(byte[] encoded)
        {
            this.privateKey = new Ed25519PrivateKeyParameters(encoded, 0);
        }

        Ed25519PrivateKey(Ed25519PrivateKeyParameters privateKey)
        {
            this.privateKey = privateKey;
        }

        public Ed25519PublicKey GeneratePublicKey()
        {
            return new Ed25519PublicKey(this.privateKey.GeneratePublicKey());
        }

    }
}