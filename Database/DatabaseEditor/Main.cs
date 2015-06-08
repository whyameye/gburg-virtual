using System;
using System.Windows.Forms;

// Program Class
// Author: Kyle McCarty
namespace DatabaseEditor
{
    /// <summary>
    /// This class is literally only responsible for generating the editor
    /// window and initially running its class.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// This is the method that runs the editor window class. It has
        /// no further purpose.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set base settings
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Run the editor
            Application.Run(new EditorForm());
        }
    }
}
