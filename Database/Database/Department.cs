using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

// Department Class
// Author: Kyle McCarty

/// <summary>
/// This class represents a Department. It holds information concerning
/// the Department's ID, name, description, and associated Media Files
/// as well as the methods necessary to manipulate these. A Department
/// is also a DataType. A Department part of the Department data class
/// for the purposes of determining when IDs.
/// overlap.
/// </summary>
public class Department : DataType
{
    private List<Image> imageList = new List<Image>();
    private List<Text> textList = new List<Text>();
    private List<Video> videoList = new List<Video>();

    /// <summary>
    /// Basic constructor which produces an empty Department object.
    /// Note that the ID fields must be set before the Department can
    /// be safely added to the database.
    /// </summary>
    public Department() { }

    /// <summary>
    /// Produces a Department object.
    /// </summary>
    /// <param name="deptID">Represents the Department's ID.</param>
    public Department(int deptID) { id = deptID; }

    /// <summary>
    /// Produces a Department object.
    /// </summary>
    /// <param name="deptID">Represents the Department's ID.</param>
    /// <param name="deptName">Represents the Department's name.</param>
    /// <param name="deptDescription">Represents a descreption of the Department.</param>
    public Department(int deptID, String deptName, String deptDescreption)
    {
        id = deptID;
        name = deptName;
        desc = deptDescreption;
    }

    /// <summary>
    /// Associates a Media File with this Department.
    /// </summary>
    /// <param name="f">Indicates the Media File that is to be associated with
    /// this Department.</param>
    public void AddMedia(MediaFile f)
    {
        if (f is Text) { textList.Add((Text)f); }
        else if (f is Image) { imageList.Add((Image)f); }
        else if (f is Video) { videoList.Add((Video)f); }
    }

    /// <summary>
    /// Provides the Media File of type Text of the requested index that is
    /// associated with this Department. If said file does not exist, null is
    /// returned.
    /// </summary>
    /// <param name="index">Represents the index of the desired object.</param>
    /// <returns>Provides a Text object if the file exists, otherwise
    /// returns null.</returns>
    public Text GetText(int index)
    {
        if (textList.Count < index) { return textList[index]; }
        else { return null; }
    }

    /// <summary>
    /// Provides the Media File of type Image of the requested index that is
    /// associated with this Department. If said file does not exist, null is
    /// returned.
    /// </summary>
    /// <param name="index">Represents the index of the desired object.</param>
    /// <returns>Provides a Image object if the file exists, otherwise
    /// returns null.</returns>
    public Image GetImage(int index)
    {
        if (imageList.Count < index) { return imageList[index]; }
        else { return null; }
    }

    /// <summary>
    /// Provides the Media File of type ideo of the requested index that is
    /// associated with this Department. If said file does not exist, null is
    /// returned.
    /// </summary>
    /// <param name="index">Represents the index of the desired object.</param>
    /// <returns>Provides a Video object if the file exists, otherwise
    /// returns null.</returns>
    public Video GetVideo(int index)
    {
        if (videoList.Count < index) { return videoList[index]; }
        else { return null; }
    }

    /// <summary>
    /// Method provides the means to remove a Media File from being associated with
    /// this Department. Method covers all three types of Media File.
    /// </summary>
    /// <param name="mf">Represents the Media File that is to be removed.</param>
    public void RemoveMedia(MediaFile mf)
    {
        if (mf is Text) { if (textList.Contains((Text)mf)) { textList.Remove((Text)mf); } }
        else if (mf is Image) { if (imageList.Contains((Image)mf)) { imageList.Remove((Image)mf); } }
        else if (mf is Video) { if (videoList.Contains((Video)mf)) { videoList.Remove((Video)mf); } }
    }

    /// <summary>
    /// Method provides the means to determine if the Department contains a given
    /// Media File.
    /// </summary>
    /// <param name="mf">Represents the Media File to be searched for.</param>
    /// <returns>Returns a boolean value indicating whether or not the Media
    /// file is assocaited with this Department.</returns>
    public bool ContainsMedia(MediaFile mf)
    {
        if (mf is Text) { return textList.Contains((Text)mf); }
        if (mf is Image) { return imageList.Contains((Image)mf); }
        if (mf is Video) { return videoList.Contains((Video)mf); }

        return false;
    }

    /// <summary>
    /// Provides a textual representation of the Department object.
    /// </summary>
    /// <returns>Returns a String of the format Name:[Name]; TextList:
    /// {[Txt1], ...}; ImageList:{[Img1], ...}; VideoList:{[Vdo1], ...}</returns>
    public override String ToString()
    {
        StringBuilder s = new StringBuilder();
        s.Append("Name:\"" + name + "\"TextList:{");
        for (int i = 0; i < textList.Count; i++)
        {
            s.Append(textList[i].ID);
            if (i != textList.Count - 1) { s.Append(", "); }
        }
        s.Append("}; ImageList:{");
        for (int i = 0; i < imageList.Count; i++)
        {
            s.Append(imageList[i].ID);
            if (i != imageList.Count - 1) { s.Append(", "); }
        }
        s.Append("}; Video List:{");
        for (int i = 0; i < videoList.Count; i++)
        {
            s.Append(videoList[i].ID);
            if (i != videoList.Count - 1) { s.Append(", "); }
        }
        s.Append("}\n");

        return s.ToString();
    }

