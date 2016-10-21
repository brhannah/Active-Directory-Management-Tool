using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace ActiveDirectoryManagementTool
{
    class DictionaryManagement
    {
        // Declare class variables
        public Dictionary<string, ObservableCollection<string>> terms;
        public List<string>                                     groups;

        // Constructor
        public DictionaryManagement()
        {
            // Create an instance of the dictionary terms and the list of groups
            this.terms  = new Dictionary<string, ObservableCollection<string>>();
            this.groups = new List<string>();

            // Bind to the specified container object
            DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + BackEnd.DomainName_Property, BackEnd.User_Property, BackEnd.Pass_Property, AuthenticationTypes.Secure);

            // Create a searcher to find groups
            DirectorySearcher dirSearcher = new DirectorySearcher(dirEntry);

            // Apply a filter that will only return groups
            dirSearcher.Filter = "(&(objectCategory=group))";

            // For each group found
            foreach (SearchResult results in dirSearcher.FindAll())
            {
                // Add the entry to the list of groups
                groups.Add(results.GetDirectoryEntry().Name.Replace("CN=", ""));
            }

            // Sort the list of groups
            groups.Sort();

            // Check if a dictionary already exists
            if (System.IO.File.Exists("Dictionary.txt"))
            {
                // For each line in the file
                foreach (string line in System.IO.File.ReadAllLines("Dictionary.txt"))
                {
                    // Create a list of defined groups
                    ObservableCollection<string> definition = new ObservableCollection<string>();

                    // For each group listed
                    foreach (string group in line.Split(':')[1].Split(','))
                    {
                        // If the group is not empty
                        if (group != "")
                        {
                            // Add it to the definition
                            definition.Add(group);
                        }
                    }

                    // Add the term and its definition to the list of terms
                    this.terms.Add(line.Split(':')[0], definition);
                }
            }
        }

        public string AddWithValidation(string term)
        {
            // If the term is not an empty string
            if (term.Trim() != "")
            {
                // If the term does not yet exist in the dictionary
                if (!this.terms.Any(x => x.Key == term))
                {
                    // Add the term to the dictionary
                    this.terms.Add(term, new ObservableCollection<string>());

                    // Return an empty string to show success
                    return "";
                }
                // If the term is already in the dictionary
                else
                {
                    // Return an error message
                    return "The job title already exists";
                }
            }
            // If the term is an empty string
            else
            {
                // Return an error message
                return "The job title cannot be blank";
            }
        }

        public void AddDefinitionToTerm(string term, string definition)
        {
            // If the definition is not already listed
            if (!terms[term].Any(x => x == definition))
            {
                // Add the definition to the term
                terms[term].Add(definition);
            }
        }

        public void RemoveDefinitionFromTerm(string term, string definition)
        {
            // Remove the definition from the term
            terms[term].Remove(definition);
        }

        public void RemoveTerm(string term)
        {
            // Remove the term from the dictionary
            terms.Remove(term);
        }

        public void SaveDictionary()
        {
            using (System.IO.StreamWriter save = new System.IO.StreamWriter("Dictionary.txt"))
            {
                foreach (KeyValuePair<string, ObservableCollection<string>> x in this.terms)
                {
                    // Save the term
                    save.Write(x.Key + ":");
                    
                    // For each definition
                    foreach (string y in x.Value)
                    {
                        // Save the definition
                        save.Write(y);

                        // If this is not the last value
                        if (y != x.Value.Last())
                        {
                            save.Write(",");
                        }
                    }

                    // Add a new line
                    save.WriteLine();
                }
            }
        }
    }
}
