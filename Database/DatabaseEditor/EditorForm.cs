using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

// EditorForm Partial Class, Primary Component
// Author: Kyle McCarty
namespace DatabaseEditor
{
    /// <summary>
    /// This class is responsible for initializing all of the database editor. It has
    /// four connected partial classes that contain the bulk of its methods. These
    /// consist of EditorForm.Designer, EditorForm.POI, EditorForm.Dept, and
    /// EditorForm.Media. EditorForm.Designer is an automatically generated section of
    /// code from the visual GUI editor and should not be changed by hand for any reason.
    /// The other three can be changed and hold the methods related to their respective
    /// tabs on the database editor.
    /// </summary>
    public partial class EditorForm : Form
    {
        List<PoI> poiList = null;
        List<Department> deptList = null;
        List<MediaFile> mediaList = null;
        Datafile db = null;
        
        /// <summary>
        /// The method initializes starts the initialization process by relaying said
        /// command to the other partial class sections of the editor. It also creates
        /// a localized reference to the physical database object.
        /// </summary>
        public EditorForm()
        {
            // Initialize the window
            InitializeComponent();
            // Generate the database
            db = new Datafile(System.IO.Path.GetDirectoryName(Application.ExecutablePath));

            //db.SaveDatabaseXML();
            //Environment.Exit(0);

            // Check the architecture
            CheckArchitecture();

            // Load the data lists
            poiList = db.GetPOIList();
            deptList = db.GetDepartmentList();
            mediaList = db.GetMediaList();

            // Set the properties for the selection lists
            ListView[] select = { pSelect, dSelect, mSelect };
            foreach (ListView l in select)
            {
                l.FullRowSelect = true;
                l.AllowColumnReorder = false;
                l.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                l.ColumnWidthChanging += ListView_ColumnWidthChanging;
                l.HideSelection = false;
                l.SelectedIndexChanged += pSelect_SelectedIndexChanged;

                // Make the columns
                ColumnHeader[] listHeader = { new ColumnHeader(), new ColumnHeader() };
                listHeader[0].Text = "ID";
                listHeader[0].Width = 30;
                listHeader[1].Text = "Name";
                listHeader[1].Width = l.Width - listHeader[0].Width - 4;
                l.Columns.Add(listHeader[0]);
                l.Columns.Add(listHeader[1]);
            }

            // Populate tabs
            PopulateTabPOI();
            PopulateTabDepartment();
            PopulateTabMedia();

            // Normalize all indices
            NormalizeIndicesPOI();
            NormalizeIndicesDepartment();
            NormalizeIndicesMedia();

            // Create an input binding
            //KeyGesture key = new KeyGesture(Key.S, System.Windows.Input.ModifierKeys.Control);
            //KeyBinding kb = new KeyBinding(ApplicationCommands.Save, key);
            //CommandBinding cb = new CommandBinding(ApplicationCommands.Save);
            //cb.Executed += ModusTemporis;
        }

        //private void ModusTemporis(object obSender, ExecutedRoutedEventArgs e) { Console.Out.WriteLine("Test"); }

        // === Architecture Methods =============================================
        // ======================================================================

        /// <summary>
        /// Method creates a new text file at the designated location.
        /// </summary>
        /// <param name="path">Represents the location at which to construct the new
        /// text file.</param>
        private void CreateNewFile(String path)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.Write("");
            writer.Close();
        }

        /// <summary>
        /// Method creates a new directory at the designated location.
        /// </summary>
        /// <param name="path">Represents the location at which the directory should
        /// be created.</param>
        private void CreateNewDirectory(String path) { Directory.CreateDirectory(path); }

        /// <summary>
        /// Method checks to ensure that all of the  necessary folders and database
        /// files exists before trying to load. The user will be prompted for permission
        /// to create them if they are not found.
        /// </summary>
        private void CheckArchitecture()
        {
            // Make some variables
            String[] errorList = { "PoI Datafile", "Department Datafile", "Media Datafile", "Map Icon Image Directory", "Text File Directory",
                             "Image Directory", "Video Directory" };
            bool[] errorOccur = new bool[errorList.Length];

            // Check that each needed file and directory exists
            if (!File.Exists(db.DIR_ROOT + "poi.vtd")) { errorOccur[0] = true; }
            if (!File.Exists(db.DIR_ROOT + "department.vtd")) { errorOccur[1] = true; }
            if (!File.Exists(db.DIR_ROOT + "media.vtd")) { errorOccur[2] = true; }
            if (!Directory.Exists(db.DIR_ROOT + "icons")) { errorOccur[3] = true; }
            if (!Directory.Exists(db.DIR_ROOT + "text")) { errorOccur[4] = true; }
            if (!Directory.Exists(db.DIR_ROOT + "images")) { errorOccur[5] = true; }
            if (!Directory.Exists(db.DIR_ROOT + "videos")) { errorOccur[6] = true; }

            // Determine if a file/directory was missing
            bool error = false;
            foreach (bool b in errorOccur) { if (b) { error = true; } }

            // If a file/directory was missing anywhere
            if (error)
            {
                // Get the error string
                StringBuilder s = new StringBuilder();
                for (int i = 0; i < errorList.Length; i++) { if (errorOccur[i]) { s.AppendLine("\t" + errorList[i]); } }
                String output = "The following files needed for the database are missing.\n" + s.ToString() + 
                    "\nSelect yes to create these files in the directory:\n" + db.DIR_ROOT + "\nSelecting no will close the editor.";

                // Ask the user to create the needed files/directories
                DialogResult click = MessageBox.Show(output, "Error - Files Missing", MessageBoxButtons.YesNo);

                // If the user says no, the application can not run, so cancel it
                if (click == DialogResult.No) { Environment.Exit(0); }
                // Otherwise, make the files
                else
                {
                    if (errorOccur[0]) { CreateNewFile(db.DIR_ROOT + "poi.vtd"); }
                    if (errorOccur[1]) { CreateNewFile(db.DIR_ROOT + "department.vtd"); }
                    if (errorOccur[2]) { CreateNewFile(db.DIR_ROOT + "media.vtd"); }
                    if (errorOccur[3]) { CreateNewDirectory(db.DIR_ROOT + "icons"); }
                    if (errorOccur[4]) { CreateNewDirectory(db.DIR_ROOT + "text"); }
                    if (errorOccur[5]) { CreateNewDirectory(db.DIR_ROOT + "images"); }
                    if (errorOccur[6]) { CreateNewDirectory(db.DIR_ROOT + "videos"); }
                }
            }
        }

