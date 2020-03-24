## C#SDK

### 创建项目

```json
dotnet new console -o C#-SDK
```

### 创建单元测试

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

### 运行单元测试
```json
dotnet test
```

### 删除和添加库
```json
dotnet add package BouncyCastle --version 1.8.5

dotnet remove package BouncyCastle
```

### 加密库选择

* BC  `BouncyCastle`
* `Argon2`  `Isopoh.Cryptography.Argon2`
* `AES` `System.Security.Cryptography`
