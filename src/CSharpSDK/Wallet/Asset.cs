using System;

namespace CSharp_SDK
{
    public class Asset
    {

        public String code;

        public long offering;

        public long totalamount;

        public byte[] createuser;

        public byte[] owner;

        public int allowincrease;

        public byte[] info;

        public Asset(String code, long offering, long totalamount, byte[] createuser, byte[] owner, byte[] info)
        {
            this.code = code;
            this.offering = offering;
            this.totalamount = totalamount;
            this.createuser = createuser;
            this.owner = owner;
            this.info = info;
        }

    }
}
