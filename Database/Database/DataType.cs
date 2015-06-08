using System;
using System.Collections.Generic;
using System.Text;

// DataType Class
// Author: Kyle McCarty

/// <summary>
/// A DataType is an object that sreves as data within the database. It
/// has a set of methods that are common across all data types, but is
/// otherwise abstract. Most data storage is handled by its more specific
/// child classes. Is a parent class to PoI, Department, and MediaFile.
/// </summary>
public abstract class DataType : Savable
{
    protected String desc = "Place object description here.";
    protected String name = "Unnamed object";
    protected int id = -1;

    /// <summary>
    /// This parameter represents the numerical ID associated with every data
    /// type. Note that the ID is the database's primary key and should not
    /// ever be repeated for any reason within a data class. Data classes consist
    /// of Points of Interest, Departments, and Media. IDs can overlap across
    /// data classes, however. In general, the ID should not ever be modified.
    /// Rather, the database editor should be used to make changes the database
    /// and will automatically handle any ID changes that are necessary.
    /// </summary>
    public int ID
    {
        get { return id; }

        set { id = value; }
    }

    /// <summary>
    /// This parameter represents the display name of the object.
    /// </summary>
    public String Name
    {
        get { return name; }

        set { name = value; }
    }

    /// <summary>
    /// This parameter represents the discription of the object.
    /// </summary>
    public String Description
    {
        get { return desc; }

        set { desc = value; }
    }

    /// <summary>
    /// Method returns a replica of this object. Note that the abstract method
    /// does not ever get called and should be hidden by a method within each
    /// physical subclass class.
    /// </summary>
    /// <returns>Returns null.</returns>
    public DataType Clone() { return null; }
}