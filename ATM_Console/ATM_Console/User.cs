using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ATM_Console
{
    /// <summary>
    /// Abstract class inherited by classes containing information on the user.
    /// </summary>
    abstract class User
    {
        #region Constructors
        /// <summary>
        /// Default constructor with no parameters.
        /// </summary>
        public User()
        {

        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="accountNumber">
        /// String representing the number associated with the account.
        /// </param>
        public User(string accountNumber)
        {
            this.accountNumber = accountNumber;
        }
        #endregion

        #region Destructors
        ~User()
        {

        }
        #endregion

        #region Operators

        #endregion

        #region Fields
        /// <summary>
        /// The four-digit number associated with the account.
        /// </summary>
        public readonly string accountNumber;
        /// <summary>
        /// The four-digit PIN associated with the account.
        /// Required for validation of transactions.
        /// </summary>
        protected string _password;
        #endregion

        #region Properties

        public string Password { get => _password; set => _password = value; }
        
        #endregion

        #region Methods
        private void GetAccountInfo()
        {
            StreamReader streamReader;

            ///<todo>
            /// Encapsulate assembly and streamreader in their own try-catch statements.
            /// </todo>

            streamReader = new StreamReader("Account" + accountNumber + ".txt");

            _password = streamReader.ReadLine();

            streamReader.Close();
        }

        /// <summary>
        /// Reports a string containing an error message to the console.
        /// </summary>
        /// <param name="error">
        /// String containing the error message to be reported.
        /// </param>
        private void ReportError(string error)
        {
            Console.WriteLine(error);
        }


        public bool CheckPassword(string pin)
        {
            if (pin == _password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
