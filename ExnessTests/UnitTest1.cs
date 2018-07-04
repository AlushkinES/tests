using System;
using ExnessTests.PgReq;
using ExnessTests.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExnessTests.dto;

namespace ExnessTests
{
    [TestClass]
    public class UnitTest1
    {
		[TestInitialize()]
		public void Startup()
        {
			var execPg = new ExecPg();
			string query = "INSERT INTO categories(id, name) VALUES "+
				"('1ef43957-05a1-4900-819e-a4d8646188d5', 'System utilities'),"+
				"('58bd2c46-6f8f-4474-9ba4-1038730e01d4','Testing software');";
			execPg.ExecQuery(query);

			query = "INSERT INTO vendors(id, name, raiting, categories) VALUES " +
				"('587d6b11-1491-456a-8e5c-d28d99ffdded', 'Testing corp', '5', '{1ef43957-05a1-4900-819e-a4d8646188d5,58bd2c46-6f8f-4474-9ba4-1038730e01d4}');";      
			execPg.ExecQuery(query);
        }

		[TestCleanup()]
		public void Cleanup()
        {
			var execPg = new ExecPg();
			string query = "delete from vendors where id ='587d6b11-1491-456a-8e5c-d28d99ffdded'";
			execPg.ExecQuery(query);
			query = "delete from categories where id::text in " +
				"('1ef43957-05a1-4900-819e-a4d8646188d5','58bd2c46-6f8f-4474-9ba4-1038730e01d4')";
			execPg.ExecQuery(query);
        }

        [TestMethod]
		public void TestMethodGetVendorById()
        {
			var result = new webresponsedata();
			result = Vendors.getVendorById("587d6b11-1491-456a-8e5c-d28d99ffdded");
			Assert.AreEqual("OK", result.status);
			StringAssert.Contains(result.body,"Testing corp");
		}

		[TestMethod]
		public void TestMethodGetVendorByWrongId()
        {
            var result = new webresponsedata();
            result = Vendors.getVendorById("123");
            Assert.AreEqual("OK", result.status);
			StringAssert.Contains(result.body, "is not found");
        }
    }
}
