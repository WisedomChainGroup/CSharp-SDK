using System;
using Newtonsoft.Json;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace CSharp_SDK
{
    public class TxUtility
    {

        private static long serviceCharge = 200000L;

        private static long rate = 100000000L;

        public static string GetHashTimeBlock(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            var collections = RLP.Decode(newPayload) as RLPCollection;
            byte[] assetHash = collections[0].RLPData;
            byte[] pubkeyHash = collections[1].RLPData;
            JObject json = new JObject { { "assetHash", assetHash.ToHex() }, { "pubkeyHash", pubkeyHash.ToHex() } };
            return json.ToString();
        }

        public static string GetHashTimeBlockGet(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            var collections = RLP.Decode(newPayload) as RLPCollection;
            byte[] transferHash = collections[0].RLPData;
            string originText = collections[1].RLPData.ToStringFromRLPDecoded();
            long timestamp = collections[2].RLPData.ToIntFromRLPDecoded();
            JObject json = new JObject { { "transferHash", transferHash.ToHex() }, { "originText", originText } };
            return json.ToString();
        }

        public static string GetHashTimeBlockTransfer(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            var collections = RLP.Decode(newPayload) as RLPCollection;
            long value = collections[0].RLPData.ToIntFromRLPDecoded();
            byte[] hashResult = collections[1].RLPData;
            long timestamp = collections[2].RLPData.ToIntFromRLPDecoded();
            JObject json = new JObject { { "value", value }, { "hashResult", hashResult.ToHex() }, { "timestamp", timestamp } };
            return json.ToString();
        }

        public static string GetHashHeightBlock(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            var collections = RLP.Decode(newPayload) as RLPCollection;
            byte[] assetHash = collections[0].RLPData;
            byte[] pubkeyHash = collections[1].RLPData;
            JObject json = new JObject { { "assetHash", assetHash.ToHex() }, { "pubkeyHash", pubkeyHash.ToHex() } };
            return json.ToString();
        }

        public static string GetHashHeightBlockGet(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            var collections = RLP.Decode(newPayload) as RLPCollection;
            byte[] transferHash = collections[0].RLPData;
            string originText = collections[1].RLPData.ToStringFromRLPDecoded();
            JObject json = new JObject { { "transferHash", transferHash.ToHex() }, { "originText", originText } };
            return json.ToString();
        }

        public static string GetHashHeightBlockTransfer(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            var collections = RLP.Decode(newPayload) as RLPCollection;
            long value = collections[0].RLPData.ToIntFromRLPDecoded();
            byte[] hashResult = collections[1].RLPData;
            long height = collections[2].RLPData.ToIntFromRLPDecoded();
            JObject json = new JObject { { "value", value }, { "hashResult", hashResult.ToHex() }, { "height", height } };
            return json.ToString();
        }

        public static string GetMultTransfer(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            MultTransfer multTransfer = RLPUtils.DecodeMultTransfer(newPayload);
            List<string> from = multTransfer.from.Select(x => x.ToHex()).ToList();
            List<string> signatures = multTransfer.signatures.Select(x => x.ToHex()).ToList();
            List<string> pubHashList = multTransfer.pubkeyHashList.Select(x => x.ToHex()).ToList();
            JObject json = new JObject { { "to", multTransfer.to.ToHex() }, { "origin", multTransfer.origin }, { "dest", multTransfer.dest }, { "value", multTransfer.value }, { "signatures", signatures.ToString() }, { "pubHashList", pubHashList.ToString() }, { "from", from.ToString() } };
            return json.ToString();
        }

        public static string GetMultiple(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            Multiple multiple = RLPUtils.DecodeMultiple(newPayload);
            List<string> pubList = multiple.pubList.Select(x => x.ToHex()).ToList();
            List<string> signatures = multiple.signatures.Select(x => x.ToHex()).ToList();
            List<string> pubHashList = multiple.pubkeyHashList.Select(x => x.ToHex()).ToList();
            JObject json = new JObject { { "assetHash", multiple.assetHash.ToHex() }, { "max", multiple.max }, { "min", multiple.min }, { "pubList", pubList.ToString() }, { "signatures", signatures.ToString() }, { "pubHashList", pubHashList.ToString() } };
            return json.ToString();
        }

        public static string GetAssetTransfer(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            var collections = RLP.Decode(newPayload) as RLPCollection;
            byte[] from = collections[0].RLPData;
            byte[] to = collections[1].RLPData;
            long value = collections[2].RLPData.ToIntFromRLPDecoded();
            JObject json = new JObject { { "from", from.ToHex() }, { "to", to.ToHex() }, { "value", value } };
            return json.ToString();
        }

        public static string GetAssetChangeOwner(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            byte[] newOwner = RLP.Decode(newPayload).RLPData;
            JObject json = new JObject { "newOwner", newOwner.ToHex() };
            return json.ToString();
        }

        public static string GetAssetIncreased(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            long amount = RLP.Decode(newPayload).RLPData.ToIntFromRLPDecoded();
            JObject json = new JObject { "assetIncreased", { "amount", amount } };
            return json.ToString();
        }

        public static string GetAsset(byte[] payload)
        {
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            Asset asset = RLPUtils.DecodeAsset(newPayload);
            JObject json = new JObject { { "code", asset.code }, { "offering", asset.offering }, { "totalAmount", asset.totalAmount }, { "createUser", asset.createUser.ToHex() }, { "owner", asset.owner.ToHex() }, { "allowIncrease", asset.allowIncrease }, { "info", asset.info.ToHex() } };
            return json.ToString();
        }

        public static string CreateHashHeightBlockTransferForDeploy(string fromPubkeyStr, string prikeyStr, string txGetHash, long nonce, BigDecimal amount, string hashResult, BigDecimal blockHeight)
        {
            string newHashResult = hashResult.Replace(" ", "");
            if (newHashResult.Equals(""))
            {
                JObject json = new JObject { { "message", "hash result can not be null" } };
                return json.ToString();
            }
            byte[] hashResultUtf8 = System.Text.Encoding.UTF8.GetBytes(newHashResult);
            if (hashResultUtf8.Length > 512 || hashResultUtf8.Length <= 0)
            {
                JObject json = new JObject { { "message", "hash result length is too large or too short" } };
                return json.ToString();
            }
            string rawTransactionHex = HashHeightBlockTransferForDeploy(fromPubkeyStr, txGetHash, nonce, amount, Sha3Keccack.Current.CalculateHash(hashResultUtf8), blockHeight);
            if (rawTransactionHex.Contains("must be a positive number"))
            {
                return rawTransactionHex;
            }
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string HashHeightBlockTransferForDeploy(string fromPubkeyStr, string txHash, long nonce, BigDecimal amount, byte[] hashResult, BigDecimal blockHeight)
        {
            amount = BigDecimal.Multiply(amount, new BigDecimal(rate));

            Tuple<bool, string> tupleAmount = JudgeBigDecimalIsValid(amount, "amount");
            if (!tupleAmount.Item1)
            {
                return tupleAmount.Item2;
            }
            if (blockHeight.CompareTo(BigDecimal.Zero) < 0)
            {
                JObject json = new JObject { { "message", "block height must be positive long number" } };
                return json.ToString();
            }
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
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] toPubkeyHash = RipemdManager.getHash(txHash.HexToByteArray());
            //构造payload
            byte[] payload = RLPUtils.EncodeList(amount.ToLong().ToBytesForRLPEncoding(), hashResult, blockHeight.ToLong().ToBytesForRLPEncoding());
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x06 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateHashHeightBlockGetForDeploy(string fromPubkeyStr, string prikeyStr, string txGetHash, long nonce, string transferHash, string originText)
        {
            if (originText.Equals(""))
            {
                JObject json = new JObject { { "message", "origin test can not be null" } };
                return json.ToString();
            }
            string newOriginText = originText.Replace(" ", "");
            byte[] originTextUtf8 = System.Text.Encoding.UTF8.GetBytes(newOriginText);
            if (originTextUtf8.Length > 512 || originTextUtf8.Length <= 0)
            {
                JObject json = new JObject { { "message", "origin text length is too large or too short" } };
                return json.ToString();
            }
            string rawTransactionHex = HashHeightBlockGetForDeploy(fromPubkeyStr, txGetHash, nonce, transferHash.HexToByteArray(), newOriginText);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }


        public static string HashHeightBlockGetForDeploy(string fromPubkeyStr, string txHash, long nonce, byte[] transferHash, string originText)
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
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] toPubkeyHash = RipemdManager.getHash(txHash.HexToByteArray());
            byte[] payload = RLPUtils.EncodeList(transferHash, originText.ToBytesForRLPEncoding());
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x07 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateHashHeightBlockForDeploy(string fromPubkeyStr, string prikeyStr, long nonce, string assetHash, string pubkeyHash)
        {
            byte[] hash;
            if (assetHash.Equals("0000000000000000000000000000000000000000"))
            {
                hash = assetHash.HexToByteArray();
            }
            else
            {
                hash = RipemdManager.getHash(assetHash.HexToByteArray());
            }
            string rawTransactionHex = HashHeightBlockForDeploy(fromPubkeyStr, nonce, hash, pubkeyHash.HexToByteArray());
            
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string HashHeightBlockForDeploy(string fromPubkeyStr, long nonce, byte[] assetHash, byte[] pubkeyHash)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0X07;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希,填0
            byte[] toPubkeyHash = "0000000000000000000000000000000000000000".HexToByteArray();
            byte[] payload = RLPUtils.EncodeList(assetHash, pubkeyHash);
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x03 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateHashTimeBlockTransferForDeploy(string fromPubkeyStr, string prikeyStr, String txGetHash, long nonce, BigDecimal amount, string hashResult, BigDecimal timestamp)
        {
            string newHashResult = hashResult.Replace(" ", "");
            if (newHashResult.Equals(""))
            {
                JObject json = new JObject { { "message", "hash result can not be null" } };
                return json.ToString();
            }
            byte[] hashResultUtf8 = System.Text.Encoding.UTF8.GetBytes(newHashResult);
            if (hashResultUtf8.Length > 512 || hashResultUtf8.Length <= 0)
            {
                JObject json = new JObject { { "message", "hash result length is too large or too short" } };
                return json.ToString();
            }
            string rawTransactionHex = HashTimeBlockTransferForDeploy(fromPubkeyStr, txGetHash, nonce, amount, Sha3Keccack.Current.CalculateHash(hashResultUtf8), timestamp);
            if (rawTransactionHex.Contains("must be a positive number"))
            {
                return rawTransactionHex;
            }
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string HashTimeBlockTransferForDeploy(string fromPubkeyStr, string txHash, long nonce, BigDecimal amount, byte[] hashResult, BigDecimal timestamp)
        {
            amount = BigDecimal.Multiply(amount, new BigDecimal(rate));

            Tuple<bool, string> tupleAmount = JudgeBigDecimalIsValid(amount, "amount");
            if (!tupleAmount.Item1)
            {
                return tupleAmount.Item2;
            }
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
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希,填0
            byte[] toPubkeyHash = RipemdManager.getHash(txHash.HexToByteArray());
            byte[] payload = RLPUtils.EncodeList(amount.ToLong().ToBytesForRLPEncoding(), hashResult, timestamp.ToLong().ToBytesForRLPEncoding());
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x04 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateHashTimeBlockGetForDeploy(string fromPubkeyStr, string prikeyStr, string txGetHash, long nonce, string transferHash, string originText)
        {
            if (originText.Equals(""))
            {
                JObject json = new JObject { { "message", "origin test can not be null" } };
                return json.ToString();
            }
            string newOriginText = originText.Replace(" ", "");
            byte[] originTextUtf8 = System.Text.Encoding.UTF8.GetBytes(newOriginText);
            if (originTextUtf8.Length > 512 || originTextUtf8.Length <= 0)
            {
                JObject json = new JObject { { "message", "origin text length is too large or too short" } };
                return json.ToString();
            }
            string rawTransactionHex = HashTimeBlockGetForDeploy(fromPubkeyStr, txGetHash, nonce, transferHash.HexToByteArray(), newOriginText);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string HashTimeBlockGetForDeploy(string fromPubkeyStr, string txHash, long nonce, byte[] transferHash, string originText)
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
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //部署事务的事务哈希
            byte[] toPubkeyHash = RipemdManager.getHash(txHash.HexToByteArray());
            byte[] payload = RLPUtils.EncodeList(transferHash, originText.ToBytesForRLPEncoding());
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x05 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateHashTimeBlockForDeploy(string fromPubkeyStr, string prikeyStr, long nonce, string assetHash, string pubkeyHash)
        {
            byte[] hash;
            if (assetHash.Equals("0000000000000000000000000000000000000000"))
            {
                hash = assetHash.HexToByteArray();
            }
            else
            {
                hash = RipemdManager.getHash(assetHash.HexToByteArray());
            }
            string rawTransactionHex = HashTimeBlockForDeploy(fromPubkeyStr, nonce, hash, pubkeyHash.HexToByteArray());
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string HashTimeBlockForDeploy(string fromPubkeyStr, long nonce, byte[] assetHash, byte[] pubkeyHash)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0X07;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希,填0
            String toPubkeyHashStr = "0000000000000000000000000000000000000000";
            byte[] toPubkeyHash = toPubkeyHashStr.HexToByteArray();
            byte[] payload = RLPUtils.EncodeList(assetHash, pubkeyHash);
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x02 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateMultiSignatureToDeployForRuleSignSplice(string prikeyStr, string pubkeyFirstSign, string fromPubkey, string txHashRule, long nonce, string signFirst, string pubkeyOther, string signOther, int type)
        {
            byte[] payload = ParsePayloadFromSignRawBasicTransaction(signFirst.HexToByteArray());
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            MultTransfer multTransfer = RLPUtils.DecodeMultTransfer(newPayload);
            List<byte[]> signatures = multTransfer.signatures;
            int origin = multTransfer.origin;
            int dest = multTransfer.dest;
            byte[] to = multTransfer.to;
            long value = multTransfer.value;
            //公钥数组
            List<byte[]> fromList = new List<byte[]>();
            if (multTransfer.from.ToArray().Length == 0)
            {
                fromList.Add(fromPubkey.HexToByteArray());
            }
            else
            {
                for (int i = 0; i < multTransfer.from.ToArray().Length; i++)
                {
                    fromList.Add(multTransfer.from[i]);
                }
            }
            if (!fromPubkey.Equals(pubkeyOther))
            {
                fromList.Add(pubkeyOther.HexToByteArray());
            }
            List<byte[]> pubkeyHashList = multTransfer.pubkeyHashList;
            string rawTransactionHex;
            if (type == 1)
            {
                rawTransactionHex = CreateMultiSignatureForTransferSplice(fromPubkey, txHashRule, nonce, origin, dest, fromList, signatures, to, new BigDecimal(value), pubkeyHashList);
            }
            else if (type == 2 || type == 3)
            {
                byte[] sign = ParseSignatureFromSignRawBasicTransaction(signOther.HexToByteArray());
                Ed25519PublicKey ed25519PublicKey = new Ed25519PublicKey(pubkeyOther.HexToByteArray());
                bool isValid = ed25519PublicKey.verify(pubkeyFirstSign.HexToByteArray(), sign);
                if (!isValid)
                {
                    JObject json = new JObject { { "message", "others sign is wrong" } };
                    return json.ToString();
                }
                signatures.Add(sign);
                rawTransactionHex = CreateMultiSignatureForTransferSplice(fromPubkey, txHashRule, nonce, origin, dest, fromList, signatures, to, new BigDecimal(value), pubkeyHashList);
            }
            else
            {
                JObject json = new JObject { { "message", "type must be 1, 2, 3" } };
                return json.ToString();
            }
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateMultiSignatureForTransferSplice(string fromPubkeyStr, string txHash, long nonce, int origin, int dest, List<byte[]> from, List<byte[]> signatures, byte[] to, BigDecimal amount, List<byte[]> pubkeyHashList)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0x08;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] toPubkeyHash = RipemdManager.getHash(txHash.HexToByteArray());
            byte[] payload = RLPUtils.EncodeMultTransfer(origin.ToBytesForRLPEncoding(), dest.ToBytesForRLPEncoding(), from, signatures, to, amount.ToLong().ToBytesForRLPEncoding(), pubkeyHashList);
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x03 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateMultiSignatureToDeployForRuleOther(string fromPubkeyStr, string pubkeyFirstSign, string prikeyStr, bool isPutSign)
        {
            byte[] signRawBasicTransaction = SignRawBasicTransactionAndIsSign(pubkeyFirstSign, prikeyStr, isPutSign).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            JObject json = new JObject { { "pubkeyOther", signRawBasicTransaction.ToHex() }, { "signOther", fromPubkeyStr }, { "data", txHash }, { "message", traninfo } };
            return json.ToString();
        }

        public static string SignRawBasicTransactionAndIsSign(string rawTransactionHex, string prikeyStr, bool isPutSign)
        {
            byte[] rawTransaction = rawTransactionHex.HexToByteArray();
            //私钥字节数组
            byte[] privkey = prikeyStr.HexToByteArray();
            //version
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
            byte[] sign = Utils.CopyByteArray(rawTransaction, 58, 64);
            //to
            byte[] to = Utils.CopyByteArray(rawTransaction, 122, 20);
            //payloadlen
            byte[] payloadlen = Utils.CopyByteArray(rawTransaction, 142, 4);
            //payload
            byte[] payload = Utils.CopyByteArray(rawTransaction, 146, (int)NumericsUtils.decodeUint32(payloadlen));
            byte[] rawTransactionNoSign = Utils.Combine(version, type, nonce, from, gasprice, amount, sign, to, payloadlen, payload);
            byte[] rawTransactionNoSig = Utils.Combine(version, type, nonce, from, gasprice, amount);
            //签名数据
            byte[] sig = new byte[64];
            if (isPutSign)
            {
                sig = new Ed25519PrivateKey(privkey).sign(rawTransactionNoSign);
            }
            byte[] transha = Sha3Keccack.Current.CalculateHash(Utils.Combine(rawTransactionNoSig, sig, to, payloadlen, payload));
            byte[] signRawBasicTransaction = Utils.Combine(version, transha, type, nonce, from, gasprice, amount, sig, to, payloadlen, payload);
            return signRawBasicTransaction.ToHex();
        }

        public static string CreateMultiSignatureToDeployForRuleFirst(string fromPubkeyStr, string prikeyStr, string txHashRule, long nonce, int origin, int dest, List<string> signatures, string to, BigDecimal value, List<string> pubkeyHashList)
        {
            List<byte[]> signs = signatures.Select(x => x.HexToByteArray()).ToList();
            List<byte[]> pubkeyHashs = pubkeyHashList.Select(x => x.HexToByteArray()).ToList();
            string rawTransactionHexFirst = CreateMultiSignatureForTransferFirst(fromPubkeyStr, txHashRule, nonce, origin, dest, new List<byte[]>(), new List<byte[]>(), to.HexToByteArray(), value, pubkeyHashs);
            if (rawTransactionHexFirst.Contains("must be a positive number"))
            {
                return rawTransactionHexFirst;
            }
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHexFirst, prikeyStr).HexToByteArray();
            byte[] sign = ParseSignatureFromSignRawBasicTransaction(signRawBasicTransaction);
            string rawTransactionHexFirstSign = CreateMultiSignatureForTransferFirst(fromPubkeyStr, txHashRule, nonce, origin, dest, new List<byte[]>(), new List<byte[]>() { sign }, to.HexToByteArray(), value, pubkeyHashs);
            if (rawTransactionHexFirstSign.Contains("must be a positive number"))
            {
                return rawTransactionHexFirstSign;
            }
            byte[] signRawBasicTransactionSign = SignRawBasicTransaction(rawTransactionHexFirstSign, prikeyStr).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransactionSign, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            JObject json = new JObject { { "pubkeyFirstSign", rawTransactionHexFirst }, { "pubkeyFirst", fromPubkeyStr }, { "signFirst", traninfo }, { "data", txHash } };
            return json.ToString();

        }

        public static string CreateMultiSignatureForTransferFirst(string fromPubkeyStr, string txHash, long nonce, int origin, int dest, List<byte[]> from, List<byte[]> signatures, byte[] to, BigDecimal amount, List<byte[]> pubkeyHashList)
        {
            amount = BigDecimal.Multiply(amount, new BigDecimal(rate));

            Tuple<bool, string> tupleAmount = JudgeBigDecimalIsValid(amount, "amount");
            if (!tupleAmount.Item1)
            {
                return tupleAmount.Item2;
            }
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0x08;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] toPubkeyHash = RipemdManager.getHash(txHash.HexToByteArray());
            byte[] payload = RLPUtils.EncodeMultTransfer(origin.ToBytesForRLPEncoding(), dest.ToBytesForRLPEncoding(), from, signatures, to, amount.ToLong().ToBytesForRLPEncoding(), pubkeyHashList);
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x03 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateMultipleToDeployForRuleSignSplice(string prikeyStr, string pubFirstSign, string frompubkey, long nonce, string signFirst, string pubkeyOther, string signOther)
        {
            byte[] sign = ParseSignatureFromSignRawBasicTransaction(signOther.HexToByteArray());
            Ed25519PublicKey ed25519PublicKey = new Ed25519PublicKey(pubkeyOther.HexToByteArray());
            bool isValid = ed25519PublicKey.verify(pubFirstSign.HexToByteArray(), sign);
            if (!isValid)
            {
                JObject json = new JObject { { "message", "others sign is wrong" } };
                return json.ToString();
            }
            byte[] payload = ParsePayloadFromSignRawBasicTransaction(signFirst.HexToByteArray());
            byte[] newPayload = Utils.CopyByteArray(payload, 1, payload.Length - 1);
            Multiple multiple = RLPUtils.DecodeMultiple(newPayload);
            byte[] assetHash = multiple.assetHash;
            int max = multiple.max;
            int min = multiple.min;
            List<byte[]> pubHashList = multiple.pubkeyHashList;

            List<byte[]> publicKeyList = new List<byte[]>();
            if (multiple.pubList.ToArray().Length == 0)
            {
                publicKeyList.Add(frompubkey.HexToByteArray());
            }
            else
            {
                for (int i = 0; i < multiple.pubList.ToArray().Length; i++)
                {
                    publicKeyList.Add(multiple.pubList[i]);
                }
            }
            if (!frompubkey.Equals(pubkeyOther))
            {
                publicKeyList.Add(pubkeyOther.HexToByteArray());
            }
            List<byte[]> list = multiple.signatures;
            list.Add(sign);
            byte[] rawTransactionHex = CreateMultipleForRuleSplice(frompubkey, nonce, assetHash, max, min, publicKeyList, list, pubHashList).HexToByteArray();
            string txHash = Utils.CopyByteArray(rawTransactionHex, 1, 32).ToHex();
            string traninfo = rawTransactionHex.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateMultipleForRuleSplice(string fromPubkeyStr, long nonce, byte[] assetHash, int max, int min, List<byte[]> pubList, List<byte[]> signatures, List<byte[]> pubkeyHashList)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0x07;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            String toPubkeyHashStr = "0000000000000000000000000000000000000000";
            byte[] toPubkeyHash = toPubkeyHashStr.HexToByteArray();
            byte[] payload = RLPUtils.EncodeMultiple(assetHash, max.ToBytesForRLPEncoding(), min.ToBytesForRLPEncoding(), pubList, signatures, pubkeyHashList);
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x01 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateMultipleToDeployForRuleOther(string fromPubkeyStr, string pubFirstSign, string prikeyStr, bool isPutSign)
        {
            byte[] signRawBasicTransaction = SignRawBasicTransactionByIsSign(pubFirstSign, prikeyStr, isPutSign).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            JObject json = new JObject { { "pubkeyOther", fromPubkeyStr }, { "signOther", traninfo }, { "data", txHash }, { "message", traninfo } };
            return json.ToString();
        }

        private static string SignRawBasicTransactionByIsSign(string rawTransactionHex, string prikeyStr, bool isPutSign)
        {
            byte[] rawTransaction = rawTransactionHex.HexToByteArray();
            //私钥字节数组
            byte[] privkey = prikeyStr.HexToByteArray();
            //version
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
            byte[] RawTransactionNoSign = Utils.Combine(version, type, nonce, from, gasprice, amount, signo, to, payloadlen, payload);
            byte[] RawTransactionNoSig = Utils.Combine(version, type, nonce, from, gasprice, amount);
            //签名数据
            byte[] sig = new byte[64];
            if (isPutSign)
            {
                sig = new Ed25519PrivateKey(privkey).sign(RawTransactionNoSign);
            }
            Sha3Keccack sha3Keccack = Sha3Keccack.Current;
            byte[] transha = sha3Keccack.CalculateHash(Utils.Combine(RawTransactionNoSig, sig, to, payloadlen, payload));
            byte[] signRawBasicTransaction = Utils.Combine(version, transha, type, nonce, from, gasprice, amount, sig, to, payloadlen, payload);
            return signRawBasicTransaction.ToHex();
        }

        public static string CreateMultipleToDeployForRuleFirst(string fromPubkeyStr, string prikeyStr, long nonce, string assetHash, int max, int min, List<string> publicKeyHashList)
        {
            byte[] hash;
            if (assetHash.Equals("0000000000000000000000000000000000000000"))
            {
                hash = assetHash.HexToByteArray();
            }
            else
            {
                hash = RipemdManager.getHash(assetHash.HexToByteArray());
            }
            List<byte[]> pubKeyHashList = publicKeyHashList.Select(x => x.HexToByteArray()).ToList();
            string rawTransactionHex = CreateMultipleForRuleFirst(fromPubkeyStr, nonce, hash, max, min, new List<byte[]>(), new List<byte[]>(), pubKeyHashList);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] sign = ParseSignatureFromSignRawBasicTransaction(signRawBasicTransaction);
            string rawTransactionHexFirstSign = CreateMultipleForRuleFirst(fromPubkeyStr, nonce, hash, max, min, new List<byte[]>(), new List<byte[]>() { sign }, pubKeyHashList);
            byte[] signRawBasicTransactionSign = SignRawBasicTransaction(rawTransactionHexFirstSign, prikeyStr).HexToByteArray();
            string txHash = Utils.CopyByteArray(signRawBasicTransactionSign, 1, 32).ToHex();
            string traninfo = signRawBasicTransaction.ToHex();
            JObject json = new JObject { { "pubkeyFirstSign", rawTransactionHex }, { "pubkeyFirst", fromPubkeyStr }, { "signFirst", traninfo }, { "data", txHash }, { "message", traninfo } };
            return json.ToString();
        }

        private static byte[] ParseSignatureFromSignRawBasicTransaction(byte[] msg)
        {
            msg = Utils.CopyByteArray(msg, 1, msg.Length - 1);
            msg = Utils.CopyByteArray(msg, 32, msg.Length - 32);
            msg = Utils.CopyByteArray(msg, 1, msg.Length - 1);
            msg = Utils.CopyByteArray(msg, 8, msg.Length - 8);
            msg = Utils.CopyByteArray(msg, 32, msg.Length - 32);
            msg = Utils.CopyByteArray(msg, 8, msg.Length - 8);
            msg = Utils.CopyByteArray(msg, 8, msg.Length - 8);
            //sig
            return Utils.CopyByteArray(msg, 0, 64);
        }

        private static byte[] ParsePayloadFromSignRawBasicTransaction(byte[] msg)
        {
            msg = Utils.CopyByteArray(msg, 1, msg.Length - 1);
            msg = Utils.CopyByteArray(msg, 32, msg.Length - 32);
            msg = Utils.CopyByteArray(msg, 1, msg.Length - 1);
            msg = Utils.CopyByteArray(msg, 8, msg.Length - 8);
            msg = Utils.CopyByteArray(msg, 32, msg.Length - 32);
            msg = Utils.CopyByteArray(msg, 8, msg.Length - 8);
            msg = Utils.CopyByteArray(msg, 8, msg.Length - 8);
            //sig
            msg = Utils.CopyByteArray(msg, 0, msg.Length - 64);
            msg = Utils.CopyByteArray(msg, 0, msg.Length - 20);
            byte[] payloadLen = Utils.CopyByteArray(msg, 0, 4);
            msg = Utils.CopyByteArray(msg, 0, msg.Length - 4);
            return Utils.CopyByteArray(msg, 0, (int)NumericsUtils.decodeUint32(payloadLen));
        }

        public static string CreateMultipleForRuleFirst(string fromPubkeyStr, long nonce, byte[] assetHash, int max, int min, List<byte[]> pubList, List<byte[]> signatures, List<byte[]> publicKeyHashList)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0x07;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] toPubkeyHash = "0000000000000000000000000000000000000000".HexToByteArray();
            byte[] payload = RLPUtils.EncodeMultiple(assetHash, max.ToBytesForRLPEncoding(), min.ToBytesForRLPEncoding(), pubList, signatures, publicKeyHashList);
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x01 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateSignToDeployForRuleTransfer(string fromPubkeyStr, string tranTxHash, string prikeyStr, long nonce, string from, string to, BigDecimal amount)
        {
            string rawTransactionHex = CreateDeployForRuleAssetTransfer(fromPubkeyStr, tranTxHash, nonce, from.HexToByteArray(), to.HexToByteArray(), amount);
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateDeployForRuleAssetTransfer(string fromPubkeyStr, string txHash, long nonce, byte[] from, byte[] to, BigDecimal amount)
        {
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0x08;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] hash = txHash.HexToByteArray();
            byte[] toPubkeyHash = RipemdManager.getHash(hash);
            byte[] payload = RLPUtils.EncodeList(from, to, BigDecimal.Multiply(amount, new BigDecimal(rate)).ToLong().ToBytesForRLPEncoding());
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x01 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }

        public static string CreateSignToDeployForRuleAssetIncreased(string fromPubkeyStr, string tranTxHash, string prikeyStr, long nonce, BigDecimal amount)
        {
            string rawTransactionHex = CreateCallForRuleAssetIncreased(fromPubkeyStr, tranTxHash, nonce, amount);
            if (rawTransactionHex.Contains("must be a positive number"))
            {
                return rawTransactionHex;
            }
            byte[] signRawBasicTransaction = SignRawBasicTransaction(rawTransactionHex, prikeyStr).HexToByteArray();
            byte[] hash = Utils.CopyByteArray(signRawBasicTransaction, 1, 32);
            String txHash = hash.ToHex();
            String traninfo = signRawBasicTransaction.ToHex();
            APIResult result = new APIResult(txHash, traninfo);
            return JsonConvert.SerializeObject(result);
        }

        public static string CreateCallForRuleAssetIncreased(string fromPubkeyStr, string txHash, long nonce, BigDecimal amount)
        {
            amount = BigDecimal.Multiply(amount, new BigDecimal(rate));

            Tuple<bool, string> tupleAmount = JudgeBigDecimalIsValid(amount, "amount");
            if (!tupleAmount.Item1)
            {
                return tupleAmount.Item2;
            }
            //版本号
            byte[] version = new byte[1];
            version[0] = 0x01;
            //类型
            byte[] type = new byte[1];
            type[0] = 0x08;
            //Nonce 无符号64位
            byte[] newNonce = NumericsUtils.encodeUint64(nonce + 1);
            //签发者公钥哈希 20字节
            byte[] fromPubkeyHash = fromPubkeyStr.HexToByteArray();
            //gas单价
            byte[] gasPrice = NumericsUtils.encodeUint64(obtainServiceCharge(100000L, serviceCharge));
            //分享收益 无符号64位
            byte[] shareAmount = NumericsUtils.encodeUint64(0);
            //为签名留白
            byte[] signull = new byte[64];
            //接收者公钥哈希
            byte[] hash = txHash.HexToByteArray();
            byte[] toPubkeyHash = RipemdManager.getHash(hash);
            byte[] payload = RLPUtils.EncodeList(amount.ToLong().ToBytesForRLPEncoding());
            byte[] payLoadLength = NumericsUtils.encodeUint32(payload.Length + 1);
            byte[] allPayload = Utils.Combine(payLoadLength, new byte[] { 0x02 }, payload);
            byte[] rawTransaction = Utils.Combine(version, type, newNonce, fromPubkeyHash, gasPrice, shareAmount, signull, toPubkeyHash, allPayload);
            return rawTransaction.ToHex();
        }


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
            if (rawTransactionHex.Contains("must be a positive number"))
            {
                return rawTransactionHex;
            }
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

            Tuple<bool, string> tupleOffering = JudgeBigDecimalIsValid(offering, "offering");
            if (!tupleOffering.Item1)
            {
                return tupleOffering.Item2;
            }
            Tuple<bool, string> tupleTotalAmount = JudgeBigDecimalIsValid(totalAmount, "total amount");
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

        private static Tuple<bool, string> JudgeBigDecimalIsValid(BigDecimal value, string key)
        {
            if (value.CompareTo(BigDecimal.Zero) <= 0 || value.CompareTo(new BigDecimal(long.MaxValue)) > 0)
            {
                return new Tuple<bool, string>(false, key + " must be a positive number");
            }
            return new Tuple<bool, string>(true, "value is ok");
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
