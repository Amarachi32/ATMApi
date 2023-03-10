
using ATMApp.Domain.Entities;
using ATMApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Domain.Interfaces
{
    public interface ITransaction
    {
         Task InsertTransaction(int _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc);
        void ViewTransaction();
    }
}