    /// <summary>
    /// Converts the Department object to the textual format used in the database.
    /// This method is used internally for outputting to the database.
    /// </summary>
    /// <returns>Returns a String containing all the information in the
    /// Department formatted as it is in the database.</returns>
    internal override string GetSaveOutput()
    {
        StringBuilder s = new StringBuilder();
        s.AppendLine("<" + Constants.TAG_DEPARTMENT + ">");
        s.AppendLine("<" + Constants.TAG_ID + ">" + id);
        s.AppendLine("<" + Constants.TAG_NAME + ">" + name);
        s.AppendLine("<" + Constants.TAG_DESCRIPTION + ">" + desc);
        for (int i = 0; i < textList.Count; i++) { s.AppendLine("<" + Constants.TAG_TEXT + ">" + textList[i].ID); }
        for (int i = 0; i < imageList.Count; i++) { s.AppendLine("<" + Constants.TAG_IMAGE + ">" + imageList[i].ID); }
        for (int i = 0; i < videoList.Count; i++) { s.AppendLine("<" + Constants.TAG_VIDEO + ">" + videoList[i].ID); }
        s.AppendLine("</" + Constants.TAG_DEPARTMENT + ">");

        return s.ToString();
    }

    /// <summary>
    /// Method returns a single list which contains all the Media Files that are
    /// associated with this Department.
    /// </summary>
    /// <returns>Returns a List of type MediaFile with all the associated Media
    /// for this Department.</returns>
    public List<MediaFile> getAllMedia()
    {
        // Create a new list
        List<MediaFile> mediaList = new List<MediaFile>();

        // Add all the Media Files from each list
        foreach (Text t in textList) { mediaList.Add(t); }
        foreach (Image i in imageList) { mediaList.Add(i); }
        foreach (Video v in videoList) { mediaList.Add(v); }

        // Return the conglomeration
        return mediaList;
    }

    /// <summary>
    /// Method clears all of the Media lists associated with this Department.
    /// </summary>
    public void ClearMedia()
    {
        imageList.Clear();
        textList.Clear();
        videoList.Clear();
    }

    /// <summary>
    /// Methods reutrns a new Department object with the same properties as the calling object.
    /// Note that method will retain the references to this object's Media Files, but these references
    /// are not themselves cloned, so any changes made to them will affect the original. Only fields of
    /// this physical object are cloned newly. Method replaces the general Clone method of abstract class
    /// DataType.
    /// </summary>
    /// <returns>Returns a new Department object that is an exact replica of the calling object.</returns>
    new public Department Clone()
    {
        Department d = new Department();
        d.ID = id;
        d.Name = name;
        d.Description = desc;
        for (int i = 0; i < imageList.Count; i++) { d.AddMedia(imageList[i]); }
        for (int i = 0; i < textList.Count; i++) { d.AddMedia(textList[i]); }
        for (int i = 0; i < videoList.Count; i++) { d.AddMedia(videoList[i]); }

        return d;
    }

    /// <summary>
    /// This method produces and XML element node containing the object's data and
    /// returns it.
    /// </summary>
    /// <returns>Returns an XmlElement representing the Department object.</returns>
    internal override XmlElement GetXML(XmlDocument doc)
    {
        // Create the element nodes
        XmlElement dept = XMLConstants.CreateXmlElement(doc, XMLConstants.DEPT);
        XmlElement valID = XMLConstants.CreateXmlElement(doc, XMLConstants.ID, "" + id);
        XmlElement valName = XMLConstants.CreateXmlElement(doc, XMLConstants.NAME, name);
        XmlElement valDesc = XMLConstants.CreateXmlElement(doc, XMLConstants.DESC, desc);

        // Create the Media File association nodes
        List<XmlElement> valMedia = new List<XmlElement>();
        foreach (Text m in textList)
        {
            XmlElement curDept = XMLConstants.CreateXmlElement(doc, XMLConstants.MEDIA_ASSOC, "" + m.ID);
            valMedia.Add(curDept);
        }
        foreach (Image m in imageList)
        {
            XmlElement curDept = XMLConstants.CreateXmlElement(doc, XMLConstants.MEDIA_ASSOC, "" + m.ID);
            valMedia.Add(curDept);
        }
        foreach (Video m in videoList)
        {
            XmlElement curDept = XMLConstants.CreateXmlElement(doc, XMLConstants.MEDIA_ASSOC, "" + m.ID);
            valMedia.Add(curDept);
        }

        // Add the element nodes to the Department collection node
        dept.AppendChild(valID);
        dept.AppendChild(valName);
        dept.AppendChild(valDesc);
        foreach (XmlElement e in valMedia) { dept.AppendChild(e); }

        // Return the element node
        return dept;
    }
}
