using System;

namespace CSharp_SDK
{
    public class Address
    {

        private PublicKey publicKey;

        private String address;

        public Address(PublicKey publicKey)
        {
            this.address = KeystoreUtils.PubkeyToAddress(publicKey.getBytes());
        }

        public String getAddress()
        {
            return address;
        }

    }
}