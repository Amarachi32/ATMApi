using ATMApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }

        [ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }
        
        public DateTime TransactionDate { get; set; }
        public  TransactionType TransactionType { get; set; }
        public string? Descriprion { get; set; }
        public decimal TransactionAmount { get; set; }

        // navigation properties
        //[ForeignKey("UserAccountId")]
        public virtual UserAccount UserAccount { get; set; }
    }
}
