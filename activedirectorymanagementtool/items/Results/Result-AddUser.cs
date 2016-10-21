using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryManagementTool
{
    public class UserAddResult
    {
        // Declare class variables
        private string title;
        private string firstName;
        private string lastName;
        public  string password;
        private bool   changePasswordAtNextLogon;
        private bool   cannotChangePassword;
        private bool   passwordNeverExpires;
        private bool   accountIsDisabled;
        private string status;

        // Constructor
        public UserAddResult()
        {
            // Initialize class variables
            this.title                     = string.Empty;
            this.firstName                 = string.Empty;
            this.lastName                  = string.Empty;
            this.password                  = string.Empty;
            this.changePasswordAtNextLogon = false;
            this.cannotChangePassword      = false;
            this.passwordNeverExpires      = false;
            this.accountIsDisabled         = false;
            this.status                    = string.Empty;
        }

        // title Property (getter/setter)
        public string title_Property
        {
            get { return this.title; }
            set { this.title = value; }
        }

        // firstName Property (getter/setter)
        public string firstName_Property
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        // lastName Property (getter/setter)
        public string lastName_Property
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        // changePasswordAtNextLogon Property (getter/setter)
        public bool changePasswordAtNextLogon_Property
        {
            get { return this.changePasswordAtNextLogon; }
            set { this.changePasswordAtNextLogon = value; }
        }

        // cannotChangePassword Property (getter/setter)
        public bool cannotChangePassword_Property
        {
            get { return this.cannotChangePassword; }
            set { this.cannotChangePassword = value; }
        }

        // passwordNeverExpires Property (getter/setter)
        public bool passwordNeverExpires_Property
        {
            get { return this.passwordNeverExpires; }
            set { this.passwordNeverExpires = value; }
        }

        // accountIsDisabled Property (getter/setter)
        public bool accountIsDisabled_Property
        {
            get { return this.accountIsDisabled; }
            set { this.accountIsDisabled = value; }
        }

        // status Property (getter/setter)
        public string status_Property
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
