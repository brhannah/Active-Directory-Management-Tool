using System.Windows.Controls.Ribbon;

namespace ActiveDirectoryManagementTool
{
    public partial class RibbonBarFrontEnd
    {
        private Ribbon                   bar;
        private RibbonQuickAccessToolBar quickAccessToolbar;
        private RibbonApplicationMenu    applicationMenu;

        public void Create_RibbonBar()
        {
            this.bar                = new Ribbon();
            this.quickAccessToolbar = new RibbonQuickAccessToolBar();
            this.applicationMenu    = new RibbonApplicationMenu();

            Create_ApplicationMenu();
            //Create_QuickAccessToolBar();
            Create_Tabs();
        }

        private void Create_ApplicationMenu()
        {
            this.applicationMenu.Label = "File";
            
            RibbonApplicationMenuItem exit = new RibbonApplicationMenuItem();
            exit.Header = "Exit";

            // Create the source
            exit.ImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.exit);
            exit.KeyTip      = "X";
            exit.Click      += exit_Click;
            this.applicationMenu.Items.Add(exit);

            this.bar.ApplicationMenu = this.applicationMenu;
        }
        
        private void Create_QuickAccessToolBar()
        {
            RibbonButton ribbonSaveBtn = new RibbonButton();

            this.quickAccessToolbar.Items.Add(ribbonSaveBtn);
            this.bar.QuickAccessToolBar = this.quickAccessToolbar;
        }

        private void Create_Tabs()
        {
            Create_HomeTab();
            Create_UserTab();
            Create_GroupTab();
            Create_ComputerTab();
        }

        private void Create_HomeTab()
        {
            int buttonWidth = 75;

            RibbonTab homeTab = new RibbonTab();

            homeTab.Header = "Home";
            this.bar.Items.Add(homeTab);
            
            RibbonGroup generalRibbonGroup  = new RibbonGroup();
            RibbonGroup filterRibbonGroup   = new RibbonGroup();
            
            generalRibbonGroup.Header  = "General";
            filterRibbonGroup.Header   = "Filters";

            RibbonButton addObject     = new RibbonButton();
            addObject.Label            = "Add Object";
            addObject.Width            = buttonWidth;
            addObject.Click           += addObject_Click;
            addObject.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.Generic_Add);

            generalRibbonGroup.Items.Add(addObject);

