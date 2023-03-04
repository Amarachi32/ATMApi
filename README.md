# ATM Operations using Entity Framework Core🤷‍♀️


This project demonstrates how to perform basic ATM operations using Entity Framework Core in C#.
It includes functionality for user login, deposit, withdrawal, and transaction history..

## Requirements
 + Visual Studio 2019 or later
 + .NET Core 3.1 or later
 + SQL Server

 ## Getting Started
 To get started, clone the repository to your local machine and open the solution in Visual Studio.

 ## Running the Application

 When you run the application, you will be prompted to log in with a cardnumber and cardpin.
 You can use the following credentials to log in:
 | CardNumber  | CardPin  |  AccountNumber |   |   |
|---|---|---|---|---|
|   |   |   |   |   |
|   |   |   |   |   |
|   |   |   |   |   |
 |  CardNumber     | CardPin |  | AccountNumber |
| ----------- | ----------- |  ----------- |
|  321321  | 123123      |  123456      |
| 654654   | 456456      | 456789      |
| 987987   | 789789      | 123555      |

   

   After logging in, you will be presented with a menu of options:
   1. View Balance
   2. Deposit
   3. Withdraw
   4. Transfer
   5. View Transaction
   Log Out

To perform an operation, simply select the corresponding menu option and follow the prompts.


## Code Overview
The application is built using Entity Framework Core and C#. The main classes are:

`AtmDbContext`: The DbContext class that represents the database and provides 
   access to the `User` and `Transaction` entities.
   
`UserAccount`: The entity that represents a user in the database. It has properties for the user's ID, cardnumber, cardpin, accountnumber etc.

`Transaction`: The entity that represents a transaction in the database. It has properties for the transaction's ID, user ID, type (deposit or withdrawal), amount, and date.

`Entry`: The main program class that handles user input and output.

The application uses Entity Framework Core to interact with the database. When the user performs an operation, the application retrieves the necessary data from the database,
performs the operation, and saves the changes back to the database.

## Conclusion
This project demonstrates how to perform basic ATM operations using Entity Framework Core in C#. It includes functionality for user login, deposit, withdrawal, and transaction history. You can use this project as a starting point 
for your own ATM application or as a learning resource to help you understand Entity Framework Core.

## Software Development Summary😃👓👓
* **Technology**: C#👓
* **Framework**: .NET6
* **Project Type**: Console
* **IDE**: Visual Studio (Version 2022)
* **Paradigm or pattern of programming**: Object-Oriented Programming (OOP)
* **DataBase**:Sql Server
* **Data**:Entity Framework

