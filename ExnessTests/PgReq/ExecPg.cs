using System;
using Npgsql;

namespace ExnessTests.PgReq
{
    public struct ExecPg
    {
		public void ExecQuery(string query)
		{
			string connectionString = "Server=localhost;Port=5433;User Id=postgres;Password=123;Database=postgres;";

			NpgsqlConnection conn = new NpgsqlConnection(connectionString);
			try
            {
    			using(NpgsqlCommand comm = new NpgsqlCommand(query, conn))
    			{
    				
                    conn.Open();
                    string result = comm.ExecuteScalar().ToString();
                    Console.WriteLine(result);
                    conn.Close();
                }
    		}
			catch (Exception e)
			{
				Console.WriteLine("An error occurred: '{0}'", e);
			}                      
		}
    }
}
