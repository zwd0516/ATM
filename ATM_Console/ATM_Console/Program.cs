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
        #region Fields
        public const string CONTINUE_PROMPT = "\n\nPress ENTER to continue.";
        ///public const string LINE_SEPARATOR = "\n\t\t*\t*\t*\n";
        ///public const string HEADER_CHECK_BALANCE = "CHECK BALANCE" + LINE_SEPARATOR;
        ///public const string HEADER_MAKE_DEPOSIT = "MAKE A DEPOSIT";
        #endregion

        #region Methods
        static void Main(string[] args)
        {
            string input;
            bool truthiness = false;

            input = RetrieveAccount();
            Account account = new Account(input);
            Console.WriteLine("Found Account " + account.AccountNumber + "." + CONTINUE_PROMPT);
            Console.ReadLine();

            /// Reset values of input and truthiness.
            input = "";

            /// This do-while loop contains the bulk of where the interesting stuff happens.
            do
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?\n1. Check balance.\n2. Make a deposit.\n3. Make a withdrawal.\n4. Exit.");
                input = Console.ReadLine();
                switch (input)
                {
                    /// Check the balance of the Account.
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Please enter your PIN.");
                        input = Console.ReadLine();
                        if (account.CheckPassword(input) == true)
                        {
                            /// Clear the console to protect the user's password.
                            Console.Clear();
                            /// While computations involving floats are considerably faster, decimal will be perfectly adequate.
                            Console.WriteLine("PIN accepted.\n\nYour current balance is $" + (decimal)account.Balance / 100 + "." + CONTINUE_PROMPT);
                            truthiness = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect PIN." + CONTINUE_PROMPT);
                            truthiness = false;
                        }
                        Console.ReadLine();
                        break;

                    /// Make a deposit.
                    case "2":
                        Console.WriteLine("How much would you like to deposit?");
                        input = Console.ReadLine();
                        decimal balance;
                        try
                        {
                            balance = Convert.ToDecimal(input);
                        }
                        catch
                        {
                            Console.WriteLine("Error -- Input rejected.\nPlease enter a numerical value." + CONTINUE_PROMPT);
                            Console.ReadLine();
                            truthiness = false;
                            break;
                        }
                        Console.WriteLine("Please enter your PIN to deposit $" + balance + " to your account.");
                        input = Console.ReadLine();
                        if (account.CheckPassword(input) == true)
                        {
                            Console.Clear();
                            Console.WriteLine("PIN accepted. Depositing $" + balance + " to your account...\n");
                            if (account.ChangeBalance(balance))
                            {
                            Console.WriteLine("Transaction completed. Your new balance is $" + (decimal)account.Balance / 100 + "." + CONTINUE_PROMPT);
                            truthiness = true;
                            }
                            else
                            {
                                Console.WriteLine("An error occured while processing your transaction." + CONTINUE_PROMPT);
                                truthiness = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Incorrect PIN." + CONTINUE_PROMPT);
                            truthiness = false;
                        }
                        Console.ReadLine();
                        break;

                    case "4":
                        truthiness = true;
                        break;

                    default:
                        ///Console.WriteLine("Please select from one of the listed options.");
                        truthiness = false;
                        break;
                }
            }
            while (truthiness == false);

            Console.Clear();
            Console.WriteLine("Thank you for your business!");
            Console.ReadLine();
        }

        static string RetrieveAccount()
        {
            string input = "";
            bool truthiness = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Please enter your account number.");
                input = Console.ReadLine();
                /// Check if file with supplied number exists.
                //if (File.Exists(ACCOUNT_FILE_DIRECTORY + "Account" + input + ".txt"))
                //{
                    /// Meet conditions to break do-while loop.
                    truthiness = true;
                //}
                //else
                //{
                StreamReader sr = new StreamReader("Account" + input + ".txt");
                    Console.WriteLine("Error -- Account not found." + CONTINUE_PROMPT);
                    truthiness = false;
                    Console.ReadLine();
                //}
            } while (truthiness == false);

            return input;
        }
        #endregion
    }
}
