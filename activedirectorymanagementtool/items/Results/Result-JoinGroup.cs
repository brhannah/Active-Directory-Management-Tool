using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryManagementTool
{
    public class GroupJoinResult
    {
        // Declare class variables
        private string objectInstance;
        private string group;
        private string status;

        // Constructor
        public GroupJoinResult()
        {
            // Initialize class variables
            this.objectInstance = string.Empty;
            this.group          = string.Empty;
            this.status         = string.Empty;
        }

        // objectInstance Property (getter/setter)
        public string objectInstance_Property
        {
            get { return this.objectInstance; }
            set { this.objectInstance = value; }
        }

        // group Property (getter/setter)
        public string group_Property
        {
            get { return this.group; }
            set { this.group = value; }
        }

        // status Property (getter/setter)
        public string status_Property
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
