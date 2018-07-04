using System;
using System.Collections.Generic;
using System.Net;
using ExnessTests.dto;

namespace ExnessTests.Exntenstions
{
    public class Response
    {
		//Ответ на запрос
		public static webresponsedata exec_web(string url, Dictionary<string, string> headers, string method, string reqbody = "")
		{
			var req = HttpWebRequest.Create(url);
			req.Method = method;

			foreach (var header in headers)
			{
				if (header.Key == "Content-Type")
				{
					req.ContentType = header.Value;
				}
				else if (header.Key == "Content-Length")
				{
					req.ContentLength = long.Parse(header.Value);
				}
				else
				{
					req.Headers[header.Key] = header.Value;
				}
			}
			if (reqbody != "")
			{
				req.WriteBody(reqbody);
			}
			var response = (HttpWebResponse)req.GetResponse();
			var body = response.ReadBody();

			return new webresponsedata() { body = body, status = response.StatusCode.ToString() };
		}
    }
}
