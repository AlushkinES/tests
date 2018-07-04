using System;
using System.Collections.Generic;

namespace Exness.dto
{
	class exnessObj{
		
		public class Category
        {
            public Guid id { get; set; }
            public string name { get; set; }
        }

        public class Vendors
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public Int32 rating { get; set; }
			public List <Category> category { get; set; }
        }
	}
}
