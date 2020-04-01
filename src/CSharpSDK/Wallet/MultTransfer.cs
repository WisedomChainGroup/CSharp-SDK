using System.Collections.Generic;

namespace CSharp_SDK
{
    public class MultTransfer
    {

        public int origin;//0是普通账户地址，1是多签地址

        public int dest;

        public List<byte[]> from;

        public List<byte[]> signatures;

        public byte[] to;

        public long value;

        public List<byte[]> pubkeyHashList;//公钥哈希数组
    }
}