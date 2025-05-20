using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7aliDashBoard.Shared.Requests
{
    public class PagedRequestDto
    {
        public string Search { get; set; }
        public string SortBy { get; set; } = "Id";
        public bool Ascending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
