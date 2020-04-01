## C#SDK

### Create project

```json
dotnet new console -o CSharp_SDK
```

### Running Unit Tests
```json
cd test/CSharpSDKTest && dotnet test
```

### Delete and add Libraries
```json
dotnet add package BouncyCastle --version 1.8.5

dotnet remove package BouncyCastle
```

### Encryption library selection

* `BC`  `BouncyCastle`
* `Argon2`  `Isopoh.Cryptography.Argon2`
* `AES` `LibAES-CTR`
* `sha3Keccack` `Org.BouncyCastle.Crypto.Digests`
* `RIPEMD160` `DevHawk.RIPEMD160`
* `Base58` `SimpleBase`

### Third party library selection
* `json` `Newtonsoft.Json`
* `Hex` `Nethereum.Hex`

### Local method

##### generate Keystore

```c#
WalletUtility.FromPassword(string password)
```

##### convert PublicKey to Address

```c#
KeystoreUtils.PubkeyToAddress(byte[] pubkey)
```

##### convert PublicKeyHash to Address

```c#
KeystoreUtils.PubkeyHashToAddress(byte[] publicHash)
```

##### convert PrivateKey to PublicKey

```C#
KeystoreUtils.PrivatekeyToPublicKey(string privateKey)
```

##### convert Address to PublicKeyHash

```C#
KeystoreUtils.PubkeyHashToAddress(byte[] publicHash)
```

##### build transfer accounts transaction

```c#
TxUtility.ClientToTransferAccount(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, string prikeyStr, long nonce)
fromPubkeyStr   sender publicKey
toPubkeyHashStr receiver publicKeyHash
amount  transfer amount(should be new BigDecimal(100))
prikeyStr   sender privateKey
nonce   get by node  
```

##### build prove transaction

```c#
TxUtility.ClientToTransferProve(string fromPubkeyStr, long nonce, byte[] payload, string prikeyStr)
fromPubkeyStr   sender publicKey
payload recording somethings
prikeyStr   sender privateKey
nonce   get by node  
```

##### build vote transaction

```c#
TxUtility.ClientToTransferVote(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string prikeyStr)
fromPubkeyStr   initiate a vote publicKey
toPubkeyHashStr the person being voted publicKeyHash
amount  vote amount(should be new BigDecimal(100))
prikeyStr   initiate a vote privateKey
nonce   get by node
```

##### build withdrawal of voting transaction

```c#
TxUtility.ClientToTransferVoteWithdraw(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string prikeyStr, string txid)
fromPubkeyStr   initiate a vote publicKey
toPubkeyHashStr the person being voted publicKeyHash
amount  vote amount(should be new BigDecimal(100))
prikeyStr   initiate a vote privateKey
nonce   get by node
txid  vote transaction id
```

##### build mortgage transaction

```c#
TxUtility.ClientToTransferMortgage(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string prikeyStr)
fromPubkeyStr   initiate a mortgage publicKey
toPubkeyHashStr the person being mortgaged publicKeyHash
amount  vote amount(should be new BigDecimal(100))
prikeyStr   initiate a mortgage privateKey
nonce   get by node
```

##### build withdrawal of mortgaging transaction

```c#
TxUtility.ClientToTransferMortgageWithdraw(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string txid, string prikeyStr)
fromPubkeyStr   initiate a mortgage publicKey
toPubkeyHashStr the person being mortgaged publicKeyHash
amount  mortgage amount(should be new BigDecimal(100))
prikeyStr   initiate a mortgage privateKey
nonce   get by node
txid  mortgage transaction id
```

##### build deploy asset transaction

```c#
TxUtility.CreateSignToDeployForRuleAsset(string fromPubkeyStr, string prikeyStr, long nonce, string code, BigDecimal offering, string createUser, string owner, int allowIncrease, string info)
fromPubkeyStr   sender publicKey
prikeyStr sender prikeyStr
code  asset code
offering   opening issuance limit
createUser create ruler publicKey
owner owner address
allowIncrease 1 allowed, 0 not allowed
nonce   get by node
info  asset information
```

##### build change asset owner transaction

```c#
TxUtility.CreateSignToDeployforAssetChangeowner(string fromPubkeyStr, string tranTxHash, string prikeyStr, long nonce, string newOwner)
fromPubkeyStr   sender publicKey
tranTxHash transaction hash
prikeyStr  sender prikeyStr
nonce   get by node
newOwner  new owner address
```

##### build increase asset transaction

```c#
TxUtility.CreateSignToDeployForRuleAssetIncreased(string fromPubkeyStr, string tranTxHash, string prikeyStr, long nonce, BigDecimal amount)
fromPubkeyStr   sender publicKey
tranTxHash transaction hash
prikeyStr  sender prikeyStr
nonce   get by node
amount  increase amount(should be new BigDecimal(100))
```

##### build transfer asset accounts transaction
```c#
TxUtility.CreateSignToDeployForRuleTransfer(string fromPubkeyStr, string tranTxHash, string prikeyStr, long nonce, string from, string to, BigDecimal amount)
fromPubkeyStr   sender publicKey
tranTxHash transaction hash
prikeyStr  sender prikeyStr
nonce   get by node
from   publicKey
to  publicKeyHash
amount  transfer amount(should be new BigDecimal(100))
```

##### build deploy multiple rule transaction
```c#
TxUtility.CreateMultipleToDeployForRuleFirst(string fromPubkeyStr, string prikeyStr, long nonce, string assetHash, int max, int min, List<string> publicKeyHashList)
fromPubkeyStr   sender publicKey
prikeyStr  sender prikeyStr
nonce   get by node
assetHash asset hash
max   maximum number of signatures
min   minimum number of signatures
publicKeyHashList publicKeyHash list
```

