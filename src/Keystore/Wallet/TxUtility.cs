using System;
using Newtonsoft.Json;
using Nethereum.Hex.HexConvertors.Extensions;

namespace C__SDK
{
    public class TxUtility
    {

        private static long serviceCharge = 200000L;

        private static long rate = 100000000L;

        public static string ClientToTransferVoteWithdraw(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string prikeyStr, string txid)
        {
            string rawTransactionHex = CreateRawVoteWithdrawTransaction(fromPubkeyStr, toPubkeyHashStr, amount, nonce, txid);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateRawVoteWithdrawTransaction(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string txid)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型：投票撤回
            byte[] type = new byte[1];
            type[0] = 0x0d;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(20000L, serviceCharge));
            //转账金额 无符号64位
            BigDecimal bdAmount = BigDecimal.Multiply(amount, new BigDecimal(rate));
            byte[] Amount = NumericsUtils.encodeUint64(bdAmount.ToLong());
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] toPubkeyHash = toPubkeyHashStr.HexToByteArray();
            //payload
            byte[] payload = txid.HexToByteArray();
            //长度
            byte[] payloadlen = NumericsUtils.encodeUint32(payload.Length);
            byte[] allPayload = Utils.Combine(payloadlen, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, Amount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string ClientToTransferVote(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string prikeyStr)
        {
            string rawTransactionHex = CreateRawVoteTransaction(fromPubkeyStr, toPubkeyHashStr, amount, nonce);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateRawVoteTransaction(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型：投票
            byte[] type = new byte[1];
            type[0] = 0x02;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(20000L, serviceCharge));
            //转账金额 无符号64位
            BigDecimal bdAmount = BigDecimal.Multiply(amount, new BigDecimal(rate));
            byte[] Amount = NumericsUtils.encodeUint64(bdAmount.ToLong());
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] toPubkeyHash = toPubkeyHashStr.HexToByteArray();
            //长度
            byte[] allPayload = NumericsUtils.encodeUint32(0);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, Amount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }


        public static string ClientToTransferProve(string fromPubkeyStr, long nonce, byte[] payload, string prikeyStr)
        {
            string rawTransactionHex = CreateRawProveTransaction(fromPubkeyStr, payload, nonce);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateRawProveTransaction(string fromPubkeyStr, byte[] payload, long nonce)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型：抵押撤回
            byte[] type = new byte[1];
            type[0] = 0x03;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //本金 无符号64位
            byte[] Amount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] toPubkeyHash = new byte[20];
            //长度
            byte[] payloadlen = NumericsUtils.encodeUint32(payload.Length);
            byte[] allPayload = Utils.Combine(payloadlen, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, Amount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }


        public static string ClientToTransferAccount(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, string prikeyStr, long nonce)
        {
            String rawTransactionHex = CreateRawTransaction(fromPubkeyStr, toPubkeyHashStr, amount, nonce);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
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

        public static long obtainServiceCharge(long gas, long total)
        {
            BigDecimal a = new BigDecimal(gas);
            BigDecimal b = new BigDecimal(total);

            BigDecimal divide = BigDecimal.Divide(b, a);
            return divide.ToLong();
        }

    }

}
