using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;

// EditorForm Partial Class, Point of Interest Component
// Author: Kyle McCarty
namespace DatabaseEditor
{
    /// <summary>
    /// This class is connected to its parent, EditorForm, and contains all methods
    /// pertaining solely to the PoI tab of the database editor.
    /// </summary>
    public partial class EditorForm : Form
    {
        private bool poiNull = false;

        // === Component Constructors ===========================================
        // ======================================================================

        /// <summary>
        /// This method handles some extra initialization of various components
        /// that is not handled by the auto-generated code from the visual GUI
        /// editor. All changes to GUI components must be done either here or
        /// via the visual GUI editor. The GUI editor output should not be altered
        /// in any fashion.
        /// </summary>
        public void PopulateTabPOI()
        {
            // Populate Department list
            for (int i = 0; i < deptList.Count; i++) { pDeptList.Items.Add(deptList[i].Name); }

            // Add listener for the icon combo box
            pIconCombo.SelectedIndexChanged += pIconCombo_SelectedIndexChanged;

            // Add listener for the Department list
            pDeptList.LostFocus += CheckedListBox_LostFocus;

            // Populate the PoI icon combo box. Support .png, .bmp, and .jpg
            int pathLength = db.DIR_ICON.Length;
            String[] png = Directory.GetFiles(db.DIR_ICON, "*.png");
            for (int i = 0; i < png.Length; i++) { pIconCombo.Items.Add(png[i].Substring(pathLength)); }
            String[] bmp = Directory.GetFiles(db.DIR_ICON, "*.bmp");
            for (int i = 0; i < bmp.Length; i++) { pIconCombo.Items.Add(bmp[i].Substring(pathLength)); }
            String[] jpg = Directory.GetFiles(db.DIR_ICON, "*.jpg");
            for (int i = 0; i < jpg.Length; i++) { pIconCombo.Items.Add(jpg[i].Substring(pathLength)); }

            // Add the points of interest to the list
            if (poiList.Count != 0)
            {
                foreach (PoI p in poiList) { pSelect.Items.Add(GetSelectRow(p.ID, p.Name)); }
                pSelect.Items[0].Selected = true;
            }
            else { NullifyPOI(); }
        }

        // === General Methods ==================================================
        // ======================================================================

        /// <summary>
        /// This method sets the point of interest editor page to the "null"
        /// settings. It is used when no point of interest is selected on the PoI
        /// selection list.
        /// </summary>
        private void NullifyPOI()
        {
            // Set PoI fields to their "null" values
            pIdField.Text = "";
            pNameField.Text = "No Point Selected";
            pDescField.Text = "";
            pXField.Value = 0;
            pYField.Value = 0;
            for (int i = 0; i < pDeptList.Items.Count; i++) { pDeptList.SetItemChecked(i, false); }
            int nullIndex = pIconCombo.Items.IndexOf("null.png");
            pIconCombo.SelectedIndex = nullIndex;
            pIconBox.Image = pIconBox.Image = GetImage(db.DIR_ICON + "null.png", pIconBox.Width, pIconBox.Height);

            // Mark the PoI interface as null
            poiNull = true;
            SetEnabledPOI(!poiNull);
        }

        /// <summary>
        /// Method sets all of the components for the PoI interface to either be
        /// enabled or disabled according to the parameter.
        /// </summary>
        /// <param name="enabled">Represents the state to which the components
        /// are to be set.</param>
        private void SetEnabledPOI(bool enabled)
        {
            pIdField.Enabled = enabled;
            pNameField.Enabled = enabled;
            pDescField.Enabled = enabled;
            pDeptList.Enabled = enabled;
            pIconBox.Enabled = enabled;
            pIconCombo.Enabled = enabled;
            pXField.Enabled = enabled;
            pYField.Enabled = enabled;
            pSaveButton.Enabled = enabled;
        }

        /// <summary>
        /// This method updates the information for the PoI on the editor GUI to
        /// that of the indicated index. Note that since this only called in
        /// response to the PoI list being updated, it has no safety measures to
        /// ensure that the index is valid.
        /// </summary>
        /// <param name="index">Represents the index for the PoI which is to have
        /// it information displayed.</param>
        private void LoadPOI(int index)
        {
            // If the interface is null, enable it
            if (poiNull)
            {
                poiNull = !poiNull;
                SetEnabledPOI(!poiNull);
            }

            // Load the PoI
            PoI p = poiList[index];

            // Set PoI fields to the value loaded from the database
            pIdField.Text = "" + p.ID;
            pNameField.Text = p.Name;
            pDescField.Text = p.Description;
            pXField.Value = Convert.ToDecimal(p.X);
            pYField.Value = Convert.ToDecimal(p.Y);

            // Clear the Department check list
            for (int i = 0; i < pDeptList.Items.Count; i++) { pDeptList.SetItemChecked(i, false); }

            // Get the PoI's Department list and check off the ones it has
            List<Department> poiDeptList = p.GetDepartments();
            for (int i = 0; i < poiDeptList.Count; i++) { pDeptList.SetItemChecked(poiDeptList[i].ID, true); }

            // Set up the image combo box
            int iconIndex = pIconCombo.Items.IndexOf(p.IconName);
            pIconCombo.SelectedIndex = iconIndex;
            pIconBox.Image = pIconBox.Image = GetImage(db.DIR_ICON + p.IconName, pIconBox.Width, pIconBox.Height);
        }

