using System;
using Org.BouncyCastle.Crypto.Parameters;
namespace C__SDK
{
    public class Ed25519PublicKey
    {
        private Ed25519PublicKeyParameters publicKey;
        public Ed25519PublicKey(byte[] encoded)
        {
            this.publicKey = new Ed25519PublicKeyParameters(encoded, 0);
        }

        public Ed25519PublicKey(Ed25519PublicKeyParameters publicKey)
        {
            this.publicKey = publicKey;
        }

        public byte[] GetEncoded()
        {
            return this.publicKey.GetEncoded();
        }

    }
}