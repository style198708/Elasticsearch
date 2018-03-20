using System;
using System.Collections.Generic;
using System.Text;
using Elasticsearch.Net;
using Nest;
using System.Linq;


namespace ConsoleApp8
{
    public class ElasticsearchHelper
    {
        public ElasticClient Client { get; set; }

        static ElasticsearchHelper()
        {
            Config config = TYSystem.BaseFramework.Configuration.Config.Bind<Config>("");
            ConnectionSettings connectionSettings = new ConnectionSettings();
            if (config.ElasticsearchType == EnumElasticsearch.Single)
            {
                connectionSettings = new ConnectionSettings(new Uri(config.ConfigUrl));
            }
            else if (config.ElasticsearchType == EnumElasticsearch.Multiple)
            {
                StaticConnectionPool pool = new StaticConnectionPool(config.ConfigUrl.Split(",").Select(p => new Uri(p)).ToList());
                connectionSettings = new ConnectionSettings(pool);
            }
            else
            {
                StaticConnectionPool pool = new StaticConnectionPool(config.ConfigUrl.Split(",").Select(p => new Node(new Uri(p))).ToList());
                connectionSettings = new ConnectionSettings(pool);
            }
            ElasticClient Client = new ElasticClient(connectionSettings);
        }

        /// <summary>
        /// 创建索引(数据库)
        /// </summary>
        public void CreateIndex<T>()
        {
            IIndexState indexState = new IndexState()
            {
                Settings = new IndexSettings()
                {
                    NumberOfReplicas = 1,//副本数
                    NumberOfShards = 5//分片数
                }
            };
            //创建并Mapping
            Client.CreateIndex("test-index3", p => p.InitializeUsing(indexState).Mappings(m => m.Map<VendorPriceInfo>(mp => mp.AutoMap())));
        }
    }
}
