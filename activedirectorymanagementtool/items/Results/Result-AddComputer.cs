using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryManagementTool
{
    public class ComputerAddResult
    {
        // Declare class variables
        private string title;
        private string status;

        // Constructor
        public ComputerAddResult()
        {
            // Initialize class variables
            this.title  = string.Empty;
            this.status = string.Empty;
        }

        // title Property (getter/setter)
        public string title_Property
        {
            get { return this.title; }
            set { this.title = value; }
        }

        // status Property (getter/setter)
        public string status_Property
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
