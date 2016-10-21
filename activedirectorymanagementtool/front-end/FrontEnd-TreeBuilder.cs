using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;

using System.Windows.Controls;
using System.Collections.ObjectModel;

using Xceed.Wpf.Toolkit;

namespace ActiveDirectoryManagementTool
{
    public partial class FrontEnd
    {
        List<string>       allowedSchemaClasses;
        List<TreeViewItem> treeViewItemList;

        public TreeView BuildTree(TreeViewItem treeViewItem = null, ObservableCollection<Tree> ADObjs = null, bool refresh = false)
        {
            if (ADObjs == null)
            {
                // Create a new list of tree view items
                treeViewItemList = new List<TreeViewItem>();

                // Initialize the list of allowed schema classes
                allowedSchemaClasses = new List<string> {
                    "domain",
                    "domainDNS",
                    "builtinDomain",
                    "container",
                    "organizationalUnit",
                    "lostAndFound",
                    "msDS-QuotaContainer" };

                // Get all of the AD objects
                ADObjs = BackEnd.getDistinguishedDomainName();
            }

            // Add items to root node
            foreach (Tree x in ADObjs)
            {
                if (x != null)
                {
                    if (allowedSchemaClasses.Contains(x.schemaClass))
                    {
                        if (treeViewItem == null)
                        {
                            if (!refresh)
                            {
                                treeView = new TreeView();
                            }
                            treeViewItem            = new TreeViewItem();
                            treeViewItem.Header     = x.commonName_Property;
                            treeViewItem.Tag        = x;
                            treeViewItem.IsExpanded = true;
                            treeViewItemList.Add(treeViewItem);
                    
                            treeView.Items.Insert(0, treeViewItem);
                    
                            BuildTree(treeViewItem, x.childNode, refresh);
                        }
                        else
                        {
                            if (!BackEnd.flatView_Property)
                            {
                                TreeViewItem tempTreeViewItem = new TreeViewItem();
                                tempTreeViewItem.Header       = x.commonName_Property;
                                tempTreeViewItem.Tag          = x;
                                treeViewItemList.Add(tempTreeViewItem);
                                treeViewItem.Items.Add(tempTreeViewItem);
                    
                                BuildTree(tempTreeViewItem, x.childNode, refresh);
                            }
                        }
                    }
                }
            }

            return treeView;
        }

        public TreeView RebuildTree()
        {
            foreach (TreeViewItem x in treeViewItemList)
            {
                treeView.Items.Remove(x);
            }

            string distinguishedName = BackEnd.LDAPPath_Property.Split('/').Last();

            if(distinguishedName == BackEnd.DomainName_Property)
            {
                distinguishedName = "DC=" + BackEnd.DomainName_Property.Replace(".", ",DC=");
            }

            TreeView refreshedTreeView = BuildTree(null, null, true);

            foreach (TreeViewItem x in treeViewItemList)
            {
                if (((Tree)x.Tag).distinguishedName == distinguishedName)
                {
                    x.IsSelected = true;
                    x.IsExpanded = true;
                    x.BringIntoView();
                    x.Focus();
                }
            }

            return refreshedTreeView;
        }
    }
}
