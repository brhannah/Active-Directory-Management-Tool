using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Controls.Primitives;

namespace ActiveDirectoryManagementTool
{
    public class GetInfo : Window
    {
        public GetInfo()
        {
            // Create a dock to place the questions
            DockPanel questionDock = new DockPanel();

            // Set window properties
            this.Title                 = "Active Directory Management Tool - Logon";
            this.Width                 = 350;
            this.Height                = 250;
            this.ResizeMode            = System.Windows.ResizeMode.NoResize;
            this.WindowState           = System.Windows.WindowState.Normal;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Content               = questionDock;
            this.Background            = Brushes.WhiteSmoke;
            this.Topmost               = false;
            this.Show();
            this.Activate();
            this.ShowActivated         = true;
            this.KeyDown              += GetInfo_KeyDown;
            this.FontSize              = 15;
            this.Icon                  = RibbonBarFrontEnd.ConvertToImageSource(global::ActiveDirectoryManagementTool.Properties.Resources.generic_computer);

            TextBlock domainTextBlock = new TextBlock();
            TextBlock userTextBlock   = new TextBlock();
            TextBlock passTextBlock   = new TextBlock();

            domainTextBlock.Text = "Domain:";
            userTextBlock.Text   = "Username:";
            passTextBlock.Text   = "Password:";

            TextBox     domainTextBox = new TextBox();
            TextBox     userTextBox   = new TextBox();
            PasswordBox passTextBox   = new PasswordBox();

            domainTextBox.Width          = this.Width - 25;
            userTextBox.Width            = this.Width - 25;
            passTextBox.Width            = this.Width - 25;
            passTextBox.PasswordChanged += passTextBox_PasswordChanged;

            Binding domainBinding = new Binding();
            Binding userBinding   = new Binding();

            domainBinding.Mode                = BindingMode.OneWayToSource;
            domainBinding.Path                = new PropertyPath(typeof(BackEnd).GetProperty("DomainName_Property"));
            domainBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            domainTextBox.SetBinding(TextBox.TextProperty, domainBinding);

            userBinding.Mode                = BindingMode.OneWayToSource;
            userBinding.Path                = new PropertyPath(typeof(BackEnd).GetProperty("User_Property"));
            userBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            userTextBox.SetBinding(TextBox.TextProperty, userBinding);

            Button loginButton = new Button();

            loginButton.Content = "Login";
            loginButton.Height  = 25;
            loginButton.Width   = 75;
            loginButton.Click  += loginButton_Click;

            DockPanel.SetDock(domainTextBlock, Dock.Top);
            DockPanel.SetDock(domainTextBox,   Dock.Top);
            DockPanel.SetDock(userTextBlock,   Dock.Top);
            DockPanel.SetDock(userTextBox,     Dock.Top);
            DockPanel.SetDock(passTextBlock,   Dock.Top);
            DockPanel.SetDock(passTextBox,     Dock.Top);
            DockPanel.SetDock(loginButton,     Dock.Top);

            questionDock.Children.Add(domainTextBlock);
            questionDock.Children.Add(domainTextBox);
            questionDock.Children.Add(userTextBlock);
            questionDock.Children.Add(userTextBox);
            questionDock.Children.Add(passTextBlock);
            questionDock.Children.Add(passTextBox);
            questionDock.Children.Add(loginButton);

            // Draw focus to the domain textbox
            this.Focus();
            domainTextBox.Focus();
        }

        void passTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passTextBox = sender as PasswordBox;
            BackEnd.Pass_Property = passTextBox.Password;
        }

        void GetInfo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                testAuth();
            }

            if (e.Key == System.Windows.Input.Key.Escape)
            {
                if (BackEnd.isAuthenticated_Property)
                {
                    this.Close();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            testAuth();
        }

        private void testAuth()
        {
            this.IsEnabled      = false;
            string loginMessage = BackEnd.testAuth();

            if (BackEnd.isAuthenticated_Property)
            {
                bool ribbonOn = true; // Used to determine if the RibbonWindow should be used instead of the Window

                // If the ribbon is turned off
                if (!ribbonOn)
                {
                    // Execute the program using the menu bar
                    //MenuBarFrontEnd menuBarFrontEnd = new MenuBarFrontEnd();
                    //foreach (Tree x in BackEnd.getADObjects("/DC=cyber,DC=net"))
                    //{
                    //    Console.WriteLine(x.DistinguishedName);
                    //    foreach (Tree y in x.ChildNode)
                    //    {
                    //        C`onsole.WriteLine(y.DistinguishedName);
                    //    }
                    //}
                    //Console.ReadLine();
                }
                // If the ribbon is turned on
                else
                {
                    // Execute the program using the ribbon bar
                    RibbonBarFrontEnd ribbonBarFrontEnd = new RibbonBarFrontEnd();
                }

                this.Close();
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(loginMessage, "Active Directory Management Tool - Logon", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.IsEnabled = true;
        }
    }
}
