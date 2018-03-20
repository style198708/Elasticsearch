using Chloe.Entity;
using System;
using System.Collections.Generic;
using TYSystem.BaseFramework.Chloe;
using Microsoft.Extensions.Configuration;
using Nest;
using System.Linq;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {

            //var builder = new ConfigurationBuilder();
            //builder.AddJsonFile("json1.json");

            //var configuration = builder.Build();

            //Console.WriteLine($"name：{configuration["name"]}  age：{configuration["age"]}");

            //TYSystem.BaseFramework.Elasticsearch.ElasticsearchHelper.CreateIndex();

            var connSettings = new ConnectionSettings(new Uri("http://192.168.126.136:9200/")).DefaultIndex("test-index");



            var data = new VendorPriceInfo() { priceID = 12, modifyTime = DateTime.Now, pvc_Name = "1212", pvc_Desc = "1212", Location = new GeoLocation(12, 34) };
            //var res = Client.CreateIndex("test2");
            //var res = Client.CreateIndex("index", i => i.Mappings(m => m.Map<ElasticsearchIndex>(ms => ms.AutoMap())));
            IIndexState indexState = new IndexState()
            {
                Settings = new IndexSettings()
                {
                    NumberOfReplicas = 1,//副本数
                    NumberOfShards = 5//分片数
                }
            };
            ElasticClient Client = new ElasticClient(connSettings);

            //Client.Search<ElasticsearchIndex>(s => s.Index("test-index"));

            //var S = Client.Index(index, o => o.Index("test-index"));

            //Client.CreateIndex("test-index3", p => p.InitializeUsing(indexState).Mappings(m => m.Map<ElasticsearchIndex>(mp => mp.AutoMap())));



            var res = Client.Index(data, o => o.Type<VendorPriceInfo>());

            //var response = Client.Get(new DocumentPath<VendorPriceInfo>(12), pd => pd.Index("test-index"));

            //var list = Client.Search<ElasticsearchIndex>(s => s.From(0).Size(50));

            //int pageSize = 10;
            //int pageIndex = 1;

            // var result1 = Client.Search<ElasticsearchIndex, ElasticsearchIndex>(s => s.Index("test-index").Size(15));

            //var result = Client.Search<VendorPriceInfo>(s => s.Index("test-index").Query(q => q.MatchAll())
            //    .Size(pageSize)
            //    .From((pageIndex - 1) * pageSize)
            //    .Sort(st => st.Descending(d => d.priceID)));

            var response = Client.Search<VendorPriceInfo>(s => s
                        .From(0)
                        .Size(10)
                        .Query(q => q.Term(t => t.priceID, 12)));

           

            //var result = Client.Search<VendorPriceInfo>(new SearchRequest()
            //{
            //    Sort = new List<ISort>
            //        {
            //            new SortField { Field = "vendorPrice", Order = SortOrder.Ascending }
            //        },
            //    Size = 10,
            //    From = 0,
            //    Query = new TermQuery()
            //    {
            //        Field = "priceID",
            //        Value = 12
            //    }
            //||
            //new TermQuery
            //{
            //    Field = "priceID",
            //    Value = 8
            //}
            //});
            Console.ReadKey();




            //Student student = SqlServerHelp.ConfigInit().ToEntity<Student>();
            //List<Student> list = SqlServerHelp.ConfigInit<Student>().Query<Student>().Where(p => p.Name == "jack").Skip(0).Take(10).ToList();
            //List<Mem_MemberAccount> list1 = SqlServerHelp.ConfigInit<Mem_MemberAccount>().Query<Mem_MemberAccount>().Skip(0).Take(10).ToList();

            //GetConfigName<Student>();
            Console.WriteLine("Hello World!");
        }

        public static string GetConfigName<T>()
        {
            Type type = typeof(T);
            TableAttribute table = (TableAttribute)type.GetCustomAttributes(false)[0];
            return table.ConfigName;
        }
    }
}
