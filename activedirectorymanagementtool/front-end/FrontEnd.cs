using System.Windows;                     // UI object properties
using System.Windows.Controls;            // UI objects
using System.Windows.Controls.Primitives; // UI objects
using System.Windows.Data;                // Binding
using System.Collections.Generic;         // Lists

namespace ActiveDirectoryManagementTool
{
    public partial class FrontEnd : DockPanel
    {
        public  TreeView                            treeView;
        public  Xceed.Wpf.DataGrid.DataGridControl  mainDataGrid;
        public  TextBlock                           statusBarContent;
        private Binding                             mainDataGridItemsBinding;

        public FrontEnd()
        {
            // Create status bar and the item to be placed within the status bar
            StatusBar     statusBar        = new StatusBar();
            StatusBarItem statusBarItem    = new StatusBarItem();
                          statusBarContent = new TextBlock();

            // Set the properties of the status bar
            statusBarItem.Content = statusBarContent;
            statusBarItem.Margin  = new Thickness(5, 0, 0, 2);

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
            this.treeView = BuildTree();

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

            // Position the splitter on the second column (middle) of the grid
            Grid.SetRow(      gridSplitter, 0);
            Grid.SetColumn(   gridSplitter, 1);

            // Add the splitter to the second column (middle) of the grid
            grid.Children.Add(gridSplitter);

            // Create the grid for the data
            mainDataGrid = new Xceed.Wpf.DataGrid.DataGridControl();

            // Create the binding that updates the grid's data as the selected tree object is changed
            mainDataGridItemsBinding = new Binding();

            // Select the root of the tree
            ((TreeViewItem)treeView.Items[0]).IsSelected      = true;
            ((TreeViewItem)treeView.Items[0]).BringIntoView();
            ((TreeViewItem)treeView.Items[0]).Focus();

            // Configure the binding to update the grid's data
            mainDataGridItemsBinding.Source              = treeView;
            mainDataGridItemsBinding.Mode                = BindingMode.OneWay;
            mainDataGridItemsBinding.Path                = new PropertyPath("SelectedItem.Tag.childNode_Property");
            mainDataGridItemsBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            mainDataGrid.SetBinding(Xceed.Wpf.DataGrid.DataGridControl.ItemsSourceProperty, mainDataGridItemsBinding);

            // Configure the data grid
            mainDataGrid.ReadOnly            = true;
            mainDataGrid.SelectionUnit       = Xceed.Wpf.DataGrid.SelectionUnit.Row;
            mainDataGrid.SelectionMode       = SelectionMode.Extended;
            mainDataGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            mainDataGrid.VerticalAlignment   = VerticalAlignment.Stretch;
            mainDataGrid.View                = new Xceed.Wpf.DataGrid.Views.TableView() { ColumnStretchMode = Xceed.Wpf.DataGrid.Views.ColumnStretchMode.Last };
            //mainDataGrid.RowHeaderWidth      = 20;
            //mainDataGrid.CanUserResizeRows   = false;
            //mainDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.Horizontal;

            // Position the data grid on the grid
            Grid.SetRow(   mainDataGrid, 0);
            Grid.SetColumn(mainDataGrid, 2);

            // Add the data gird to the grid
            grid.Children.Add(mainDataGrid);
        }
    }
}
