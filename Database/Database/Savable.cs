using System;
using System.Xml;

// Savable Abstract Class
// Author: Kyle McCarty

/// <summary>
/// This class represents any object that can be saved to the database. It
/// has only one method that is required for implementation, namely Savable,
/// which converts the object to a database-readable text form. This class
/// has as a child class DataType.
/// </summary>
public abstract class Savable
{
    /// <summary>
    /// Method should generate a String containing all the details of the object
    /// in the psuedo-XML format used by the database for information storage and
    /// retrieval.
    /// </summary>
    /// <returns>Should return a String containing defining the object in
    /// database-readable format.</returns>
    internal abstract String GetSaveOutput();

    /// <summary>
    /// This method produces and XML element node containing the object's data and
    /// returns it.
    /// </summary>
    /// <returns>Returns an XmlElement representing the object.</returns>
    internal abstract XmlElement GetXML(XmlDocument doc);
}
