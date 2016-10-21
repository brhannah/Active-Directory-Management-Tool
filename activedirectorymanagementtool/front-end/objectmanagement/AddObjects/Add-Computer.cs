using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;

namespace ActiveDirectoryManagementTool
{
    public class ComputerAdd : Window
    {
        // Create an integer to keep track of the current window
        private int currentFrame = 0;

        // Declare class objects
        private List<Grid>  computerAddWindows;
        private TextBlock   initialInstructionsTextBlock;
        private TextBlock   selectFileInstructionsTextBlock;
        private TextBlock   reviewInstructionsTextBlock;
        private TextBlock   singleInstructionsTextBlock;
        private TextBlock   singleHostnameTextBlock;
        private StackPanel  initialRadioButtonsStackPanel;
        private StackPanel  selectFileStackPanel;
        private StackPanel  reviewStackPanel;
        private StackPanel  singleStackPanel;
        private RadioButton singleRadioBtn;
        private RadioButton importRadioBtn;
        private Microsoft.Win32.OpenFileDialog selectFileDialogBox;
        private TextBox     selectFileTextBox;
        private TextBox     singleHostnameTextBox;
        private DataGrid    reviewDataGrid;
        private Button      initialBackBtn;
        private Button      initialNextBtn;
        private Button      selectFileBrowseBtn;
        private Button      selectFileBackBtn;
        private Button      selectFileNextBtn;
        private Button      reviewBackBtn;
        private Button      reviewNextBtn;
        private Button      singleBackBtn;
        private Button      singleNextBtn;

