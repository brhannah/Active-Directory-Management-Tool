using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace ActiveDirectoryManagementTool
{
    static public partial class ObjectManagement
    {
        public static List<GroupJoinResult> AssignJobTitles(string jobTitle)
        {
            // Get a copy of the dictionary
            DictionaryManagement dictionary = new DictionaryManagement();

            // Create a list of results
            List<GroupJoinResult> results = new List<GroupJoinResult>();

            // For each selected object
            foreach (Tree x in BackEnd.selectedADObjs_Property)
            {
                // If the object is a user
                if (x.schemaClass == "user")
                {
                    // Bind to the specified user
                    DirectoryEntry userentry = new DirectoryEntry("LDAP://" + BackEnd.DomainName_Property + "/" + x.distinguishedName, BackEnd.User_Property, BackEnd.Pass_Property, AuthenticationTypes.Secure);
                    
                    // Set the user's job title
                    userentry.Properties["title"].Value = jobTitle;

                    // Save the changes
                    userentry.CommitChanges();

                    // For each definition of the selected dictionary term
                    foreach (string groupName in dictionary.terms[jobTitle])
                    {
                        // Create a new result
                        results.Add(new GroupJoinResult());
                        
                        // Link the result to the AD object and group
                        results.Last().objectInstance_Property = x.commonName_Property;
                        results.Last().group_Property          = groupName;
                        
                        // Attempt to add the AD object to the group
                        results.Last().status_Property = JoinSingleObjectToGroup(x.distinguishedName, groupName);
                    }
                }
            }

            // Return the results
            return results;
        }
    }
}
