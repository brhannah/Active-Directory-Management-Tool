using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryManagementTool
{
    static public partial class ObjectManagement
    {
        public static string JoinSingleObjectToGroup(string distinguishedName, string groupName)
        {
            // Bind to the domain object
            DirectoryEntry domainEntry = new DirectoryEntry("LDAP://" + BackEnd.DomainName_Property, BackEnd.User_Property, BackEnd.Pass_Property);
            
            // Create a searcher that can find the group in the domain
            DirectorySearcher domainSearcher = new DirectorySearcher(domainEntry);
            
            // Set the properties of the search
            domainSearcher.SearchScope = SearchScope.Subtree;
            domainSearcher.Filter = "(&(objectCategory=group)(samaccountname=" + groupName + "))";

            // Create the entry for the group
            DirectoryEntry groupEntry;

            // Try to find the group
            try
            {
                // Find the group
                groupEntry = domainSearcher.FindOne().GetDirectoryEntry();
            }
            // If the group was not found
            catch
            {
                return "The group was not found";
            }
            
            // Bind to the group's container object
            PrincipalContext groupsDomainContext = new PrincipalContext(ContextType.Domain, BackEnd.DomainName_Property, groupEntry.Parent.Path.Replace("LDAP://" + BackEnd.DomainName_Property + "/", ""),    BackEnd.User_Property, BackEnd.Pass_Property);

            // Bind to the object's container object
            PrincipalContext objectDomainContext = new PrincipalContext(ContextType.Domain, BackEnd.DomainName_Property, BackEnd.LDAPPath_Property.Replace("LDAP://" + BackEnd.DomainName_Property + "/", ""), BackEnd.User_Property, BackEnd.Pass_Property);

            // Get the specified group
            GroupPrincipal group = GroupPrincipal.FindByIdentity(groupsDomainContext, groupName);

            // Try to add the object to the group
            try
            {
                // Attempt to add the object to the group
                group.Members.Add(objectDomainContext, IdentityType.DistinguishedName, distinguishedName);

                // Save the changes
                group.Save();

                // Return the result
                return "Success";
            }
            // If the program failed to add the AD object
            catch (DirectoryServicesCOMException e)
            {
                // Return an error message
                return e.Message.ToString();
            }
            // If the program failed to add the AD object
            catch (Exception e)
            {
                // If the error is "The principal already exists in the store."
                if (e.Message.ToString() == "The principal already exists in the store.")
                {
                    // Translate to english
                    return "The object is already a member of this group";
                }

                // Return an error message
                return e.Message.ToString();
            }
        }

        public static List<GroupJoinResult> JoinMultipleObjectsToGroup(string groupName)
        {
            // Create a list of results
            List<GroupJoinResult> results = new List<GroupJoinResult>();

            // For each selected object
            foreach (Tree x in BackEnd.selectedADObjs_Property)
            {
                // Create a new result
                results.Add(new GroupJoinResult());

                // Link the result to the AD object and group
                results.Last().objectInstance_Property = x.commonName_Property;
                results.Last().group_Property          = groupName;

                // Attempt to add the AD object to the group
                results.Last().status_Property = JoinSingleObjectToGroup(x.distinguishedName, groupName);
            }

            // Return the results
            return results;
        }
    }
}
