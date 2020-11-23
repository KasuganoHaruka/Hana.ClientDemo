using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientDemo
{
    public static class ConsulHelper
    {
        public static void ConsulRegist(this IConfiguration configuration ) 
        {
            ConsulClient client = new ConsulClient(c=> 
            {
                c.Address = new Uri("http://localhost:8500");
                c.Datacenter = "dcl";
            });

            //控制台启动时的参数
            string ip = string.IsNullOrWhiteSpace(configuration["ip"]) ? "127.0.0.1" : configuration["ip"];   
            int port = string.IsNullOrWhiteSpace(configuration["port"]) ? 1 : int.Parse(configuration["port"]);   
            int weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);

            client.Agent.ServiceRegister(new AgentServiceRegistration() 
            {   
                ID = "service" + Guid.NewGuid(),
                Name = "ClientDemo",    //服务名称
                Address = ip,
                Port = port,
                Tags = new string[] { weight.ToString() },  //标签
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(10),    //间隔10秒一次
                    HTTP = $"http://{ip}:{port}/Api/Health/Index",
                    Timeout = TimeSpan.FromSeconds(5),  //检测等待时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)   //失败20s后移除
                }

            });
        }


    }
}
