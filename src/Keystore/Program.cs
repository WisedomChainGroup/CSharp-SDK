using System;
using Nethereum.Hex.HexConvertors.Extensions;

namespace C__SDK
{
    class Program
    {

        private static long serviceCharge = 200000L;

        private static long rate = 100000000L;

        static void Main(string[] args)
        {
            // byte[] byteArray = {
            //   0,  54, 101, 196 };
            // string result = CreateRawTransaction("27724e1b96cebc66acb8682d12eb92dac4df6a4ea0b509f852dcd77ebeeabb93", "0e015bc15fa1b0156d3f62b16b397d6120faae5b", new BigDecimal(100), 3L);
            // byte[] signRawBasicTransaction = SignRawBasicTransaction(result, "d8dde33d8a3bdbeeb52843ff0e2a31140bdbf32997213e8ca871a2aa1724b68f").HexToByteArray();
            string s = TxUtility.ClientToTransferAccount("27724e1b96cebc66acb8682d12eb92dac4df6a4ea0b509f852dcd77ebeeabb93", "0e015bc15fa1b0156d3f62b16b397d6120faae5b", new BigDecimal(100),"d8dde33d8a3bdbeeb52843ff0e2a31140bdbf32997213e8ca871a2aa1724b68f", 4L);
            // byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            // String txHash = hash.ToHex();
            // String traninfo = signRawBasicTransaction.ToHex();
            // Console.WriteLine("---------txHash-------" + txHash);
            Console.WriteLine("---------s-------" + s);
        }

        public static String SignRawBasicTransaction(String rawTransactionHex, String prikeyStr)
        {
            byte[] rawTransaction = rawTransactionHex.HexToByteArray();
            byte[] privkey = prikeyStr.HexToByteArray();
            byte[] version = Utils.CopyByteArray(rawTransaction, 0, 1);
            //type
            byte[] type = Utils.CopyByteArray(rawTransaction, 1, 1);
            //nonce
            byte[] nonce = Utils.CopyByteArray(rawTransaction, 2, 8);
            //from
            byte[] from = Utils.CopyByteArray(rawTransaction, 10, 32);
            //gasprice
            byte[] gasprice = Utils.CopyByteArray(rawTransaction, 42, 8);
            //amount
            byte[] amount = Utils.CopyByteArray(rawTransaction, 50, 8);
            //signo
            byte[] signo = Utils.CopyByteArray(rawTransaction, 58, 64);
            //to
            byte[] to = Utils.CopyByteArray(rawTransaction, 122, 20);

            //payloadlen
            byte[] payloadlen = Utils.CopyByteArray(rawTransaction, 142, 4);

            //payload
            byte[] payload = Utils.CopyByteArray(rawTransaction, 146, (int)NumericsUtils.decodeUint32(payloadlen));

            byte[] rawTransactionNoSign = Utils.Combine(version, type, nonce, from, gasprice, amount, signo, to, payloadlen, payload);
            byte[] rawTransactionNoSig = Utils.Combine(version, type, nonce, from, gasprice, amount);
            byte[] sig = new Ed25519PrivateKey(privkey).sign(rawTransactionNoSign);
            Sha3Keccack sha3Keccack = Sha3Keccack.Current;
            byte[] transha = sha3Keccack.CalculateHash(Utils.Combine(rawTransactionNoSig, sig, to, payloadlen, payload));
            byte[] signRawBasicTransaction = Utils.Combine(version, transha, type, nonce, from, gasprice, amount, sig, to, payloadlen, payload);
            return signRawBasicTransaction.ToHex();
        }

        public static string CreateRawTransaction(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce)
        {
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型：WDC转账
            byte[] type = new byte[1];
            type[0] = 0x01;
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(50000L, serviceCharge));
            BigDecimal bdAmount = BigDecimal.Multiply(amount, new BigDecimal(rate));
            byte[] Amount = NumericsUtils.encodeUint64(bdAmount.ToLong());
            byte[] signull = new byte[64];
            byte[] toPubkeyHash = toPubkeyHashStr.HexToByteArray();
            byte[] allPayload = NumericsUtils.encodeUint32(0);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, Amount, signull, toPubkeyHash, allPayload);
            return new string(rawTransaction.ToHex());
        }

        public static long obtainServiceCharge(long gas, long total)
        {
            BigDecimal a = new BigDecimal(gas);
            BigDecimal b = new BigDecimal(total);

            BigDecimal divide = BigDecimal.Divide(b, a);
            return divide.ToLong();
        }
    }
}
