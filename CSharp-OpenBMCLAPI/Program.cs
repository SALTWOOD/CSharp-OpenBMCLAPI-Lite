﻿using CSharpOpenBMCLAPI.Modules;
using CSharpOpenBMCLAPI.Modules.Statistician;
using Newtonsoft.Json;
using System.Reflection;
using TeraIO.Runnable;
using YamlDotNet.Serialization;

namespace CSharpOpenBMCLAPI
{
    internal class Program : RunnableBase
    {
        public Program() : base() { }

        static void Main(string[] args)
        {
            Logger.Instance.LogSystem($"Starting CSharp-OpenBMCLAPI v{ClusterRequiredData.Config.clusterVersion}");
            Logger.Instance.LogSystem("高性能、低メモリ占有！");
            Logger.Instance.LogSystem($"运行时环境：{Utils.GetRuntime()}");
            Program program = new Program();
            program.Start();
            program.WaitForStop();
        }



        // 获取配置文件
        protected Config GetConfig()
        {
            // 配置文件名
            const string configFileName = "config.yml";
            // 配置文件路径
            string configPath = Path.Combine(ClusterRequiredData.Config.clusterWorkingDirectory, configFileName);
            // 如果配置文件不存在
            if (!File.Exists(configPath))
            {
                // 创建配置文件
                Config config = new Config();
                Serializer serializer = new Serializer();
                File.WriteAllText(configPath, serializer.Serialize(config));
                return config;
            }
            else
            {
                // 读取配置文件
                string file = File.ReadAllText(configPath);
                Deserializer deserializer = new Deserializer();
                // 反序列化配置文件
                Config? config = deserializer.Deserialize<Config>(file);
                Config result;
                // 如果反序列化失败
                if (config != null)
                {
                    result = config;
                }
                else
                {
                    result = new Config();
                }
                Serializer serializer = new Serializer();
                // 重新序列化配置文件
                File.WriteAllText(configPath, serializer.Serialize(config));
                return result;
            }
        }

        protected override int Run(string[] args)
        {
            try
            {
                Directory.CreateDirectory(ClusterRequiredData.Config.clusterWorkingDirectory);
                Directory.CreateDirectory("working");
                const string bsonFile = "totals.bson";
                string bsonFilePath = Path.Combine(ClusterRequiredData.Config.clusterWorkingDirectory, bsonFile);
                ClusterRequiredData.Config = GetConfig();

                int returns = 0;

                if (File.Exists(bsonFilePath))
                {
                    DataStatistician t = Utils.BsonDeserializeObject<DataStatistician>(File.ReadAllBytes(bsonFilePath)).ThrowIfNull();
                    ClusterRequiredData.DataStatistician = t;
                }
                else
                {
                    using (var file = File.Create(bsonFilePath))
                    {
                        file.Write(Utils.BsonSerializeObject(ClusterRequiredData.DataStatistician));
                    }
                }

                const string environment = "working/.env.json";

                if (!File.Exists(environment)) throw new FileNotFoundException($"请在程序目录下新建 {environment} 文件，然后填入 \"ClusterId\" 和 \"ClusterSecret\"以启动集群！");

                // 从 .env.json 读取密钥然后 FetchToken
                ClusterInfo info = JsonConvert.DeserializeObject<ClusterInfo>(File.ReadAllTextAsync(environment).Result);
                ClusterRequiredData requiredData = new(info);
                Logger.Instance.LogSystem($"Cluster id: {info.ClusterID}");
                TokenManager token = new TokenManager(info);
                token.FetchToken().Wait();

                requiredData.Token = token;

                Cluster cluster = new(requiredData);
                Logger.Instance.LogSystem($"成功创建 Cluster 实例");
                AppDomain.CurrentDomain.ProcessExit += (sender, e) => Utils.ExitCluster(cluster).Wait();
                Console.CancelKeyPress += (sender, e) => Utils.ExitCluster(cluster).Wait();

                cluster.Start();

                return returns;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ExceptionToDetail());
                Console.ReadKey();
                return -1;
            }
        }
    }
}
