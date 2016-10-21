using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;

namespace ActiveDirectoryManagementTool
{
    public partial class BackEnd
    {
        // Declare class variables
        private static string                     DomainName      = string.Empty;                     // Domain to manage (i.e. "DOMAIN.NET")
        private static string                     User            = string.Empty;                     // User to authenticate as (i.e. "DOMAIN\USER")
        private static string                     Pass            = string.Empty;                     // Password associated with user's account
        private static string                     LDAPPath        = string.Empty;                     // Store the LDAP path that is currently selected
        private static bool                       isAuthenticated = false;                            // Flag to determine if the user was able to authenticate to the given domain
        private static ObservableCollection<Tree> selectedADObjs  = new ObservableCollection<Tree>(); // Objects currently selected by the user

        // DomainName Property (getter/setter)
        public static string DomainName_Property
        {
            get { return DomainName; }
            set { DomainName = value; }
        }

        // User Property (getter/setter)
        public static string User_Property
        {
            get { return User; }
            set { User = value; }
        }

        // Pass Property (getter/setter)
        public static string Pass_Property
        {
            get { return Pass; }
            set { Pass = value; }
        }

        // LDAPPath Property (getter/setter)
        public static string LDAPPath_Property
        {
            get { return LDAPPath; }
            set { LDAPPath = value; }
        }

        // isAuthenticated Property (getter/setter)
        public static bool isAuthenticated_Property
        {
            get { return isAuthenticated; }
            set { isAuthenticated = value; }
        }

        // selectedADObjs Property (getter/setter)
        public static ObservableCollection<Tree> selectedADObjs_Property
        {
            get { return selectedADObjs; }
            set { selectedADObjs = value; }
        }

        public static string testAuth()
        {
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + DomainName, User, Pass, AuthenticationTypes.Secure);

            try
            {
                // Authenticate to AD
                string directoryEntryName = directoryEntry.Name;

                directoryEntry.Close();
                directoryEntry.Dispose();

                isAuthenticated = true;

                return "Successfully authenticated to the " + DomainName + " domain as " + User + "!";
            }
            catch (DirectoryServicesCOMException e)
            {
                directoryEntry.Close();
                directoryEntry.Dispose();

                isAuthenticated = false;

                // Provide an error message
                return e.Message.ToString() + " Please try again.";
            }
            catch (Exception e)
            {
                directoryEntry.Close();
                directoryEntry.Dispose();

                isAuthenticated = false;

                // Provide an error message
                return e.Message.ToString() + " The domain name may be incorrect.";
            }
        }
    }
}
