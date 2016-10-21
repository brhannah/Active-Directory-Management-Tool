//********************************************************************
//
// Active Directory Management Tool
//
// Graduate Project
//
// Submitted to the Faculty of
// the School of Engineering and Computing Sciences
// Texas A&M University - Corpus Christi
// Corpus Christi, TX
//
// In Partial Fulfillment of the Requirements for the Degree of
// Master of Science in Computer Science
//
// by
//
// Bradley R Hannah
// Spring 2015
//
// Committe Members
//
// Dr. Dulal Kar
// Committee Chairperson
//
// Dr. Longzhuang Li
// Committee Member
//
//********************************************************************

using System;         // STAThread
using System.Windows; // Application

namespace ActiveDirectoryManagementTool
{
    public partial class ActiveDirectoryManagementTool
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Declare local variables
            Application app = new Application(); // Encapsulates a WPF application

            app.Run(new GetInfo());
        }
    }
}