        public ComputerAdd()
        {
            // Create the list of grids to place GUI objects
            this.computerAddWindows = new List<Grid>();

            // Create the pages for the wizard
            this.computerAddWindows.Add(new Grid()); // Initial page (single or import?)
            this.computerAddWindows.Add(new Grid()); // Import - Select a file
            this.computerAddWindows.Add(new Grid()); // Import - Review
            this.computerAddWindows.Add(new Grid()); // Single

            // For each grid
            foreach (Grid computerAddWindow in this.computerAddWindows)
            {
                // Set grid's properties
                computerAddWindow.Margin = new Thickness(10);

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
                computerAddWindow.ColumnDefinitions.Add(blankCol);
                computerAddWindow.ColumnDefinitions.Add(contentCol);
                computerAddWindow.ColumnDefinitions.Add(backCol);
                computerAddWindow.ColumnDefinitions.Add(nextCol);

                // Create grid rows
                RowDefinition instructionRow = new RowDefinition();
                RowDefinition contentRow     = new RowDefinition();
                RowDefinition controlRow     = new RowDefinition();

                // Set row properties
                instructionRow.Height = new GridLength(100);
                contentRow.Height     = new GridLength(305);
                controlRow.Height     = new GridLength(25);

                // Add rows to grid
                computerAddWindow.RowDefinitions.Add(instructionRow);
                computerAddWindow.RowDefinitions.Add(contentRow);
                computerAddWindow.RowDefinitions.Add(controlRow);
            }

            // Set the properties of the window
            this.Content               = this.computerAddWindows[currentFrame];
            this.Width                 = 500;
            this.Height                = 500;
            this.Title                 = "Add Computer(s)";
            this.ResizeMode            = ResizeMode.NoResize;
            this.WindowState           = WindowState.Normal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Background            = Brushes.WhiteSmoke;
            this.Topmost               = false;
            this.Activate();
            this.ShowActivated         = true;
            this.Icon                  = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            #region Initial page (single or import?)
            // Create a textblock for instructions
            this.initialInstructionsTextBlock = new TextBlock();

            // Provide the user with instructions
            this.initialInstructionsTextBlock.Text         = "Would you like to add a single computer or import a list of computers?";
            this.initialInstructionsTextBlock.TextWrapping = TextWrapping.Wrap;

            // Create a panel for the radio button objects
            this.initialRadioButtonsStackPanel = new StackPanel();

            // Create the radio button objects
            this.singleRadioBtn = new RadioButton();
            this.importRadioBtn = new RadioButton();

            // Group the radio buttons
            this.singleRadioBtn.GroupName = "computerAdd";
            this.importRadioBtn.GroupName = "computerAdd";

            // Add text to the radio buttons
            this.singleRadioBtn.Content = "Add a single computer";
            this.importRadioBtn.Content = "Import a list of computers";

            // Add space between the radio buttons
            this.singleRadioBtn.Margin = new Thickness(10);
            this.importRadioBtn.Margin = new Thickness(10);

            // Add the radio buttons to the panel
            this.initialRadioButtonsStackPanel.Children.Add(this.singleRadioBtn);
            this.initialRadioButtonsStackPanel.Children.Add(this.importRadioBtn);

            // Create the Back and Next buttons to provide user flow control
            this.initialBackBtn = new Button();
            this.initialNextBtn = new Button();

            // Set the Back Button's properties
            this.initialBackBtn.Content   = "Back";
            this.initialBackBtn.Width     = 90;
            this.initialBackBtn.Height    = 25;
            this.initialBackBtn.IsEnabled = false;
            this.initialBackBtn.Click    += backBtn_Click;

            // Set the Next button's properties
            this.initialNextBtn.Content = "Next";
            this.initialNextBtn.Width   = 90;
            this.initialNextBtn.Height  = 25;
            this.initialNextBtn.Click  += nextBtn_Click;

            // Set positions of GUI objects in the grid
            Grid.SetColumn(    this.initialInstructionsTextBlock,  0);
            Grid.SetRow(       this.initialInstructionsTextBlock,  0);
            Grid.SetColumnSpan(this.initialInstructionsTextBlock,  4);
            Grid.SetColumn(    this.initialRadioButtonsStackPanel, 1);
            Grid.SetRow(       this.initialRadioButtonsStackPanel, 1);
            Grid.SetColumnSpan(this.initialRadioButtonsStackPanel, 3);
            Grid.SetColumn(    this.initialBackBtn,                2);
            Grid.SetRow(       this.initialBackBtn,                2);
            Grid.SetColumn(    this.initialNextBtn,                3);
            Grid.SetRow(       this.initialNextBtn,                2);

            // Add all GUI objects to the grid
            this.computerAddWindows[0].Children.Add(this.initialInstructionsTextBlock);
            this.computerAddWindows[0].Children.Add(this.initialRadioButtonsStackPanel);
            this.computerAddWindows[0].Children.Add(this.initialBackBtn);
            this.computerAddWindows[0].Children.Add(this.initialNextBtn);
            #endregion

            #region // Import - Select a file
            // Create a textblock for instructions
            this.selectFileInstructionsTextBlock = new TextBlock();

            // Provide the user with instructions
            this.selectFileInstructionsTextBlock.Text = "When importing computers using a CSV file, the CSV file should be formatted to have a single column containing the hostnames of the computers.\n\nPlease select the CSV file containing the list of computers to import:\n(Note: The computers will be created in ";
            this.selectFileInstructionsTextBlock.Inlines.Add(new System.Windows.Documents.Bold(new System.Windows.Documents.Run(BackEnd.LDAPPath_Property.Replace("LDAP://", "").Replace(BackEnd.DomainName_Property + "/", "").Split(',')[0].Remove(0, (BackEnd.LDAPPath_Property.EndsWith(BackEnd.DomainName_Property)) ? 0 : 3))));
            this.selectFileInstructionsTextBlock.Inlines.Add(")");
            this.selectFileInstructionsTextBlock.TextWrapping = TextWrapping.Wrap;

            // Create a panel for the GUI objects
            this.selectFileStackPanel = new StackPanel();

            // Create the textbox for the filename
            selectFileTextBox = new TextBox();

            // Create the open file dialog box
            selectFileDialogBox = new Microsoft.Win32.OpenFileDialog();

            // Set the properties of the dialog box
            selectFileDialogBox.Filter     = "CSV File (.csv)|*.csv|All Files (*.*)|*.*";
            selectFileDialogBox.DefaultExt = ".csv";

            // Create the browse button
            selectFileBrowseBtn = new Button();

            // Set the properties of the browse button
            selectFileBrowseBtn.Content = "Browse";
            selectFileBrowseBtn.Width   = 90;
            selectFileBrowseBtn.Click  += selectFileBrowseBtn_Click;

            // Add GUI objects to the stack panel
            this.selectFileStackPanel.Children.Add(this.selectFileTextBox);
            this.selectFileStackPanel.Children.Add(this.selectFileBrowseBtn);

            // Create the Back and Next buttons to provide user flow control
            this.selectFileBackBtn = new Button();
            this.selectFileNextBtn = new Button();

            // Set the Back Button's properties
            this.selectFileBackBtn.Content = "Back";
            this.selectFileBackBtn.Width   = 90;
            this.selectFileBackBtn.Height  = 25;
            this.selectFileBackBtn.Click  += backBtn_Click;

            // Set the Next button's properties
            this.selectFileNextBtn.Content = "Begin Import";
            this.selectFileNextBtn.Width   = 90;
            this.selectFileNextBtn.Height  = 25;
            this.selectFileNextBtn.Click  += nextBtn_Click;

            // Set positions of GUI objects in the grid
            Grid.SetColumn(    this.selectFileInstructionsTextBlock, 0);
            Grid.SetRow(       this.selectFileInstructionsTextBlock, 0);
            Grid.SetColumnSpan(this.selectFileInstructionsTextBlock, 4);
            Grid.SetColumn(    this.selectFileStackPanel,            1);
            Grid.SetRow(       this.selectFileStackPanel,            1);
            Grid.SetColumnSpan(this.selectFileStackPanel,            3);
            Grid.SetColumn(    this.selectFileBackBtn,               2);
            Grid.SetRow(       this.selectFileBackBtn,               2);
            Grid.SetColumn(    this.selectFileNextBtn,               3);
            Grid.SetRow(       this.selectFileNextBtn,               2);

            // Add all GUI objects to the grid
            this.computerAddWindows[1].Children.Add(this.selectFileInstructionsTextBlock);
            this.computerAddWindows[1].Children.Add(this.selectFileStackPanel);
            this.computerAddWindows[1].Children.Add(this.selectFileBackBtn);
            this.computerAddWindows[1].Children.Add(this.selectFileNextBtn);
            #endregion

            #region // Import - Review
            // Create a textblock for instructions
            this.reviewInstructionsTextBlock = new TextBlock();

            // Provide the user with instructions
            this.reviewInstructionsTextBlock.Text         = "The import is complete!\n\nPlease review the imported computers and observe any error messages:";
            this.reviewInstructionsTextBlock.TextWrapping = TextWrapping.Wrap;

            // Create a panel for the instructions and data grid
            this.reviewStackPanel = new StackPanel();

            // Create the Back and Next buttons to provide user flow control
            this.reviewBackBtn = new Button();
            this.reviewNextBtn = new Button();

            // Set the Back Button's properties
            this.reviewBackBtn.Content = "Back";
            this.reviewBackBtn.Width   = 90;
            this.reviewBackBtn.Height  = 25;
            this.reviewBackBtn.Click  += backBtn_Click;

            // Set the Next button's properties
            this.reviewNextBtn.Content = "Close";
            this.reviewNextBtn.Width   = 90;
            this.reviewNextBtn.Height  = 25;
            this.reviewNextBtn.Click  += nextBtn_Click;

            // Set positions of GUI objects in the grid
            Grid.SetColumn(    this.reviewInstructionsTextBlock, 0);
            Grid.SetRow(       this.reviewInstructionsTextBlock, 0);
            Grid.SetColumnSpan(this.reviewInstructionsTextBlock, 4);
            Grid.SetColumn(    this.reviewStackPanel,            1);
            Grid.SetRow(       this.reviewStackPanel,            1);
            Grid.SetColumnSpan(this.reviewStackPanel,            3);
            Grid.SetColumn(    this.reviewBackBtn,               2);
            Grid.SetRow(       this.reviewBackBtn,               2);
            Grid.SetColumn(    this.reviewNextBtn,               3);
            Grid.SetRow(       this.reviewNextBtn,               2);

            // Add all GUI objects to the grid
            this.computerAddWindows[2].Children.Add(this.reviewInstructionsTextBlock);
            this.computerAddWindows[2].Children.Add(this.reviewStackPanel);
            this.computerAddWindows[2].Children.Add(this.reviewBackBtn);
            this.computerAddWindows[2].Children.Add(this.reviewNextBtn);
            #endregion

            #region // Single
            // Create a textblock for instructions
            this.singleInstructionsTextBlock = new TextBlock();

            // Provide the user with instructions
            this.singleInstructionsTextBlock.Text         = "Please provide the computer's hostname:\n(Note: The computer will be created in ";
            this.singleInstructionsTextBlock.Inlines.Add(new System.Windows.Documents.Bold(new System.Windows.Documents.Run(BackEnd.LDAPPath_Property.Replace("LDAP://", "").Replace(BackEnd.DomainName_Property + "/", "").Split(',')[0].Remove(0, (BackEnd.LDAPPath_Property.EndsWith(BackEnd.DomainName_Property)) ? 0 : 3))));
            this.singleInstructionsTextBlock.Inlines.Add(")");
            this.singleInstructionsTextBlock.TextWrapping = TextWrapping.Wrap;
            
            // Create a panel for the GUI objects
            this.singleStackPanel = new StackPanel();

            // Create the label for the text box
            singleHostnameTextBlock = new TextBlock();

            // Set the properties for the text block
            singleHostnameTextBlock.Text = "Hostname:";

            // Create the text box for the hostname
            singleHostnameTextBox = new TextBox();

            // Add items to the stack panel
            singleStackPanel.Children.Add(singleHostnameTextBlock);
            singleStackPanel.Children.Add(singleHostnameTextBox);

            // Create the Back and Next buttons to provide user flow control
            this.singleBackBtn = new Button();
            this.singleNextBtn = new Button();

            // Set the Back Button's properties
            this.singleBackBtn.Content = "Back";
            this.singleBackBtn.Width   = 90;
            this.singleBackBtn.Height  = 25;
            this.singleBackBtn.Click  += backBtn_Click;

            // Set the Next button's properties
            this.singleNextBtn.Content = "Save and Close";
            this.singleNextBtn.Width   = 90;
            this.singleNextBtn.Height  = 25;
            this.singleNextBtn.Click  += nextBtn_Click;

            // Set positions of GUI objects in the grid
            Grid.SetColumn(    this.singleInstructionsTextBlock, 0);
            Grid.SetRow(       this.singleInstructionsTextBlock, 0);
            Grid.SetColumnSpan(this.singleInstructionsTextBlock, 4);
            Grid.SetColumn(    this.singleStackPanel,            1);
            Grid.SetRow(       this.singleStackPanel,            1);
            Grid.SetColumnSpan(this.singleStackPanel,            3);
            Grid.SetColumn(    this.singleBackBtn,               2);
            Grid.SetRow(       this.singleBackBtn,               2);
            Grid.SetColumn(    this.singleNextBtn,               3);
            Grid.SetRow(       this.singleNextBtn,               2);

            // Add all GUI objects to the grid
            this.computerAddWindows[3].Children.Add(this.singleInstructionsTextBlock);
            this.computerAddWindows[3].Children.Add(this.singleStackPanel);
            this.computerAddWindows[3].Children.Add(this.singleBackBtn);
            this.computerAddWindows[3].Children.Add(this.singleNextBtn);
            #endregion
        }

