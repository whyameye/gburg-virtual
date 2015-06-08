using System;
using System.Text;
using System.IO;
using System.Xml;

// MediaFile Class
// Author: Kyle McCarty

/// <summary>
/// This class represents a Media File. It holds information concerning
/// the Media File's directory, and file name, in addition to all the
/// information contained within a DataType. A Media File is a child
/// class of DataType. A MediaFile is part of the Media data class for
/// determining when IDs overlap.
/// </summary>
public abstract class MediaFile : DataType
{
    protected String fileName = "";

    /// <summary>
    /// Sets the name of the Media File. Note that the file extension is required
    /// in order for the reference to work. i.e. "sphere.png"
    /// </summary>
    /// <param name="mediaFileName">Represents the name of the Media File.</param>
    //public void SetFileName(String mediaFileName) { fileName = mediaFileName; }

    /// <summary>
    /// Provides the files file name.
    /// </summary>
    /// <returns>Returns a <c>String</c> containing the file name.</returns>
    //public String GetFileName() { return fileName; }

    public String FileName
    {
        get { return fileName; }

        set { fileName = value; }
    }

    /// <summary>
    /// Provides a textual representation of the Media File object.
    /// </summary>
    /// <returns>Returns a String of the format Type:[Type]; Name:[Name];
    /// Dir:[Dir]</returns>
    public override String ToString()
    {
        String type = "None";
        if (this is Text) { type = "Text"; }
        else if (this is Image) { type = "Image"; }
        else if (this is Video) { type = "Video"; }

        return "Type: \"" + type + "\"; Name:\"" + name + "; FileName:\"" + fileName + "\"";
    }

    /// <summary>
    /// Converts the Media File object to the textual format used in the database.
    /// This method is used internally for outputting to the database.
    /// </summary>
    /// <returns>Returns a String containing all the information in the
    /// Media File formatted as it is in the database.</returns>
    internal override String GetSaveOutput()
    {
        // Create necessary variables
        StringBuilder s = new StringBuilder();
        int type = -1;

        // Determine the type of this base file
        if (this is Text) { type = Constants.MEDIA_TEXT; }
        else if (this is Image) { type = Constants.MEDIA_IMAGE; }
        else if (this is Video) { type = Constants.MEDIA_VIDEO; }

        // Write the save output
        s.AppendLine("<" + Constants.TAG_MEDIA[type] + ">");
        s.AppendLine("<" + Constants.TAG_ID + ">" + id);
        s.AppendLine("<" + Constants.TAG_NAME + ">" + name);
        s.AppendLine("<" + Constants.TAG_DESCRIPTION + ">" + desc);
        s.AppendLine("<" + Constants.TAG_FILENAME + ">" + fileName);
        s.AppendLine("</" + Constants.TAG_MEDIA[type] + ">");

        // Return the save output
        return s.ToString();
    }

    /// <summary>
    /// This method generates a duplicate of this object. Note that the abstract class
    /// method is replaced by a class specific method in each subclass which returns an
    /// object of its specific class. The generalized method does not return anything.
    /// </summary>
    /// <returns>Returns null.</returns>
    new public MediaFile Clone() { return null; }

    /// <summary>
    /// Method generates an XML element based on the XML tags defined in the XMLConstants class and
    /// returns it as a String object.
    /// </summary>
    /// <returns>Returns an XML element as String representing the PoI object.</returns>
    internal override XmlElement GetXML(XmlDocument doc)
    {
        // Create the element nodes
        XmlElement media = XMLConstants.CreateXmlElement(doc, XMLConstants.MEDIA);
        XmlElement valID = XMLConstants.CreateXmlElement(doc, XMLConstants.ID, "" + id);
        XmlElement valName = XMLConstants.CreateXmlElement(doc, XMLConstants.NAME, name);
        XmlElement valDesc = XMLConstants.CreateXmlElement(doc, XMLConstants.DESC, desc);
        XmlElement valFileName = XMLConstants.CreateXmlElement(doc, XMLConstants.FILE_NAME, fileName);

        // Add the element nodes to the Department collection node
        media.AppendChild(valID);
        media.AppendChild(valName);
        media.AppendChild(valDesc);
        media.AppendChild(valFileName);

        // Return the element node
        return media;
    }
}
