using System.Windows.Controls;    // DockPanel
using System.Windows;             // Used to inherit from Window class

namespace ActiveDirectoryManagementTool
{
    public partial class MenuBarFrontEnd : Window
    {
        public MenuBarFrontEnd()
        {
            // Create a DockPanel to place UI objects
            DockPanel dock = new DockPanel();

            // Set the RibbonWindow's properties
            this.Title       = "Active Directory Management Tool";
            this.WindowState = WindowState.Maximized;
            this.Content     = dock;
            this.Icon        = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            // Create the menu bar
            Menu menu = new Menu();

            #region File Menu
            // Create the File menu
            MenuItem file = new MenuItem();

            // Set the File menu's properties
            file.Header = "File";

            // Add the File menu to the menu bar
            menu.Items.Add(file);

            #region Exit Menu Item
            // Create the Exit menu item
            MenuItem exit = new MenuItem();

            // Set the Exit menu item's properties
            exit.Header = "_Exit";

            // Add the Exit menu item
            file.Items.Add(exit);
            #endregion
            #endregion

            // Dock menu at top of panel
            DockPanel.SetDock(menu, Dock.Top);
            dock.Children.Add(menu);

            // Create the main body for the UI
            FrontEnd mainBody = new FrontEnd();

            // Add the main body for the UI to the bottom of the DockPanel
            DockPanel.SetDock(mainBody, Dock.Bottom);
            dock.Children.Add(mainBody);
        }
    }
}
