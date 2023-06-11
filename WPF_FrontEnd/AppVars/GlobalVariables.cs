using RESTServer.Models;
using System.Collections.Generic;

namespace WPF_FrontEnd.AppVars
{
    /// <summary>
    /// Class that holds variable that might be used anywhere in app
    /// </summary>
    public class GlobalVariables
    {
        /// <summary>
        /// Variable that holds current user
        /// </summary>
        public static User CurrentUser { get; set; }

        /// <summary>
        /// Variable that holds current users notes
        /// </summary>
        public static List<Note> CurrentNotes { get; set; }

        /// <summary>
        /// Variable that holds link to our MainWindow
        /// </summary>
        public static MainWindow MainWindow { get; set; }
    }
}
