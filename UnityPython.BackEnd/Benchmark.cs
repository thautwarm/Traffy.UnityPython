// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Security.Cryptography;
// using BenchmarkDotNet.Attributes;
// using BenchmarkDotNet.Running;
// using static Traffy.Objects.ExtMonoAsyn;
// using Traffy.Objects;

// namespace MyBenchmarks
// {
//     public class Md5VsSha256
//     {
//         private const int N = 10000;
//         private readonly List<int> data = new List<int>();

//         public static IEnumerable<int> f1(List<int> lst)
//         {
//             foreach (var i in lst)
//             {
//                 yield return i;
//             }
//             foreach (var i in lst)
//             {
//                 yield return i;
//             }
//         }

//         public static async MonoAsync<int> f2(List<int> lst)
//         {
//             foreach (var i in lst)
//             {
//                 await Yield(i);
//             }
//             foreach (var i in lst)
//             {
//                 await Yield(i);
//             }
//             return 1;
//         }


//         public Md5VsSha256()
//         {
//             for (int i = 0; i < N; i++)
//             {
//                 data.Add(i);
//             }
//         }

//         [Benchmark]
//         public int testf2()
//         {
//             return f2(data).Enum().Sum();
//         }
//         [Benchmark]
//         public int testf1()
//         {
//             return f1(data).Sum();
//         }



//     }

//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
//         }
//     }
// }