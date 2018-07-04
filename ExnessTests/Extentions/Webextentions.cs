using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExnessTests.Exntenstions
{
	public static class WebExtensions
    {
        /// получить тело веб-запроса
        public static string ReadBody(this WebResponse src)
        {
            using (StreamReader reader = new StreamReader(src.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
        /// записать тело веб-запроса
        public static void WriteBody(this WebRequest src, string body)
        {
            if (body != null && body != "")
            {
                using (StreamWriter writer = new StreamWriter(src.GetRequestStream(), Encoding.UTF8))
                {
                    writer.Write(body);
                }
            }
        }
    }
}
