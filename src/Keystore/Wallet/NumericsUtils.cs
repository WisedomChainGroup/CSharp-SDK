using System;

namespace C__SDK
{

}
public class NumericsUtils
{
    public static byte[] encodeUint64(long l)
    {
        byte[] temp = BitConverter.GetBytes(l);
        Array.Reverse(temp);
        return temp;
    }

    public static byte[] encodeUint32(int l)
    {
          byte[] temp = BitConverter.GetBytes(l);
        Array.Reverse(temp);
        return temp;
    }

    public static long decodeUint32(byte[] data)
    {
        Array.Reverse(data);
        return BitConverter.ToUInt32(data, 0);
    }

}
