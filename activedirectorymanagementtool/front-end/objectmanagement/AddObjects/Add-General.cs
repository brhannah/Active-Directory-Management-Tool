using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ActiveDirectoryManagementTool
{
    class GeneralAdd : Window
    {
        // Declare class variables
        RadioButton       userRadioBtn;
        RadioButton       computerRadioBtn;
        RadioButton       groupRadioBtn;
        RibbonBarFrontEnd parentWindow;

        // Constructor
        public GeneralAdd(RibbonBarFrontEnd parentWindow)
        {
            // Save the parent window
            this.parentWindow = parentWindow;

            // Create a grid to place GUI objects
            Grid generalAddWindow = new Grid();

            // Set grid's properties
            generalAddWindow.Margin = new Thickness(10);

            // Create grid columns
            ColumnDefinition blankCol   = new ColumnDefinition();
            ColumnDefinition contentCol = new ColumnDefinition();
            ColumnDefinition backCol    = new ColumnDefinition();
            ColumnDefinition nextCol    = new ColumnDefinition();

            // Set column properties
            blankCol.Width   = new GridLength(15);
            contentCol.Width = new GridLength(235);
            backCol.Width    = new GridLength(100);
            nextCol.Width    = new GridLength(100);

            // Add columns to grid
            generalAddWindow.ColumnDefinitions.Add(blankCol);
            generalAddWindow.ColumnDefinitions.Add(contentCol);
            generalAddWindow.ColumnDefinitions.Add(backCol);
            generalAddWindow.ColumnDefinitions.Add(nextCol);

            // Create grid rows
            RowDefinition instructionRow = new RowDefinition();
            RowDefinition contentRow     = new RowDefinition();
            RowDefinition controlRow     = new RowDefinition();

            // Set row properties
            instructionRow.Height = new GridLength(50);
            contentRow.Height     = new GridLength(355);
            controlRow.Height     = new GridLength(25);

            // Add rows to grid
            generalAddWindow.RowDefinitions.Add(instructionRow);
            generalAddWindow.RowDefinitions.Add(contentRow);
            generalAddWindow.RowDefinitions.Add(controlRow);

            // Set the properties of the window
            this.Content               = generalAddWindow;
            this.Width                 = 500;
            this.Height                = 500;
            this.Title                 = "Add Object(s)";
            this.ResizeMode            = System.Windows.ResizeMode.NoResize;
            this.WindowState           = WindowState.Normal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Background            = Brushes.WhiteSmoke;
            this.Topmost               = false;
            this.Activate();
            this.ShowActivated         = true;
            this.Icon                  = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            // Create a textblock for instructions
            TextBlock instructionsTextBlock = new TextBlock();

            // Provide the user with instructions
            instructionsTextBlock.Text = "Please select the type of object to add:";

            // Create a panel for the radio button objects
            StackPanel radioButtonsStackPanel = new StackPanel();

            // Create the radio button objects
            this.userRadioBtn     = new RadioButton();
            this.computerRadioBtn = new RadioButton();
            this.groupRadioBtn    = new RadioButton();

            // Group the radio buttons
            this.userRadioBtn.GroupName     = "generalAdd";
            this.computerRadioBtn.GroupName = "generalAdd";
            this.groupRadioBtn.GroupName    = "generalAdd";

            // Add text to the radio buttons
            this.userRadioBtn.Content     = "User(s)";
            this.computerRadioBtn.Content = "Computer(s)";
            this.groupRadioBtn.Content    = "Group(s)";

            // Add space between the radio buttons
            this.userRadioBtn.Margin     = new Thickness(10);
            this.computerRadioBtn.Margin = new Thickness(10);
            this.groupRadioBtn.Margin    = new Thickness(10);

            // Add the radio buttons to the panel
            radioButtonsStackPanel.Children.Add(this.userRadioBtn);
            radioButtonsStackPanel.Children.Add(this.computerRadioBtn);
            radioButtonsStackPanel.Children.Add(this.groupRadioBtn);

            // Create a Next button to take the user to the next window
            Button nextBtn = new Button();

            // Set the Next button's properties
            nextBtn.Content = "Next";
            nextBtn.Width   = 100;
            nextBtn.Height  = 25;
            nextBtn.Click  += nextBtn_Click;

            // Set positions of GUI objects in the grid
            Grid.SetColumn(    instructionsTextBlock,  0);
            Grid.SetRow(       instructionsTextBlock,  0);
            Grid.SetColumnSpan(instructionsTextBlock,  4);
            Grid.SetColumn(    radioButtonsStackPanel, 1);
            Grid.SetRow(       radioButtonsStackPanel, 1);
            Grid.SetColumnSpan(radioButtonsStackPanel, 3);
            Grid.SetColumn(    nextBtn,                3);
            Grid.SetRow(       nextBtn,                2);

            // Add all GUI objects to the grid
            generalAddWindow.Children.Add(instructionsTextBlock);
            generalAddWindow.Children.Add(radioButtonsStackPanel);
            generalAddWindow.Children.Add(nextBtn);
        }

        void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            // If the user selected to add a user
            if (this.userRadioBtn.IsChecked.Value)
            {
                // Hide this window
                this.Hide();

                // Create the add window
                this.parentWindow.user_add_Click();

                // Close this window
                this.Close();
            }
            // If the user selected to add a group
            else if (this.groupRadioBtn.IsChecked.Value)
            {
                // Hide this window
                this.Hide();

                // Create the add window
                this.parentWindow.group_create_Click();

                // Close this window
                this.Close();
            }
            // If the user selected to add a computer
            else if (this.computerRadioBtn.IsChecked.Value)
            {
                // Hide this window
                this.Hide();

                // Create the add window
                this.parentWindow.computer_add_Click();

                // Close this window
                this.Close();
            }
            // If the user has not selected anything
            else
            {
                // Display an error message
                Xceed.Wpf.Toolkit.MessageBox.Show("Please select an option", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
