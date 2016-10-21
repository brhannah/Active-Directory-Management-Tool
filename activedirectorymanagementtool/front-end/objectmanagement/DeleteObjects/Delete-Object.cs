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
    public class DeleteObject : Window
    {
        // Create an integer to keep track of the current window
        private int currentFrame = 0;

        // Declare class objects
        private List<Grid>  deleteObjectWindows;
        private TextBlock   reviewInstructionsTextBlock;
        private StackPanel  reviewStackPanel;
        private DataGrid    reviewDataGrid;
        private Button      reviewCloseBtn;

        public DeleteObject()
        {
            // Create the list of grids to place GUI objects
            this.deleteObjectWindows = new List<Grid>();

            // Create the pages for the wizard
            this.deleteObjectWindows.Add(new Grid()); // Review deleted objects

            // For each grid
            foreach (Grid computerAddWindow in this.deleteObjectWindows)
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
            this.Content               = this.deleteObjectWindows[currentFrame];
            this.Width                 = 500;
            this.Height                = 500;
            this.Title                 = "Delete Objects";
            this.ResizeMode            = ResizeMode.NoResize;
            this.WindowState           = WindowState.Normal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Background            = Brushes.WhiteSmoke;
            this.Topmost               = false;
            this.Activate();
            this.ShowActivated         = true;
            this.Icon                  = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            #region // Review deleted objects
            // Create a textblock for instructions
            this.reviewInstructionsTextBlock = new TextBlock();

            // Provide the user with instructions
            this.reviewInstructionsTextBlock.Text         = "The deletion process is complete!\n\nPlease review the deleted objects and observe any error messages:";
            this.reviewInstructionsTextBlock.TextWrapping = TextWrapping.Wrap;

            // Create a panel for the instructions and data grid
            this.reviewStackPanel = new StackPanel();

            // Create the data grid
            this.reviewDataGrid = new DataGrid();

            // Set the properties of the data grid
            this.reviewDataGrid.ItemsSource         = ObjectManagement.DeleteMultipleObjects();
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

            // Add the data grid to the stack panel
            this.reviewStackPanel.Children.Add(this.reviewDataGrid);

            // Create the close button
            this.reviewCloseBtn = new Button();

            // Set the Close button's properties
            this.reviewCloseBtn.Content = "Close";
            this.reviewCloseBtn.Width   = 90;
            this.reviewCloseBtn.Height  = 25;
            this.reviewCloseBtn.Click  += nextBtn_Click;

            // Set positions of GUI objects in the grid
            Grid.SetColumn(    this.reviewInstructionsTextBlock, 0);
            Grid.SetRow(       this.reviewInstructionsTextBlock, 0);
            Grid.SetColumnSpan(this.reviewInstructionsTextBlock, 4);
            Grid.SetColumn(    this.reviewStackPanel,            1);
            Grid.SetRow(       this.reviewStackPanel,            1);
            Grid.SetColumnSpan(this.reviewStackPanel,            3);
            Grid.SetColumn(    this.reviewCloseBtn,               3);
            Grid.SetRow(       this.reviewCloseBtn,               2);

            // Add all GUI objects to the grid
            this.deleteObjectWindows[0].Children.Add(this.reviewInstructionsTextBlock);
            this.deleteObjectWindows[0].Children.Add(this.reviewStackPanel);
            this.deleteObjectWindows[0].Children.Add(this.reviewCloseBtn);
            #endregion
        }

        void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            // Close the window
            this.Close();
        }
    }
}
