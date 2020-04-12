using System;
using System.Linq;
namespace CSharp_SDK
{

}
public class NumericsUtils
{
    public static byte[] encodeUint64(long l)
    {
        return BitConverter.GetBytes(l).Reverse().ToArray();
    }

    public static byte[] encodeUint32(int l)
    {
        return BitConverter.GetBytes(l).Reverse().ToArray();
    }

    public static long decodeUint32(byte[] data)
    {

        return BitConverter.ToUInt32(data.Reverse().ToArray(), 0);
    }

    public static Tuple<byte, byte[]> getLengthByte(int length)
    {
        var tmpLength = length;
        byte byteNum = 0;

        while (tmpLength != 0)
        {
            ++byteNum;
            tmpLength = tmpLength >> 8;
        }
        tmpLength = length;
        var lenBytes = new byte[byteNum];
        for (var i = 0; i < byteNum; ++i)
            lenBytes[byteNum - 1 - i] = (byte)(tmpLength >> (8 * i));
        return new Tuple<byte, byte[]>(byteNum, lenBytes);
    }

}
