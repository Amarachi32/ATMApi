
using ATMApp.Domain.DataBase;
using ATMApp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ATMApp atmApp = new ATMApp();
            atmApp.InitializeData();
            atmApp.Run();              
           
        }
    }
}
