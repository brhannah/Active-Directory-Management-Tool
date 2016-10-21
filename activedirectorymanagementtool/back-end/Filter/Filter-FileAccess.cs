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
        public void SaveFilters()
        {
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binarySerialization = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.FileStream fs = new System.IO.FileStream("Filters.dat", System.IO.FileMode.Create);
                binarySerialization.Serialize(fs, this.filters);
                fs.Close();
            }
            catch { }
        }

        public static void LoadFilters(ref FilterManagement filterManagementInstance)
        {
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binarySerialization = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.FileStream fs = new System.IO.FileStream("Filters.dat", System.IO.FileMode.Open);
                fs.Position = 0;
                filterManagementInstance.filters = (Dictionary<string, List<List<object>>>)binarySerialization.Deserialize(fs);
                fs.Close();

                foreach (KeyValuePair<string, List<List<object>>> x in filterManagementInstance.filters)
                {
                    filterManagementInstance.filterNames.Add(x.Key);
                }
            }
            catch { }
        }
    }
}
