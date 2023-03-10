using ATMApp.Domain.Enums;
using System.Threading.Tasks;

namespace ATMApp.Domain.Interfaces
{
    public interface ITransaction
    {
        Task InsertTransaction(int _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc);
        void ViewTransaction();
    }
}
