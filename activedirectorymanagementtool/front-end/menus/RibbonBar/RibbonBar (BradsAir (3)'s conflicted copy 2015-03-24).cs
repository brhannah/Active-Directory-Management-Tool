using Microsoft.Windows.Controls.Ribbon;

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
            exit.KeyTip = "X";
            this.applicationMenu.Items.Add(exit);

            RibbonApplicationMenuItem options = new RibbonApplicationMenuItem();
            options.Header = "Options";

            options.ImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.exit);
            options.KeyTip = "O";
            this.applicationMenu.FooterPaneContent = options;

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

            RibbonButton refresh = new RibbonButton();
            refresh.Label = "Refresh";
            refresh.Width = buttonWidth;
            refresh.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.refresh);

            generalRibbonGroup.Items.Add(refresh);

            RibbonButton properties = new RibbonButton();
            properties.Label = "Properties";
            properties.Width = buttonWidth;
            properties.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.properties);

            generalRibbonGroup.Items.Add(properties);

            RibbonButton filter_display = new RibbonButton();
            filter_display.Label = "Display Filter";
            filter_display.Width = buttonWidth;
            filter_display.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.filter);

            filterRibbonGroup.Items.Add(filter_display);

            RibbonButton filter_clear = new RibbonButton();
            filter_clear.Label = "Clear Filter";
            filter_clear.Width = buttonWidth;
            filter_clear.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.filter);

            filterRibbonGroup.Items.Add(filter_clear);

            RibbonButton filter_save = new RibbonButton();
            filter_save.Label = "Save Display Filter";
            filter_save.Width = buttonWidth;
            filter_save.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.filter);

            filterRibbonGroup.Items.Add(filter_save);

            RibbonButton filter_manage = new RibbonButton();
            filter_manage.Label = "Manage Filters";
            filter_manage.Width = buttonWidth;
            filter_manage.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.filter);

            filterRibbonGroup.Items.Add(filter_manage);

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

            RibbonButton user_add = new RibbonButton();
            user_add.Label = "Add User(s) to OU";
            user_add.Width = buttonWidth;
            user_add.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_user_add);

            userRibbonGroup.Items.Add(user_add);

            RibbonButton user_remove = new RibbonButton();
            user_remove.Label = "Remove Selected User(s)";
            user_remove.Width = buttonWidth;
            user_remove.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_user_remove);

            userRibbonGroup.Items.Add(user_remove);

            RibbonButton user_changepw = new RibbonButton();
            user_changepw.Label = "Change User(s) Password";
            user_changepw.Width = buttonWidth;
            user_changepw.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_user_add);

            userRibbonGroup.Items.Add(user_changepw);

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

            RibbonButton group_create = new RibbonButton();
            group_create.Label = "Create Group(s)";
            group_create.Width = buttonWidth;
            group_create.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_user_add);

            groupRibbonGroup.Items.Add(group_create);

            RibbonButton group_add = new RibbonButton();
            group_add.Label = "Add Group(s) Members";
            group_add.Width = buttonWidth;
            group_add.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_user_add);

            groupRibbonGroup.Items.Add(group_add);

            RibbonButton group_remove = new RibbonButton();
            group_remove.Label = "Remove Selected Group(s)";
            group_remove.Width = buttonWidth;
            group_remove.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_user_remove);

            groupRibbonGroup.Items.Add(group_remove);

            RibbonButton group_dictionary = new RibbonButton();
            group_dictionary.Label = "Manage Group Dictionary";
            group_dictionary.Width = buttonWidth;
            group_dictionary.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.dictionary);

            groupRibbonGroup.Items.Add(group_dictionary);

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

            RibbonButton computer_add = new RibbonButton();
            computer_add.Label = "Add Computer(s) to OU";
            computer_add.Width = buttonWidth;
            computer_add.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            computerRibbonGroup.Items.Add(computer_add);

            RibbonButton computer_remove = new RibbonButton();
            computer_remove.Label = "Remove Selected Computer(s)";
            computer_remove.Width = buttonWidth;
            computer_remove.LargeImageSource = ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            computerRibbonGroup.Items.Add(computer_remove);

            computerTab.Items.Add(computerRibbonGroup);
        }

        // http://joe-bq-wang.iteye.com/blog/1685024
        private System.Windows.Media.ImageSource ConvertToImageSource(System.Drawing.Bitmap bmp)
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
    }
}
