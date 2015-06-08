using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Xml;

// PoI Class
// Author: Kyle McCarty

/// <summary>
/// This class represents a point of interest. It holds information concerning
/// the point of interest's ID, name, description, map image file, map coordinates,
/// and associated departments as well as the methods necessary to manipulate
/// these. A point of interest is also a DataType.
/// </summary>
public class PoI : DataType
{
    private String fileName;
    private Point loc = new Point();
    private List<Department> deptList = new List<Department>();

    /// <summary>
    /// Basic constructor which produces an empty point of interest
    /// object. Note that the ID fields must be set before the point
    /// of interest be safely added to the database.
    /// </summary>
    public PoI()
    {
        fileName = "null.png";
        loc.X = loc.Y = 0;
    }

    /// <summary>
    /// Produces a point of interest object.
    /// </summary>
    /// <param name="poiID">Represents the point of interest's ID.</param>
    public PoI(int poiID)
    {
        id = poiID;
        fileName = "null.png";
        loc.X = loc.Y = 0;
    }

    /// <summary>
    /// Produces a point of interest object.
    /// </summary>
    /// <param name="poiID">Represents the point of interest's ID.</param>
    /// <param name="poiName">Represents the point of interest's name.</param>
    /// <param name="poiDescription">Represents a description of the point of interest.</param>
    /// <param name="x">Represents the point of interest's x-position on the map.</param>
    /// <param name="y">Represents the point of interest's y-position on the map.</param>
    public PoI(int poiID, String poiName, String poiDescription, double x, double y)
    {
        id = poiID;
        name = poiName;
        desc = poiDescription;
        fileName = "null.png";
        loc.X = x;
        loc.Y = y;
    }

    /// <summary>
    /// This parameter represents the file name of the map icon image used by this
    /// Point of Interest.
    /// </summary>
    public String IconName
    {
        get { return fileName; }

        set { fileName = value; }
    }

    /// <summary>
    /// This parameter represents the location on the map at which this Point of
    /// Interest should appear.
    /// </summary>
    public Point Location
    {
        get { return loc; }

        set { loc = value; }
    }

    /// <summary>
    /// This parameter contains the x-coordinate of the Point of Interest's map
    /// location.
    /// </summary>
    public double X
    {
        get { return loc.X; }

        set { loc.X = value; }
    }

    /// <summary>
    /// This parameter contains the y-coordinate of the Point of Interest's map
    /// location.
    /// </summary>
    public double Y
    {
        get { return loc.Y; }

        set { loc.Y = value; }
    }

    /// <summary>
    /// Associates a Department with this point of interest.
    /// </summary>
    /// <param name="d">Represents the Department which is to be associated.</param>
    public void AddDepartment(Department d) { deptList.Add(d); }

    /// <summary>
    /// Removes a Department association from this point of interest. Method does noting if an invalid
    /// Department ID is given.
    /// </summary>
    /// <param name="deptID">Represents the ID of the Department to be removed.</param>
    public void RemoveDepartment(int deptID)
    {
        for (int i = 0; i < deptList.Count; i++) { if (deptList[i].ID == deptID) { deptList.Remove(deptList[i]); } }
    }

    /// <summary>
    /// Removes a Department association from this point of interest.
    /// </summary>
    /// <param name="d">Represents the Department which is to be removed.</param>
    public void RemoveDepartment(Department d) { if (deptList.Contains(d)) { deptList.Remove(d); } }

    /// <summary>
    /// Method completely removes all departments from this PoI object.
    /// </summary>
    public void ClearDepartments() { deptList.Clear(); }

    /// <summary>
    /// Provides all of the departments that are associated with this point of interest.
    /// </summary>
    /// <returns>Returns a Department[] containing the associated departments.</returns>
    public List<Department> GetDepartments()
    {
        //Department[] d = new Department[deptList.Count];
        //for (int i = 0; i < deptList.Count; i++) { d[i] = deptList[i]; }

        return deptList;
    }

    /// <summary>
    /// Provides a specific Department associated with this point of interest. If the
    /// index requested does not exist, method returns null.
    /// </summary>
    /// <param name="index">Returns a Department if one exists at the requested index.
    /// Otherwise, returns null.</param>
    /// <returns></returns>
    public Department GetDepartment(int index)
    {
        if (deptList.Count <= index) { return deptList[index]; }
        else { return null; }
    }

