using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

// EditorForm Partial Class, Media File Component
// Author: Kyle McCarty
namespace DatabaseEditor
{
    /// <summary>
    /// This class is connected to its parent, EditorForm, and contains all methods
    /// pertaining solely to the Media tab of the database editor.
    /// </summary>
    public partial class EditorForm : Form
    {
        private bool nullMedia = false;
        private bool adminChange = false;
        private System.Drawing.Bitmap[] mediaIcon = new System.Drawing.Bitmap[3];

        // === Component Constructors ===========================================
        // ======================================================================

        /// <summary>
        /// This method handles some extra initialization of various components
        /// that is not handled by the auto-generated code from the visual GUI
        /// editor. All changes to GUI components must be done either here or
        /// via the visual GUI editor. The GUI editor output should not be altered
        /// in any fashion.
        /// </summary>
        private void PopulateTabMedia()
        {
            // Set the icons
            int size = 10;
            mediaIcon[0] = GetInternalImage("DatabaseEditor.Images.text.png", size, size);
            mediaIcon[1] = GetInternalImage("DatabaseEditor.Images.image.png", size, size);
            mediaIcon[2] = GetInternalImage("DatabaseEditor.Images.video.png", size, size);

            // Load the list icons
            ImageList iconList = new ImageList();
            iconList.Images.Add(mediaIcon[0]);
            iconList.Images.Add(mediaIcon[1]);
            iconList.Images.Add(mediaIcon[2]);
            mSelect.SmallImageList = iconList;

            // Apply listener to Media selection list
            mSelect.ItemSelectionChanged += mSelect_SelectedIndexChanged;

            // Add the listener to the file reference combo box
            mFileCombo.SelectedIndexChanged += mFileCombo_SelectedIndexChanged;

            // Add listener to the type selection box
            mTypeCombo.SelectedIndexChanged += mTypeCombo_SelectedIndexChanged;

            // Media Files
            for(int i = 0; i < mediaList.Count; i++) {
                // Add a new row for the Media File
                MediaFile m = mediaList[i];
                mSelect.Items.Add(GetSelectRow(m.ID, m.Name));

                // Set its icon to appropriate type
                int type = -1;
                if (m is Text) { type = 0; }
                else if (m is Image) { type = 1; }
                else if (m is Video) { type = 2; }
                mSelect.Items[i].ImageIndex = type;
            }

            // Readjust columns to allow for image
            mSelect.Columns[0].Width = 45;
            mSelect.Columns[1].Width = mSelect.Width - mSelect.Columns[0].Width - 4;

            if (mSelect.Items.Count > 0) { mSelect.Items[0].Selected = true; }
            else { NullifyMedia(); }
        }

        // === General Methods ==================================================
        // ======================================================================

        /// <summary>
        /// This method changes the value displayed on the Media File type
        /// component. It is important to change this setting through this method
        /// as it bypasses the listener that triggers from said setting being
        /// altered.
        /// </summary>
        /// <param name="newSelection">Represents the index which the type
        /// component should display.</param>
        private void ChangeTypeCombo(int newSelection)
        {
            adminChange = true;
            mTypeCombo.SelectedIndex = newSelection;
            adminChange = false;
        }

        /// <summary>
        /// Method sets all of the components for the Media interface to
        /// either be enabled or disabled according to the parameter.
        /// </summary>
        /// <param name="enabled">Represents whether the components should be
        /// enabled or disabled. True is enabled, false is disabled.</param>
        private void SetEnabledMedia(bool enabled)
        {
            mIdField.Enabled = enabled;
            mTypeCombo.Enabled = enabled;
            mNameField.Enabled = enabled;
            mDescField.Enabled = enabled;
            mFileCombo.Enabled = enabled;
            mSaveButton.Enabled = enabled;
            mPreviewField.Enabled = enabled;
        }

        /// <summary>
        /// This method sets the Media File editor page to the "null" settings.
        /// It is used when no point of interest is selected on the Media File
        /// selection list.
        /// </summary>
        private void NullifyMedia()
        {
            // Set all Media fields to the "null" value
            mIdField.Text = "";
            mNameField.Text = "No Media File Selected";
            mDescField.Text = "";
            ChangeTypeCombo(-1);
            mFileCombo.SelectedItem = null;
            mPreviewBox.Visible = false;
            mPreviewField.Visible = true;
            mPreviewField.Text = "";

            // Disabled all fields
            nullMedia = true;
            SetEnabledMedia(!nullMedia);
        }

