namespace C__SDK
{
    /// <summary>
    ///     Wrapper class for decoded elements from an RLP encoded byte array.
    /// </summary>
    public interface IRLPElement
    {
        byte[] RLPData { get; }
    }
}