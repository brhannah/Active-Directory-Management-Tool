using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace ActiveDirectoryManagementTool
{
    class DictionaryManager : Window
    {
        // Declare class variables
        DictionaryManagement dictionary;
        StackPanel           dictionaryStackPanel;

        // Constructor
        public DictionaryManager()
        {
            // Get the current dictionary
            dictionary = new DictionaryManagement();

            // Create a panel to place all UI items
            DockPanel dictionaryManagerDockPanel = new DockPanel();

            // Create a panel for the dictionary items
            dictionaryStackPanel = new StackPanel();

            // Create a scroll viewer to place the panel
            ScrollViewer dictionaryScrollViewer = new ScrollViewer();

            // Set the properties for the scroll viewer
            dictionaryScrollViewer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            dictionaryScrollViewer.Content           = dictionaryStackPanel;

            // Set window properties
            this.Title                 = "Job Title Manager";
            this.Width                 = 900;
            this.MinWidth              = 900;
            this.Height                = 500;
            this.MinHeight             = 500;
            this.WindowState           = System.Windows.WindowState.Normal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Content               = dictionaryManagerDockPanel;
            this.Background            = Brushes.WhiteSmoke;
            this.Topmost               = false;
            this.Show();
            this.Activate();
            this.ShowActivated         = true;
            this.Icon                  = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            // Create a grid to store the new term UI items
            Grid newTermGrid = new Grid();
            
            // Set the properties for the grid
            newTermGrid.Margin = new Thickness(5, 2, 5, 5);

            // Create the columns for the grid
            ColumnDefinition textColumn       = new ColumnDefinition();
            ColumnDefinition newTermColumn    = new ColumnDefinition();
            ColumnDefinition addNewTermColumn = new ColumnDefinition();

            // Set the column properties
            textColumn.Width       = new GridLength();
            newTermColumn.Width    = new GridLength();
            addNewTermColumn.Width = new GridLength();

            // Add the columns to the grid
            newTermGrid.ColumnDefinitions.Add(textColumn);
            newTermGrid.ColumnDefinitions.Add(newTermColumn);
            newTermGrid.ColumnDefinitions.Add(addNewTermColumn);

            // Create the row for the grid
            RowDefinition newTermRow = new RowDefinition();

            // Set the row's properties
            newTermRow.Height = new GridLength(25);

            // Add the row to the grid
            newTermGrid.RowDefinitions.Add(newTermRow);

            // Create a text block for the new term
            TextBlock newTermTextBlock = new TextBlock();

            // Set the properties for the text block
            newTermTextBlock.Text                = "New Job Title:";
            newTermTextBlock.Margin              = new Thickness(5, 2, 5, 2);
            newTermTextBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            newTermTextBlock.VerticalAlignment   = System.Windows.VerticalAlignment.Center;

            // Create a text box to create a new dictionary term
            TextBox newTermTextBox = new TextBox();

            // Set the properties for the text box
            newTermTextBox.Margin              = new Thickness(5, 2, 5, 2);
            newTermTextBox.Width               = 250;
            newTermTextBox.Height              = 25;
            newTermTextBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            newTermTextBox.KeyDown            += newTermTextBox_KeyDown;

            // Create a button to add the term
            Button addNewTermButton = new Button();

            // Set the button's properties
            addNewTermButton.Height              = 25;
            addNewTermButton.Width               = 100;
            addNewTermButton.Content             = "Add Job Title";
            addNewTermButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            addNewTermButton.Tag                 = newTermTextBox;
            addNewTermButton.Click              += addNewTermButton_Click;

            // Position the GUI objects on the grid
            Grid.SetColumn(newTermTextBlock, 0);
            Grid.SetRow(   newTermTextBlock, 0);
            Grid.SetColumn(newTermTextBox,   1);
            Grid.SetRow(   newTermTextBox,   0);
            Grid.SetColumn(addNewTermButton, 2);
            Grid.SetRow(   addNewTermButton, 0);

            // Add the new term objects to the dock panel
            newTermGrid.Children.Add(newTermTextBlock);
            newTermGrid.Children.Add(newTermTextBox);
            newTermGrid.Children.Add(addNewTermButton);

            // For each dictionary term
            foreach (KeyValuePair<string, ObservableCollection<string>> x in dictionary.terms)
            {
                // Create a grid to place the term and its definition
                Grid termGrid = new Grid();

                // Set the properties for the grid
                termGrid.Margin = new Thickness(5, 5, 5, 10);

                // Create the columns for the grid
                ColumnDefinition termColumn       = new ColumnDefinition();
                ColumnDefinition groupsColumn     = new ColumnDefinition();
                ColumnDefinition definitionColumn = new ColumnDefinition();

                // Add the columns to the grid
                termGrid.ColumnDefinitions.Add(termColumn);
                termGrid.ColumnDefinitions.Add(groupsColumn);
                termGrid.ColumnDefinitions.Add(definitionColumn);

                // Create the row for the term
                RowDefinition termRow   = new RowDefinition();
                RowDefinition blankRow  = new RowDefinition();
                RowDefinition buttonRow = new RowDefinition();

                // Set the row's properties
                termRow.Height   = new GridLength(50);
                blankRow.Height  = new GridLength(25);
                buttonRow.Height = new GridLength(40);

                // Add the row to the grid
                termGrid.RowDefinitions.Add(termRow);
                termGrid.RowDefinitions.Add(blankRow);
                termGrid.RowDefinitions.Add(buttonRow);

                // Create a border for the bottom of the grid
                Border col0BottomBorder = new Border();
                Border col1BottomBorder = new Border();
                Border col2BottomBorder = new Border();

                // Set the border properties
                col0BottomBorder.BorderBrush     = Brushes.Black;
                col0BottomBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                col1BottomBorder.BorderBrush     = Brushes.Black;
                col1BottomBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                col2BottomBorder.BorderBrush     = Brushes.Black;
                col2BottomBorder.BorderThickness = new Thickness(0, 0, 0, 1);

                // Create the text block for the term
                TextBlock termTextBlock = new TextBlock();

                // Set the text block's properties
                termTextBlock.Text         = x.Key;
                termTextBlock.Height       = 50;
                termTextBlock.TextWrapping = TextWrapping.Wrap;

                // Create the delete button
                Button deleteTermButton = new Button();

                // Set the properties of the button
                deleteTermButton.Height              = 25;
                deleteTermButton.Width               = 100;
                deleteTermButton.Content             = "Delete Job Title";
                deleteTermButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                deleteTermButton.Tag                 = new List<object> { x.Key.ToString(), termGrid };
                deleteTermButton.Click              += deleteTermButton_Click;

                // Create the combo box for the group names
                ComboBox groupsComboBox = new ComboBox();

                // Set the properties for the combo box
                groupsComboBox.ItemsSource   = dictionary.groups;
                groupsComboBox.Height        = 25;
                groupsComboBox.Width         = 250;
                groupsComboBox.SelectedIndex = 0;

                // Create the add button
                Button addGroupButton = new Button();

                // Set the properties of the add button
                addGroupButton.Height  = 25;
                addGroupButton.Width   = 100;
                addGroupButton.Content = "Add Group";
                addGroupButton.Tag     = new List<object> { x.Key.ToString(), groupsComboBox };
                addGroupButton.Click  += addGroupButton_Click;

                // Create the list view for the definition
                ListView definitionListView = new ListView();

                // Set the properties of the list view
                definitionListView.Height        = 75;
                definitionListView.ItemsSource   = x.Value;
                definitionListView.SelectionMode = SelectionMode.Single;
                
                // Create the remove button
                Button removeGroupButton = new Button();

                // Set the properties for the remove button
                removeGroupButton.Height  = 25;
                removeGroupButton.Width   = 100;
                removeGroupButton.Content = "Remove Group";
                removeGroupButton.Tag     = new List<object> { x.Key.ToString(), definitionListView };
                removeGroupButton.Click  += removeGroupButton_Click;

                // Set the positions of the GUI objects on the grid
                Grid.SetColumn( termTextBlock,      0);
                Grid.SetRow(    termTextBlock,      0);
                Grid.SetColumn( deleteTermButton,   0);
                Grid.SetRow(    deleteTermButton,   2);
                Grid.SetColumn( groupsComboBox,     1);
                Grid.SetRow(    groupsComboBox,     0);
                Grid.SetColumn( addGroupButton,     1);
                Grid.SetRow(    addGroupButton,     2);
                Grid.SetColumn( definitionListView, 2);
                Grid.SetRow(    definitionListView, 0);
                Grid.SetRowSpan(definitionListView, 2);
                Grid.SetColumn( removeGroupButton,  2);
                Grid.SetRow(    removeGroupButton,  2);
                Grid.SetColumn(col0BottomBorder,    0);
                Grid.SetRow(   col0BottomBorder,    2);
                Grid.SetColumn(col1BottomBorder,    1);
                Grid.SetRow(   col1BottomBorder,    2);
                Grid.SetColumn(col2BottomBorder,    2);
                Grid.SetRow(   col2BottomBorder,    2);

                // Add the GUI objects to the grid
                termGrid.Children.Add(termTextBlock);
                termGrid.Children.Add(deleteTermButton);
                termGrid.Children.Add(groupsComboBox);
                termGrid.Children.Add(addGroupButton);
                termGrid.Children.Add(definitionListView);
                termGrid.Children.Add(removeGroupButton);
                termGrid.Children.Add(col0BottomBorder);
                termGrid.Children.Add(col1BottomBorder);
                termGrid.Children.Add(col2BottomBorder);

                // Add the grid to the stack panel
                dictionaryStackPanel.Children.Add(termGrid);
            }

            // Create a grid to store the new term UI items
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

            // Create buttons to save or cancel
            Button saveDictionaryButton   = new Button();
            Button cancelDictionaryButton = new Button();

            // Set the save button's properties
            saveDictionaryButton.Width               = 100;
            saveDictionaryButton.Content             = "OK";
            saveDictionaryButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            saveDictionaryButton.Click              += saveDictionaryButton_Click;

            // Set the cancel button's properties
            cancelDictionaryButton.Width               = 100;
            cancelDictionaryButton.Content             = "Cancel";
            cancelDictionaryButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            cancelDictionaryButton.Click              += cancelDictionaryButton_Click;

            // Position the GUI objects on the grid
            Grid.SetColumn(saveDictionaryButton,   1);
            Grid.SetRow(   saveDictionaryButton,   0);
            Grid.SetColumn(cancelDictionaryButton, 2);
            Grid.SetRow(   cancelDictionaryButton, 0);

            // Add the new term objects to the dock panel
            saveGrid.Children.Add(saveDictionaryButton);
            saveGrid.Children.Add(cancelDictionaryButton);

            // Set the position of the items on the panel
            DockPanel.SetDock(newTermGrid,            Dock.Top);
            DockPanel.SetDock(dictionaryScrollViewer, Dock.Left);
            DockPanel.SetDock(saveGrid,               Dock.Bottom);

            // Add the items to the panel
            dictionaryManagerDockPanel.Children.Add(newTermGrid);
            dictionaryManagerDockPanel.Children.Add(saveGrid);
            dictionaryManagerDockPanel.Children.Add(dictionaryScrollViewer);
        }

        void newTermTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // If the user pressed enter
            if (e.Key == System.Windows.Input.Key.Return)
            {
                // Convert the sender back to a text box
                TextBox newTermTextBox = sender as TextBox;

                // If the term was successfuly added
                if (addNewTerm(newTermTextBox.Text))
                {
                    // Clear the text box
                    newTermTextBox.Clear();
                }
            }
        }

        void cancelDictionaryButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window
            this.Close();
        }

        void saveDictionaryButton_Click(object sender, RoutedEventArgs e)
        {
            // Save the dictionary
            dictionary.SaveDictionary();

            // Close the window
            this.Close();
        }

        void addNewTermButton_Click(object sender, RoutedEventArgs e)
        {
            // Convert the sender back to a button
            Button addNewTermButton = sender as Button;

            // Get the text box
            TextBox newTermTextBox = (TextBox)addNewTermButton.Tag;

            // If the term was successfuly added
            if(addNewTerm(newTermTextBox.Text))
            {
                // Clear the text box
                newTermTextBox.Clear();
            }
        }

        bool addNewTerm(string term)
        {
            // Add the term to the dictionary
            string addResult = dictionary.AddWithValidation(term);

            // If the add was successful
            if (addResult == "")
            {
                // Create a grid to place the term and its definition
                Grid termGrid = new Grid();

                // Set the properties for the grid
                termGrid.Margin = new Thickness(5, 5, 5, 10);

                // Create the columns for the grid
                ColumnDefinition termColumn       = new ColumnDefinition();
                ColumnDefinition groupsColumn     = new ColumnDefinition();
                ColumnDefinition definitionColumn = new ColumnDefinition();

                // Add the columns to the grid
                termGrid.ColumnDefinitions.Add(termColumn);
                termGrid.ColumnDefinitions.Add(groupsColumn);
                termGrid.ColumnDefinitions.Add(definitionColumn);

                // Create the row for the term
                RowDefinition termRow   = new RowDefinition();
                RowDefinition blankRow  = new RowDefinition();
                RowDefinition buttonRow = new RowDefinition();

                // Set the row's properties
                termRow.Height   = new GridLength(50);
                blankRow.Height  = new GridLength(25);
                buttonRow.Height = new GridLength(40);

                // Add the row to the grid
                termGrid.RowDefinitions.Add(termRow);
                termGrid.RowDefinitions.Add(blankRow);
                termGrid.RowDefinitions.Add(buttonRow);

                // Create a border for the bottom of the grid
                Border col0BottomBorder = new Border();
                Border col1BottomBorder = new Border();
                Border col2BottomBorder = new Border();

                // Set the border properties
                col0BottomBorder.BorderBrush     = Brushes.Black;
                col0BottomBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                col1BottomBorder.BorderBrush     = Brushes.Black;
                col1BottomBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                col2BottomBorder.BorderBrush     = Brushes.Black;
                col2BottomBorder.BorderThickness = new Thickness(0, 0, 0, 1);

                // Create the text block for the term
                TextBlock termTextBlock = new TextBlock();

                // Set the text block's properties
                termTextBlock.Text         = term;
                termTextBlock.Height       = 50;
                termTextBlock.TextWrapping = TextWrapping.Wrap;

                // Create the delete button
                Button deleteTermButton = new Button();

                // Set the properties of the button
                deleteTermButton.Height              = 25;
                deleteTermButton.Width               = 100;
                deleteTermButton.Content             = "Delete Job Title";
                deleteTermButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                deleteTermButton.Tag                 = new List<object> { term, termGrid };
                deleteTermButton.Click              += deleteTermButton_Click;

                // Create the combo box for the group names
                ComboBox groupsComboBox = new ComboBox();

                // Set the properties for the combo box
                groupsComboBox.ItemsSource   = dictionary.groups;
                groupsComboBox.Height        = 25;
                groupsComboBox.Width         = 250;
                groupsComboBox.SelectedIndex = 0;

                // Create the add button
                Button addGroupButton = new Button();

                // Set the properties of the add button
                addGroupButton.Height  = 25;
                addGroupButton.Width   = 100;
                addGroupButton.Content = "Add Group";
                addGroupButton.Tag     = new List<object> { term, groupsComboBox };
                addGroupButton.Click  += addGroupButton_Click;

                // Create the list view for the definition
                ListView definitionListView = new ListView();

                // Set the properties of the list view
                definitionListView.Height        = 75;
                definitionListView.ItemsSource = dictionary.terms[term];
                definitionListView.SelectionMode = SelectionMode.Single;
                
                // Create the remove button
                Button removeGroupButton = new Button();

                // Set the properties for the remove button
                removeGroupButton.Height  = 25;
                removeGroupButton.Width   = 100;
                removeGroupButton.Content = "Remove Group";
                removeGroupButton.Tag     = new List<object> { term, definitionListView };
                removeGroupButton.Click  += removeGroupButton_Click;

                // Set the positions of the GUI objects on the grid
                Grid.SetColumn( termTextBlock,      0);
                Grid.SetRow(    termTextBlock,      0);
                Grid.SetColumn( deleteTermButton,   0);
                Grid.SetRow(    deleteTermButton,   2);
                Grid.SetColumn( groupsComboBox,     1);
                Grid.SetRow(    groupsComboBox,     0);
                Grid.SetColumn( addGroupButton,     1);
                Grid.SetRow(    addGroupButton,     2);
                Grid.SetColumn( definitionListView, 2);
                Grid.SetRow(    definitionListView, 0);
                Grid.SetRowSpan(definitionListView, 2);
                Grid.SetColumn( removeGroupButton,  2);
                Grid.SetRow(    removeGroupButton,  2);
                Grid.SetColumn(col0BottomBorder,    0);
                Grid.SetRow(   col0BottomBorder,    2);
                Grid.SetColumn(col1BottomBorder,    1);
                Grid.SetRow(   col1BottomBorder,    2);
                Grid.SetColumn(col2BottomBorder,    2);
                Grid.SetRow(   col2BottomBorder,    2);

                // Add the GUI objects to the grid
                termGrid.Children.Add(termTextBlock);
                termGrid.Children.Add(deleteTermButton);
                termGrid.Children.Add(groupsComboBox);
                termGrid.Children.Add(addGroupButton);
                termGrid.Children.Add(definitionListView);
                termGrid.Children.Add(removeGroupButton);
                termGrid.Children.Add(col0BottomBorder);
                termGrid.Children.Add(col1BottomBorder);
                termGrid.Children.Add(col2BottomBorder);

                // Add the grid to the stack panel
                dictionaryStackPanel.Children.Add(termGrid);
                
                // Return a success status
                return true;
            }
            // If the add was not successful
            else
            {
                // Display the error message
                Xceed.Wpf.Toolkit.MessageBox.Show(addResult, this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);

                // Return a failed status
                return false;
            }
        }

        void deleteTermButton_Click(object sender, RoutedEventArgs e)
        {
            // Convert the sender back to a button
            Button deleteTermButton = sender as Button;

            // Remove the term from the dictionary
            dictionary.RemoveTerm((string)((List<object>)deleteTermButton.Tag)[0]);

            // Remove the term from the panel
            dictionaryStackPanel.Children.Remove((Grid)((List<object>)deleteTermButton.Tag)[1]);
        }

        void addGroupButton_Click(object sender, RoutedEventArgs e)
        {
            // Conver the sender back to a button
            Button addGroupButton = sender as Button;

            // If a group is selected
            if (((ComboBox)((List<object>)addGroupButton.Tag)[1]).SelectedIndex >= 0)
            {
                // Add the group to the definition
                dictionary.AddDefinitionToTerm((string)((List<object>)addGroupButton.Tag)[0], ((ComboBox)((List<object>)addGroupButton.Tag)[1]).SelectedValue.ToString());
            }
        }

        void removeGroupButton_Click(object sender, RoutedEventArgs e)
        {
            // Conver the sender back to a button
            Button removeGroupButton = sender as Button;

            // If a group is selected
            if (((ListView)((List<object>)removeGroupButton.Tag)[1]).SelectedIndex >= 0)
            {
                // Remove the group from the definition
                dictionary.RemoveDefinitionFromTerm((string)((List<object>)removeGroupButton.Tag)[0], ((ListView)((List<object>)removeGroupButton.Tag)[1]).SelectedValue.ToString());
            }
        }
    }
}