        /// <summary>
        /// This method updates the information for the Media File on the editor
        /// GUI to that of the indicated index. Note that since this only called
        /// in response to the Media File list being updated, it has no safety
        /// measures to ensure that the index is valid.
        /// </summary>
        /// <param name="index">Represents the index for the Department which is
        /// to have it information displayed.</param>
        private void LoadMedia(int index)
        {
            // If necessary, enable all components
            if (nullMedia)
            {
                nullMedia = !nullMedia;
                SetEnabledMedia(!nullMedia);
            }

            // Get the Media File to be loaded
            MediaFile m = mediaList[index];

            // Set the components to match the file's data
            mIdField.Text = "" + m.ID;
            mNameField.Text = m.Name;
            mDescField.Text = m.Description;

            // The remaining components depend on the type
            // of the Media File that is being loaded
            String[] filter = null;
            String path = "";
            int pathLength = 0;
            if (m is Text)
            {
                // Set the type
                ChangeTypeCombo(0);

                // Set the filter type for the file combo
                filter = new String[1];
                filter[0] = "*.txt";
                path = db.DIR_TEXT;
                pathLength = db.DIR_TEXT.Length;

                // Load the preview
                mPreviewBox.Visible = false;
                mPreviewField.Visible = true;
                mPreviewField.Text = GetTextFile(db.DIR_TEXT + m.FileName);
            }
            else if (m is Image)
            {
                // Set the type
                ChangeTypeCombo(1);

                // Set the filter type for the file combo
                filter = new String[3];
                filter[0] = "*.png"; filter[1] = "*.bmp"; filter[2] = "*.jpg";
                path = db.DIR_IMAGE;
                pathLength = db.DIR_IMAGE.Length;

                // Load the preview
                mPreviewBox.Visible = true;
                mPreviewField.Visible = false;
                mPreviewBox.Image = GetImage(db.DIR_IMAGE + m.FileName, mPreviewBox.Width, mPreviewBox.Height);
            }
            else if (m is Video)
            {
                // Set the type
                ChangeTypeCombo(2);

                // Set the filter type for the file combo
                filter = new String[1];
                filter[0] = "*.mp4";
                path = db.DIR_VIDEO;
                pathLength = db.DIR_VIDEO.Length;

                // Load the preview
                mPreviewBox.Visible = false;
                mPreviewField.Visible = true;
                mPreviewField.Text = "No preview is available for video objects.";
            }

            // Populate the file name combo
            mFileCombo.Items.Clear();
            String[][] files = new String[filter.Length][];
            for (int i = 0; i < filter.Length; i++) { files[i] = Directory.GetFiles(path, filter[i]); }
            foreach (String[] sa in files)
            {
                foreach (String s in sa) { mFileCombo.Items.Add(s.Substring(pathLength)); }
            }

            // Set the combo box to the appropriate value
            int fileIndex = -1;
            Console.Out.Write("File Name: " + m.FileName);
            if(m.FileName.CompareTo("") != 0) {fileIndex = mFileCombo.Items.IndexOf(m.FileName);}
            mFileCombo.SelectedIndex = fileIndex;
            Console.Out.WriteLine("\tCombo Index: " + mFileCombo.SelectedIndex);
        }

        /// <summary>
        /// Method ensures that the Media Files' indices match their indices
        /// on the database's Media File list.
        /// </summary>
        private void NormalizeIndicesMedia()
        {
            // Set each Media object's ID to its index
            for (int i = 0; i < mediaList.Count; i++)
            {
                mediaList[i].ID = i;
                mSelect.Items[i].SubItems[0].Text = "" + i;
            }
        }

        // === Component Listeners ==============================================
        // ======================================================================

        /// <summary>
        /// This listener registers when a user selects an object on the Media File
        /// list and updates the information listed accordingly. This listener should
        /// not be changed - rather any changes that need be made should be made to
        /// the methods LoadMedia and NullifyMedia respectively.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void mSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mSelect.SelectedItems.Count > 0) { LoadMedia(mSelect.SelectedItems[0].Index); }
            else { NullifyMedia(); }
        }

        /// <summary>
        /// This listener operates the add Media File button and when activated creates
        /// a new Media File and adds it to the list. This includes adding it to the GUI
        /// list, the internal list, and also making it the currently selected Media File
        /// on the GUI. ID is set to be one higher than the last ID in the Media File list.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void mAddButton_Click(object sender, EventArgs e)
        {
            // Create a new Media File at the next available index
            MediaFile newMedia = new Text();
            if (mediaList.Count != 0) { newMedia.ID = mediaList[mediaList.Count - 1].ID + 1; }
            else { newMedia.ID = 0; }

            // Add it to the list
            ListViewItem newItem = GetSelectRow(newMedia.ID, newMedia.Name);
            mSelect.Items.Add(newItem);

            // Add Media File to the Media File list
            mediaList.Add(newMedia);

            // Unselect the current item
            if (mSelect.SelectedItems.Count != 0) { mSelect.SelectedItems[0].Selected = false; }

            // Set it to be the selected item
            mSelect.Items[mSelect.Items.Count - 1].Selected = true;
            mSelect.Items[mSelect.Items.Count - 1].ImageIndex = 0;

            // Add the new Department to the PoI Department list
            dMediaList.Items.Add(newMedia.Name);
        }

