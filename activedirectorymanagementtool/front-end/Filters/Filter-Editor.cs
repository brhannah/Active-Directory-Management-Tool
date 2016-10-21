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
    public class FilterEditor : Window
    {
        // Declare class variables
        DockPanel                                   filterEditorDockPanel;
        Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid propertyEditor;
        DockPanel                                   propertyDockPanel;
        ScrollViewer                                gridScrollViewer;
        Grid                                        filterGrid;
        List<List<object>>                          filter;
        FilterManagement                            filters;
        string                                      filterName;
        ComboBox                                    attribComboBox;
        System.Windows.Data.Binding                 attributeBinding;
        System.Windows.Data.Binding                 operatorOptionsBinding;
        System.Windows.Data.Binding                 selectedOperatorBinding;
        DataTemplate                                textBlockDataTemplate;

        public FilterEditor(FilterManagement filterInstance, string filterName)
        {
            // Allow the instance of the filter to be accessed in functions
            this.filters    = filterInstance;
            this.filterName = filterName;
            this.filter     = filterInstance.filters_Property[filterName];

            // Create a panel to place the UI objects
            filterEditorDockPanel = new DockPanel();

            // Set window properties
            this.Title                 = "Filter Editor";
            this.Width                 = 900;
            this.MinWidth              = 900;
            this.Height                = 500;
            this.MinHeight             = 500;
            this.WindowState           = System.Windows.WindowState.Normal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Content               = filterEditorDockPanel;
            this.Background            = Brushes.WhiteSmoke;
            this.Topmost               = false;
            this.Activate();
            this.ShowActivated         = true;
            this.Closing += FilterEditor_Closing;
            this.Icon                  = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            // Create a panel for the property editor
            propertyDockPanel = new DockPanel();

            // Set the properties for the panel
            propertyDockPanel.Width               = 400;
            propertyDockPanel.VerticalAlignment   = System.Windows.VerticalAlignment.Stretch;
            propertyDockPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

            // Create a place to edit the values
            propertyEditor = new Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid();

            // Set the properties for the property editor
            propertyEditor.AutoGenerateProperties = false;
            propertyEditor.VerticalAlignment      = System.Windows.VerticalAlignment.Stretch;
            propertyEditor.HorizontalAlignment    = System.Windows.HorizontalAlignment.Stretch;

            // Set the position on the panel
            DockPanel.SetDock(propertyEditor, Dock.Right);

            // Add the property editor to the panel
            propertyDockPanel.Children.Add(propertyEditor);

            // Create a panel to place the add attribute section
            StackPanel addAttribStackPanel = new StackPanel();

            // Set the properties of the panel
            addAttribStackPanel.Orientation       = Orientation.Horizontal;
            addAttribStackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            // Create a Textblock for the add attribute section
            TextBlock addAttribTextBlock = new TextBlock();

            // Set the properties for the addAttrib text block
            addAttribTextBlock.Text              = "Select an Attribute:";
            addAttribTextBlock.Height            = 25;
            addAttribTextBlock.Margin            = new Thickness(5);
            addAttribTextBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            // Create a combobox for the user to select an attribute to add
            attribComboBox = new ComboBox();

            // Set the properties of the combobox
            attribComboBox.ItemsSource   = FilterManagement.attributes;
            attribComboBox.Height        = 25;
            attribComboBox.Width         = 250;
            attribComboBox.Margin        = new Thickness(5);
            attribComboBox.SelectedIndex = 0;

            // Create a button to add the selected attribute to the filter
            Button addAttribButton = new Button();

            // Set the properties of the button
            addAttribButton.Content = "Add Attribute";
            addAttribButton.Height  = 25;
            addAttribButton.Width   = 100;
            addAttribButton.Margin  = new Thickness(5);
            addAttribButton.Tag     = attribComboBox;
            addAttribButton.Click  += addAttribButton_Click;

            // Add UI objects to the panel
            addAttribStackPanel.Children.Add(addAttribTextBlock);
            addAttribStackPanel.Children.Add(attribComboBox);
            addAttribStackPanel.Children.Add(addAttribButton);

            // Create a Scrollviewer for the grid
            gridScrollViewer = new ScrollViewer();

            // Create a grid that will represent the filter
            filterGrid = new Grid();

            // Set the properties of the scrollviewer
            gridScrollViewer.HorizontalAlignment           = System.Windows.HorizontalAlignment.Stretch;
            gridScrollViewer.VerticalAlignment             = System.Windows.VerticalAlignment.Stretch;
            gridScrollViewer.Content                       = filterGrid;
            gridScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            gridScrollViewer.VerticalScrollBarVisibility   = ScrollBarVisibility.Visible;

            // For each column
            for (int i = 0; i < filter.Count; i++)
            {
                // If there is not an OR column
                if (this.filterGrid.ColumnDefinitions.Count == 0)
                {
                    // Add the OR column
                    this.filterGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(25) });
                }

                // If the heading row has not been made yet
                if (this.filterGrid.RowDefinitions.Count == 0)
                {
                    // Add the row to the grid
                    filterGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
                }

                // Add the column to the grid
                filterGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });

                // Create a string to store the attribute's name
                string attrib = string.Empty;

                // Determine the type of object
                switch (filter[i][0].GetType().ToString())
                {
                    case "ActiveDirectoryManagementTool.StringFilter":
                        attrib = ((StringFilter)filter[i][0]).attribute_Property;
                        break;

                    case "ActiveDirectoryManagementTool.IntegerFilter":
                        attrib = ((IntegerFilter)filter[i][0]).attribute_Property;
                        break;

                    case "ActiveDirectoryManagementTool.DateTimeFilter":
                        attrib = ((DateTimeFilter)filter[i][0]).attribute_Property;
                        break;

                    case "ActiveDirectoryManagementTool.BooleanFilter":
                        attrib = ((BooleanFilter)filter[i][0]).attribute_Property;
                        break;
                }

                // Create a text block to store the attribute's name
                TextBlock attribNameTextBlock = new TextBlock();

                // Set the properties of the text block
                attribNameTextBlock.Text = attrib;

                // Set the attribute's name as the heading
                Grid.SetColumn(attribNameTextBlock, i + 1);
                Grid.SetRow(   attribNameTextBlock, 0);

                // Add the textblock to the grid
                this.filterGrid.Children.Add(attribNameTextBlock);

                // For each row
                for (int ii = 0; ii < filter[i].Count; ii++)
                {
                    // Starting at the third row in the grid (second element in the list)
                    if (ii == 1)
                    {
                        // Create an OR text block
                        TextBlock orTextBlock = new TextBlock();

                        // Set the properties for the text block
                        orTextBlock.Text                = "OR";
                        orTextBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        orTextBlock.VerticalAlignment   = System.Windows.VerticalAlignment.Center;

                        // Set the position on the grid
                        Grid.SetColumn(orTextBlock, 0);
                        Grid.SetRow(   orTextBlock, ii + 1);

                        // Add the OR text block to the grid
                        this.filterGrid.Children.Add(orTextBlock);
                    }

                    // Add the row to the grid
                    filterGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });

                    // Create a button for this row related to this object
                    Button cellButton = new Button();
                    
                    // Set the properties
                    cellButton.Tag        = (i).ToString() + "," + (ii).ToString();
                    cellButton.Background = Brushes.White;
                    cellButton.Click     += cellButton_Click;
                    cellButton.SetBinding(Button.ContentProperty, new System.Windows.Data.Binding("displayFilter_Property") { Source = this.filter[i][ii], Mode = System.Windows.Data.BindingMode.OneWay });

                    
                    // Set the button's position on the grid
                    Grid.SetColumn(cellButton, i  + 1);
                    Grid.SetRow(   cellButton, ii + 1);
                    
                    // Add the button to the grid
                    this.filterGrid.Children.Add(cellButton);
                }
            }

            // Create a panel for the buttons
            DockPanel buttonDockPanel = new DockPanel();

            // Set the properties for the panel
            buttonDockPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            buttonDockPanel.VerticalAlignment   = System.Windows.VerticalAlignment.Bottom;

            // Create a button to add an OR condition
            Button addOrButton = new Button();

            // Set the properties for the button
            addOrButton.Content             = "Add OR Condition";
            addOrButton.Height              = 25;
            addOrButton.Width               = 150;
            addOrButton.Margin              = new Thickness(5);
            addOrButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            addOrButton.Click              += addOrButton_Click;

            // Create a save button
            Button saveButton = new Button();

            // Set the properties for the button
            saveButton.Content             = "Close";
            saveButton.Height              = 25;
            saveButton.Width               = 100;
            saveButton.Margin              = new Thickness(5);
            saveButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            saveButton.Click              += saveButton_Click;

            // Set the position of the buttons
            DockPanel.SetDock(addOrButton, Dock.Left);
            DockPanel.SetDock(saveButton,  Dock.Right);

            // Add buttons to the panel
            buttonDockPanel.Children.Add(addOrButton);
            buttonDockPanel.Children.Add(saveButton);

            // Set the position of the UI objects on the panel
            DockPanel.SetDock(propertyDockPanel,   Dock.Right);
            DockPanel.SetDock(addAttribStackPanel, Dock.Top);
            DockPanel.SetDock(buttonDockPanel,     Dock.Bottom);
            DockPanel.SetDock(gridScrollViewer,    Dock.Top);

            // Add UI objects to the panel
            filterEditorDockPanel.Children.Add(propertyDockPanel);
            filterEditorDockPanel.Children.Add(addAttribStackPanel);
            filterEditorDockPanel.Children.Add(buttonDockPanel);
            filterEditorDockPanel.Children.Add(gridScrollViewer);
        }

        void FilterEditor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Save the filter
            saveButton_Click();
        }

        void addAttribButton_Click(object sender, RoutedEventArgs e)
        {
            // Add the attribute to the filter
            this.filters.AddAttributeToFilter(this.filterName, attribComboBox.SelectedValue.ToString().Split(',')[0].Replace("[", "").Trim(), attribComboBox.SelectedValue.ToString().Split(',')[1].Replace("]", "").Trim());

            // If this is the first attribute
            if (this.filterGrid.ColumnDefinitions.Count == 0)
            {
                // Add a column for the OR statement
                this.filterGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(25) });

                // Add a row for the header and the item itself
                this.filterGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
                this.filterGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
            }

            // Add a column for the attribute
            this.filterGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });

            // Creat a textblock to store the header
            TextBlock headerTextBlock = new TextBlock();

            // Set the properties of the textblock
            headerTextBlock.Text = attribComboBox.SelectedValue.ToString().Split(',')[0].Replace("[", "").Trim();

            // Set the position of the header for this attribute
            Grid.SetColumn(headerTextBlock, this.filterGrid.ColumnDefinitions.Count - 1);
            Grid.SetRow(   headerTextBlock, 0);

            // Add the header for this attribute
            this.filterGrid.Children.Add(headerTextBlock);

            // For each row of data
            for (int i = 0; i < this.filter[0].Count; i++)
            {
                // Create a button for this row related to this object
                Button cellButton = new Button();

                // Set the properties
                cellButton.Tag        = (this.filter.Count - 1).ToString() + "," + (i).ToString();
                cellButton.Background = Brushes.White;
                cellButton.Click     += cellButton_Click;
                cellButton.SetBinding(Button.ContentProperty, new System.Windows.Data.Binding("displayFilter_Property") { Source = this.filter[this.filter.Count - 1][i], Mode = System.Windows.Data.BindingMode.OneWay });

                // Set the button's position on the grid
                Grid.SetColumn(cellButton, this.filter.Count);
                Grid.SetRow(   cellButton, i + 1);

                // Add the button to the grid
                this.filterGrid.Children.Add(cellButton);
            }
        }

        void addOrButton_Click(object sender, RoutedEventArgs e)
        {
            // Add the attribute to the filter
            this.filters.AddOrStatementToFilter(this.filter);

            // Add a new row to the grid
            this.filterGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });

            // For each column (attribute)
            for (int i = 0; i < this.filter.Count; i++)
            {
                // Create an OR textblock
                TextBlock orTextBlock = new TextBlock();

                // Set the properties of the textblock
                orTextBlock.Text                = "OR";
                orTextBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                orTextBlock.VerticalAlignment   = System.Windows.VerticalAlignment.Center;

                // Create a button for this row related to this object
                Button cellButton = new Button();

                // Set the properties for the button
                cellButton.Tag        = (i).ToString() + "," + (filter[0].Count - 1).ToString();
                cellButton.Background = Brushes.White;
                cellButton.Click     += cellButton_Click;
                cellButton.SetBinding(Button.ContentProperty, new System.Windows.Data.Binding("displayFilter_Property") { Source = this.filter[i][this.filter[0].Count - 1], Mode = System.Windows.Data.BindingMode.OneWay });

                // Set the button's position on the grid
                Grid.SetColumn(orTextBlock, 0);
                Grid.SetRow(   orTextBlock, this.filter[0].Count);
                Grid.SetColumn(cellButton,  i + 1);
                Grid.SetRow(   cellButton,  this.filter[0].Count);

                // Add the button to the grid
                this.filterGrid.Children.Add(orTextBlock);
                this.filterGrid.Children.Add(cellButton);
            }
        }

        void cellButton_Click(object sender, RoutedEventArgs e)
        {
            // Remove the current property editor
            propertyDockPanel.Children.Clear();

            // Create a place to edit the values
            propertyEditor = new Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid();

            // Set the properties for the property editor
            propertyEditor.AutoGenerateProperties = false;
            propertyEditor.VerticalAlignment      = System.Windows.VerticalAlignment.Stretch;
            propertyEditor.HorizontalAlignment    = System.Windows.HorizontalAlignment.Stretch;

            // Set the position on the panel
            DockPanel.SetDock(propertyEditor, Dock.Right);

            // Add the property editor to the panel
            propertyDockPanel.Children.Add(propertyEditor);

            // Only display the attribute, operators, and the value
            propertyEditor.PropertyDefinitions.Add(new Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinition() { TargetProperties = { "attribute_Property" }, DisplayName       = "Attribute" });
            propertyEditor.PropertyDefinitions.Add(new Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinition() { TargetProperties = { "operatorOptions_Property" }, DisplayName = "Operator" });
            propertyEditor.PropertyDefinitions.Add(new Xceed.Wpf.Toolkit.PropertyGrid.PropertyDefinition() { TargetProperties = { "value_Property" }, DisplayName           = "Value" });
            attributeBinding = new System.Windows.Data.Binding("attribute_Property");
            
            // Display the attribute using a text block
            textBlockDataTemplate            = new DataTemplate();
            textBlockDataTemplate.VisualTree = new FrameworkElementFactory(typeof(TextBlock));
            textBlockDataTemplate.VisualTree.SetBinding(TextBlock.TextProperty, attributeBinding);
            textBlockDataTemplate.Seal();
            
            // Display the operators in a combobox
            operatorOptionsBinding            = new System.Windows.Data.Binding("operatorOptions_Property");
            selectedOperatorBinding           = new System.Windows.Data.Binding("selectedOperator_Property");
            DataTemplate comboBoxDataTemplate = new DataTemplate();
            comboBoxDataTemplate.VisualTree   = new FrameworkElementFactory(typeof(ComboBox));
            comboBoxDataTemplate.VisualTree.SetBinding(ComboBox.ItemsSourceProperty, operatorOptionsBinding);
            comboBoxDataTemplate.VisualTree.SetBinding(ComboBox.SelectedIndexProperty, selectedOperatorBinding);
            comboBoxDataTemplate.Seal();

            // Apply the templates
            propertyEditor.EditorDefinitions.Add(new Xceed.Wpf.Toolkit.PropertyGrid.EditorTemplateDefinition() { TargetProperties = { "attribute_Property" }, EditingTemplate = textBlockDataTemplate });
            propertyEditor.EditorDefinitions.Add(new Xceed.Wpf.Toolkit.PropertyGrid.EditorTemplateDefinition() { TargetProperties = { "operatorOptions_Property" }, EditingTemplate = comboBoxDataTemplate });

            Button cellButton = sender as Button;
            int x = int.Parse(((string)cellButton.Tag).Split(',')[0]);
            int y = int.Parse(((string)cellButton.Tag).Split(',')[1]);
            attributeBinding.Source        = this.filter[x][y];
            operatorOptionsBinding.Source  = this.filter[x][y];
            selectedOperatorBinding.Source = this.filter[x][y];
            propertyEditor.SelectedObject  = this.filter[x][y];
        }

        void saveButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            // Save the filter
            this.filters.Save(this.filterName);

            try
            {
                // Close the window
                this.Close();
            }
            catch { }
        }
    }
}