        // === General Methods ==================================================
        // ======================================================================

       /// <summary>
        /// Method creates an Bitmap object from the file at the given path and
       /// resizes it to fit within the dimensions provided, but retain its
       /// proportions.
       /// </summary>
       /// <param name="path">Represents the file path of the file from which
       /// the image is to be drawn.</param>
       /// <param name="width">Represents the maximum width of the image.</param>
        /// <param name="height">Represents the maximum height of the image.</param>
       /// <returns>Returns a Bitmap image object of the specified image with the
       /// requested dimensions.</returns>
        private Bitmap GetImage(String path, int maxWidth, int maxHeight)
        {
            // Create a bitmap
            Bitmap temp = null;

            // If the file exists, load it, otherwise, use the null image
            if (!File.Exists(path)) { temp = GetInternalImage("DatabaseEditor.Images.null.png", 154, 154); }
            else { temp = new Bitmap(path); }
            
            // Determine the difference between the image's actual size and the
            // size boundaries provided.
            int[] delta = { temp.Width - maxWidth, temp.Height - maxHeight };

            // If the image is already small enough to fit in the bounds, we
            // don't need to do anything.
            if (delta[0] <= 0 && delta[1] <= 0) { return temp; }

            // Otherwise, we need to scale it so that it fits.
            int newWidth = 0;
            int newHeight = 0;
            if (delta[0] > delta[1])
            {
                newWidth = maxWidth;
                newHeight = (maxWidth * temp.Height) / temp.Width;
            }
            else
            {
                newWidth = (maxHeight * temp.Width) / temp.Height;
                newHeight = maxHeight;
            }

            // Return the scaled image
            return new Bitmap(temp, newWidth, newHeight);
        }

        /// <summary>
        /// Method provides access to internal image resources and returns them
        /// as a Bitmap of the given size.
        /// </summary>
        /// <param name="path">Represents the path to the image resource. This
        /// should be of the form "DatabaseEditor.Images.[name].[extension]".
        /// For example, "DatabaseEditor.Images.null.png".</param>
        /// <param name="width">Represents the width of the returned image.</param>
        /// <param name="height">Represents the height of the returned image.</param>
        /// <returns>Returns a Bitmap object of the requested internal image of
        /// width and height specified by their respective variables.</returns>
        private Bitmap GetInternalImage(String path, int width, int height)
        {
            // Get the resource file
            Assembly a = Assembly.GetExecutingAssembly();
            Bitmap temp = new Bitmap(a.GetManifestResourceStream(path));

            // Return it formatted to the right size
            return new Bitmap(temp, width, height);
        }

        /// <summary>
        /// Method accesses a text file and returns its contents, if it exists.
        /// </summary>
        /// <param name="path">Represents the path to the text file.</param>
        /// <returns>Returns a String object containing the text file, or an
        /// error message if it does not exist.</returns>
        private String GetTextFile(String path)
        {
            // Make sure that the file exists
            if (File.Exists(path)) { return System.IO.File.OpenText(path).ReadToEnd(); }
            else { return "Null File: No text file of the selected name exists."; }
        }

        /// <summary>
        /// Method generates a row object for the selector lists. This method can
        /// be called for any of the three selector lists.
        /// </summary>
        /// <param name="id">Represents the value of the "ID" column.</param>
        /// <param name="name">Represent sthe value of the "Name" column.</param>
        /// <returns></returns>
       private ListViewItem GetSelectRow(int id, String name)
       {
           ListViewItem newItem = new ListViewItem();
           newItem.Text = "" + id;
           ListViewItem.ListViewSubItem newSubItem = new ListViewItem.ListViewSubItem();
           newSubItem.Text = name;
           newItem.SubItems.Add(newSubItem);

           return newItem;
       }

        // === Component Listeners ==============================================
        // ======================================================================

        private void EditorForm_Load(object sender, EventArgs e) { }

        /// <summary>
        /// This listener register when a user tries to resize the columns in the
        /// selection lists and cancels the action, thus ensuring that the columns
        /// can not be altered.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = ((ListView)sender).Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// Listener determines when a checked list has lost focus and removes the
        /// selection highlight from it. This listener should only be applied to
        /// CheckedListBox objects.
        /// </summary>
        /// <param name="sender">The object which triggered the listener. This should
        /// always be a CheckedListBox.</param>
        /// <param name="e">The event which triggered the listener.</param>
        private void CheckedListBox_LostFocus(object sender, EventArgs e) { ((CheckedListBox)sender).SelectedIndex = -1; }
    }
}
