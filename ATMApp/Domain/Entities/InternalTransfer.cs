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
