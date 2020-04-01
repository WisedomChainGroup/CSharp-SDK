using System.Collections.Generic;

namespace CSharp_SDK
{
    public class RLPCollection : List<IRLPElement>, IRLPElement
    {
        public byte[] RLPData { get; set; }
    }
}