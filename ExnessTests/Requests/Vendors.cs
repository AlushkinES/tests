using System;
using System.Collections.Generic;
using ExnessTests.Exntenstions;
using ExnessTests.dto;

namespace ExnessTests.Requests
{
    public class Vendors
    {
		public static webresponsedata getVendorById(string id)
        {
			string vendorcontroller = "http://localhost:5000/api/vendor/"+id;

            var headers = new Dictionary<string, string>()
            {
                { "Content-Type", "application/json" }
            };
            var method = "GET";
			var resp = Response.exec_web(vendorcontroller, headers, method);
			return resp;
        }
    }
}
