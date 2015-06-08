using System;
using System.Xml;

static class XMLConstants
{
    // === XML Tag References ===============================================
    // ======================================================================

    // XML tag for overall document
    internal static String DATABASE = "Data";

    // XML tags for PoI, Department, and Media datafiles
    internal static String POI_DATA = "POIData";
    internal static String DEPT_DATA = "DepartmentData";
    internal static String MEDIA_DATA = "MediaData";

    // XML tags for individual PoI, Department, and Media objects
    internal static String POI = "POI";
    internal static String DEPT = "Department";
    internal static String MEDIA = "Media";
    
    // Shared element tags
    internal static String ID = "ID";
    internal static String NAME = "Name";
    internal static String DESC = "Discription";

    // PoI-specific elements
    internal static String LOC = "Location";
    internal static String ICON = "Icon";
    internal static String DEPT_ASSOC = "DeptID";

    // Department-specific elements
    internal static String MEDIA_ASSOC = "MediaID";

    // Media File-specific tags
    internal static String FILE_NAME = "FileName";

    // === Helper Methods ===================================================
    // ======================================================================

    /// <summary>
    /// This method produces an XML element with only the tag and no value.
    /// It is used to create higher level XML tags that do not have specific
    /// values, but rather have several additional tagged values within them.
    /// </summary>
    /// <param name="document">Indicates the document currently being used. It
    /// is used to generate the new element object with the same properties as
    /// the document.</param>
    /// <param name="elementTag">Indicates the tag that is to be created. This
    /// should be the value that falls between the brackets in the XML
    /// document.</param>
    /// <returns>Returns an XmlElement object with no textual data.</returns>
    internal static XmlElement CreateXmlElement(XmlDocument document, String elementTag)
    {
        // Create the new element
        XmlElement newElement = document.CreateElement(elementTag);

        // Return the new element
        return newElement;
    }

    /// <summary>
    /// This method creates an XML element with ab included, enclosed value.
    /// It is used to quickly and concisely create XML elements that encompass
    /// single values.
    /// </summary>
    /// <param name="document">Indicates the document currently being used. It
    /// is used to generate the new element and element text objects with the
    /// same properties as the document.</param>
    /// <param name="elementTag">Indicates the tag that is to be created. This
    /// should be the value that falls between the brackets in the XML
    /// document.</param>
    /// <param name="val">Indicates the textual value that is to lie between
    /// the tags.</param>
    /// <returns>Returns an XmlElement object with the indicated textual data.
    /// </returns>
    internal static XmlElement CreateXmlElement(XmlDocument document, String elementTag, String val)
    {
        // Create the new element
        XmlElement newElement = document.CreateElement(elementTag);
        // Create the textual data and associate it with the element
        XmlText newValue = document.CreateTextNode(val);
        newElement.AppendChild(newValue);

        // Return the element
        return newElement;
    }
}
