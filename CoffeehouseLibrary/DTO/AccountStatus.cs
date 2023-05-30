using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeehouseLibrary.DTO
{
    public class AccountStatus
    {
        public int AccountId { get; set; }
        public bool? IsBanned { get; set; }
    }
}