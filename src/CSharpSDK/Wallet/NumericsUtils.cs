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

}