        void selectFileBrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            if(selectFileDialogBox.ShowDialog() == true)
            {
                selectFileTextBox.Text = selectFileDialogBox.FileName;
            }
        }

        void backBtn_Click(object sender, RoutedEventArgs e)
        {
            // If the user wanted to add a single computer
            if (singleRadioBtn.IsChecked == true)
            {
                // Go to the first frame
                this.currentFrame = 0;
                this.Content      = this.computerAddWindows[this.currentFrame];
            }
            else
            {
                // Go to the previous frame
                this.currentFrame--;
                this.Content = this.computerAddWindows[this.currentFrame];
            }
        }

        void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            // Depending on which frame the user is on
            switch (currentFrame)
            {
                // Initial page (single or import)
                case 0:
                    // If the user wanted to add a single computer
                    if (singleRadioBtn.IsChecked == true)
                    {
                        // Go to the last frame
                        this.currentFrame = this.computerAddWindows.Count - 1;
                        this.Content      = this.computerAddWindows[this.currentFrame];
                    }
                    // If the user wanted to import a list of computers
                    else if (importRadioBtn.IsChecked == true)
                    {
                        // Go to the next frame
                        this.currentFrame++;
                        this.Content = this.computerAddWindows[this.currentFrame];
                    }
                    // If the user has not selected an option
                    else
                    {
                        // Ask the user to select an option
                        Xceed.Wpf.Toolkit.MessageBox.Show("Please select an option", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    break;

                // Import - select a file
                case 1:
                    // If the user has provided a file and the file exists
                    if (selectFileTextBox.Text != "" && System.IO.File.Exists(selectFileTextBox.Text))
                    {
                        // Begin importing the data
                        List<ComputerAddResult> results = ObjectManagement.AddMultipleObjects<ComputerAddResult>(selectFileTextBox.Text, "computer");

                        // Create the data grid for the user to review the results
                        this.reviewDataGrid = new DataGrid();

                        // Set the properties of the data grid
                        this.reviewDataGrid.ItemsSource         = results;
                        this.reviewDataGrid.IsReadOnly          = true;
                        this.reviewDataGrid.SelectionUnit       = DataGridSelectionUnit.FullRow;
                        this.reviewDataGrid.SelectionMode       = DataGridSelectionMode.Extended;
                        this.reviewDataGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
                        this.reviewDataGrid.VerticalAlignment   = VerticalAlignment.Stretch;
                        this.reviewDataGrid.RowHeaderWidth      = 20;
                        this.reviewDataGrid.CanUserResizeRows   = false;
                        this.reviewDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.Horizontal;
                        this.reviewDataGrid.MaxHeight           = 300;
                        this.reviewDataGrid.Height              = 300;
                        this.reviewDataGrid.MaxWidth            = 430;
                        this.reviewDataGrid.Width               = 430;

                        // Clear the stack panel and add the new data grid
                        this.reviewStackPanel.Children.Clear();
                        this.reviewStackPanel.Children.Add(this.reviewDataGrid);

                        // Go to the next frame
                        this.currentFrame++;
                        this.Content = this.computerAddWindows[this.currentFrame];
                    }
                    // If the user has not selected an option
                    else
                    {
                        // Ask the user to select an option
                        Xceed.Wpf.Toolkit.MessageBox.Show("Please provide a valid path to a CSV file to import", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    break;

                // Import - Review
                case 2:
                    // Close the window
                    this.Close();

                    break;

                // Single
                case 3:
                    // Create the object to be added
                    ComputerAddResult singleComputerToAdd = new ComputerAddResult()
                    {
                        title_Property = this.singleHostnameTextBox.Text
                    };

                    
                    // Attempt to add the object to Active Directory
                    string message = ObjectManagement.AddSingleObject<ComputerAddResult>(singleComputerToAdd, "computer");

                    // If the program was able to add the object
                    if (message == "Success")
                    {
                        // Display a success message
                        Xceed.Wpf.Toolkit.MessageBox.Show("The computer " + singleHostnameTextBox.Text + " has been added to the domain", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                   
                        // Close the window
                        this.Close();
                    }
                    // If the program failed to add the object
                    else
                    {
                        // Display an error message
                        Xceed.Wpf.Toolkit.MessageBox.Show(message, this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    break;
            }
        }
    }
}
