using System;
using System.Collections;
using System.Windows.Forms;

// EditorForm Partial Class, Department Component
// Author: Kyle McCarty
namespace DatabaseEditor
{
    /// <summary>
    /// This class is connected to its parent, EditorForm, and contains all methods
    /// pertaining solely to the Department tab of the database editor.
    /// </summary>
    public partial class EditorForm : Form
    {
        private bool nullDept = false;

        // === Component Constructors ===========================================
        // ======================================================================

        /// <summary>
        /// This method handles some extra initialization of various components
        /// that is not handled by the auto-generated code from the visual GUI
        /// editor. All changes to GUI components must be done either here or
        /// via the visual GUI editor. The GUI editor output should not be altered
        /// in any fashion.
        /// </summary>
        private void PopulateTabDepartment()
        {
            // Apply listener to the dSelect list
            dSelect.ItemSelectionChanged += dSelect_SelectedIndexChanged;

            // Apply listener to the dMediaList list
            dMediaList.LostFocus += CheckedListBox_LostFocus;

            // Load the departments
            foreach (Department d in deptList) { dSelect.Items.Add(GetSelectRow(d.ID, d.Name)); }
            if (dSelect.Items.Count > 0) { dSelect.Items[0].Selected = true; }
            else { NullifyDepartment(); }

            // Load the Media list
            foreach (MediaFile m in mediaList) { dMediaList.Items.Add(m.Name); }
        }

        // === General Methods ==================================================
        // ======================================================================

        /// <summary>
        /// Method sets all of the components for the Department interface to
        /// either be enabled or disabled according to the parameter.
        /// </summary>
        /// <param name="enabled">Represents whether the components should be
        /// enabled or disabled. True is enabled, false is disabled.</param>
        private void SetEnabledDepartment(bool enabled)
        {
            dIdField.Enabled = enabled;
            dNameField.Enabled = enabled;
            dDescField.Enabled = enabled;
            dMediaList.Enabled = enabled;
            dSaveButton.Enabled = enabled;
        }

        /// <summary>
        /// This method sets the Department editor page to the "null" settings.
        /// It is used when no point of interest is selected on the Department
        /// selection list.
        /// </summary>
        private void NullifyDepartment()
        {
            // Set all Department fields to the "null" value
            dIdField.Text = "";
            dNameField.Text = "No Department Selected";
            dDescField.Text = "";
            for (int i = 0; i < dMediaList.Items.Count; i++) { dMediaList.SetItemChecked(i, false); }

            // Disabled all Department components
            nullDept = true;
            SetEnabledDepartment(!nullDept);
        }

        /// <summary>
        /// This method updates the information for the Department on the editor
        /// GUI to that of the indicated index. Note that since this only called
        /// in response to the Department list being updated, it has no safety
        /// measures to ensure that the index is valid.
        /// </summary>
        /// <param name="index">Represents the index for the Department which is
        /// to have it information displayed.</param>
        private void LoadDepartment(int index)
        {
            // Enable the Department components if necessary
            if (nullDept)
            {
                nullDept = !nullDept;
                SetEnabledDepartment(!nullDept);
            }

            // Get the Department to be loaded
            Department d = deptList[index];

            // Set the component values to match the Department's
            dIdField.Text = "" + d.ID;
            dNameField.Text = d.Name;
            dDescField.Text = d.Description;
            for (int i = 0; i < dMediaList.Items.Count; i++) { dMediaList.SetItemChecked(i, false); }
            foreach (MediaFile mf in d.getAllMedia()) { dMediaList.SetItemChecked(mf.ID, true); }
        }

        /// <summary>
        /// Method closes any gaps between indices for departments.
        /// </summary>
        private void NormalizeIndicesDepartment()
        {
            // Give each Department an ID equal to its index. Note that since the
            // departments are referenced as pointers, this does not damage the
            // integrity of the database.
            for (int i = 0; i < deptList.Count; i++)
            {
                deptList[i].ID = i;
                dSelect.Items[i].SubItems[0].Text = "" + i;
            }
        }

