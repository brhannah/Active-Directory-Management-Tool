using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using System.Threading;

namespace ActiveDirectoryManagementTool
{
    public partial class BackEnd
    {
        // Declare class variables
        private static ObservableCollection<Tree> ADObjs                   = new ObservableCollection<Tree>(); // Tree structure containing all AD objects
        private static bool                       flatView                 = false;
        private static string                     filter                   = "(objectCategory=*)";
        private static List<string>               nonfilteredSchemaClasses = new List<string>() {
                    "domain",
                    "domainDNS",
                    "builtinDomain",
                    "container",
                    "organizationalUnit",
                    "lostAndFound",
                    "msDS-QuotaContainer" };

        // ADObjs Property (getter/setter)
        public static ObservableCollection<Tree> ADObjs_Property
        {
            get { return ADObjs; }
            set { ADObjs = value; }
        }

        // filter Property (getter/setter)
        public static string filter_Property
        {
            get { return filter; }
            set { filter = value; }
        }

        // flatView Property (getter/setter)
        public static bool flatView_Property
        {
            get { return flatView; }
            set { flatView = value; }
        }

        public static ObservableCollection<Tree> getDistinguishedDomainName()
        {
            // Replace the user provided domain name with the official domain name
            DomainName = Domain.GetDomain(new DirectoryContext(DirectoryContextType.Domain, DomainName, User, Pass)).Name;

            // Create the default LDAP path
            LDAPPath = "LDAP://" + DomainName;

            // Create a copy of the distinguished domain name
            string distinguishedName = "DC=" + DomainName_Property.Replace(".", ",DC=");

            // Clear the current tree
            ADObjs.Clear();

            // Add the root of the tree
            ADObjs.Add(new Tree(distinguishedName, DomainName, "domain", "", "Enabled"));

            // Get the directory entry for the domain itself
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + DomainName, User, Pass, AuthenticationTypes.Secure);

            // Create a filter
            DirectorySearcher dirSearcher = new DirectorySearcher(directoryEntry);
            if (flatView)
            {
                dirSearcher.SearchScope = SearchScope.Subtree;
                dirSearcher.Filter      = filter;
            }
            else
            {
                dirSearcher.SearchScope = SearchScope.OneLevel;
                dirSearcher.Filter      = filter;

                if (nonfilteredSchemaClasses.Contains(directoryEntry.SchemaClassName))
                {
                    dirSearcher.Filter = "(|" + filter + "(|(objectClass=domain)"             +
                                                           "(objectClass=domainDNS)"          +
                                                           "(objectClass=builtinDomain)"      +
                                                           "(objectClass=container)"          +
                                                           "(objectClass=organizationalUnit)" +
                                                           "(objectClass=lostAndFound)"       +
                                                           "(objectClass=msDS-QuotaContainer)))";
                }
            }

            // Collect the search results
            SearchResultCollection searchResults = dirSearcher.FindAll();

            // List of threads
            Thread[] listOfThreads = new Thread[searchResults.Count];

            // Get the OUs in the domain
            for (int i = 0; i < searchResults.Count; i++)
            {
                SearchResult result = searchResults[i];
                //listOfThreads[i] = new Thread(() =>
                //{
                    DirectoryEntry x = result.GetDirectoryEntry();
                    PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, BackEnd.DomainName_Property, x.Path.Replace("LDAP://" + BackEnd.DomainName_Property + "/", ""), BackEnd.User_Property, BackEnd.Pass_Property);

                    string title = string.Empty;
                    string enabled = string.Empty;

                    // if the object is a computer
                    if (x.SchemaClassName == "computer")
                    {
                        ComputerPrincipal newComputer = ComputerPrincipal.FindByIdentity(domainContext, x.Properties["samAccountName"].Value.ToString());
                        enabled = (newComputer.Enabled == true) ? "Enabled" : "Disabled";
                    }
                    // If the object is a user
                    else if (x.SchemaClassName == "user")
                    {
                        UserPrincipal newComputer = UserPrincipal.FindByIdentity(domainContext, x.Properties["samAccountName"].Value.ToString());
                        enabled = (newComputer.Enabled == true) ? "Enabled" : "Disabled";

                        try
                        {
                            // Get the user's job title
                            title = x.Properties["title"].Value.ToString();
                        }
                        catch { }
                    }

