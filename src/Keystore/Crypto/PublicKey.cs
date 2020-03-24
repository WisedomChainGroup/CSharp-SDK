
using System;
using System.Runtime.Serialization;
namespace C__SDK
{
    [Serializable]
    class PublicKey : ISerializable
    {
        public byte[] k;
        public String algorithm;
        public String format;

        public PublicKey()
        {
            // Empty constructor required to compile.
        }

        public PublicKey(byte[] k)
        {
            this.k = k;
        }

        public PublicKey(byte[] k, String algorithm, String format)
        {
            this.k = k;
            this.algorithm = algorithm;
            this.format = format;
        }

        public PublicKey(SerializationInfo info, StreamingContext context)
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
            return copy;
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

    }
}