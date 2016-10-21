using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace ActiveDirectoryManagementTool
{
    [Serializable]
    public partial class FilterManagement
    {
        // Declare class variables
        private ObservableCollection<string>           filterNames;
        private Dictionary<string, List<List<object>>> filters;

        // filterNames Property (getter/setter)
        public ObservableCollection<string> filterNames_Property
        {
            get { return this.filterNames; }
            set { this.filterNames = value; }
        }

        // filters Property (getter/setter)
        public Dictionary<string, List<List<object>>> filters_Property
        {
            get { return this.filters; }
            set { this.filters = value; }
        }

        public FilterManagement()
        {
            // Start from scratch
            filterNames = new ObservableCollection<string>();
            filters     = new Dictionary<string, List<List<object>>>();
        }

        public void RemoveFilter(string filterName)
        {
            // Remove the filter
            filters.Remove(filterName);
            filterNames.Remove(filterName);
        }

        public string AddWithValidation(string filterName)
        {
            // Try to add the filter
            try
            {
                // Attempt to add filter
                filters.Add(filterName, new List<List<object>>());
                filterNames.Add(filterName);

                // Return success message
                return "Success";
            }
            // If the filter could not be added
            catch
            {
                // Return error message
                return "A filter with that name already exists";
            }
        }

        public void ApplyFilter(string filterName)
        {
            // If there are any filters
            if (this.filters[filterName].Count > 0)
            {
                // Store the filter
                string appliedFilter = "(|";
            
                // For each OR statement (row separated)
                for (int i = 0; i < this.filters[filterName][0].Count; i++)
                {
                    // Add the AND statements
                    appliedFilter += "(&";
            
                    // For each AND statement (column separated)
                    for (int ii = 0; ii < this.filters[filterName].Count; ii++)
                    {
                        switch (filters[filterName][ii][i].GetType().ToString())
                        {
                            case "ActiveDirectoryManagementTool.StringFilter":
                                if (((StringFilter)this.filters[filterName][ii][i]).selectedOperator != 0)
                                {
                                    appliedFilter += "(" + ((StringFilter)this.filters[filterName][ii][i]).filter_Property + ")";
                                }
                                break;
            
                            case "ActiveDirectoryManagementTool.IntegerFilter":
                                if (((IntegerFilter)this.filters[filterName][ii][i]).selectedOperator != 0)
                                {
                                    appliedFilter += "(" + ((IntegerFilter)this.filters[filterName][ii][i]).filter_Property + ")";
                                }
                                break;
            
                            case "ActiveDirectoryManagementTool.DateTimeFilter":
                                if (((DateTimeFilter)this.filters[filterName][ii][i]).selectedOperator != 0)
                                {
                                    appliedFilter += "(" + ((DateTimeFilter)this.filters[filterName][ii][i]).filter_Property + ")";
                                }
                                break;
            
                            case "ActiveDirectoryManagementTool.BooleanFilter":
                                if (((BooleanFilter)this.filters[filterName][ii][i]).selectedOperator != 0)
                                {
                                    appliedFilter += "(" + ((BooleanFilter)this.filters[filterName][ii][i]).filter_Property + ")";
                                }
                                break;
                        }
                    }
            
                    // Close the AND statement
                    appliedFilter += ")";
                }
            
                // Close the OR statement
                appliedFilter += ")";
            
                // Apply the filter
                BackEnd.filter_Property = appliedFilter;
            }
        }
    }
}
