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
        public static string DeleteSingleObject(string distinguishedName, string commonName, bool firstAttempt = true)
        {
            // Attempt to delete the AD object
            try
            {
                // Get the directory entries for both the selected container in the tree and the selected object in the data grid
                DirectoryEntry dirEntry      = new DirectoryEntry(BackEnd.LDAPPath_Property,                                         BackEnd.User_Property, BackEnd.Pass_Property, AuthenticationTypes.Secure);
                DirectoryEntry generalObject = new DirectoryEntry("LDAP://" + BackEnd.DomainName_Property + "/" + distinguishedName, BackEnd.User_Property, BackEnd.Pass_Property, AuthenticationTypes.Secure);
                
                // Delete the object
                dirEntry.Children.Remove(generalObject);

                // Return a success message
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
                // If access was denied and this is the first attempt to fix the issue
                if (e.Message.ToString() == "Access is denied.\r\n" && firstAttempt)
                {
                    // Get the object
                    DirectoryEntry generalObject = new DirectoryEntry("LDAP://" + BackEnd.DomainName_Property + "/" + distinguishedName, BackEnd.User_Property, BackEnd.Pass_Property, AuthenticationTypes.Secure);
                    
                    // For each access control rule put in place to protect the object (not including inherited ACLs)
                    foreach (ActiveDirectoryAccessRule x in generalObject.ObjectSecurity.GetAccessRules(true, false, typeof(System.Security.Principal.NTAccount)))
                    {
                        // If the rule applies to everyone
                        if (x.IdentityReference.Value == "Everyone")
                        {
                            // If the rule is specificly for DeleteTree and Delete rights
                            if (x.ActiveDirectoryRights.ToString() == "DeleteTree, Delete")
                            {
                                // Ask the user if they want to bypass the protection
                                System.Windows.MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("The object \"" + commonName + "\" is protected from accidental deletion. Would you like to delete it anyway?", "Delete Object", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.No);

                                // If the user wants to remove the object even though it is protected
                                if (result == System.Windows.MessageBoxResult.Yes)
                                {
                                    // Remove the access rule
                                    generalObject.ObjectSecurity.RemoveAccessRule(x);
                                }
                                // If the user wants to leave the object protected
                                else
                                {
                                    // Return the original error message
                                    return e.Message.ToString();
                                }
                            }
                        }
                    }

                    // Save new rules
                    generalObject.CommitChanges();

                    // Try again
                    return DeleteSingleObject(distinguishedName, commonName, false);
                }
                // If access was not denied (if the deletion failed for some other reason)
                else
                {
                    // Return an error message
                    return e.Message.ToString();
                }
            }
        }

        public static List<GenericDeleteResult> DeleteMultipleObjects()
        {
            List<GenericDeleteResult> results = new List<GenericDeleteResult>();

            foreach (Tree ADObject in BackEnd.selectedADObjs_Property)
            {
                GenericDeleteResult x = new GenericDeleteResult()
                {
                    commonName_Property        = ADObject.commonName_Property,
                    distinguishedName_Property = ADObject.distinguishedName,
                    schemaClass_Property       = ADObject.schemaClass
                };

                x.status_Property = DeleteSingleObject(x.distinguishedName_Property, x.commonName_Property);

                results.Add(x);
            }

            return results;
        }
    }
}
