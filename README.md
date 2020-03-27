## C#SDK

### Create project


```json
dotnet new console -o C#-SDK
```

### Create Unit Tests

```json
dotnet new sln -o unit-testing-using-dotnet-test
cd unit-testing-using-dotnet-test
dotnet new classlib -o PrimeService
ren .\PrimeService\Class1.cs PrimeService.cs
dotnet sln add ./PrimeService/PrimeService.csproj
dotnet new xunit -o PrimeService.Tests
dotnet add ./PrimeService.Tests/PrimeService.Tests.csproj reference ./PrimeService/PrimeService.csproj
dotnet sln add ./PrimeService.Tests/PrimeService.Tests.csproj
```

### Running Unit Tests
```json
dotnet test
```

### Delete and add Libraries
```json
dotnet add package BouncyCastle --version 1.8.5

dotnet remove package BouncyCastle
```

### Encryption library selection

* BC  `BouncyCastle`
* `Argon2`  `Isopoh.Cryptography.Argon2`
* `AES` `LibAES-CTR`
* `sha3Keccack` `Org.BouncyCastle.Crypto.Digests`
* `RIPEMD160` `DevHawk.RIPEMD160`
* `Base58` `SimpleBase`

### Third party library selection
* `json` `Newtonsoft.Json`

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
fromPubkeyStr   sender publicKey
toPubkeyHashStr receiver publicKeyHash
amount  vote amount(should be new BigDecimal(100))
prikeyStr   sender privateKey
nonce   get by node
```

##### build withdrawal of voting transaction

```c#
TxUtility.ClientToTransferVoteWithdraw(string fromPubkeyStr, string toPubkeyHashStr, BigDecimal amount, long nonce, string prikeyStr, string txid)
fromPubkeyStr   initiate a vote publicKey
toPubkeyHashStr the person being voted publicKeyHash
amount  vote amount(should be new BigDecimal(100))
prikeyStr   sender privateKey
nonce   get by node
txid  vote transaction id
```
