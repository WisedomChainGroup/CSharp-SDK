using System;

namespace C__SDK
{
    public class Address
    {
        private PublicKey publicKey;

        public Address(PublicKey pubkey)
        {
            this.publicKey = pubkey;
        }
    }
}