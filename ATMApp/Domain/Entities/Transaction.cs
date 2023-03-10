using ATMApp.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMApp.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }

        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? Descriprion { get; set; }
        public decimal TransactionAmount { get; set; }

        // navigation properties
        //[ForeignKey("UserAccountId")]
        public virtual UserAccount UserAccount { get; set; }
    }
}