            RibbonButton deleteObject     = new RibbonButton();
            deleteObject.Label            = "Delete Object";
            deleteObject.Width            = buttonWidth;
            deleteObject.Click           += deleteObject_Click;
            deleteObject.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.Generic_Delete);

            generalRibbonGroup.Items.Add(deleteObject);

            RibbonButton selectAll      = new RibbonButton();
            selectAll.Label             = "Select All";
            selectAll.Width             = buttonWidth;
            selectAll.Click            += selectAll_Click;
            selectAll.LargeImageSource  = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.SelectAll);

            generalRibbonGroup.Items.Add(selectAll);

            RibbonButton refresh      = new RibbonButton();
            refresh.Label             = "Refresh";
            refresh.Width             = buttonWidth;
            refresh.Click            += refresh_Click;
            refresh.LargeImageSource  = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.refresh);

            generalRibbonGroup.Items.Add(refresh);

            RibbonButton filter_manage     = new RibbonButton();
            filter_manage.Label            = "Filters";
            filter_manage.Width            = buttonWidth;
            filter_manage.Click           += filter_manage_Click;
            filter_manage.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.Filter);

            filterRibbonGroup.Items.Add(filter_manage);

            RibbonButton filter_clear     = new RibbonButton();
            filter_clear.Label            = "Clear Filter";
            filter_clear.Width            = buttonWidth;
            filter_clear.Click           += filter_clear_Click;
            filter_clear.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.Filter_Clear);

            filterRibbonGroup.Items.Add(filter_clear);

            homeTab.Items.Add(generalRibbonGroup);
            homeTab.Items.Add(filterRibbonGroup);
        }

        private void Create_UserTab()
        {
            int buttonWidth = 75;

            RibbonTab userTab = new RibbonTab();

            userTab.Header = "Users";
            this.bar.Items.Add(userTab);

            RibbonGroup userRibbonGroup = new RibbonGroup();

            userRibbonGroup.Header = "Users";

            RibbonButton user_add     = new RibbonButton();
            user_add.Label            = "Add User(s)";
            user_add.Width            = buttonWidth;
            user_add.Click           += user_add_Click;
            user_add.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.User_Add);

            userRibbonGroup.Items.Add(user_add);

            userTab.Items.Add(userRibbonGroup);
        }

        private void Create_GroupTab()
        {
            int buttonWidth = 75;

            RibbonTab groupTab = new RibbonTab();

            groupTab.Header = "Groups";
            this.bar.Items.Add(groupTab);

            RibbonGroup groupRibbonGroup = new RibbonGroup();

            groupRibbonGroup.Header = "Groups";

            RibbonButton group_create     = new RibbonButton();
            group_create.Label            = "Add Group(s)";
            group_create.Width            = buttonWidth;
            group_create.Click           += group_create_Click;
            group_create.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.Group_Add);

            groupRibbonGroup.Items.Add(group_create);

            RibbonButton group_dictionary     = new RibbonButton();
            group_dictionary.Label            = "Manage Job Titles";
            group_dictionary.Width            = buttonWidth;
            group_dictionary.Click           += group_dictionary_Click;
            group_dictionary.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.dictionary);

            groupRibbonGroup.Items.Add(group_dictionary);

            RibbonButton group_assignJobTitle     = new RibbonButton();
            group_assignJobTitle.Label            = "Assign Job Title";
            group_assignJobTitle.Width            = buttonWidth;
            group_assignJobTitle.Click           += group_assignJobTitle_Click;
            group_assignJobTitle.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.AssignJob);

            groupRibbonGroup.Items.Add(group_assignJobTitle);

            groupTab.Items.Add(groupRibbonGroup);
        }

        private void Create_ComputerTab()
        {
            int buttonWidth = 75;

            RibbonTab computerTab = new RibbonTab();

            computerTab.Header = "Computers";
            this.bar.Items.Add(computerTab);

            RibbonGroup computerRibbonGroup = new RibbonGroup();

            computerRibbonGroup.Header = "Computers";

            RibbonButton computer_add  = new RibbonButton();
            computer_add.Label         = "Add Computer(s)";
            computer_add.Width         = buttonWidth;
            computer_add.Click        += computer_add_Click;
            computer_add.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.Computer_Add);

            computerRibbonGroup.Items.Add(computer_add);

            computerTab.Items.Add(computerRibbonGroup);
        }

        // http://joe-bq-wang.iteye.com/blog/1685024
        public static System.Windows.Media.ImageSource ConvertToImageSource(System.Drawing.Bitmap bmp)
        {
            var ms = new System.IO.MemoryStream();
            var bitmap = new System.Windows.Media.Imaging.BitmapImage();
            bitmap.BeginInit();

            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            bitmap.StreamSource = ms;
            bitmap.EndInit();
            return bitmap;
        }

        void exit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        void addObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Create the add window
            GeneralAdd generalAddWindow = new GeneralAdd(this);

            // Display the add window and wait until the user is done
            generalAddWindow.ShowDialog();

            // Refresh the window
            RefreshWindow();
        }

        void filter_manage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Get the current filter's values
            string currentFilter   = BackEnd.filter_Property;
            bool   currentFlatView = BackEnd.flatView_Property;

            // Create the filter manager window
            FilterManager filterManagerWindow = new FilterManager();

            // Display the filter manager window and wait until the user is done
            filterManagerWindow.ShowDialog();

            // If the user applied a new filter
            if (currentFilter != BackEnd.filter_Property || currentFlatView != BackEnd.flatView_Property)
            {
                // Refresh the window
                RefreshWindow();
            }
        }

        void filter_clear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Clear the filter and return to a tree view
            BackEnd.ClearFilter();

            // Refresh the window
            RefreshWindow();
        }

        public void user_add_Click(object sender = null, System.Windows.RoutedEventArgs e = null)
        {
            // Create the add window
            UserAdd userAddWindow = new UserAdd();

            // Display the add window and wait until the user is done
            userAddWindow.ShowDialog();

            // Refresh the window
            RefreshWindow();
        }

        public void group_create_Click(object sender = null, System.Windows.RoutedEventArgs e = null)
        {
            // Create the add window
            GroupAdd groupAddWindow = new GroupAdd();

            // Display the add window and wait until the user is done
            groupAddWindow.ShowDialog();

            // Refresh the window
            RefreshWindow();
        }

        public void computer_add_Click(object sender = null, System.Windows.RoutedEventArgs e = null)
        {
            // Create the add window
            ComputerAdd computerAddWindow = new ComputerAdd();

            // Display the add window and wait until the user is done
            computerAddWindow.ShowDialog();

            // Refresh the window
            RefreshWindow();
        }

        void deleteObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (mainBody.mainDataGrid.SelectedItems.Count == 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Please select an object to delete.", "Delete Objects", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
            else if (mainBody.mainDataGrid.SelectedItems.Count > 1)
            {
                // Create the delete window
                DeleteObject deleteObjectsWindow = new DeleteObject();

                // Display the add window and wait until the user is done
                deleteObjectsWindow.ShowDialog();

                // Refresh the window
                RefreshWindow();
            }
            else
            {
                string result = ObjectManagement.DeleteSingleObject(BackEnd.selectedADObjs_Property[0].distinguishedName, BackEnd.selectedADObjs_Property[0].commonName_Property);

                Xceed.Wpf.Toolkit.MessageBox.Show(result, "Delete Objects", System.Windows.MessageBoxButton.OK, (result == "Success") ? System.Windows.MessageBoxImage.Information : System.Windows.MessageBoxImage.Warning);

                RefreshWindow();
            }
        }

        void selectAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainBody.mainDataGrid.Focus();
            foreach(object x in mainBody.mainDataGrid.Items)
            {
                mainBody.mainDataGrid.SelectedItems.Add(x);
            }
        }

        void refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RefreshWindow();
        }

        public void RefreshWindow()
        {
            if (BackEnd.flatView_Property)
            {
                ((System.Windows.Controls.TreeViewItem)mainBody.treeView.Items[0]).IsSelected = true;
            }
            Tree x = (Tree)((System.Windows.Controls.TreeViewItem)mainBody.treeView.SelectedItem).Tag;
            Tree y = BackEnd.RefreshBranch(ref x);
            mainBody.treeView = mainBody.RebuildTree();
        }

        void group_dictionary_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Create the dictionary manager window
            DictionaryManager dictionaryManagerWindow = new DictionaryManager();
        }

        void group_assignJobTitle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Create the assign job title window
            AssignJobTitle assignJobTitleWindow = new AssignJobTitle();

            try
            {
                // Display the add window and wait until the user is done
                assignJobTitleWindow.ShowDialog();
            }
            catch { }

            // Refresh the window
            RefreshWindow();
        }
    }
}
