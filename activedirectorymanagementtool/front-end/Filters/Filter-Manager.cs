using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Data;

namespace ActiveDirectoryManagementTool
{
    class FilterManager : Window
    {
        // Declare class variables
        FilterManagement filters;
        ListView         filtersListView;

        // Constructor
        public FilterManager()
        {
            // Create a set of filters
            filters = new FilterManagement();

            // Try to load filters from the XML file
            try
            {
                // Attempt to load the filters
                FilterManagement.LoadFilters(ref filters);
            }
            catch { }

            // Create a panel to place all UI items
            DockPanel filterManagerDockPanel = new DockPanel();

            // Set window properties
            this.Title                 = "Filter Manager";
            this.Width                 = 600;
            this.MinWidth              = 600;
            this.Height                = 300;
            this.MinHeight             = 300;
            this.WindowState           = System.Windows.WindowState.Normal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Content               = filterManagerDockPanel;
            this.Background            = Brushes.WhiteSmoke;
            this.Topmost               = false;
            this.Activate();
            this.ShowActivated         = true;
            this.Icon                  = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            // Create a grid to store the new filter UI items
            Grid newFilterGrid = new Grid();
            
            // Set the properties for the grid
            newFilterGrid.Margin = new Thickness(5, 2, 5, 5);

            // Create the columns for the grid
            ColumnDefinition textColumn         = new ColumnDefinition();
            ColumnDefinition newFilterColumn    = new ColumnDefinition();
            ColumnDefinition addNewFilterColumn = new ColumnDefinition();

            // Set the column properties
            textColumn.Width         = new GridLength();
            newFilterColumn.Width    = new GridLength();
            addNewFilterColumn.Width = new GridLength();

            // Add the columns to the grid
            newFilterGrid.ColumnDefinitions.Add(textColumn);
            newFilterGrid.ColumnDefinitions.Add(newFilterColumn);
            newFilterGrid.ColumnDefinitions.Add(addNewFilterColumn);

            // Create the row for the grid
            RowDefinition newFilterRow = new RowDefinition();

            // Set the row's properties
            newFilterRow.Height = new GridLength(25);

            // Add the row to the grid
            newFilterGrid.RowDefinitions.Add(newFilterRow);

            // Create a text block for the new filter
            TextBlock newFilterTextBlock = new TextBlock();

            // Set the properties for the text block
            newFilterTextBlock.Text                = "New Filter Name:";
            newFilterTextBlock.Margin              = new Thickness(5, 2, 5, 2);
            newFilterTextBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            newFilterTextBlock.VerticalAlignment   = System.Windows.VerticalAlignment.Center;

            // Create a text box to create a new filter
            TextBox newFilterTextBox = new TextBox();

            // Set the properties for the text box
            newFilterTextBox.Margin              = new Thickness(5, 2, 5, 2);
            newFilterTextBox.Width               = 250;
            newFilterTextBox.Height              = 25;
            newFilterTextBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            newFilterTextBox.KeyDown            += newFilterTextBox_KeyDown;

            // Create a button to add the filter
            Button addNewFilterButton = new Button();

            // Set the button's properties
            addNewFilterButton.Height              = 25;
            addNewFilterButton.Width               = 100;
            addNewFilterButton.Content             = "Create Filter";
            addNewFilterButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            addNewFilterButton.Tag                 = newFilterTextBox;
            addNewFilterButton.Click              += addNewFilterButton_Click;

            // Position the GUI objects on the grid
            Grid.SetColumn(newFilterTextBlock, 0);
            Grid.SetRow(   newFilterTextBlock, 0);
            Grid.SetColumn(newFilterTextBox,   1);
            Grid.SetRow(   newFilterTextBox,   0);
            Grid.SetColumn(addNewFilterButton, 2);
            Grid.SetRow(   addNewFilterButton, 0);

            // Add the new filter objects to the dock panel
            newFilterGrid.Children.Add(newFilterTextBlock);
            newFilterGrid.Children.Add(newFilterTextBox);
            newFilterGrid.Children.Add(addNewFilterButton);

            // Create a grid for the filter management section
            Grid filterManagementGrid = new Grid();

            // Create the grid columns
            ColumnDefinition filterNamesColumn = new ColumnDefinition();
            ColumnDefinition buttonsColumn     = new ColumnDefinition();

            // Add the columns to the grid
            filterManagementGrid.ColumnDefinitions.Add(filterNamesColumn);
            filterManagementGrid.ColumnDefinitions.Add(buttonsColumn);

            // Create a row for the grid
            RowDefinition filterRow = new RowDefinition();

            // Add the row to the grid
            filterManagementGrid.RowDefinitions.Add(filterRow);

            // Create the list view
            filtersListView = new ListView();

            // Set the properties of the filters list view
            filtersListView.ItemsSource = filters.filterNames_Property;
            filtersListView.Margin      = new Thickness(5);

            // Create a panel for the buttons
            StackPanel buttonStackPanel = new StackPanel();

            // Create the buttons
            Button applyFilterButton  = new Button();
            Button editFilterButton   = new Button();
            Button deleteFilterButton = new Button();

            // Set the properties of the apply filter button
            applyFilterButton.Height  = 25;
            applyFilterButton.Content = "Apply Selected Filter";
            applyFilterButton.Margin  = new Thickness(5);
            applyFilterButton.Click  += applyFilterButton_Click;

            // Set the properties of the edit filter button
            editFilterButton.Height  = 25;
            editFilterButton.Content = "Edit Selected Filter";
            editFilterButton.Margin  = new Thickness(5);
            editFilterButton.Click  += editFilterButton_Click;

            // Set the properties of the delete filter button
            deleteFilterButton.Height  = 25;
            deleteFilterButton.Content = "Delete Selected Filter";
            deleteFilterButton.Margin  = new Thickness(5);
            deleteFilterButton.Click  += deleteFilterButton_Click;

            // Add the buttons to the panel
            buttonStackPanel.Children.Add(applyFilterButton);
            buttonStackPanel.Children.Add(editFilterButton);
            buttonStackPanel.Children.Add(deleteFilterButton);

            // Set the grid positions
            Grid.SetColumn(filtersListView,  0);
            Grid.SetRow(   filtersListView,  0);
            Grid.SetColumn(buttonStackPanel, 1);
            Grid.SetRow(   buttonStackPanel, 0);

            // Add the UI objects to the grid
            filterManagementGrid.Children.Add(filtersListView);
            filterManagementGrid.Children.Add(buttonStackPanel);

            // Create a grid to store the save UI items
            Grid saveGrid = new Grid();
            
            // Set the properties for the grid
            saveGrid.Margin              = new Thickness(5, 2, 5, 5);
            saveGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            saveGrid.VerticalAlignment   = System.Windows.VerticalAlignment.Bottom;

            // Create the columns for the grid
            ColumnDefinition blankColumn  = new ColumnDefinition();
            ColumnDefinition saveColumn   = new ColumnDefinition();
            ColumnDefinition cancelColumn = new ColumnDefinition();

            // Set the column properties
            blankColumn.Width  = new GridLength();
            saveColumn.Width   = new GridLength(110);
            cancelColumn.Width = new GridLength(110);

            // Add the columns to the grid
            saveGrid.ColumnDefinitions.Add(blankColumn);
            saveGrid.ColumnDefinitions.Add(saveColumn);
            saveGrid.ColumnDefinitions.Add(cancelColumn);

            // Create the row for the grid
            RowDefinition saveRow = new RowDefinition();

            // Set the row's properties
            saveRow.Height = new GridLength(25);

            // Add the row to the grid
            saveGrid.RowDefinitions.Add(saveRow);

            // Create toggle for flat view
            CheckBox flatViewCheckBox = new CheckBox();

            // Set the properties for the flatView Checkbox
            flatViewCheckBox.Content = "Flat View";

            // Create the binding for the flatView
            Binding flatViewBinding = new Binding();

            // Set the properties for flatView's binding
            flatViewBinding.Mode                = BindingMode.TwoWay;
            flatViewBinding.Path                = new PropertyPath(typeof(BackEnd).GetProperty("flatView_Property"));
            flatViewBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            flatViewCheckBox.SetBinding(CheckBox.IsCheckedProperty, flatViewBinding);

            // Create buttons to save or cancel
            Button saveFilterButton   = new Button();
            Button cancelFilterButton = new Button();

            // Set the save button's properties
            saveFilterButton.Width               = 100;
            saveFilterButton.Content             = "Save Filters";
            saveFilterButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            saveFilterButton.Click              += saveFilterButton_Click;

            // Set the cancel button's properties
            cancelFilterButton.Width               = 100;
            cancelFilterButton.Content             = "Cancel";
            cancelFilterButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            cancelFilterButton.Click              += cancelFilterButton_Click;

            // Position the GUI objects on the grid
            Grid.SetColumn(flatViewCheckBox,   0);
            Grid.SetRow(   flatViewCheckBox,   0);
            Grid.SetColumn(saveFilterButton,   1);
            Grid.SetRow(   saveFilterButton,   0);
            Grid.SetColumn(cancelFilterButton, 2);
            Grid.SetRow(   cancelFilterButton, 0);

            // Add the objects to the grid
            saveGrid.Children.Add(flatViewCheckBox);
            saveGrid.Children.Add(saveFilterButton);
            saveGrid.Children.Add(cancelFilterButton);

            // Set the position of the items on the panel
            DockPanel.SetDock(newFilterGrid,        Dock.Top);
            DockPanel.SetDock(filterManagementGrid, Dock.Top);
            DockPanel.SetDock(saveGrid,             Dock.Bottom);

            // Add the items to the panel
            filterManagerDockPanel.Children.Add(newFilterGrid);
            filterManagerDockPanel.Children.Add(saveGrid);
            filterManagerDockPanel.Children.Add(filterManagementGrid);
        }

