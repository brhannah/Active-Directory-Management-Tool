using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryManagementTool
{
    [Serializable]
    public class DateTimeFilter : Filter
    {
        // Class variables
        private DateTime value;

        // value Property (getter/setter)
        public DateTime value_Property
        {
            get { return this.value; }
            set { this.value = value; this.UpdateFilter(); }
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
                    base.filter                 = base.attribute + "=" + this.value.ToFileTimeUtc();
                    base.displayFilter_Property = "= " + this.value.ToString();
                    break;

                // Not Equal to
                case 2:
                    base.filter                 = "!" + base.attribute + "=" + this.value.ToFileTimeUtc();
                    base.displayFilter_Property = "!= " + this.value.ToString();
                    break;

                // Less Than or Equal to
                case 3:
                    base.filter                 = base.attribute + "<=" + this.value.ToFileTimeUtc();
                    base.displayFilter_Property = "<= " + this.value.ToString();
                    break;

                // Greater Than or Eqal to
                case 4:
                    base.filter                 = base.attribute + ">=" + this.value.ToFileTimeUtc();
                    base.displayFilter_Property = ">= " + this.value.ToString();
                    break;

                // Any Value
                case 5:
                    base.filter                 = base.attribute + "=*";
                    base.displayFilter_Property = "Any Value";
                    break;

                // No Value
                case 6:
                    base.filter                 = "!" + base.attribute + "=*";
                    base.displayFilter_Property = "No Value";
                    break;
            }
        }

        // Constructor
        public DateTimeFilter(string attribute, string filter = "")
        {
            base.attribute = attribute;
            base.filter    = filter;
            this.value     = DateTime.Today;

            // Set the options
            base.operatorOptions.Add("Not an Active Filter");
            base.operatorOptions.Add("Equal to");
            base.operatorOptions.Add("Not Equal to");
            base.operatorOptions.Add("Less Than or Equal to");
            base.operatorOptions.Add("Greater Than or Equal to");
            base.operatorOptions.Add("Any Value");
            base.operatorOptions.Add("No Value");
        }

        // http://stackoverflow.com/questions/18614810/how-to-convert-active-directory-pwdlastset-to-date-time
        public static Int64 ConvertADSLargeIntegerToInt64(object adsLargeInteger)
        {
            var highPart = (Int32)adsLargeInteger.GetType().InvokeMember("HighPart", System.Reflection.BindingFlags.GetProperty, null, adsLargeInteger, null);
            var lowPart  = (Int32)adsLargeInteger.GetType().InvokeMember("LowPart",  System.Reflection.BindingFlags.GetProperty, null, adsLargeInteger, null);
            return highPart * ((Int64)UInt32.MaxValue + 1) + lowPart;
        }
    }
}
