using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesDirectory.Models
{
    public class DirectoryModel
    {
        public DateTime LastUpdate { get; set; }
        public List<Service> Services { get; set; }

        public class Service
        {
            public string Name { get; set; }
            public string Endpoint { get; set; }
            public DateTime LastUpdate { get; set; }
            public string Status { get; set; }
        }
    }
}
