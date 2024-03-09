﻿using Newtonsoft.Json;
using System.ComponentModel;

namespace CSharpOpenBMCLAPI.Modules
{
    public class Config
    {
        // 指示 token 应当在距离其失效前的多少毫秒进行刷新
        public int refreshTokenTime;
        // 指示应该将要服务的文件放在哪里（服务路径）
        public string clusterFileDirectory;
        // 指示节点端的版本，不应由用户更改
        [Browsable(false)]
        public string clusterVersion;
        // 用户访问时使用的 IP 或域名
        [JsonProperty("host")]
        public string HOST
        {
            get
            {
                if (string.IsNullOrEmpty(this._host))
                {
                    //HttpClient client = new HttpClient();
                    //this._host = client.GetAsync("https://4.ipw.cn/").Result.Content.ReadAsStringAsync().Result;
                    this._host = $"";
                }
                return this._host;
            }
            set => this._host = value;
        }
        // 对外服务端口
        [JsonProperty("port")]
        public int PORT { get; set; }
        // 是否使用自定义域名
        public bool byoc;
        // 指示是否执行快速上线，若为 true 则每次都不执行
        public bool noFastEnable;

        private string _host = "";

        public Config()
        {
            this.refreshTokenTime = 1800000;
            this.clusterFileDirectory = "./";
            this.clusterVersion = "1.9.7";

            this.HOST = "";
            this.PORT = 4000;
            this.byoc = false;
            this.noFastEnable = false;
        }
    }
}
