using System;

namespace CSharp_SDK
{
    public class Keystore
    {
        public string address;
        public Crypto crypto;
        public Kdfparams kdfparams;
        public string id;
        public string version;
        public string mac;
        public string kdf;
        public Keystore(string address, Crypto crypto, string id, string version, string mac, string kdf, Kdfparams kdfparams)
        {
            this.address = address;
            this.crypto = crypto;
            this.id = id;
            this.version = version;
            this.mac = mac;
            this.kdf = kdf;
            this.kdfparams = kdfparams;

        }
        public Keystore()
        {
        }

        public string getAddress()
        {
            return address;
        }

        public void setAddress(string address)
        {
            this.address = address;
        }

        public Crypto getCrypto()
        {
            return crypto;
        }

        public void setCrypto(Crypto crypto)
        {
            this.crypto = crypto;
        }

        public string getId()
        {
            return id;
        }

        public void setId(string id)
        {
            this.id = id;
        }

        public string getVersion()
        {
            return version;
        }

        public void setVersion(string version)
        {
            this.version = version;
        }

        public string getMac()
        {
            return mac;
        }

        public void setMac(string mac)
        {
            this.mac = mac;
        }

        public string getKdf()
        {
            return kdf;
        }

        public void setKdf(string kdf)
        {
            this.kdf = kdf;
        }

        public Kdfparams getKdfparams()
        {
            return kdfparams;
        }

        public void setKdfparams(Kdfparams kdfparams)
        {
            this.kdfparams = kdfparams;
        }
    }
}