        /// <summary>
        /// Method closes any gaps between indices for POIs.
        /// </summary>
        private void NormalizeIndicesPOI()
        {
            // Normalize the PoI indices first. Since nothing refernces them
            // we can just assign them an index.
            for (int i = 0; i < poiList.Count; i++)
            {
                poiList[i].ID = i;
                pSelect.Items[i].Text = "" + i;
            }
        }

        // === Component Listeners ==============================================
        // ======================================================================

        /// <summary>
        /// This listener registers when a user selects an object on the PoI list and
        /// updates the information listed accordingly. This listener should not be
        /// changed - rather any changes that need be made should be made to the methods
        /// LoadPOI and NullifyPOI respectively.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void pSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pSelect.SelectedItems.Count > 0) { LoadPOI(pSelect.SelectedItems[0].Index); }
            else { NullifyPOI(); }
        }

        /// <summary>
        /// This listener operates the add PoI button and when activated creates a new PoI
        /// and adds it to the list. This includes adding it to the GUI list, the internal
        /// list, and also making it the currently selected PoI on the GUI. ID is set to be
        /// one higher than the last ID in the PoI list.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void pAddButton_Click(object sender, EventArgs e)
        {
            // Create a new PoI at the next available index
            PoI newPOI = new PoI();
            if (poiList.Count != 0) { newPOI.ID = poiList[poiList.Count - 1].ID + 1; }
            else { newPOI.ID = 0; }

            // Add it to the list
            ListViewItem newItem = GetSelectRow(newPOI.ID, newPOI.Name);
            pSelect.Items.Add(newItem);

            // Add PoI to the PoI List
            poiList.Add(newPOI);

            // Unselect the current item
            if (pSelect.SelectedItems.Count != 0) { pSelect.SelectedItems[0].Selected = false; }

            // Set it to be the selected item
            pSelect.Items[pSelect.Items.Count - 1].Selected = true;
        }

        /// <summary>
        /// This listener handles the deletion of a PoI from the PoI lists.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void pRemoveButton_Click(object sender, EventArgs e)
        {
            if (pSelect.SelectedItems.Count != 0)
            {
                // Get the index of the PoI to be deleted
                int index = pSelect.SelectedItems[0].Index;
                // Remove it from the PoI List on the GUI and internally
                pSelect.Items[index].Selected = false;
                pSelect.Items.RemoveAt(index);
                poiList.RemoveAt(index);

                // Renormalize the indices to ensure proper referencing
                NormalizeIndicesPOI();

                // Set the GUI PIO list to display the PoI closest to the deleted one
                if (index < pSelect.Items.Count) { pSelect.Items[index].Selected = true; }
                else if (pSelect.Items.Count > 0) { pSelect.Items[pSelect.Items.Count - 1].Selected = true; }
            }
        }

        /// <summary>
        /// This listener governs the save button. It copies all of the data currently dispalyed
        /// on the editor to the object being edited and then orders the database to save.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void pSaveButton_Click(object sender, EventArgs e)
        {
            if (pSelect.SelectedItems.Count != 0)
            {
                // Get the selected PoI
                int poiIndex = pSelect.SelectedItems[0].Index;
                PoI p = poiList[poiIndex];

                // Update it to match the information currently shown
                p.Name = pNameField.Text;
                p.Description = pDescField.Text;
                p.Location = new Point(Convert.ToDouble(pXField.Value), Convert.ToDouble(pYField.Value));
                
                // Set the departments to those selected
                p.ClearDepartments();
                for (int i = 0; i < deptList.Count; i++) { if (pDeptList.GetItemChecked(i) == true) { p.AddDepartment(deptList[i]); } }

                // Set the icon file to the one selected
                p.IconName = (String)pIconCombo.Items[pIconCombo.SelectedIndex];

                // Update the select list
                pSelect.Items[poiIndex].SubItems[1].Text = pNameField.Text;

                // Save the database
                db.SaveDatabase();
            }
        }

        /// <summary>
        /// Listener responds to changes in the PoI icon selection box. It changes the
        /// currently selected map icon for the PoI.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void pIconCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String imagePath = db.DIR_ICON + pIconCombo.SelectedItem;
            pIconBox.Image = GetImage(imagePath, pIconBox.Width, pIconBox.Height);
        }
    }
}