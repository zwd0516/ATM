using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATM_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            bool truthiness = false;

            do
            {
                Console.WriteLine("Please enter your account number.");
                input = Console.ReadLine();
                StreamReader streamReader;
                try
                {
                    streamReader = new StreamReader("Account" + input + ".txt");
                    truthiness = true;
                    Console.WriteLine(streamReader.ReadLine());
                    streamReader.Close();
                }
                catch
                {
                    Console.WriteLine("Error -- Account not found.\nPress ENTER to continue.");
                    //truthiness = false;
                }

                if (truthiness == true)
                {
                    break;
                }
                Console.ReadLine();
            } while (truthiness == false);

            Account account = new Account(input);
            Console.WriteLine("Found Account " + account.AccountNumber + ".");
            
            /// This do-while loop contains the bulk of where the interesting stuff happens.
            do
            {
                Console.WriteLine("What would you like to do?\n1. Check balance.\n2. Make a deposit.\n3. Make a withdrawal.\n4. Exit.");
                input = Console.ReadLine();
                switch (input)
                {
                    /// Check the balance of the Account.
                    case "1":
                        Console.WriteLine("Please enter your PIN.");
                        input = Console.ReadLine();
                        if (account.CheckPassword(input) == true)
                        {
                            /// While computations involving floats are considerably faster, decimal will be perfectly adequate.
                            decimal balance = (decimal)account.Balance / 100;
                            Console.WriteLine("Your current balance is $" + balance + ".");
                            /// Zero the value of balance before it is dereferenced.
                            /// Probably unnecessary, but I don't yet know the capabilities of C#'s garbage-collection mechanism.
                            /// Definitely a frivolous precaution considering the glaring vulnerability presented by
                            /// the fact that account text files aren't even encrypted.
                            balance = 0;
                            truthiness = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect PIN.");
                            truthiness = false;
                        }
                        Console.ReadLine();
                        break;

                    /// Make a deposit.
                    case "2":
                        Console.WriteLine("How much would you like to deposit?");
                        input = Console.ReadLine();
                        Console.WriteLine("Please enter your PIN.");
                        input = Console.ReadLine();
                        if (account.CheckPassword(input) == true)
                        {
                            /// While computations involving floats are considerably faster, decimal will be perfectly adequate.
                            decimal balance = (decimal)account.Balance / 100;
                            Console.WriteLine("Your current balance is $" + balance + ".");
                            /// Zero the value of balance before it is dereferenced.
                            /// Probably unnecessary, but I don't yet know the capabilities of C#'s garbage-collection mechanism.
                            /// Definitely a frivolous precaution considering the glaring vulnerability presented by
                            /// the fact that account text files aren't even encrypted.
                            balance = 0;
                            truthiness = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect PIN.");
                            truthiness = false;
                        }
                        Console.ReadLine();
                        break;

                    case "4":
                        truthiness = true;
                        break;

                    default:
                        Console.WriteLine("Please select from one of the listed options.");
                        truthiness = false;
                        break;
                }
            }
            while (truthiness == false);

            Console.WriteLine("Thank you for your business!");
            Console.ReadLine();
        }
    }
}
