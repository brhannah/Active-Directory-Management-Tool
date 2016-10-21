using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryManagementTool
{
    public class GenericDeleteResult
    {
        // Declare class variables
        private string commonName;
        private string distinguishedName;
        private string schemaClass;
        private string status;

        // Constructor
        public GenericDeleteResult()
        {
            // Initialize class variables
            this.commonName        = string.Empty;
            this.distinguishedName = string.Empty;
            this.schemaClass       = string.Empty;
            this.status            = string.Empty;
        }

        // commonName Property (getter/setter)
        public string commonName_Property
        {
            get { return this.commonName; }
            set { this.commonName = value; }
        }

        // distinguishedName Property (getter/setter)
        public string distinguishedName_Property
        {
            get { return this.distinguishedName; }
            set { this.distinguishedName = value; }
        }

        // schemaClass Property (getter/setter)
        public string schemaClass_Property
        {
            get { return this.schemaClass; }
            set { this.schemaClass = value; }
        }

        // status Property (getter/setter)
        public string status_Property
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
