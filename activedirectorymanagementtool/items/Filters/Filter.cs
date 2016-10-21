using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ActiveDirectoryManagementTool
{
    [Serializable]
    public class Filter : INotifyPropertyChanged
    {
        // Declare class variables
        protected string       attribute;
        protected string       filter;
        protected string       displayFilter;
        protected List<string> operatorOptions;
        public    int          selectedOperator;

        [field: NonSerialized]
        public    event PropertyChangedEventHandler PropertyChanged;

        // attribute Property (getter/setter)
        public string attribute_Property
        {
            get { return this.attribute; }
            set { this.attribute = value; }
        }

        // filter Property (getter/setter)
        public string filter_Property
        {
            get { return this.filter; }
            set { this.filter = value; }
        }

        // operatorOptions Property (getter/setter)
        public List<string> operatorOptions_Property
        {
            get { return this.operatorOptions; }
            set { this.operatorOptions = value; }
        }

        // displayFilter Property (getter/setter)

        public string displayFilter_Property
        {
            get { return this.displayFilter; }
            set { this.displayFilter = value; OnPropertyChanged("displayFilter_Property"); }
        }

        // Constructor
        public Filter()
        {
            this.attribute        = string.Empty;
            this.filter           = string.Empty;
            this.operatorOptions  = new List<string>();
            this.selectedOperator = 0;
        }

        // https://msdn.microsoft.com/en-us/library/ms743695%28v=vs.110%29.aspx
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
