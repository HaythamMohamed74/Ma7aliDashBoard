﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.OrderEntities
{
    public class OrderAddress:BaseEntity
    {
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string  Street { get; set; }
        public string  City { get; set; }
        public string? PostalCode { get; set; }

    }
}
