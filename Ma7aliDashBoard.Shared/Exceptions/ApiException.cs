﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7aliDashBoard.Shared.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
    }

}
