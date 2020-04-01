using System;
using System.Reflection;
using System.IO;

namespace CSharp_SDK
{
    class Program
    {

        static void Main(string[] args)
        {
            string transaction = TxUtility.ClientToTransferAccount("27724e1b96cebc66acb8682d12eb92dac4df6a4ea0b509f852dcd77ebeeabb93", "0e015bc15fa1b0156d3f62b16b397d6120faae5b", new BigDecimal(10000), "d8dde33d8a3bdbeeb52843ff0e2a31140bdbf32997213e8ca871a2aa1724b68f", 5L);
            Console.WriteLine(transaction);

            // CreateDllInstance(AppDomain.CurrentDomain.BaseDirectory,"./Wallet/WalletUtility.cs");
            // string assemblyName = new AssemblyName("./Wallet/WalletUtility.cs").Name;
            // if (assemblyName.EndsWith(".resources"))
            // {
            //     return;
            // }

            // string dllName = assemblyName + ".dll";
            // string dllFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName);
            // using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream(typeof(Program).Namespace + ".Resources." + dllName))
            // {
            //     byte[] data = new byte[stream.Length];
            //     stream.Read(data, 0, data.Length);
            //     File.WriteAllBytes(dllFullPath, data);
            // }

            // Assembly.LoadFrom(dllFullPath);
            // AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            // {
            //     string assemblyName = new AssemblyName(args.Name).Name;
            //     if (assemblyName.EndsWith(".resources"))
            //     {
            //         return null;
            //     }

            //     string dllName = assemblyName + ".dll";
            //     string dllFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName);
            //     using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream(typeof(Program).Namespace + ".Resources." + dllName))
            //     {
            //         byte[] data = new byte[stream.Length];
            //         stream.Read(data, 0, data.Length);
            //         File.WriteAllBytes(dllFullPath, data);
            //     }

            //     return Assembly.LoadFrom(dllFullPath);
            // };
        }

        public static object CreateDllInstance(string dllFullPath, string classNameWithNameSpace, params object[] objs)
        {
            Assembly ass = Assembly.LoadFrom(dllFullPath);
            Type t = ass.GetType(classNameWithNameSpace);   //参数必须是类的全名
            return Activator.CreateInstance(t, objs);
        }
    }
}
