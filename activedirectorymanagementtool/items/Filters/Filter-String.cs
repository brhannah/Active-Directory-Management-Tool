using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryManagementTool
{
    [Serializable]
    public class StringFilter : Filter
    {
        // Class variables
        private string value;

        // value Property (getter/setter)
        public string value_Property
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

                // Equal to
                case 1:
                    base.filter                 = base.attribute + "=" + this.value.Replace("(", @"\28").Replace(")", @"\29").Replace("&", @"\26").Replace("|", @"\7c").Replace("=", @"\3d").Replace(">", @"\3e").Replace("<", @"\3c").Replace("~", @"\7e").Replace("*", @"\2a").Replace("/", @"\2f").Replace(@"\", @"\5c");
                    base.displayFilter_Property = "= " + this.value;
                    break;
                    
                // Not Equal to
                case 2:
                    base.filter                 = "!" + base.attribute + "=" + this.value.Replace("(", @"\28").Replace(")", @"\29").Replace("&", @"\26").Replace("|", @"\7c").Replace("=", @"\3d").Replace(">", @"\3e").Replace("<", @"\3c").Replace("~", @"\7e").Replace("*", @"\2a").Replace("/", @"\2f").Replace(@"\", @"\5c");
                    base.displayFilter_Property = "!= " + this.value;
                    break;
                    
                // Starts With
                case 3:
                    base.filter                 = base.attribute + "=" + this.value.Replace("(", @"\28").Replace(")", @"\29").Replace("&", @"\26").Replace("|", @"\7c").Replace("=", @"\3d").Replace(">", @"\3e").Replace("<", @"\3c").Replace("~", @"\7e").Replace("*", @"\2a").Replace("/", @"\2f").Replace(@"\", @"\5c") + "*";
                    base.displayFilter_Property = "Starts With: " + this.value;
                    break;
                    
                // Ends With
                case 4:
                    base.filter                 = base.attribute + "=*" + this.value.Replace("(", @"\28").Replace(")", @"\29").Replace("&", @"\26").Replace("|", @"\7c").Replace("=", @"\3d").Replace(">", @"\3e").Replace("<", @"\3c").Replace("~", @"\7e").Replace("*", @"\2a").Replace("/", @"\2f").Replace(@"\", @"\5c");
                    base.displayFilter_Property = "Ends With: " + this.value;
                    break;
                    
                // Contains
                case 5:
                    base.filter                 = base.attribute + "=*" + this.value.Replace("(", @"\28").Replace(")", @"\29").Replace("&", @"\26").Replace("|", @"\7c").Replace("=", @"\3d").Replace(">", @"\3e").Replace("<", @"\3c").Replace("~", @"\7e").Replace("*", @"\2a").Replace("/", @"\2f").Replace(@"\", @"\5c") + "*";
                    base.displayFilter_Property = "Contains: " + this.value;
                    break;
                    
                // Any Value
                case 6:
                    base.filter                 = base.attribute + "=*";
                    base.displayFilter_Property = "Any Value";
                    break;
                    
                // No Value
                case 7:
                    base.filter                 = "!" + base.attribute + "=*";
                    base.displayFilter_Property = "No Value";
                    break;
            }

            // If there is no value
            if (this.value == "" && base.selectedOperator != 6 && base.selectedOperator != 7)
            {
                base.filter                 = "";
                base.displayFilter_Property = "";
            }
        }

        // Constructor
        public StringFilter(string attribute, string filter = "")
        {
            base.attribute = attribute;
            base.filter    = filter;
            this.value     = string.Empty;

            // Set the operators
            base.operatorOptions.Add("Not an Active Filter");
            base.operatorOptions.Add("Equal to");
            base.operatorOptions.Add("Not Equal to");
            base.operatorOptions.Add("Starts With");
            base.operatorOptions.Add("Ends With");
            base.operatorOptions.Add("Contains");
            base.operatorOptions.Add("Any Value");
            base.operatorOptions.Add("No Value");
        }
    }
}