    /// <summary>
    /// Provides a textual representation of the point of interest object.
    /// </summary>
    /// <returns>Returns a String of the format Name:[Name]; Location:([x],[y]); 
    /// IconPath:[Path]; Departments:{[Dpt1], [Dpt2], ...}</returns>
    public override String ToString()
    {
        StringBuilder s = new StringBuilder();
        s.Append("Name:\"" + name + "\"; Location:(" + loc.X + ", " + loc.Y + "); IconName:\"" + fileName + "\"");
        s.Append("\nDepartments:{");

        for (int i = 0; i < deptList.Count; i++)
        {
            s.Append(deptList[i].ID);
            if (i != deptList.Count - 1) { s.Append(", "); }
        }
        s.Append("}/n");

        return s.ToString();
    }

    /// <summary>
    /// Converts the point of interest object to the textual format used in the database.
    /// This method is used internally for outputting to the database.
    /// </summary>
    /// <returns>Returns a String containing all the information in the point of interest
    /// formatted as it is in the database.</returns>
    internal override String GetSaveOutput()
    {
        StringBuilder s = new StringBuilder();
        s.AppendLine("<" + Constants.TAG_POI + ">");
        s.AppendLine("<" + Constants.TAG_ID + ">" + id);
        s.AppendLine("<" + Constants.TAG_NAME + ">" + name);
        s.AppendLine("<" + Constants.TAG_LOCATION + ">" + loc.X + "," + loc.Y);
        s.AppendLine("<" + Constants.TAG_DESCRIPTION + ">" + desc);
        s.AppendLine("<" + Constants.TAG_FILENAME + ">" + fileName);
        for (int i = 0; i < deptList.Count; i++) { s.AppendLine("<" + Constants.TAG_DEPARTMENT + ">" + deptList[i].ID); }
        s.AppendLine("</" + Constants.TAG_POI + ">");

        return s.ToString();
    }

    /// <summary>
    /// Methods reutrns a new PoI object with the same properties as the calling object. Note that
    /// method will retain the references to this object's Department objects, but these references
    /// are not themselves cloned, so any changes made to them will affect the original. Only fields of
    /// this physical object are cloned newly. Method replaces the general Clone method of abstract class
    /// DataType.
    /// </summary>
    /// <returns>Returns a new PoI object that is an exact replica of the calling object.</returns>
    new public PoI Clone()
    {
        PoI p = new PoI();
        p.ID = id;
        p.Name = name;
        p.Description = desc;
        p.Location = new Point(loc.X, loc.Y);
        for (int i = 0; i < deptList.Count; i++) { p.AddDepartment(deptList[i]); }

        return p;
    }

    /// <summary>
    /// This method produces and XML element node containing the object's data and
    /// returns it.
    /// </summary>
    /// <returns>Returns an XmlElement representing the PoI object.</returns>
    internal override XmlElement GetXML(XmlDocument doc)
    {
        // Create the element nodes
        XmlElement poi = XMLConstants.CreateXmlElement(doc, XMLConstants.POI);
        XmlElement valID = XMLConstants.CreateXmlElement(doc, XMLConstants.ID, "" + id);
        XmlElement valName = XMLConstants.CreateXmlElement(doc, XMLConstants.NAME, name);
        XmlElement valDesc = XMLConstants.CreateXmlElement(doc, XMLConstants.DESC, desc);
        XmlElement valLoc = XMLConstants.CreateXmlElement(doc, XMLConstants.LOC, loc.X + "," + loc.Y);
        XmlElement valIcon = XMLConstants.CreateXmlElement(doc, XMLConstants.ICON, fileName);

        // Create the Department association nodes
        List<XmlElement> pDept = new List<XmlElement>();
        foreach (Department d in deptList)
        {
            XmlElement curDept = XMLConstants.CreateXmlElement(doc, XMLConstants.DEPT_ASSOC, "" + d.ID);
            pDept.Add(curDept);
        }

        // Add the element nodes to the PoI collection node
        poi.AppendChild(valID);
        poi.AppendChild(valName);
        poi.AppendChild(valDesc);
        poi.AppendChild(valLoc);
        poi.AppendChild(valIcon);
        foreach (XmlElement e in pDept) { poi.AppendChild(e); }

        // Return the element node
        return poi;
    }
}