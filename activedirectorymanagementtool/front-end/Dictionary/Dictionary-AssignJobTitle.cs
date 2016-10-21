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
    public class AssignJobTitle : Window
    {
        // Create an integer to keep track of the current window
        private int currentFrame = 0;

        // Declare class objects
        private List<Grid>  assignJobTitleWindows;
        private TextBlock   initialInstructionsTextBlock;
        private TextBlock   reviewInstructionsTextBlock;
        private StackPanel  initialWindowStackPanel;
        private StackPanel  reviewStackPanel;
        private ComboBox    jobTitlesComboBox;
        private DataGrid    reviewDataGrid;
        private Button      initialBackBtn;
        private Button      initialNextBtn;
        private Button      reviewBackBtn;
        private Button      reviewNextBtn;

        public AssignJobTitle()
        {
            // Set initial window properties
            this.Title = "Assign a Job Title";
            this.Hide();

            // Get a copy of the dictionary
            DictionaryManagement dictionary = new DictionaryManagement();

            // If there are dictionary terms
            if (dictionary.terms.Count > 0)
            {
                // If any of the selected objects are users
                if (BackEnd.selectedADObjs_Property.Any(x => x.schemaClass == "user"))
                {
                    // Create the list of grids to place GUI objects
                    this.assignJobTitleWindows = new List<Grid>();
                    
                    // Create the pages for the wizard
                    this.assignJobTitleWindows.Add(new Grid()); // Initial page (Select Job Title)
                    this.assignJobTitleWindows.Add(new Grid()); // Review
                    
                    // For each grid
                    foreach (Grid assignJobTitleWindow in this.assignJobTitleWindows)
                    {
                        // Set grid's properties
                        assignJobTitleWindow.Margin = new Thickness(10);
                    
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
                        assignJobTitleWindow.ColumnDefinitions.Add(blankCol);
                        assignJobTitleWindow.ColumnDefinitions.Add(contentCol);
                        assignJobTitleWindow.ColumnDefinitions.Add(backCol);
                        assignJobTitleWindow.ColumnDefinitions.Add(nextCol);
                    
                        // Create grid rows
                        RowDefinition instructionRow = new RowDefinition();
                        RowDefinition contentRow     = new RowDefinition();
                        RowDefinition controlRow     = new RowDefinition();
                    
                        // Set row properties
                        instructionRow.Height = new GridLength(100);
                        contentRow.Height     = new GridLength(305);
                        controlRow.Height     = new GridLength(25);
                    
                        // Add rows to grid
                        assignJobTitleWindow.RowDefinitions.Add(instructionRow);
                        assignJobTitleWindow.RowDefinitions.Add(contentRow);
                        assignJobTitleWindow.RowDefinitions.Add(controlRow);
                    }
                    
                    // Set the properties of the window
                    this.Content               = this.assignJobTitleWindows[currentFrame];
                    this.Width                 = 500;
                    this.Height                = 500;
                    this.ResizeMode            = ResizeMode.NoResize;
                    this.WindowState           = WindowState.Normal;
                    this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    this.Background            = Brushes.WhiteSmoke;
                    this.Topmost               = false;
                    this.Activate();
                    this.ShowActivated         = true;
                    this.Icon                  = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

                    #region Initial page (Select Job Title)
                    // Create a textblock for instructions
                    this.initialInstructionsTextBlock = new TextBlock();
                    
                    // Provide the user with instructions
                    this.initialInstructionsTextBlock.Text         = "When the selected users are assigned a job title, the user will be joined to the groups that a person with that job title needs to be a member of.\n\nNote: This will not remove the selected users from the groups they are currently joined to.\n\nPlease assign a job title to the selected users:";
                    this.initialInstructionsTextBlock.TextWrapping = TextWrapping.Wrap;

                    // Create a combo box for the job titles
                    jobTitlesComboBox = new ComboBox();

                    // Set the properties for the combo box
                    jobTitlesComboBox.ItemsSource   = dictionary.terms.Keys;
                    jobTitlesComboBox.Margin        = new Thickness(10);
                    jobTitlesComboBox.SelectedIndex = 0;

                    // Create a panel for the radio button objects
                    this.initialWindowStackPanel = new StackPanel();
                    
                    // Add the radio buttons to the panel
                    this.initialWindowStackPanel.Children.Add(this.jobTitlesComboBox);
                    
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
                    Grid.SetColumn(    this.initialInstructionsTextBlock, 0);
                    Grid.SetRow(       this.initialInstructionsTextBlock, 0);
                    Grid.SetColumnSpan(this.initialInstructionsTextBlock, 4);
                    Grid.SetColumn(    this.initialWindowStackPanel,      1);
                    Grid.SetRow(       this.initialWindowStackPanel,      1);
                    Grid.SetColumnSpan(this.initialWindowStackPanel,      3);
                    Grid.SetColumn(    this.initialBackBtn,               2);
                    Grid.SetRow(       this.initialBackBtn,               2);
                    Grid.SetColumn(    this.initialNextBtn,               3);
                    Grid.SetRow(       this.initialNextBtn,               2);
                    
                    // Add all GUI objects to the grid
                    this.assignJobTitleWindows[0].Children.Add(this.initialInstructionsTextBlock);
                    this.assignJobTitleWindows[0].Children.Add(this.initialWindowStackPanel);
                    this.assignJobTitleWindows[0].Children.Add(this.initialBackBtn);
                    this.assignJobTitleWindows[0].Children.Add(this.initialNextBtn);
                    #endregion
                    
                    #region // Review
                    // Create a textblock for instructions
                    this.reviewInstructionsTextBlock = new TextBlock();
                    
                    // Provide the user with instructions
                    this.reviewInstructionsTextBlock.Text         = "The job titles have been applied!\n\nPlease review the users and group memberships below and observe any error messages:";
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
                    this.assignJobTitleWindows[1].Children.Add(this.reviewInstructionsTextBlock);
                    this.assignJobTitleWindows[1].Children.Add(this.reviewStackPanel);
                    this.assignJobTitleWindows[1].Children.Add(this.reviewBackBtn);
                    this.assignJobTitleWindows[1].Children.Add(this.reviewNextBtn);
                    #endregion
                }
                // If none of the selected objects are users
                else
                {
                    // Ask the user to select some users
                    Xceed.Wpf.Toolkit.MessageBox.Show("Please select users before attempting to assign job titles", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                    // Close the window
                    this.Close();
                }
            }
            // If there are no dictionary terms
            else
            {
                // Ask the user to select some users
                Xceed.Wpf.Toolkit.MessageBox.Show("Please add a job titles to the dictionary", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);

                // Close the window
                this.Close();
            }
        }

        void backBtn_Click(object sender, RoutedEventArgs e)
        {
            // Go to the previous frame
            this.currentFrame--;
            this.Content = this.assignJobTitleWindows[this.currentFrame];
        }

        void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            // Depending on which frame the user is on
            switch (currentFrame)
            {
                // Initial page (Select Job Title)
                case 0:
                    // Begin importing the data
                    List<GroupJoinResult> results = ObjectManagement.AssignJobTitles(jobTitlesComboBox.SelectedValue.ToString());

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
                    this.Content = this.assignJobTitleWindows[this.currentFrame];

                    break;

                // Review
                case 1:
                    // Close the window
                    this.Close();

                    break;
            }
        }
    }
}
