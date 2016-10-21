using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace ActiveDirectoryManagementTool
{
    public static partial class BackEnd
    {
        // Declare class variables
        private static string Domain          = string.Empty; // Domain to manage (i.e. "DOMAIN.NET")
        private static string User            = string.Empty; // User to authenticate as (i.e. "DOMAIN\USER")
        private static string Pass            = string.Empty; // Password associated with user's account
        private static bool   isAuthenticated = false;        // Flag to determine if the user was able to authenticate to the given domain

        // Domain Property (getter/setter)
        public static string Domain_Property
        {
            get { return Domain; }
            set { Domain = value; }
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

        // isAuthenticated Property (getter/setter)
        public static bool isAuthenticated_Property
        {
            get { return isAuthenticated; }
            set { isAuthenticated = value; }
        }

        public static string testAuth()
        {
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + Domain, User, Pass, AuthenticationTypes.Secure);

            try
            {
                // Authenticate to AD
                string directoryEntryName = directoryEntry.Name;

                directoryEntry.Close();
                directoryEntry.Dispose();

                isAuthenticated = true;

                return "Successfully authenticated to the " + Domain + " domain as " + User + "! isAuthenticated = " + isAuthenticated.ToString();
            }
            catch (DirectoryServicesCOMException e)
            {
                directoryEntry.Close();
                directoryEntry.Dispose();

                isAuthenticated = false;

                // Provide an error message
                return "ERROR: " + e.Message.ToString() + " The username or password may be incorrect. DOMAIN = " + Domain + " USERNAME = " + User + " PASSWORD = " + Pass;
            }
            catch (Exception e)
            {
                directoryEntry.Close();
                directoryEntry.Dispose();

                isAuthenticated = false;

                // Provide an error message
                return "ERROR: " + e.Message.ToString() + " The domain name may be incorrect. DOMAIN = " + Domain + " USERNAME = " + User + " PASSWORD = " + Pass;
            }
        }

        public static List<Tree> getADObjects(string distinguishedName = "")
        {
            // Create a list to store OUs
            List<Tree> ADObjs = new List<Tree>();

            // Authenticate to AD
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + Domain + distinguishedName, User, Pass, AuthenticationTypes.Secure);

            // Get the OUs in the domain
            foreach (DirectoryEntry x in directoryEntry.Children)
            {
                Console.WriteLine(x.SchemaClassName + " + " + x.Path.ToString());
                //bool domainNodeAdded = false;
                //foreach(Tree y in ADObjs)
                //{
                //    if (y.Name == x.Path.ToString().Remove(0, "LDAP://".Length).Split('/')[0])
                //    {
                //        domainNodeAdded = true;
                //    }
                //}
                //
                //if (!domainNodeAdded)
                //{
                //    ADObjs.Add(new Tree(x.Path.ToString().Remove(0, "LDAP://".Length).Split('/')[0]));
                //}

                //remove the LDAP prefix from the path

                x.Close();
                x.Dispose();
            }

            directoryEntry.Close();
            directoryEntry.Dispose();

            // Return the list of OUs or an error
            return ADObjs;
        }
    }
}
