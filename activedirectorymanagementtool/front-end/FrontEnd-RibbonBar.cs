using System.Windows;                  // WindowState
using System.Windows.Controls;         // DockPanel, TreeView
using System.Windows.Controls.Ribbon;  // RibbonWindow
using System.Windows.Data;             // Binding

namespace ActiveDirectoryManagementTool
{
    public partial class RibbonBarFrontEnd : RibbonWindow
    {
        // Declare class variables
        public  FrontEnd  mainBody;
        private DockPanel dock;

        // Constructor
        public RibbonBarFrontEnd()
        {
            // Create a DockPanel to place UI objects
            dock = new DockPanel();

            // Set the RibbonWindow's properties
            this.Title       = "Active Directory Management Tool";
            this.WindowState = WindowState.Maximized;
            this.Content     = dock;
            this.Show();
            this.Icon        = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            // Create the RibbonBar
            Create_RibbonBar();

            // Add the RibbonBar to the top of the DockPanel
            DockPanel.SetDock(bar, Dock.Top);
            dock.Children.Add(bar);

            // Create the main body for the UI
            mainBody = new FrontEnd();

            // Assign event handlers to UI items in the FrontEnd instance
            mainBody.treeView.SelectedItemChanged  += treeView_SelectedItemChanged;
            mainBody.mainDataGrid.SelectionChanged += mainDataGrid_SelectedCellsChanged;

            // Add the main body for the UI to the bottom of the DockPanel
            DockPanel.SetDock(mainBody, Dock.Bottom);
            dock.Children.Add(mainBody);
        }

        void mainDataGrid_SelectedCellsChanged(object sender, Xceed.Wpf.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            Xceed.Wpf.DataGrid.DataGridControl mainDataGrid = sender as Xceed.Wpf.DataGrid.DataGridControl;
            BackEnd.selectedADObjs_Property.Clear();
            foreach (Tree x in mainDataGrid.SelectedItems)
            {
                BackEnd.selectedADObjs_Property.Add(x);
            }
        }

        void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                // Cast the sending object as a TreeView item
                TreeView treeView = sender as TreeView;

                // Save the selected item to the BackEnd
                mainBody.statusBarContent.Text = BackEnd.LDAPPath_Property = "LDAP://" + BackEnd.DomainName_Property + "/" + ((Tree)((TreeViewItem)(treeView.SelectedItem)).Tag).distinguishedName;
            }
            catch { }
        }
    }
}