                    // Add the child to the tree structure
                    Tree node = new Tree(x.Path.ToString().Split('/').Last(), DomainName, x.SchemaClassName, title, enabled);
                    ADObjs[0].childNode.Add(node);

                    if (!flatView)
                    {
                        // Get the node's children
                        getADObjects(ref node, x.Path.ToString().Split('/').Last());
                    }

                    x.Close();
                    x.Dispose();
                //});
                //listOfThreads[i].Start();
            }

            //for (int i = 0; i < searchResults.Count; i++)
            //{
            //    listOfThreads[i].Join();
            //}

            directoryEntry.Close();
            directoryEntry.Dispose();

            return ADObjs;
        }

        public static ObservableCollection<Tree> getADObjects(ref Tree node, string distinguishedName = "")
        {
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + DomainName + "/" + distinguishedName, User, Pass, AuthenticationTypes.Secure);

            // Create a filter
            DirectorySearcher dirSearcher = new DirectorySearcher(directoryEntry);
            dirSearcher.SearchScope = SearchScope.OneLevel;
            dirSearcher.Filter      = filter;

            if (nonfilteredSchemaClasses.Contains(directoryEntry.SchemaClassName))
            {
                dirSearcher.Filter = "(|" + filter + "(|(objectClass=domain)"             +
                                                       "(objectClass=domainDNS)"          +
                                                       "(objectClass=builtinDomain)"      +
                                                       "(objectClass=container)"          +
                                                       "(objectClass=organizationalUnit)" +
                                                       "(objectClass=lostAndFound)"       +
                                                       "(objectClass=msDS-QuotaContainer)))";
            }

            // Collect the search results
            SearchResultCollection searchResults = dirSearcher.FindAll();
            
            // List of threads
            Thread[] listOfThreads = new Thread[searchResults.Count];

            // Get the OUs in the domain
            for (int i = 0; i < searchResults.Count; i++)
            {
                DirectoryEntry   x             = searchResults[i].GetDirectoryEntry();
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, BackEnd.DomainName_Property, x.Path.Replace("LDAP://" + BackEnd.DomainName_Property + "/", ""), BackEnd.User_Property, BackEnd.Pass_Property);

                string title   = string.Empty;
                string enabled = string.Empty;

                // if the object is a computer
                if (x.SchemaClassName == "computer")
                {
                    ComputerPrincipal newComputer = ComputerPrincipal.FindByIdentity(domainContext, x.Properties["samAccountName"].Value.ToString());
                    enabled = (newComputer.Enabled == true) ? "Enabled" : "Disabled";
                }
                // If the object is a user
                else if (x.SchemaClassName == "user")
                {
                    UserPrincipal newComputer = UserPrincipal.FindByIdentity(domainContext, x.Properties["samAccountName"].Value.ToString());
                    enabled = (newComputer.Enabled == true) ? "Enabled" : "Disabled";

                    try
                    {
                        // Get the user's job title
                        title = x.Properties["title"].Value.ToString();
                    }
                    catch { }
                }

                Tree tempNode = new Tree(x.Path.ToString().Split('/').Last(), DomainName, x.SchemaClassName, title, enabled);

                node.childNode.Add(tempNode);

                listOfThreads[i] = new Thread(() => getADObjects(ref tempNode, x.Path.ToString().Split('/').Last()));
                listOfThreads[i].Start();

                x.Close();
                x.Dispose();
            }

            for (int i = 0; i < searchResults.Count; i++)
            {
                listOfThreads[i].Join();
            }

            directoryEntry.Close();
            directoryEntry.Dispose();

            // Return the list of OUs or an error
            return ADObjs;
        }

        public static Tree RefreshBranch(ref Tree node)
        {
            // Remove the current children
            node.childNode.Clear();

            // Collect the children from this point down the tree
            getADObjects(ref node, node.distinguishedName);

            return node;
        }

        public static void ClearFilter()
        {
            // Reset the Filters
            flatView = false;
            filter   = "(objectCategory=*)";
        }
    }
}