        // === Component Listeners ==============================================
        // ======================================================================

        /// <summary>
        /// This listener registers when a user selects an object on the Department
        /// list and updates the information listed accordingly. This listener should
        /// not be changed - rather any changes that need be made should be made to
        /// the methods LoadDepartment and NullifyDepartment respectively.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void dSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dSelect.SelectedItems.Count > 0) { LoadDepartment(dSelect.SelectedItems[0].Index); }
            else { NullifyDepartment(); }
        }

        /// <summary>
        /// This listener operates the add Department button and when activated creates
        /// a new Department and adds it to the list. This includes adding it to the GUI
        /// list, the internal list, and also making it the currently selected Department
        /// on the GUI. ID is set to be one higher than the last ID in the Department list.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void dAddButton_Click(object sender, EventArgs e)
        {
            // Create a new Department at the next available index
            Department newDept = new Department();
            if (deptList.Count != 0) { newDept.ID = deptList[deptList.Count - 1].ID + 1; }
            else { newDept.ID = 0; }

            // Add it to the list
            ListViewItem newItem = GetSelectRow(newDept.ID, newDept.Name);
            dSelect.Items.Add(newItem);

            // Add Department to the Department list
            deptList.Add(newDept);

            // Unselect the current item
            if (dSelect.SelectedItems.Count != 0) { dSelect.SelectedItems[0].Selected = false; }

            // Set it to be the selected item
            dSelect.Items[dSelect.Items.Count - 1].Selected = true;

            // Add the new Department to the PoI Department list
            pDeptList.Items.Add(newDept.Name);
        }

        /// <summary>
        /// This listener handles the deletion of a Department from the Department lists.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void dRemoveButton_Click(object sender, EventArgs e)
        {
            if (dSelect.SelectedItems.Count != 0)
            {
                // Get the index of the Department to be deleted
                int index = dSelect.SelectedItems[0].Index;

                // Remove the Department from any PoI's referencing it
                Department d = deptList[index];
                foreach (PoI p in poiList) { p.RemoveDepartment(d); }

                // Remove it from the Department List on the GUI and internally
                dSelect.Items[index].Selected = false;
                dSelect.Items.RemoveAt(index);
                deptList.RemoveAt(index);

                // Renormalize the indices to ensure proper referencing
                NormalizeIndicesDepartment();

                // Set the GUI Department list to display the Department closest to the deleted one
                if (index < dSelect.Items.Count) { dSelect.Items[index].Selected = true; }
                else if (dSelect.Items.Count > 0) { dSelect.Items[dSelect.Items.Count - 1].Selected = true; }

                // Update the PoI Department list
                pDeptList.Items.RemoveAt(index);

                // Refresh the list so it accounts for the change
                if (pSelect.SelectedIndices.Count != 0)
                {
                    int temp = pSelect.SelectedIndices[0];
                    pSelect.Items[temp].Selected = false;
                    pSelect.Items[temp].Selected = true;
                }
            }
        }

        /// <summary>
        /// This listener governs the save button. It copies all of the data currently dispalyed
        /// on the editor to the object being edited and then orders the database to save.
        /// </summary>
        /// <param name="sender">Represents the object that registered the change.</param>
        /// <param name="e">Represents the event object.</param>
        private void dSaveButton_Click(object sender, EventArgs e)
        {
            // Get the current Department
            Department d = deptList[dSelect.SelectedIndices[0]];

            // Set the Department's data to match the data present on the components
            d.Name = dNameField.Text;
            d.Description = dDescField.Text;

            // Update the associated Media Files
            d.ClearMedia();
            for (int i = 0; i < mediaList.Count; i++) { if (dMediaList.GetItemChecked(i)) { d.AddMedia(mediaList[i]); } }

            // Update the select list
            dSelect.Items[dSelect.SelectedIndices[0]].SubItems[1].Text = dNameField.Text;

            // Update the PoI Department checklist
            pDeptList.Items[dSelect.SelectedIndices[0]] = dNameField.Text;

            // Save the database
            db.SaveDatabase();
        }
    }
}
