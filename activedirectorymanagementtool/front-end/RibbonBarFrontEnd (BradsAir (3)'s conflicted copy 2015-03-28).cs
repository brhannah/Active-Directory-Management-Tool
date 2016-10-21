using System.Windows;                    // WindowState
using System.Windows.Controls;           // DockPanel
using Microsoft.Windows.Controls.Ribbon; // RibbonWindow
using System.Windows.Data;               // Binding

namespace ActiveDirectoryManagementTool
{
    public partial class RibbonBarFrontEnd : RibbonWindow
    {
        public RibbonBarFrontEnd()
        {
            // Create a DockPanel to place UI objects
            DockPanel dock = new DockPanel();

            // Set the RibbonWindow's properties
            this.Title       = "Active Directory Management Tool";
            this.WindowState = WindowState.Maximized;
            this.Content     = dock;
            this.Show();

            // Create the RibbonBar
            Create_RibbonBar();

            // Add the RibbonBar to the top of the DockPanel
            DockPanel.SetDock(bar, Dock.Top);
            dock.Children.Add(bar);

            // Create the main body for the UI
            FrontEnd mainBody = new FrontEnd();
            mainBody.treeView.SelectedItemChanged += treeView_SelectedItemChanged;
            mainBody.mainDataGrid.SelectedCellsChanged += mainDataGrid_SelectedCellsChanged;

            // Add the main body for the UI to the bottom of the DockPanel
            DockPanel.SetDock(mainBody, Dock.Bottom);
            dock.Children.Add(mainBody);
        }

        void mainDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid mainDataGrid = sender as DataGrid;
            string names = "";
            foreach (Tree x in mainDataGrid.SelectedItems)
            {
                names += x.commonName_Property + ", ";
            }
            MessageBox.Show(names);
        }

        void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView treeView = sender as TreeView;
            string names = "";
            names = treeView.SelectedItem.ToString();
            MessageBox.Show(names);
        }
    }
}
