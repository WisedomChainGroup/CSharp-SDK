using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
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

        public bool verify(byte[] msg, byte[] signature)
        {
            var verifier = new Ed25519Signer();
            verifier.Init(false, publicKey);
            verifier.BlockUpdate(msg, 0, msg.Length);
            return verifier.VerifySignature(signature);
        }

    }
}
