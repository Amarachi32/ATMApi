using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Domain.Entities
{
    public class InternalTransfer : BaseEntity
    {
        public int InternalTransferId { get; set; }
        public decimal TransferAmount { get; set; }
        public long ReciepeintBankAccountNumber { get; set; }
        public string RecipientBankAccountName { get; set; }
    }
}
