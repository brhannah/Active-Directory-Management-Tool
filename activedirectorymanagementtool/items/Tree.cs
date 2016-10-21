using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;

namespace ActiveDirectoryManagementTool
{
    public class Tree
    {
        public enum objectType {
            user,
            contact,
            printer,
            computer,
            sharedFolder,
            group,
            organizationalUnit,
            domain,
            domainController,
            siteObject,
            builtin,
            foreignSecurityPrincipal
        };

        public  string                     distinguishedName;
        private string                     commonName;
        public  string                     domain;
        public  string                     schemaClass;
        public  string                     title;
        public  string                     accountEnabled;
        public  ObservableCollection<Tree> childNode;

        public string commonName_Property
        {
            get { return this.commonName; }
            set
            {
                if (value.StartsWith("CN="))
                {
                    this.commonName = value.Remove(0, value.IndexOf("CN=") + "CN=".Length).Split(',')[0];
                }
                else if (value.StartsWith("OU="))
                {
                    this.commonName = value.Remove(0, value.IndexOf("OU=") + "OU=".Length).Split(',')[0];
                }
                else
                {
                    this.commonName = value.Remove(0, "DC=".Length).Replace(",DC=", ".");
                }
            }
        }

        public string accountEnabled_Property
        {
            get { return this.accountEnabled; }
            set { this.accountEnabled = value; }
        }

        public string schemaClass_Property
        {
            get { return this.schemaClass; }
            set { this.schemaClass = value; }
        }

        public string title_Property
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string distinguishedName_Property
        {
            get { return this.distinguishedName; }
            set { this.distinguishedName = value; }
        }

        public ObservableCollection<Tree> childNode_Property
        {
            get { return this.childNode; }
            set { this.childNode = value; }
        }

        public Tree(string distinguishedName, string domain, string schemaClass, string title, string accountEnabled)
        {
            this.distinguishedName   = distinguishedName;
            this.commonName_Property = distinguishedName;
            this.domain              = domain;
            this.schemaClass         = schemaClass;
            this.childNode           = new ObservableCollection<Tree>();
            this.title               = title;
            this.accountEnabled      = accountEnabled;
        }
    }
}