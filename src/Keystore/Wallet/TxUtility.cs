using System;
using Newtonsoft.Json;
using Nethereum.Hex.HexConvertors.Extensions;

namespace C__SDK
{
    public class TxUtility
    {

        private static long serviceCharge = 200000L;

        private static long rate = 100000000L;


        public static string CreateSignToDeployforAssetChangeowner(string fromPubkeyStr, string tranTxHash, string prikeyStr, long nonce, string newOwner)
        {
            byte[] owner;
            if (newOwner.Equals("0000000000000000000000000000000000000000"))
            {
                owner = newOwner.HexToByteArray();
            }
            else
            {
                owner = KeystoreUtils.AddressToPubkeyHash(newOwner).HexToByteArray();
            }
            string rawTransactionHex = CreateCallForRuleAssetChangeOwner(fromPubkeyStr, tranTxHash, nonce, owner);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateCallForRuleAssetChangeOwner(string fromPubkeyStr, string txHash, long nonce, byte[] newOwner)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0X08;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] Amount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] hash = txHash.HexToByteArray();
            byte[] toPubkeyHash = RipemdManager.getHash(hash);
            byte[] payload = RLPUtils.EncodeList(newOwner);
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x00 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, Amount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateSignToDeployForRuleAsset(string fromPubkeyStr, string prikeyStr, long nonce, string code, BigDecimal offering, string createUser, string owner, int allowIncrease, string info)
        {
            byte[] infoUtf8 = System.Text.Encoding.UTF8.GetBytes(info);
            string rawTransactionHex = CreateDeployForRuleAsset(fromPubkeyStr, nonce, code, offering, offering, createUser.HexToByteArray(), KeystoreUtils.AddressToPubkeyHash(owner).HexToByteArray(), allowIncrease, infoUtf8);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateDeployForRuleAsset(string fromPubkeyStr, long nonce, String code, BigDecimal offering, BigDecimal totalAmount, byte[] createUser, byte[] owner, int allowIncrease, byte[] info)
        {
            offering = BigDecimal.Multiply(offering, new BigDecimal(rate));
            totalAmount = BigDecimal.Multiply(totalAmount, new BigDecimal(rate));

            Tuple<bool, string> tupleOffering = JudgeBigDecimalIsValid(offering);
            if (!tupleOffering.Item1)
            {
                return tupleOffering.Item2;
            }
            Tuple<bool, string> tupleTotalAmount = JudgeBigDecimalIsValid(totalAmount);
            if (!tupleTotalAmount.Item1)
            {
                return tupleTotalAmount.Item2;
            }

            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0x07;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 32字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] Amount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            String toPubkeyHashStr = "0000000000000000000000000000000000000000";
            byte[] toPubkeyHash = toPubkeyHashStr.HexToByteArray();
            byte[] payload = RLPUtils.EncodeList(code.ToBytesForRLPEncoding(), offering.ToLong().ToBytesForRLPEncoding(), totalAmount.ToLong().ToBytesForRLPEncoding(), createUser, owner, allowIncrease.ToBytesForRLPEncoding(), info);
            //长度
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x00 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, Amount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        private static Tuple<bool, string> JudgeBigDecimalIsValid(BigDecimal value)
        {
            if (value.CompareTo(BigDecimal.Zero) <= 0 || value.CompareTo(new BigDecimal(long.MaxValue)) > 0)
            {
                return new Tuple<bool, string>(false, "offering must be a positive number");
            }
            return new Tuple<bool, string>(true, "offering is ok");
        }

        public static string ClientToTransferMortgageWithdraw(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string txid, string prikeyStr)
        {
            string rawTransactionHex = CreateRawMortgageWithdrawTransaction(fromPubkeyStr, toPubkeyHashStr, amount, nonce, txid);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateRawMortgageWithdrawTransaction(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string txid)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型：抵押撤回
            byte[] type = new byte[1];
            type[0] = 0x0f;
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

        public static string ClientToTransferMortgage(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string prikeyStr)
        {
            string rawTransactionHex = CreateRawMortgageTransaction(fromPubkeyStr, toPubkeyHashStr, amount, nonce);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateRawMortgageTransaction(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型：抵押
            byte[] type = new byte[1];
            type[0] = 0x0e;
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
            byte[] allPayload = NumericsUtils.encodeUint32(0);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, Amount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

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
