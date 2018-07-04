using System;
using System.Collections;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using Exness.dto;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Exness.PgReq
{
    public class ExecPg
    {
		public static IConfiguration Configuration { get; set; }

		public object ExecQuery(string id)
        {
			object result = null;
			var exnessCategory = new exnessObj.Category();
			var exnessVendros = new exnessObj.Vendors();
			exnessVendros.category = new List<exnessObj.Category>();

			result = null;
			Guid[] idd = null;
			ArrayList objs = new ArrayList();

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string _connectionString = Configuration.GetConnectionString("npg.postgres");

			using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                try
                {
					
					using (NpgsqlCommand com = new NpgsqlCommand("select id,name,raiting,categories from vendors where id ='" + id + "'", conn))
                    {
						conn.Open();
						NpgsqlDataReader queryResult = com.ExecuteReader();
						if (queryResult.HasRows)
                        {
							while (queryResult.Read())
                            {
								exnessVendros.id = queryResult.GetGuid(0);
								exnessVendros.name = queryResult.GetString(1);
								exnessVendros.rating = queryResult.GetInt32(2);
								idd = queryResult[3] as Guid[];               
                            }
							conn.Close();
							if (idd != null){
								for (var i = 0; i < idd.Length;i++){
									conn.Open();
									using (NpgsqlCommand com1 = new NpgsqlCommand("select name from categories where id = '" + idd[i] + "'", conn)){
                                        var name = com1.ExecuteScalar();
										if (name != null){
											exnessVendros.category.Add(new exnessObj.Category()
                                            {
                                                id = idd[i],
                                                name = name.ToString()
                                            });
										}
                                    }
									conn.Close();
                                }   
							}
							result = exnessVendros;
                        }
						else{
							result = JObject.Parse(@"{""message"": ""Vendor " + id + " is not found\"}");
						}
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: '{0}'", e);
					result = e;
                }
				return result;
            }    
        }
    }
}
