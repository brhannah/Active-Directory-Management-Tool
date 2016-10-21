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
            Create_QuickAccessToolBar();
            Create_Tabs();
        }

        private void Create_ApplicationMenu()
        {
            RibbonApplicationMenuItem exit = new RibbonApplicationMenuItem();
            exit.Header = "Exit";

            // Create the source
            //System.Windows.Media.Imaging.PngBitmapEncoder 
            System.Windows.Media.Imaging.BitmapImage img = new System.Windows.Media.Imaging.BitmapImage();
            img.BeginInit();
            img.UriSource = new System.Uri(@"file://C:\Users\Brad\Dropbox\School\Grad\Graduate Project\ActiveDirectoryManagementTool\ActiveDirectoryManagementTool\ActiveDirectoryManagementTool\Resources\Icons\ApplicationMenu\exit.png", System.UriKind.Absolute);
            img.EndInit();

            //System.Windows.Media.ImageSource tempImageSource = new System.Windows.Media.ImageSource(@"Resources\Icons\ApplicationMenu\exit.png");
            exit.ImageSource = img;
            exit.KeyTip = "X";

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
        }

        private void Create_HomeTab()
        {
            RibbonTab homeTab = new RibbonTab();
            homeTab.Header = "Home";
            this.bar.Items.Add(homeTab);
        }
    }
}
