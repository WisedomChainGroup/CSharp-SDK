using System.Collections.Generic;
using System.Linq;
using System;

namespace CSharp_SDK
{
    public class RateHeightLock
    {
        public byte[] assetHash;
        public long oneTimeDepositMultiple;

        public int withDrawPeriodHeight;

        public string withDrawRate;

        public byte[] dest;

        public Dictionary<byte[], Extract> stateMap;
    }
}