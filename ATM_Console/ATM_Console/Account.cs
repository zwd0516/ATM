using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATM_Console
{
    /// <summary>
    /// Handles information on a customer.
    /// </summary>
    class Account
    {
        #region Constructors
        /// <summary>
        /// Default constructor with no parameters.
        /// </summary>
        public Account()
        {

        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="accountNumber">
        /// String representing the number associated with the account.
        /// </param>
        public Account(string accountNumber)
        {
            _accountNumber = accountNumber;
            _accountFilePath = "Account" + _accountNumber + ".txt";
            GetAccountInfo();
        }
        #endregion

        #region Destructors
        ~Account()
        {

        }
        #endregion

        #region Operators

        #endregion

        #region Fields
        /// <summary>
        /// The four-digit number associated with the account. Read-only.
        /// </summary>
        private readonly string _accountNumber;
        /// <summary>
        /// The location of the account's file in memory.
        /// </summary>
        private readonly string _accountFilePath;
        /// <summary>
        /// The four-digit PIN associated with the account.
        /// Required for validation of transactions.
        /// </summary>
        private string _password = "";
        /// <summary>
        /// The account's balance.
        /// Measuring in cents allows representation as an integer, eliminating potential for
        /// rounding errors that could occur with float, double, or decimal.
        /// </summary>
        private int _balance = 0;
        #endregion

        #region Properties
        /// <summary>
        /// The four-digit number associated with the account. Read-only.
        /// </summary>
        public string AccountNumber { get => _accountNumber; }
        /// <summary>
        /// The four-digit PIN associated with the account.
        /// Required for validation of transactions.
        /// </summary>
        public string Password { get => _password; set => _password = value; }
        /// <summary>
        /// The account's balance.
        /// Measuring in cents allows representation as an integer to avoid potential for
        /// rounding errors that can occur with floating point variables.
        /// </summary>
        public int Balance { get => _balance; set => _balance = value; }
        #endregion

        #region Methods
        /// <summary>
        /// Tries to open the Account's associated text file and saves data in the Account.
        /// <usage>
        /// Assumes the first line in the text file contains the password, and the second line
        /// contains the balance represented in cents.
        /// </usage>
        /// </summary>
        private void GetAccountInfo()
        {
            StreamReader streamReader;
            
            try
            {
                streamReader = new StreamReader(_accountFilePath);

                Password = streamReader.ReadLine();        
                Balance = Convert.ToInt32(streamReader.ReadLine());

                streamReader.Close();
            }
            catch
            {
                Console.WriteLine("error.");
            }
        }

        /// <summary>
        /// Compares parameter with the value of Password.
        /// </summary>
        /// <param name="pin">
        /// String to be tested. Refers to "Personal Identification Number" used in ATMs.
        /// </param>
        /// <returns>
        /// True if pin is equal to Password.
        /// False if pin is not equal to Password.
        /// </returns>
        public bool CheckPassword(string pin)
        {
            if (pin == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Changes the value of Balance, then logs it in the Account's associated text file.
        /// The old balance stored in the text file is overwritten.
        /// </summary>
        /// <param name="deltaBalance">
        /// The value in dollars that will be added to Balance.
        /// Before this occurs, it is first converted from dollars to cents.
        /// </param>
        /// <returns></returns>
        public bool ChangeBalance(decimal deltaBalance)
        {
            deltaBalance *= 100;
            Balance += (int)deltaBalance;
            try
            {
                File.WriteAllText(_accountFilePath, "");
                StreamWriter sw = new StreamWriter(_accountFilePath);
                sw.WriteLine(Password);
                sw.WriteLine(Balance.ToString());
                sw.Close();
                return true;
            }
            catch { return false; }
        }
        #endregion
    }
}
