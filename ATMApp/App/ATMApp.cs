using ATMApp.Domain.DataBase;
using ATMApp.Domain.Entities;
using ATMApp.Domain.Enums;
using ATMApp.Domain.Interfaces;
using ATMApp.UI;
using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ATMApp
{
    public class ATMApp : IUserLogin, IUserAccountActions, ITransaction
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;
        private List<Transaction> _listOfTransactions;
        private const decimal minimumKeptAmount = 500;
        private readonly AppScreen screen;
        AtmBuilder atmBuild = new AtmBuilder();
        private UserAccount inputAccount;

        public ATMApp()
        {
            screen = new AppScreen();
        }

        public void Run()
        {
            var atmCon = atmBuild.CreateDbContext(null);
            var anyUser = atmCon.Users;
            AppScreen.Welcome();
            CheckUserCardNumAndPassword();
            while (true)
            {
                AppScreen.DisplayAppMenu();
                ProcessMenuoption();
            }


        }
        public async Task InitializeData()
        {
            var atmCon = atmBuild.CreateDbContext(null);
            bool anyUser = await atmCon.Users.AnyAsync();

            if (!anyUser)
            {
                userAccountList = new List<UserAccount>
              {
                  new UserAccount{ FullName = "maraxhi", AccountNumber=123456,CardNumber =321321, CardPin=123123,AccountBalance=50000.00m,IsLocked=false},
                  new UserAccount{ FullName = "chris", AccountNumber=456789,CardNumber =654654, CardPin=456456,AccountBalance=4000.00m,IsLocked=false},
                  new UserAccount{ FullName = "mccoy", AccountNumber=123555,CardNumber =987987, CardPin=789789,AccountBalance=2000.00m,IsLocked=true},
              };
                Console.WriteLine("created a user");
                await atmCon.Users.AddRangeAsync(userAccountList);
                await atmCon.SaveChangesAsync();
            }
            _listOfTransactions = new List<Transaction>();
        }
        public void CheckUserCardNumAndPassword()
        {
            bool isCorrectLogin = false;
            while (isCorrectLogin == false)
            {
                inputAccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();
                using (var context = atmBuild.CreateDbContext(null))
                {
                    var user = context.Users.FirstOrDefault(u => u.CardNumber == inputAccount.CardNumber && u.CardPin == inputAccount.CardPin);
                    if (user != null)
                    {
                        inputAccount.TotalLogin++;
                        // User authenticated
                        if (inputAccount.IsLocked || inputAccount.TotalLogin > 3)
                        {
                            AppScreen.PrintLockScreen();
                        }
                        else
                        {
                            inputAccount.TotalLogin = 0;
                            isCorrectLogin = true;
                            break;
                        }
                        AppScreen.WelcomeCustomer(inputAccount.FullName);
                    }
                    else if (isCorrectLogin == false)
                    {
                        Utility.PrintMessage("\nInvalid card number or PIN.", false);
                        inputAccount.IsLocked = inputAccount.TotalLogin == 3;
                        if (inputAccount.IsLocked)
                        {
                            AppScreen.PrintLockScreen();
                        }
                        // Authentication failed
                    }
                }
                Console.Clear();

              
            }
        }

        private void ProcessMenuoption()
        {
            switch (Validator.Convert<int>("an option:"))
            {
                case (int)AppMenu.CheckBalance:
                    CheckBalance();
                    break;
                case (int)AppMenu.PlaceDeposit:
                    PlaceDeposit();
                    break;
                case (int)AppMenu.MakeWithdrawal:
                    MakeWithDrawal();
                    break;
                case (int)AppMenu.InternalTransfer:
                    var internalTransfer = screen.InternalTransferForm();
                    ProcessInternalTransfer(internalTransfer);
                    break;
                case (int)AppMenu.ViewTransaction:
                    ViewTransaction();
                    break;
                case (int)AppMenu.Logout:
                    AppScreen.LogoutProgress();
                    Utility.PrintMessage("You have successfully logged out. Please collect " +
                        "your ATM card.");
                    Run();
                    break;
                default:
                    Utility.PrintMessage("Invalid Option.", false);
                    break;
            }
        }

        public void CheckBalance()
        {
            var atmCon = atmBuild.CreateDbContext(null);
            var balance = atmCon.Users.FirstOrDefault(u => u.CardNumber == inputAccount.CardNumber);
            if (balance != null)
            {
                Utility.PrintMessage($"Your account balance is: {Utility.FormatAmount(balance.AccountBalance)}");

            }
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("\nOnly multiples of 500 and 1000 naira allowed.\n");
            var transaction_amt = Validator.Convert<int>($"amount {AppScreen.cur}");

            //simulate counting
            Console.WriteLine("\nChecking and Counting bank notes.");
            Utility.PrintDotAnimation();
            Console.WriteLine("");

            var atmCon = atmBuild.CreateDbContext(null);

            var account = atmCon.Users.FirstOrDefault(a => a.CardNumber == inputAccount.CardNumber);
            if (account != null)
            {

                //some gaurd clause
                if (transaction_amt <= 0)
                {
                    Utility.PrintMessage("Amount needs to be greater than zero. Try again.", false); ;
                    return;
                }
                if (transaction_amt % 500 != 0)
                {
                    Utility.PrintMessage($"Enter deposit amount in multiples of 500 or 1000. Try again.", false);
                    return;
                }

                if (PreviewBankNotesCount(transaction_amt) == false)
                {
                    Utility.PrintMessage($"You have cancelled your action.", false);
                    return;
                }

                //bind transaction details to transaction object
                InsertTransaction( TransactionType.Deposit, transaction_amt, "");

                //update account balance
                account.AccountBalance += transaction_amt;

                //print success message
                Utility.PrintMessage($"Your deposit of {Utility.FormatAmount(transaction_amt)} was " +
                    $"succesful.", true);

                atmCon.SaveChanges();
            }

        }

        public void MakeWithDrawal()
        {
            var transaction_amt = 0;
            int selectedAmount = AppScreen.SelectAmount();

            var atmCon = atmBuild.CreateDbContext(null);

            var account = atmCon.Users.FirstOrDefault(a => a.CardNumber == inputAccount.CardNumber);
            if (account != null)
            {
                if (selectedAmount == -1)
                {
                    MakeWithDrawal();
                    return;
                }
                else if (selectedAmount != 0)
                {
                    transaction_amt = selectedAmount;
                }
                else
                {
                    transaction_amt = Validator.Convert<int>($"amount {AppScreen.cur}");
                }

                //input validation
                if (transaction_amt <= 0)
                {
                    Utility.PrintMessage("Amount needs to be greater than zero. Try agin", false);
                    return;
                }
                if (transaction_amt % 500 != 0)
                {
                    Utility.PrintMessage("You can only withdraw amount in multiples of 500 or 1000 naira. Try again.", false);
                    return;
                }
                //Business logic validations

                if (transaction_amt > account.AccountBalance)
                {
                    Utility.PrintMessage($"Withdrawal failed. Your balance is too low to withdraw" +
                        $"{Utility.FormatAmount(transaction_amt)}", false);
                    return;
                }
                if ((account.AccountBalance - transaction_amt) < minimumKeptAmount)
                {
                    Utility.PrintMessage($"Withdrawal failed. Your account needs to have " +
                        $"minimum {Utility.FormatAmount(minimumKeptAmount)}", false);
                    return;
                }
                
                //Bind withdrawal details to transaction object
                InsertTransaction( TransactionType.Withdrawal, transaction_amt, "Paid");
                //update account balance
                account.AccountBalance -= transaction_amt;
                //success message
                Utility.PrintMessage($"You have successfully withdrawn " +
                    $"{Utility.FormatAmount(transaction_amt)}.", true);

                /*                if (account.Balance >= 100)
                                {
                                    account.Balance -= 100;
                                    context.SaveChanges();
                                }*/

                //atmCon.SaveChanges();
            }
        }

        private bool PreviewBankNotesCount(int amount)
        {
            int thousandNotesCount = amount / 1000;
            int fiveHundredNotesCount = (amount % 1000) / 500;

            Console.WriteLine("\nSummary");
            Console.WriteLine("------");
            Console.WriteLine($"{AppScreen.cur}1000 X {thousandNotesCount} = {1000 * thousandNotesCount}");
            Console.WriteLine($"{AppScreen.cur}500 X {fiveHundredNotesCount} = {500 * fiveHundredNotesCount}");
            Console.WriteLine($"Total amount: {Utility.FormatAmount(amount)}\n\n");

            int opt = Validator.Convert<int>("1 to confirm");
            return opt.Equals(1);

        }

        public void InsertTransaction( TransactionType _tranType, decimal _tranAmount, string _desc)
        {
            var atmCon = atmBuild.CreateDbContext(null);

            //create a new transaction object
            var transaction = new Transaction()
            {
              //  TransactionId = Utility.GetTransactionId(),
               // UserAccountId = _UserBankAccountId,
                TransactionDate = DateTime.Now,
                TransactionType = _tranType,
                TransactionAmount = _tranAmount,
                Descriprion = _desc
            };
            Console.WriteLine(transaction);

            //await atmCon._listOfTransactions.AddRangeAsync(transaction);
             atmCon.Transactions.Add(transaction);
             atmCon.SaveChanges();
            //add transaction object to the list
            // _listOfTransactions.Add(transaction);
        }
        public void ViewTransaction()

        {
            using (var context = atmBuild.CreateDbContext(null))
            {
                var transactions = context.Transactions
                    .Where(t => t.TransactionId == inputAccount.UserAccountId)
                    .OrderByDescending(t => t.TransactionDate)
                    .ToList();


/*                foreach (var transaction in transactions)
                {
                    Console.WriteLine("{0}\t{1}\t{2:C}\t{3}", transaction.TransactionDate, transaction.TransactionType, transaction.TransactionAmount);
                }*/
                //check if there's a transaction
/*                if (transactions.Count <= 0)
                {
                    Utility.PrintMessage("You have no transaction yet.", true);
                }
                else
                {*/
                    var table = new ConsoleTable("Id", "Transaction Date", "Type", "Descriptions", "Amount " + AppScreen.cur);
                    foreach (var tran in transactions)
                    {
                        table.AddRow(tran.TransactionId, tran.TransactionDate, tran.TransactionType, tran.Descriprion, tran.TransactionAmount);
                    }
                    table.Options.EnableCount = true;
                    table.Write();
                    context.SaveChanges();
                Utility.PrintMessage($"You have {transactions.Count} transaction(s)", true);
                //}
                
            }



            //DataTable users = new DataTable();
           // var filteredTransactionList = _listOfTransactions.Where(t => t.UserAccountId == inputAccount.UserAccountId).ToList();

        }
        /*  public void ViewTransaction()
          {
              var filteredTransactionList = _listOfTransactions.Where(t => t.UserBankAccountId == selectedAccount.Id).ToList();
              //check if there's a transaction
              if(filteredTransactionList.Count <= 0)
              {
                  Utility.PrintMessage("You have no transaction yet.", true);
              }
             else
              {
                 // var table = new ConsoleTable("Id", "Transaction Date", "Type", "Descriptions", "Amount " + AppScreen.cur);
                  var table = new ConsoleTable("Id", "Transaction Date", "Type", "Descriptions", "Amount " + AppScreen.cur);
                  foreach(var tran in filteredTransactionList)
                  {
                      table.AddRow(tran.TransactionId, tran.TransactionDate, tran.TransactionType, tran.Descriprion, tran.TransactionAmount);
                  }
                  table.Options.EnableCount = false;
                  table.Write();
                  Utility.PrintMessage($"You have {filteredTransactionList.Count} transaction(s)", true);
              }           
          }*/
        private void ProcessInternalTransfer(InternalTransfer internalTransfer)
        {
            var atmCon = atmBuild.CreateDbContext(null);
            var sourceAccount = atmCon.Users.FirstOrDefault(a => a.CardNumber == inputAccount.CardNumber);
            var destinationAccount = atmCon.Users.FirstOrDefault(a => a.AccountNumber == internalTransfer.ReciepeintBankAccountNumber);
            if (sourceAccount != null && destinationAccount != null)
            {
                /*                if (sourceAccount.AccountBalance >= 100)
                                {
                                    sourceAccount.AccountBalance -= 100;
                                    destinationAccount.AccountBalance += 100;
                                    atmCon.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient balance");
                                }*/

                if (internalTransfer.TransferAmount <= 0)
                {
                    Utility.PrintMessage("Amount needs to be more than zero. Try again.", false);
                    return;
                }
                //check sender's account balance
                if (internalTransfer.TransferAmount > sourceAccount.AccountBalance)
                {
                    Utility.PrintMessage($"Transfer failed. You do not have enough balance" +
                        $" to transfer {Utility.FormatAmount(internalTransfer.TransferAmount)}", false);
                    return;
                }
                //check the minimum kept amount 
                if ((sourceAccount.AccountBalance - internalTransfer.TransferAmount) < minimumKeptAmount)
                {
                    Utility.PrintMessage($"Transfer failed. Your account needs to have minimum" +
                        $" {Utility.FormatAmount(minimumKeptAmount)}", false);
                    return;
                }

                //check reciever's account number is valid
/*                var selectedBankAccountReciever = (from userAcc in userAccountList
                                                   where userAcc.AccountNumber == internalTransfer.ReciepeintBankAccountNumber
                                                   select userAcc).FirstOrDefault();*/
                if (destinationAccount == null)
                {
                    Utility.PrintMessage("Transfer failed. Recieber bank account number is invalid.", false);
                    return;
                }
                //check receiver's name
                if (destinationAccount.FullName != internalTransfer.RecipientBankAccountName)
                {
                    Utility.PrintMessage("Transfer Failed. Recipient's bank account name does not match.", false);
                    return;
                }

                //add transaction to transactions record- sender
                InsertTransaction( TransactionType.Transfer, internalTransfer.TransferAmount, "Transfered " +
                    $"to {destinationAccount.AccountNumber} ({destinationAccount.FullName})");
                //update sender's account balance
                sourceAccount.AccountBalance -= internalTransfer.TransferAmount;

                //add transaction record-reciever
                InsertTransaction( TransactionType.Transfer, internalTransfer.TransferAmount, "Transfered from " +
                    $"{destinationAccount.AccountNumber}({destinationAccount.FullName})");
                //update reciever's account balance
                destinationAccount.AccountBalance += internalTransfer.TransferAmount;
                //print success message
                Utility.PrintMessage($"You have successfully transfered" +
                    $" {Utility.FormatAmount(internalTransfer.TransferAmount)} to " +
                    $"{internalTransfer.RecipientBankAccountName}", true);
                atmCon.SaveChanges();
            }
        }
    }
}
