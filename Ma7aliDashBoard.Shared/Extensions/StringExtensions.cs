using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7aliDashBoard.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string NormlizePath(this string path)
        {
            return path.Replace("\\", "/");
        }

    }
}
