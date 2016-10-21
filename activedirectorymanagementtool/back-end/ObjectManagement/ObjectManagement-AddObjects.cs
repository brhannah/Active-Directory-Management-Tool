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
        // https://msdn.microsoft.com/en-us/library/ms180851%28v=VS.80%29.aspx
        public static string AddSingleObject<T>(T objectInstance, string objectType)
        {
            // Attempt to add the AD object
            try
            {
                // Bind to the specified container object and add a new object
                DirectoryEntry   dirEntry      = new DirectoryEntry(BackEnd.LDAPPath_Property, BackEnd.User_Property, BackEnd.Pass_Property, AuthenticationTypes.Secure);
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, BackEnd.DomainName_Property, BackEnd.LDAPPath_Property.Replace("LDAP://" + BackEnd.DomainName_Property + "/", ""), BackEnd.User_Property, BackEnd.Pass_Property);

                // Depending on the object type
                switch (objectType)
                {
                    case "computer":
                        // Add the AD object
                        DirectoryEntry newComputer = dirEntry.Children.Add("CN=" + ((ComputerAddResult)(object)objectInstance).title_Property, objectType);
                        
                        // Save the new object
                        newComputer.CommitChanges();

                        // Find the object in the domain
                        ComputerPrincipal computerAccount = ComputerPrincipal.FindByIdentity(domainContext, ((ComputerAddResult)(object)objectInstance).title_Property);
                        
                        // If the object was found
                        if (computerAccount != null)
                        {
                            // Set the object's properties
                            computerAccount.SamAccountName = ((ComputerAddResult)(object)objectInstance).title_Property + "$";
                            computerAccount.Enabled        = true;
                            computerAccount.Save();
                        }

                        break;

                    case "group":
                        // Add the AD object
                        DirectoryEntry newGroup = dirEntry.Children.Add("CN=" + ((GroupAddResult)(object)objectInstance).title_Property, objectType);
                        newGroup.Properties["samAccountName"].Value = ((GroupAddResult)(object)objectInstance).title_Property;

                        // Save the new object
                        newGroup.CommitChanges();

                        break;

                    case "user":
                        // Add the AD object using the objects common name and username
                        DirectoryEntry newUser = dirEntry.Children.Add("CN=" + ((UserAddResult)(object)objectInstance).firstName_Property + " " + ((UserAddResult)(object)objectInstance).lastName_Property, objectType);
                        newUser.Properties["samAccountName"].Value = ((UserAddResult)(object)objectInstance).title_Property;

                        // Save the new object
                        newUser.CommitChanges();

                        // Find the object in the domain
                        UserPrincipal userAccount = UserPrincipal.FindByIdentity(domainContext, ((UserAddResult)(object)objectInstance).title_Property);
                        
                        // If the object was found
                        if (userAccount != null)
                        {
                            // Set the object's properties
                            userAccount.SamAccountName           = ((UserAddResult)(object)objectInstance).title_Property;
                            userAccount.GivenName                = ((UserAddResult)(object)objectInstance).firstName_Property;
                            userAccount.DisplayName              = ((UserAddResult)(object)objectInstance).firstName_Property + " " + ((UserAddResult)(object)objectInstance).lastName_Property;
                            userAccount.Surname                  = ((UserAddResult)(object)objectInstance).lastName_Property;
                            userAccount.UserPrincipalName        = ((UserAddResult)(object)objectInstance).title_Property + "@" + BackEnd.DomainName_Property;
                            userAccount.UserCannotChangePassword = ((UserAddResult)(object)objectInstance).cannotChangePassword_Property;
                            userAccount.PasswordNeverExpires     = ((UserAddResult)(object)objectInstance).passwordNeverExpires_Property;
                            userAccount.Enabled                  = !((UserAddResult)(object)objectInstance).accountIsDisabled_Property;
                            userAccount.SetPassword(((UserAddResult)(object)objectInstance).password);
                            if(((UserAddResult)(object)objectInstance).changePasswordAtNextLogon_Property)
                            {
                                userAccount.ExpirePasswordNow();
                            }

                            // Save the object's settings
                            userAccount.Save();

                            // If the user wanted the password to be changed at next logon and the user disallowed the userAccount from changing their password
                            if (((UserAddResult)(object)objectInstance).changePasswordAtNextLogon_Property && userAccount.UserCannotChangePassword)
                            {
                                return "Success - You cannot select both 'User must change password at next logon' and 'User cannot change password' for the same user.";
                            }

                            // If the user wanted the password to be changed at next logon and the user does not want the password to expire
                            if (((UserAddResult)(object)objectInstance).changePasswordAtNextLogon_Property && userAccount.PasswordNeverExpires)
                            {
                                return "Success - You have selected 'Password never expires'. The user will not be required to change the password at next logon.";
                            }
                        }

                        break;
                }

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
                // Return an error message
                return e.Message.ToString();
            }
        }

        public static List<T> AddMultipleObjects<T>(string filepath, string objectType)
        {
            // Create a list to store results in
            List<T> results = new List<T>();

            // For each line in the file
            foreach (string line in System.IO.File.ReadAllLines(filepath))
            {
                // Create an object for the line item
                T objectInstance = (T)(Activator.CreateInstance(typeof(T), new object[]{}));

                // Depending on the object's type
                switch (objectType)
                {
                    // If the object is a computer
                    case "computer":
                        ((ComputerAddResult)(object)objectInstance).title_Property  = line;
                        ((ComputerAddResult)(object)objectInstance).status_Property = AddSingleObject<ComputerAddResult>(((ComputerAddResult)(object)objectInstance), objectType);
                        break;

                    // If the object is a group
                    case "group":
                        ((GroupAddResult)(object)objectInstance).title_Property  = line;
                        ((GroupAddResult)(object)objectInstance).status_Property = AddSingleObject<GroupAddResult>(((GroupAddResult)(object)objectInstance), objectType);
                        break;

                    // If the object is a user
                    case "user":
                        ((UserAddResult)(object)objectInstance).title_Property                     = line.Split(',')[0];
                        ((UserAddResult)(object)objectInstance).firstName_Property                 = line.Split(',')[1];
                        ((UserAddResult)(object)objectInstance).lastName_Property                  = line.Split(',')[2];
                        ((UserAddResult)(object)objectInstance).password                           = line.Split(',')[3];
                        ((UserAddResult)(object)objectInstance).changePasswordAtNextLogon_Property = bool.Parse(line.Split(',')[4]);
                        ((UserAddResult)(object)objectInstance).cannotChangePassword_Property      = bool.Parse(line.Split(',')[5]);
                        ((UserAddResult)(object)objectInstance).passwordNeverExpires_Property      = bool.Parse(line.Split(',')[6]);
                        ((UserAddResult)(object)objectInstance).accountIsDisabled_Property         = bool.Parse(line.Split(',')[7]);
                        ((UserAddResult)(object)objectInstance).status_Property                    = AddSingleObject<UserAddResult>(((UserAddResult)(object)objectInstance), objectType);
                        break;
                }

                // Add the object to the results
                results.Add(objectInstance);
            }

            // Return the results
            return results;
        }
    }
}
