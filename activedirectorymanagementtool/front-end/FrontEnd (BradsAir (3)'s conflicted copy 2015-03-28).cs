using System.Windows;                     // UI object properties
using System.Windows.Controls;            // UI objects
using System.Windows.Controls.Primitives; // UI objects
using System.Windows.Data;                // Binding
using System.Collections.Generic;         // Lists

namespace ActiveDirectoryManagementTool
{
    public partial class FrontEnd : DockPanel
    {
        public TreeView treeView;
        public DataGrid mainDataGrid;

        public FrontEnd()
        {
            // Create status bar and the item to be placed within the status bar
            StatusBar     statusBar     = new StatusBar();
            StatusBarItem statusBarItem = new StatusBarItem();

            // Set the initial properties of the status bar
            statusBarItem.Content = "Player 1's Turn!";

            // Add the status bar item to the status bar
            statusBar.Items.Add(statusBarItem);

            // Dock status bar at bottom of panel
            DockPanel.SetDock(statusBar, Dock.Bottom);
            this.Children.Add(statusBar);

            // Create a grid
            Grid grid = new Grid();

            // Create columns for the grid
            ColumnDefinition treeColumnDefinition     = new ColumnDefinition();
            ColumnDefinition splitterColumnDefinition = new ColumnDefinition();
            ColumnDefinition listColumnDefinition     = new ColumnDefinition();

            // Set the properties for the tree's column
            treeColumnDefinition.Width = new GridLength(250, GridUnitType.Pixel);

            // Set the properties for the splitter's column
            splitterColumnDefinition.Width = GridLength.Auto;

            // Add the three columns to the grid (tree, splitter, list)
            grid.ColumnDefinitions.Add(treeColumnDefinition);
            grid.ColumnDefinitions.Add(splitterColumnDefinition);
            grid.ColumnDefinitions.Add(listColumnDefinition);

            // Create the row for the grid
            RowDefinition gridRow = new RowDefinition();

            // Add the row to the grid
            grid.RowDefinitions.Add(gridRow);

            // Add the grid to the bottom of the dock panel
            DockPanel.SetDock(grid, Dock.Bottom);
            this.Children.Add(grid);

            // Create a tree structure to display AD objects
            treeView = BuildTree();

            // Add the tree to the first cell (left side) of the grid
            grid.Children.Add(treeView);
            Grid.SetRow(      treeView, 0);
            Grid.SetColumn(   treeView, 0);

            // Create a splitter to separate the tree from the list (allows resizing of other grid cells)
            GridSplitter gridSplitter = new GridSplitter();

            // Set the properties for the splitter
            gridSplitter.HorizontalAlignment = HorizontalAlignment.Center; // Place the splitter in the center of the cell
            gridSplitter.VerticalAlignment   = VerticalAlignment.Stretch;  // Make the splitter vertically take up the entire cell
            gridSplitter.Width               = 5;                          // Width of the split bar
            gridSplitter.ShowsPreview        = true;                       // Show users where the splitter will be place while sliding it

            // Add the splitter to the second column (middle) of the grid
            grid.Children.Add(gridSplitter);
            Grid.SetRow(      gridSplitter, 0);
            Grid.SetColumn(   gridSplitter, 1);

            // Create the grid for the data
            mainDataGrid = new DataGrid();
            
            Binding    mainDataGridItemsBinding = new Binding();

            ((TreeViewItem)treeView.Items[0]).IsSelected = true;
            ((TreeViewItem)treeView.Items[0]).BringIntoView();
            ((TreeViewItem)treeView.Items[0]).Focus();

            mainDataGridItemsBinding.Source              = treeView;
            mainDataGridItemsBinding.Mode                = BindingMode.OneWay;
            mainDataGridItemsBinding.Path                = new PropertyPath("SelectedItem.Tag.childNode_Property");
            mainDataGridItemsBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            mainDataGrid.SetBinding(DataGrid.ItemsSourceProperty, mainDataGridItemsBinding);

            mainDataGrid.IsReadOnly          = true;
            mainDataGrid.SelectionUnit       = DataGridSelectionUnit.FullRow;
            mainDataGrid.SelectionMode       = DataGridSelectionMode.Extended;
            mainDataGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            mainDataGrid.VerticalAlignment   = VerticalAlignment.Stretch;

            // Add the data grid to the grid
            Grid.SetRow(mainDataGrid, 0);
            Grid.SetColumn(mainDataGrid, 2);

            grid.Children.Add(mainDataGrid);
        }
    }
}
