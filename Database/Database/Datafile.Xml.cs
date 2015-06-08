using System;
using System.Collections.Generic;
using System.Xml;

// Datafile.Xml Partial Class, XML Component
// Author: Kyle McCarty

/// <summary>
/// This section of the Datafile class is reponsible for the generation of
/// XML data used in saving the database. It contains a series of methods for
/// converting the database's Savable objects to an XML format as well as
/// helper methods for making this process more efficient. Note that all of
/// the Datafile.Xml class' methods are internal as they are used for saving
/// the database and shouldn't need to be accessed by external classes.
/// </summary>
public partial class Datafile
{
    // === XML Generation Methods ===========================================
    // ======================================================================

    /// <summary>
    /// This method collates the data contained within the point of interest
    /// object and formats it in an XML format as defined by the associated
    /// schema for GVirtual. All tags are referenced in the through the
    /// XMLConstants static class to ensure that if they need be changed, it
    /// can be done without having to search for every instance of the tags
    /// throughout the document.
    /// </summary>
    /// <returns>Method returns an XmlDocument object representing the PoI
    /// object. This XmlDocument can be used to save the file, if needed or
    /// desired.</returns>
    internal XmlElement GetXmlData(int type, XmlDocument doc)
    {
        // Create the collection node
        XmlElement root = null;

        // Create and add a new entry for each object type
        if (type == Constants.TYPE_POI)
        {
            // Define the collection node
            root = doc.CreateElement(XMLConstants.POI_DATA);
            // Populate the collection node
            foreach (PoI p in poiList) { root.AppendChild(p.GetXML(doc)); }
        }
        else if (type == Constants.TYPE_DEPARTMENT)
        {
            // Define the collection node
            root = doc.CreateElement(XMLConstants.DEPT_DATA);
            // Populate the collection node
            foreach (Department d in deptList) { root.AppendChild(d.GetXML(doc)); }
        }
        else if (type == Constants.TYPE_MEDIA)
        {
            // Define the collection node
            root = doc.CreateElement(XMLConstants.MEDIA_DATA);
            // Populate the collection node
            foreach (MediaFile m in mediaList) { root.AppendChild(m.GetXML(doc)); }
        }

        return root;

       // // Add the root node to the document
       // doc.AppendChild(root);

       // // Return the document
       //return doc;
    }
}