        void newFilterTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // If the user pressed enter
            if (e.Key == System.Windows.Input.Key.Return)
            {
                // Convert the sender back to a text box
                TextBox newFilterTextBox = sender as TextBox;

                // If the filter was successfuly added
                if (addNewFilter(newFilterTextBox.Text))
                {
                    // Clear the text box
                    newFilterTextBox.Clear();
                }
            }
        }

        void cancelFilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window
            this.Close();
        }

        void saveFilterButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            // Save the filters
            filters.SaveFilters();

            // Close the window
            this.Close();
        }

        void addNewFilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Convert the sender back to a button
            Button addNewFilterButton = sender as Button;

            // Get the text box
            TextBox newFilterTextBox = (TextBox)addNewFilterButton.Tag;

            // If the filter was successfuly added
            if(addNewFilter(newFilterTextBox.Text))
            {
                // Clear the text box
                newFilterTextBox.Clear();
            }
        }

        bool addNewFilter(string filterName)
        {
            if (filterName.Trim() != "")
            {
                // Add the filter
                string addResult = filters.AddWithValidation(filterName);

                // If the filter was not successfully added
                if (addResult != "Success")
                {
                    // Display the error message
                    Xceed.Wpf.Toolkit.MessageBox.Show(addResult, this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);

                    // Return false (did not add new filter)
                    return false;
                }

                // Return true (new filter successfully added)
                return true;
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("The filter must have a name", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);

                // Return false (new filter failed to add)
                return false;
            }
        }

        void deleteFilterButton_Click(object sender, RoutedEventArgs e)
        {
            // If something is selected
            if (filtersListView.SelectedItems.Count > 0)
            {
                // Convert the sender back to a button
                Button deleteFilterButton = sender as Button;

                // Remove the filter
                filters.RemoveFilter(filtersListView.SelectedValue.ToString());
            }
            // If nothing is selected
            else
            {
                // Ask the user to select a filter to be removed
                Xceed.Wpf.Toolkit.MessageBox.Show("Please select a filter to delete", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        void applyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            // If something is selected
            if (filtersListView.SelectedItems.Count > 0)
            {
                // Apply the filter
                this.filters.ApplyFilter(filtersListView.SelectedValue.ToString());

                // Save and close
                saveFilterButton_Click();
            }
            // If nothing is selected
            else
            {
                // Ask the user to select a filter to be removed
                Xceed.Wpf.Toolkit.MessageBox.Show("Please select a filter to apply", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        void editFilterButton_Click(object sender, RoutedEventArgs e)
        {
            // If something is selected
            if (filtersListView.SelectedItems.Count > 0)
            {
                // Create a window for editing the selected filter
                FilterEditor filterEditorWindow = new FilterEditor(filters, filtersListView.SelectedValue.ToString());

                // Display the window and wait for it to finish
                filterEditorWindow.ShowDialog();
            }
            // If nothing is selected
            else
            {
                // Ask the user to select a filter to be removed
                Xceed.Wpf.Toolkit.MessageBox.Show("Please select a filter to edit", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
