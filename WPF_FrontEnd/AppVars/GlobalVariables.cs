using RESTServer.Models;
using System.Collections.Generic;

namespace WPF_FrontEnd.AppVars
{
    public class GlobalVariables
    {
        public static User CurrentUser { get; set; }

        public static List<Note> CurrentNotes { get; set; }

        public static MainWindow MainWindow { get; set; }
    }
}
