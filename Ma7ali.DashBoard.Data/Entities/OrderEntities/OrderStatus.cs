using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Entities.OrderEntities
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value = "PaymentRecevied")]
        PaymentRecevied,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed
    }
}