        /// <summary>
        /// This listener handles the deletion of a Department from the Media File lists.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void mRemoveButton_Click(object sender, EventArgs e)
        {
            // Get the index of the Department to be deleted
            int index = mSelect.SelectedItems[0].Index;

            // Remove the Department from any PoI's referencing it
            MediaFile m = mediaList[index];
            foreach (Department d in deptList) { d.RemoveMedia(m); }

            // Remove it from the Department List on the GUI and internally
            mSelect.Items[index].Selected = false;
            mSelect.Items.RemoveAt(index);
            mediaList.RemoveAt(index);

            // Renormalize the indices to ensure proper referencing
            NormalizeIndicesMedia();

            // Set the GUI Department list to display the Department closest to the deleted one
            if (index < mSelect.Items.Count) { mSelect.Items[index].Selected = true; }
            else if (mSelect.Items.Count > 0) { mSelect.Items[mSelect.Items.Count - 1].Selected = true; }

            // Update the PoI Department list
            dMediaList.Items.RemoveAt(index);

            // Refresh the list so it accounts for the change
            if (dSelect.SelectedIndices.Count != 0)
            {
                int temp = dSelect.SelectedIndices[0];
                dSelect.Items[temp].Selected = false;
                dSelect.Items[temp].Selected = true;
            }
        }

        /// <summary>
        /// This listener governs the save button. It copies all of the data currently dispalyed
        /// on the editor to the object being edited and then orders the database to save.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void mSaveButton_Click(object sender, EventArgs e)
        {
            // Get the Media File
            MediaFile mf = mediaList[mSelect.SelectedIndices[0]];

            // Set the component data to match the Media File's data
            mf.Name = mNameField.Text;
            mf.Description = mDescField.Text;
            mf.FileName = (String)mFileCombo.SelectedItem;

            // Update the Media selection list
            mSelect.Items[mSelect.SelectedIndices[0]].SubItems[1].Text = mNameField.Text;
            
            // Update the Department Media list
            dMediaList.Items[mSelect.SelectedIndices[0]] = mNameField.Text;

            // Save the database
            db.SaveDatabase();
        }

        /// <summary>
        /// This listener governs the file name combo box component. It also handles loading
        /// previews of the new file when it is changed.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void mFileCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Error prevention
            if (mSelect.SelectedItems.Count == 0) { return; }

            // Get the current Media File
            MediaFile mf = mediaList[mSelect.SelectedItems[0].Index];
            // If it is Text, change the text preview to the new file
            if (mf is Text)
            {
                String path = db.DIR_TEXT + mFileCombo.SelectedItem;
                mPreviewField.Text = GetTextFile(path);
            }
            // If its an Image, replace the image with the new one
            else if (mf is Image)
            {
                String path = db.DIR_IMAGE + mFileCombo.SelectedItem;
                mPreviewBox.Image = GetImage(path, mPreviewBox.Width, mPreviewBox.Height);
            }
            // Videos don't get previews, so we do nothing ig it is one.
        }

        /// <summary>
        /// This listener governs the file type combo box. It handles replacing a given
        /// Media File of one type with a new one of another type as well as all the
        /// many replacement that this change necessitates.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void mTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If this is being changed internally, do nothing -- its just loading
            if (adminChange) { return; }

            // Get the current Media File
            MediaFile curFile = mediaList[mSelect.SelectedIndices[0]];

            // Create a new Media of the desired type. Unexpected indices default to Text.
            MediaFile newFile = null;
            int type = -1;

            if (mTypeCombo.SelectedIndex <= 0 || mTypeCombo.SelectedIndex > 2) { newFile = new Text(); type = 0; }
            else if (mTypeCombo.SelectedIndex == 1) { newFile = new Image(); type = 1; }
            else if (mTypeCombo.SelectedIndex == 2) { newFile = new Video(); type = 2; }

            // Copy the type-independent fields to the new file
            newFile.ID = curFile.ID;
            newFile.Name = curFile.Name;
            newFile.Description = curFile.Description;

            // Replace the old Media File with the new one
            foreach (Department d in deptList)
            {
                // Only replace if the file actually exists
                if (d.ContainsMedia(curFile))
                {
                    d.RemoveMedia(curFile);
                    d.AddMedia(newFile);
                }
            }
            mediaList.Remove(curFile);
            mediaList.Insert(newFile.ID, newFile);

            // Reload the Media File
            mSelect.Items[newFile.ID].Selected = false;
            mSelect.Items[newFile.ID].Selected = true;

            // Update the Media selection list
            mSelect.Items[newFile.ID].ImageIndex = type;

            // Update the Department list
            dMediaList.Items[newFile.ID] = newFile.Name;
        }
    }
}
