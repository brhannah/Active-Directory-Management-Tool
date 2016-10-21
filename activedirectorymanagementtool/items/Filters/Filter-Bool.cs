using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryManagementTool
{
    [Serializable]
    public class BooleanFilter : Filter
    {
        // Class variables
        private bool value;

        // value Property (getter/setter)
        public bool value_Property
        {
            get { return this.value; }
            set { this.value = value;  this.UpdateFilter(); }
        }

        // selectedOperator Property (getter/setter)
        public int selectedOperator_Property
        {
            get { return this.selectedOperator; }
            set { this.selectedOperator = value; this.UpdateFilter(); }
        }

        private void UpdateFilter()
        {
            switch (base.selectedOperator)
            {
                // Not an Active Filter
                case 0:
                    base.filter                 = "";
                    base.displayFilter_Property = "";
                    break;

                // Equal
                case 1:
                    base.filter                 = base.attribute + "=" + this.value.ToString();
                    base.displayFilter_Property = "= " + this.value.ToString();
                    break;

                // Any Value
                case 2:
                    base.filter                 = base.attribute + "=*";
                    base.displayFilter_Property = "Any Value";
                    break;

                // No Value
                case 3:
                    base.filter                 = "!" + base.attribute + "=*";
                    base.displayFilter_Property = "No Value";
                    break;
            }
        }

        // Constructor
        public BooleanFilter(string attribute, string filter = "")
        {
            base.attribute = attribute;
            base.filter    = filter;
            this.value     = true;

            // Add boolean operations
            base.operatorOptions.Add("Not an Active Filter");
            base.operatorOptions.Add("Equal");
            base.operatorOptions.Add("Any Value");
            base.operatorOptions.Add("No Value");
        }
    }
}
