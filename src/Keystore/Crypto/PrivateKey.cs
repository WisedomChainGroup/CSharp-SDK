
using System;
using System.Runtime.Serialization;
using System.Numerics;
using System.Linq;
namespace C__SDK
{
    [Serializable]
    class PrivateKey : ISerializable
    {
        public byte[] k;
        public String algorithm;
        public String format;

        private static String t = "1000000000000000000000000000000014def9dea2f79cd65812631a5cf5d3ec";

        public PrivateKey()
        {
            // Empty constructor required to compile.
        }

        public PrivateKey(byte[] k)
        {
            this.k = k;
        }

        public PrivateKey(byte[] k, String algorithm, String format)
        {
            this.k = k;
            this.algorithm = algorithm;
            this.format = format;
        }

        public PrivateKey(SerializationInfo info, StreamingContext context)
        {
            this.k = (byte[])info.GetValue("k", typeof(byte[]));
            this.algorithm = (string)info.GetValue("algorithm", typeof(string));
            this.format = (string)info.GetValue("format", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Use the AddValue method to specify serialized values.
            info.AddValue("k", this.k, typeof(byte[]));
            info.AddValue("algorithm", this.algorithm, typeof(string));
            info.AddValue("format", this.format, typeof(string));
        }

        public byte[] getBytes()
        {
            byte[] copy = new byte[this.k.Length];
            this.k.CopyTo(copy, 0);
            return this.k;
        }

        public byte[] getEncoded()
        {
            return this.getBytes();
        }

        public String getAlgorithm()
        {
            return algorithm;
        }

        public String getFormat()
        {
            return format;
        }

        public bool isValid()
        {
            return new BigInteger(this.k).CompareTo(new BigInteger(StringToByteArray(t))) <= 0;
        }

        public byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

    }
}