using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ATMApp.Domain.Entities
{
    [Index(nameof(FullName), nameof(AccountBalance), Name = "IX_Task_Name")]
    public class UserAccount : BaseEntity
    {
        public int UserAccountId { get; set; }
        public long CardNumber { get; set; }
        public int CardPin { get; set; }
        public long AccountNumber { get; set; }
        public string FullName { get; set; }
        public decimal AccountBalance { get; set; }
        public int TotalLogin { get; set; }
        public bool IsLocked { get; set; }

        // navigation property
       public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