##### build deploy multiple rule (other signature)transaction
```c#
TxUtility.CreateMultipleToDeployForRuleOther(string fromPubkeyStr, string pubFirstSign, string prikeyStr, bool isPutSign)
fromPubkeyStr   sender publicKey
pubFirstSign  CreateMultipleToDeployForRuleFirst return pubFirstSign
prikeyStr   prikeyStr
assetHash asset hash
isPutSign put in signature or not
```

##### build deploy multiple rule (splice signature)transaction
```c#
TxUtility.CreateMultipleToDeployForRuleSignSplice(string prikeyStr, string pubFirstSign, string frompubkey, long nonce, string signFirst, string pubkeyOther, string signOther)
frompubkey   sender publicKey
pubFirstSign  CreateMultipleToDeployForRuleFirst return pubFirstSign
prikeyStr   prikeyStr
nonce   get by node
signFirst  CreateMultipleToDeployForRuleFirst return signFirst
pubkeyOther CreateMultipleToDeployForRuleOther return pubkeyOther
signOther CreateMultipleToDeployForRuleOther return signOther
```

##### build deploy transfer multiple rule transaction
```c#
TxUtility.CreateMultiSignatureToDeployForRuleFirst(string fromPubkeyStr, string prikeyStr, string txHashRule, long nonce, int origin, int dest, List<string> signatures, string to, BigDecimal value, List<string> pubkeyHashList)
fromPubkeyStr   sender publicKey
prikeyStr  sender prikeyStr
txHashRule transaction hash
nonce   get by node
origin origin account type 1 multiple address 0 common address
dest dest account type  1 multiple address 0 common address
signatures   signature list
to   common address corresponding to pubkeyHash or multiple address corresponding to transaction hash
publicKeyHashList publicKeyHash list
```

##### build deploy transfer multiple rule (other signature)transaction
```c#
TxUtility.CreateMultiSignatureToDeployForRuleOther(string fromPubkeyStr, string pubkeyFirstSign, string prikeyStr, bool isPutSign)
fromPubkeyStr   sender publicKey
pubkeyFirstSign  CreateMultiSignatureToDeployForRuleFirst return pubkeyFirstSign
prikeyStr   prikeyStr
isPutSign put in signature or not
```

##### build deploy transfer multiple rule (splice signature)transaction
```c#
TxUtility.CreateMultiSignatureToDeployForRuleSignSplice(string prikeyStr, string pubkeyFirstSign, string fromPubkey, string txHashRule, long nonce, string signFirst, string pubkeyOther, string signOther, int type)
pubFirstSign  CreateMultiSignatureToDeployForRuleFirst return pubFirstSign
prikeyStr   prikeyStr
fromPubkey sender publicKey
txHashRule transaction hash
nonce   get by node
signFirst  CreateMultiSignatureToDeployForRuleFirst return signFirst
pubkeyOther CreateMultiSignatureToDeployForRuleOther return pubkeyOther
signOther CreateMultiSignatureToDeployForRuleOther return signOther
type 1 one to more 2 more to one 3 more to more
```

##### build hash time block transaction
```c#
TxUtility.CreateHashTimeBlockForDeploy(string fromPubkeyStr, string prikeyStr, long nonce, string assetHash, string pubkeyHash)
fromPubkeyStr sender publicKey
prikeyStr sender privatekey
nonce   get by node
assetHash   asset hash
pubkeyHash publicHashKey
```

##### build get asset time block transaction
```c#
TxUtility.CreateHashTimeBlockGetForDeploy(string fromPubkeyStr, string prikeyStr, string txGetHash, long nonce, string transferHash, string originText)
fromPubkeyStr sender publicKey
prikeyStr sender privatekey
txGetHash transaction hash
nonce   get by node
transferHash   transfer hash
originText origin text
```

##### build transfer asset time block transaction
```c#
TxUtility.CreateHashTimeBlockTransferForDeploy(string fromPubkeyStr, string prikeyStr, String txGetHash, long nonce, BigDecimal amount, string hashResult, BigDecimal timestamp)
fromPubkeyStr sender publicKey
prikeyStr sender privatekey
txGetHash transaction hash
nonce   get by node
amount  transfer amount(should be new BigDecimal(100))
hashResult   origin text
timestamp timestamp
```

##### build hash height block transaction
```c#
TxUtility.CreateHashHeightBlockForDeploy(string fromPubkeyStr, string prikeyStr, long nonce, string assetHash, string pubkeyHash)
fromPubkeyStr sender publicKey
prikeyStr sender privatekey
nonce   get by node
assetHash   asset hash
pubkeyHash publicHashKey
```

##### build hash height get asset block transaction
```c#
TxUtility.CreateHashHeightBlockGetForDeploy(string fromPubkeyStr, string prikeyStr, string txGetHash, long nonce, string transferHash, string originText)
fromPubkeyStr sender publicKey
prikeyStr sender privatekey
txGetHash transaction hash
nonce   get by node
transferHash   transfer hash
originText origin text
```

##### build hash height transfer asset block transaction
```c#
TxUtility.CreateHashHeightBlockTransferForDeploy(string fromPubkeyStr, string prikeyStr, string txGetHash, long nonce, BigDecimal amount, string hashResult, BigDecimal blockHeight)
fromPubkeyStr sender publicKey
prikeyStr sender privatekey
txGetHash transaction hash
nonce   get by node
amount  transfer amount(should be new BigDecimal(100))
hashResult   origin text
blockHeight block height
```
