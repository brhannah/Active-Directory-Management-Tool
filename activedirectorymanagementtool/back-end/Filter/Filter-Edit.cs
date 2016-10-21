using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace ActiveDirectoryManagementTool
{
    public partial class FilterManagement
    {
        #region Attributes
        public static readonly Dictionary<string, string> attributes = new Dictionary<string, string>()
        {
            { "accountExpires",                 "datetime" },
            { "accountNameHistory",             "string" },
            { "aCSPolicyName",                  "string" },
            { "adminCount",                     "integer" },
            { "adminDescription",               "string" },
            { "adminDisplayName",               "string" },
            { "altSecurityIdentities",          "string" },
            { "badPasswordTime",                "datetime" },
            { "badPwdCount",                    "integer" },
            { "c",                              "string" },
            { "canonicalName",                  "string" },
            { "cn",                             "string" },
            { "co",                             "string" },
            { "codePage",                       "integer" },
            { "comment",                        "string" },
            { "company",                        "string" },
            { "countryCode",                    "integer" },
            { "department",                     "string" },
            { "description",                    "string" },
            { "desktopProfile",                 "string" },
            { "displayName",                    "string" },
            { "division",                       "string" },
            { "employeeID",                     "string" },
            { "extensionName",                  "string" },
            { "facsimileTelephoneNumber",       "string" },
            { "flags",                          "integer" },
            { "fromEntry",                      "boolean" },
            { "garbageCollPeriod",              "integer" },
            { "generationQualifier",            "string" },
            { "givenName",                      "string" },
            { "groupPriority",                  "string" },
            { "groupsToIgnore",                 "string" },
            { "homeDirectory",                  "string" },
            { "homeDrive",                      "string" },
            { "homePhone",                      "string" },
            { "homePostalAddress",              "string" },
            { "info",                           "string" },
            { "initials",                       "string" },
            { "instanceType",                   "integer" },
            { "ipPhone",                        "string" },
            { "isCriticalSystemObject",         "boolean" },
            { "isDeleted",                      "boolean" },
            { "l",                              "string" },
            { "lastLogoff",                     "datetime" },
            { "lastLogon",                      "datetime" },
            { "localeID",                       "integer" },
            { "lockoutTime",                    "datetime" },
            { "logonCount",                     "integer" },
            { "mail",                           "string" },
            { "manager",                        "string" },
            { "maxStorage",                     "datetime" },
            { "mhsORAddress",                   "string" },
            { "middleName",                     "string" },
            { "mobile",                         "string" },
            { "mS-DS-ConsistencyChildCount",    "integer" },
            { "msNPAllowDialin",                "boolean" },
            { "msRADIUSFramedIPAddress",        "integer" },
            { "msRADIUSServiceType",            "integer" },
            { "msRASSavedFramedIPAddress",      "integer" },
            { "name",                           "string" },
            { "o",                              "string" },
            { "objectCategory",                 "string" },
            { "objectClass",                    "string" },
            { "objectVersion",                  "integer" },
            { "operatorCount",                  "integer" },
            { "otherFacsimileTelephoneNumber",  "string" },
            { "otherHomePhone",                 "string" },
            { "otherIpPhone",                   "string" },
            { "otherLoginWorkstations",         "string" },
            { "otherMailbox",                   "string" },
            { "otherMobile",                    "string" },
            { "otherPager",                     "string" },
            { "otherTelephone",                 "string" },
            { "ou",                             "string" },
            { "pager",                          "string" },
            { "personalTitle",                  "string" },
            { "physicalDeliveryOfficeName",     "string" },
            { "postalAddress",                  "string" },
            { "postalCode",                     "string" },
            { "postOfficeBox",                  "string" },
            { "preferredDeliveryMethod",        "integer" },
            { "primaryGroupID",                 "integer" },
            { "primaryInternationalISDNNumber", "string" },
            { "primaryTelexNumber",             "string" },
            { "profilePath",                    "string" },
            { "proxyAddresses",                 "string" },
            { "pwdLastSet",                     "datetime" },
            { "revision",                       "integer" },
            { "rid",                            "integer" },
            { "sAMAccountName",                 "string" },
            { "sAMAccountType",                 "integer" },
            { "scriptPath",                     "string" },
            { "sDRightsEffective",              "integer" },
            { "servicePrincipalName",           "string" },
            { "showInAdvancedViewOnly",         "boolean" },
            { "sn",                             "string" },
            { "st",                             "string" },
            { "street",                         "string" },
            { "streetAddress",                  "string" },
            { "systemFlags",                    "integer" },
            { "telephoneNumber",                "string" },
            { "textEncodedORAddress",           "string" },
            { "title",                          "string" },
            { "url",                            "string" },
            { "userAccountControl",             "integer" },
            { "userParameters",                 "string" },
            { "userPrincipalName",              "string" },
            { "userSharedFolder",               "string" },
            { "userSharedFolderOther",          "string" },
            { "userWorkstations",               "string" },
            { "uSNChanged",                     "datetime" },
            { "uSNCreated",                     "datetime" },
            { "uSNDSALastObjRemoved",           "datetime" },
            { "USNIntersite",                   "integer" },
            { "uSNLastObjRem",                  "datetime" },
            { "uSNSource",                      "datetime" },
            { "wbemPath",                       "string" },
            { "whenCreated",                    "datetime" },
            { "wWWHomePage",                    "string" }
        };
        #endregion

        public void AddAttributeToFilter(string filterName, string attribute, string type)
        {
            // Depending on the type of filter attribute
            switch (type)
            {
                case "string":
                    filters[filterName].Add(new List<object>());
                    for (int i = 0; i < filters[filterName][0].Count; i++)
                    {
                        filters[filterName].Last().Add(new StringFilter(attribute));
                    }
                    if (filters[filterName][0].Count == 0)
                    {
                        filters[filterName].Last().Add(new StringFilter(attribute));
                    }
                    break;

                case "integer":
                    filters[filterName].Add(new List<object>());
                    for (int i = 0; i < filters[filterName][0].Count; i++)
                    {
                        filters[filterName].Last().Add(new IntegerFilter(attribute));
                    }
                    if (filters[filterName][0].Count == 0)
                    {
                        filters[filterName].Last().Add(new IntegerFilter(attribute));
                    }
                    break;

                case "datetime":
                    filters[filterName].Add(new List<object>());
                    for (int i = 0; i < filters[filterName][0].Count; i++)
                    {
                        filters[filterName].Last().Add(new DateTimeFilter(attribute));
                    }
                    if (filters[filterName][0].Count == 0)
                    {
                        filters[filterName].Last().Add(new DateTimeFilter(attribute));
                    }
                    break;

                case "boolean":
                    filters[filterName].Add(new List<object>());
                    for (int i = 0; i < filters[filterName][0].Count; i++)
                    {
                        filters[filterName].Last().Add(new BooleanFilter(attribute));
                    }
                    if (filters[filterName][0].Count == 0)
                    {
                        filters[filterName].Last().Add(new BooleanFilter(attribute));
                    }
                    break;
            }
        }

        public void AddOrStatementToFilter(List<List<object>> filters)
        {
            // For each column
            for (int i = 0; i < filters.Count; i++)
            {
                // Depending on the type of filter attribute
                switch (filters[i][0].GetType().ToString())
                {
                    case "ActiveDirectoryManagementTool.StringFilter":
                        filters[i].Add(new StringFilter(((StringFilter)filters[i][0]).attribute_Property));
                        break;

                    case "ActiveDirectoryManagementTool.IntegerFilter":
                        filters[i].Add(new IntegerFilter(((IntegerFilter)filters[i][0]).attribute_Property));
                        break;

                    case "ActiveDirectoryManagementTool.DateTimeFilter":
                        filters[i].Add(new DateTimeFilter(((DateTimeFilter)filters[i][0]).attribute_Property));
                        break;

                    case "ActiveDirectoryManagementTool.BooleanFilter":
                        filters[i].Add(new BooleanFilter(((BooleanFilter)filters[i][0]).attribute_Property));
                        break;
                }
            }
        }

        public void Save(string filterName)
        {
            // Declare local variables
            List<int> columnsToDeleteList = new List<int>();
            List<int> rowsToDeleteList    = new List<int>();

            // Create a flag to determine if the column is used
            bool columnUsed;

            // For each column
            for (int i = 0; i < this.filters[filterName].Count; i++)
            {
                // Initialize the flag
                columnUsed = false;

                // For each row in the column
                for (int ii = 0; ii < this.filters[filterName][i].Count && !columnUsed; ii++)
                {
                    // If the row within the column is active
                    if (((Filter)this.filters[filterName][i][ii]).selectedOperator != 0)
                    {
                        // Flag the entire column as in use
                        columnUsed = true;
                    }
                }

                // If the column was not in use
                if (!columnUsed)
                {
                    // Flag it for deletion
                    columnsToDeleteList.Add(i);
                }
            }

            // Create a flag to determine if the column is used
            bool rowUsed;

            // For each row
            for (int i = 0; i < this.filters[filterName][0].Count; i++)
            {
                // Initialize the flag
                rowUsed = false;

                // For each column in the row
                for (int ii = 0; ii < this.filters[filterName].Count && !rowUsed; ii++)
                {
                    // If the column within the row is active
                    if (((Filter)this.filters[filterName][ii][i]).selectedOperator != 0)
                    {
                        // Flag the entire column as in use
                        rowUsed = true;
                    }
                }

                // If the row was not in use
                if (!rowUsed)
                {
                    // Flag it for deletion
                    rowsToDeleteList.Add(i);
                }
            }

            // For each column that needs to be deleted
            for (int i = columnsToDeleteList.Count - 1; i >= 0; i--)
            {
                // Delete the column
                this.filters[filterName].RemoveAt(columnsToDeleteList[i]);
            }

            // For each row that needs to be deleted
            for (int i = rowsToDeleteList.Count - 1; i >= 0; i--)
            {
                // For each column
                for (int ii = 0; ii < this.filters[filterName].Count; ii++)
                {
                    // Delete the row
                    this.filters[filterName][ii].RemoveAt(rowsToDeleteList[i]);
                }
            }
        }
    }
}
