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
    public class UserAdd : Window
    {
        // Create an integer to keep track of the current window
        private int currentFrame = 0;

        // Declare class objects
        private List<Grid>  userAddWindows;
        private TextBlock   initialInstructionsTextBlock;
        private TextBlock   selectFileInstructionsTextBlock;
        private TextBlock   reviewInstructionsTextBlock;
        private TextBlock   singleInstructionsTextBlock;
        private TextBlock   singleUserTextBlock;
        private TextBlock   singleFirstNameTextBlock;
        private TextBlock   singleLastNameTextBlock;
        private TextBlock   singlePassword1TextBlock;
        private TextBlock   singlePassword2TextBlock;
        private StackPanel  initialRadioButtonsStackPanel;
        private StackPanel  selectFileStackPanel;
        private StackPanel  reviewStackPanel;
        private StackPanel  singleStackPanel;
        private RadioButton singleRadioBtn;
        private RadioButton importRadioBtn;
        private Microsoft.Win32.OpenFileDialog selectFileDialogBox;
        private TextBox     selectFileTextBox;
        private TextBox     singleUserTextBox;
        private TextBox     singleFirstNameTextBox;
        private TextBox     singleLastNameTextBox;
        private PasswordBox singlePassword1PasswordBox;
        private PasswordBox singlePassword2PasswordBox;
        private CheckBox    singleChangePWAtLogonCheckBox;
        private CheckBox    singleCannotChangePWCheckBox;
        private CheckBox    singlePWNeverExpiresCheckBox;
        private CheckBox    singleAccountIsDisabledCheckBox;
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

        public UserAdd()
        {
            // Create the list of grids to place GUI objects
            this.userAddWindows = new List<Grid>();

            // Create the pages for the wizard
            this.userAddWindows.Add(new Grid()); // Initial page (single or import?)
            this.userAddWindows.Add(new Grid()); // Import - Select a file
            this.userAddWindows.Add(new Grid()); // Import - Review
            this.userAddWindows.Add(new Grid()); // Single

            // For each grid
            foreach (Grid userAddWindow in this.userAddWindows)
            {
                // Set grid's properties
                userAddWindow.Margin = new Thickness(10);

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
                userAddWindow.ColumnDefinitions.Add(blankCol);
                userAddWindow.ColumnDefinitions.Add(contentCol);
                userAddWindow.ColumnDefinitions.Add(backCol);
                userAddWindow.ColumnDefinitions.Add(nextCol);

                // Create grid rows
                RowDefinition contentRow     = new RowDefinition();
                RowDefinition controlRow     = new RowDefinition();

                // Set row properties
                contentRow.Height     = new GridLength(405);
                controlRow.Height     = new GridLength(25);

                // Add rows to grid
                userAddWindow.RowDefinitions.Add(contentRow);
                userAddWindow.RowDefinitions.Add(controlRow);
            }

            // Set the properties of the window
            this.Content               = this.userAddWindows[currentFrame];
            this.Width                 = 500;
            this.Height                = 500;
            this.Title                 = "Add User(s)";
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
            this.initialInstructionsTextBlock.Text         = "Would you like to add a single user or import a list of users?\n\n ";
            this.initialInstructionsTextBlock.TextWrapping = TextWrapping.Wrap;

            // Create a panel for the radio button objects
            this.initialRadioButtonsStackPanel = new StackPanel();

            // Create the radio button objects
            this.singleRadioBtn = new RadioButton();
            this.importRadioBtn = new RadioButton();

            // Group the radio buttons
            this.singleRadioBtn.GroupName = "userAdd";
            this.importRadioBtn.GroupName = "userAdd";

            // Add text to the radio buttons
            this.singleRadioBtn.Content = "Add a single user";
            this.importRadioBtn.Content = "Import a list of users";

            // Add space between the radio buttons
            this.singleRadioBtn.Margin = new Thickness(10);
            this.importRadioBtn.Margin = new Thickness(10);

            // Add the objects to the panel
            this.initialRadioButtonsStackPanel.Children.Add(this.initialInstructionsTextBlock);
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
            Grid.SetColumn(    this.initialRadioButtonsStackPanel, 0);
            Grid.SetRow(       this.initialRadioButtonsStackPanel, 0);
            Grid.SetColumnSpan(this.initialRadioButtonsStackPanel, 4);
            Grid.SetColumn(    this.initialBackBtn,                2);
            Grid.SetRow(       this.initialBackBtn,                1);
            Grid.SetColumn(    this.initialNextBtn,                3);
            Grid.SetRow(       this.initialNextBtn,                1);

            // Add all GUI objects to the grid
            this.userAddWindows[0].Children.Add(this.initialRadioButtonsStackPanel);
            this.userAddWindows[0].Children.Add(this.initialBackBtn);
            this.userAddWindows[0].Children.Add(this.initialNextBtn);
            #endregion

            #region // Import - Select a file
            // Create a textblock for instructions
            this.selectFileInstructionsTextBlock = new TextBlock();

            // Provide the user with instructions
            this.selectFileInstructionsTextBlock.Text = "When importing users using a CSV file, the CSV file should be formatted to have eight columns containing the user name, first name, last name, password, and the true or false values that determine if the user must change their password at next logon, if the user cannot change their password, if the password never expires, and if the account is disabled.\n\nPlease select the CSV file containing the list of users to import:\n(Note: The users will be created in ";
            this.selectFileInstructionsTextBlock.Inlines.Add(new System.Windows.Documents.Bold(new System.Windows.Documents.Run(BackEnd.LDAPPath_Property.Replace("LDAP://", "").Replace(BackEnd.DomainName_Property + "/", "").Split(',')[0].Remove(0, (BackEnd.LDAPPath_Property.EndsWith(BackEnd.DomainName_Property)) ? 0 : 3))));
            this.selectFileInstructionsTextBlock.Inlines.Add(")\n\n ");
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
            this.selectFileStackPanel.Children.Add(this.selectFileInstructionsTextBlock);
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
            Grid.SetColumn(    this.selectFileStackPanel, 0);
            Grid.SetRow(       this.selectFileStackPanel, 0);
            Grid.SetColumnSpan(this.selectFileStackPanel, 4);
            Grid.SetColumn(    this.selectFileBackBtn,    2);
            Grid.SetRow(       this.selectFileBackBtn,    1);
            Grid.SetColumn(    this.selectFileNextBtn,    3);
            Grid.SetRow(       this.selectFileNextBtn,    1);

            // Add all GUI objects to the grid
            this.userAddWindows[1].Children.Add(this.selectFileStackPanel);
            this.userAddWindows[1].Children.Add(this.selectFileBackBtn);
            this.userAddWindows[1].Children.Add(this.selectFileNextBtn);
            #endregion

            #region // Import - Review
            // Create a textblock for instructions
            this.reviewInstructionsTextBlock = new TextBlock();

            // Provide the user with instructions
            this.reviewInstructionsTextBlock.Text         = "The import is complete!\n\nPlease review the imported users and observe any error messages:\n\n ";
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
            Grid.SetColumn(    this.reviewStackPanel, 0);
            Grid.SetRow(       this.reviewStackPanel, 0);
            Grid.SetColumnSpan(this.reviewStackPanel, 4);
            Grid.SetColumn(    this.reviewBackBtn,    2);
            Grid.SetRow(       this.reviewBackBtn,    1);
            Grid.SetColumn(    this.reviewNextBtn,    3);
            Grid.SetRow(       this.reviewNextBtn,    1);

            // Add all GUI objects to the grid
            this.userAddWindows[2].Children.Add(this.reviewStackPanel);
            this.userAddWindows[2].Children.Add(this.reviewBackBtn);
            this.userAddWindows[2].Children.Add(this.reviewNextBtn);
            #endregion

            #region // Single
            // Create a textblock for instructions
            this.singleInstructionsTextBlock = new TextBlock();

            // Provide the user with instructions
            this.singleInstructionsTextBlock.Text         = "Please provide the user's information:\n(Note: The user will be created in ";
            this.singleInstructionsTextBlock.Inlines.Add(new System.Windows.Documents.Bold(new System.Windows.Documents.Run(BackEnd.LDAPPath_Property.Replace("LDAP://", "").Replace(BackEnd.DomainName_Property + "/", "").Split(',')[0].Remove(0, (BackEnd.LDAPPath_Property.EndsWith(BackEnd.DomainName_Property)) ? 0 : 3))));
            this.singleInstructionsTextBlock.Inlines.Add(")\n\n ");
            this.singleInstructionsTextBlock.TextWrapping = TextWrapping.Wrap;
            
            // Create a panel for the GUI objects
            this.singleStackPanel = new StackPanel();

            // Create the labels for the text boxes
            this.singleFirstNameTextBlock = new TextBlock();
            this.singleLastNameTextBlock  = new TextBlock();
            this.singleUserTextBlock      = new TextBlock();
            this.singlePassword1TextBlock = new TextBlock();
            this.singlePassword2TextBlock = new TextBlock();

            // Set the properties for the text blocks
            this.singleFirstNameTextBlock.Text = "First Name:";
            this.singleLastNameTextBlock.Text  = "Last Name:";
            this.singleUserTextBlock.Text      = "User Name:";
            this.singlePassword1TextBlock.Text = "Password:";
            this.singlePassword2TextBlock.Text = "Re-Type Password:";

            // Create the text boxes
            this.singleFirstNameTextBox     = new TextBox();
            this.singleLastNameTextBox      = new TextBox();
            this.singleUserTextBox          = new TextBox();
            this.singlePassword1PasswordBox = new PasswordBox();
            this.singlePassword2PasswordBox = new PasswordBox();

            // Set the properties of the text boxes
            this.singleFirstNameTextBox.Margin     = new Thickness(5);
            this.singleLastNameTextBox.Margin      = new Thickness(5);
            this.singleUserTextBox.Margin          = new Thickness(5);
            this.singlePassword1PasswordBox.Margin = new Thickness(5);
            this.singlePassword2PasswordBox.Margin = new Thickness(5);

            // Create the check boxes
            this.singleChangePWAtLogonCheckBox   = new CheckBox();
            this.singleCannotChangePWCheckBox    = new CheckBox();
            this.singlePWNeverExpiresCheckBox    = new CheckBox();
            this.singleAccountIsDisabledCheckBox = new CheckBox();

            // Set the properties of the check boxes
            this.singleChangePWAtLogonCheckBox.Content   = "User must change password at next logon";
            this.singleChangePWAtLogonCheckBox.Margin    = new Thickness(5);
            this.singleCannotChangePWCheckBox.Content    = "User cannot change password";
            this.singleCannotChangePWCheckBox.Margin     = new Thickness(5);
            this.singlePWNeverExpiresCheckBox.Content    = "Password never expires";
            this.singlePWNeverExpiresCheckBox.Margin     = new Thickness(5);
            this.singleAccountIsDisabledCheckBox.Content = "Account is disabled";
            this.singleAccountIsDisabledCheckBox.Margin  = new Thickness(5);

            // Add items to the stack panel
            this.singleStackPanel.Children.Add(this.singleInstructionsTextBlock);
            this.singleStackPanel.Children.Add(this.singleFirstNameTextBlock);
            this.singleStackPanel.Children.Add(this.singleFirstNameTextBox);
            this.singleStackPanel.Children.Add(this.singleLastNameTextBlock);
            this.singleStackPanel.Children.Add(this.singleLastNameTextBox);
            this.singleStackPanel.Children.Add(this.singleUserTextBlock);
            this.singleStackPanel.Children.Add(this.singleUserTextBox);
            this.singleStackPanel.Children.Add(this.singlePassword1TextBlock);
            this.singleStackPanel.Children.Add(this.singlePassword1PasswordBox);
            this.singleStackPanel.Children.Add(this.singlePassword2TextBlock);
            this.singleStackPanel.Children.Add(this.singlePassword2PasswordBox);
            this.singleStackPanel.Children.Add(this.singleChangePWAtLogonCheckBox);
            this.singleStackPanel.Children.Add(this.singleCannotChangePWCheckBox);
            this.singleStackPanel.Children.Add(this.singlePWNeverExpiresCheckBox);
            this.singleStackPanel.Children.Add(this.singleAccountIsDisabledCheckBox);

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
            Grid.SetColumn(    this.singleStackPanel, 0);
            Grid.SetRow(       this.singleStackPanel, 0);
            Grid.SetColumnSpan(this.singleStackPanel, 4);
            Grid.SetColumn(    this.singleBackBtn,    2);
            Grid.SetRow(       this.singleBackBtn,    1);
            Grid.SetColumn(    this.singleNextBtn,    3);
            Grid.SetRow(       this.singleNextBtn,    1);

            // Add all GUI objects to the grid
            this.userAddWindows[3].Children.Add(this.singleStackPanel);
            this.userAddWindows[3].Children.Add(this.singleBackBtn);
            this.userAddWindows[3].Children.Add(this.singleNextBtn);
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
            // If the user wanted to add a single user
            if (singleRadioBtn.IsChecked == true)
            {
                // Go to the first frame
                this.currentFrame = 0;
                this.Content      = this.userAddWindows[this.currentFrame];
            }
            else
            {
                // Go to the previous frame
                this.currentFrame--;
                this.Content = this.userAddWindows[this.currentFrame];
            }
        }

        void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            // Depending on which frame the user is on
            switch (currentFrame)
            {
                // Initial page (single or import)
                case 0:
                    // If the user wanted to add a single user
                    if (singleRadioBtn.IsChecked == true)
                    {
                        // Go to the last frame
                        this.currentFrame = this.userAddWindows.Count - 1;
                        this.Content      = this.userAddWindows[this.currentFrame];
                    }
                    // If the user wanted to import a list of users
                    else if (importRadioBtn.IsChecked == true)
                    {
                        // Go to the next frame
                        this.currentFrame++;
                        this.Content = this.userAddWindows[this.currentFrame];
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
                        List<UserAddResult> results = ObjectManagement.AddMultipleObjects<UserAddResult>(selectFileTextBox.Text, "user");

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
                        this.reviewStackPanel.Children.Add(this.reviewInstructionsTextBlock);
                        this.reviewStackPanel.Children.Add(this.reviewDataGrid);

                        // Go to the next frame
                        this.currentFrame++;
                        this.Content = this.userAddWindows[this.currentFrame];
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
                    // If the user has provided enough accurate information
                    if (this.singleUserTextBox.Text              != "" &&
                        this.singleFirstNameTextBox.Text         != "" &&
                        this.singleLastNameTextBox.Text          != "" &&
                        this.singlePassword1PasswordBox.Password != "" &&
                        this.singlePassword1PasswordBox.Password == this.singlePassword2PasswordBox.Password)
                    {
                        // Create the object to be added
                        UserAddResult singleUserToAdd = new UserAddResult()
                        {
                            title_Property                     = this.singleUserTextBox.Text,
                            firstName_Property                 = this.singleFirstNameTextBox.Text,
                            lastName_Property                  = this.singleLastNameTextBox.Text,
                            password                           = this.singlePassword1PasswordBox.Password,
                            changePasswordAtNextLogon_Property = this.singleChangePWAtLogonCheckBox.IsChecked.Value,
                            cannotChangePassword_Property      = this.singleCannotChangePWCheckBox.IsChecked.Value,
                            passwordNeverExpires_Property      = this.singlePWNeverExpiresCheckBox.IsChecked.Value,
                            accountIsDisabled_Property         = this.singleAccountIsDisabledCheckBox.IsChecked.Value
                        };

                        // Attempt to add the object to Active Directory
                        string message = ObjectManagement.AddSingleObject<UserAddResult>(singleUserToAdd, "user");

                        // If the program was able to add the object
                        if (message.StartsWith("Success"))
                        {
                            // If the user was added with conflicting settings
                            if (message != "Success")
                            {
                                // Display a success message
                                Xceed.Wpf.Toolkit.MessageBox.Show(message.Replace("Success - ", ""), this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                            }

                            // Display a success message
                            Xceed.Wpf.Toolkit.MessageBox.Show("The user " + singleUserTextBox.Text + " has been added to the domain", this.Title, MessageBoxButton.OK, MessageBoxImage.Information);

                            // Close the window
                            this.Close();
                        }
                        // If the program failed to add the object
                        else
                        {
                            // Create the user's distinguished name
                            string distinguishedName = "CN=" + this.singleFirstNameTextBox.Text + " " + this.singleLastNameTextBox.Text + "," + BackEnd.LDAPPath_Property.Split('/').Last();

                            // Attempt to delete the user
                            string result = ObjectManagement.DeleteSingleObject(distinguishedName, this.singleFirstNameTextBox.Text + " " + this.singleLastNameTextBox.Text);

                            // Display an error message
                            Xceed.Wpf.Toolkit.MessageBox.Show(message, this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    // If the user has not provided enough accurate information
                    else
                    {
                        // If the passwords do not match
                        if (this.singlePassword1PasswordBox.Password != this.singlePassword2PasswordBox.Password)
                        {
                            // Notify the user that the passwords do not match
                            Xceed.Wpf.Toolkit.MessageBox.Show("The passwords do not match", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        // If the passwords do match
                        else
                        {
                            // Ask the user to fill in all fields
                            Xceed.Wpf.Toolkit.MessageBox.Show("Please provide a first name, last name, username, and password", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    break;
            }
        }
    }
}
