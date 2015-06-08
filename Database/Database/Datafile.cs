using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;

// Datafile Partial Class, Primary Component
// Author: Kyle McCarty

/// <summary>
/// This class represents the physical database to be reference by the project. It
/// contains methods to access the point of interest, Department, and Media Files
/// by identification number. It also handles loading the databases into memory and
/// building the references needed. The Datafile class is further supplemented by 
/// the class Datafile.Xml, which contains methods used for generating XML save data.
/// </summary>
public partial class Datafile
{
    // Database lists
    private List<PoI> poiList = new List<PoI>();
    private List<Department> deptList = new List<Department>();
    private List<MediaFile> mediaList = new List<MediaFile>();

    // Constants
    public String DIR_TEXT; public String DIR_IMAGE; public String DIR_VIDEO;
    public String DIR_ICON; public String DIR_ROOT; public int LIST_MEDIA = 2; public int LIST_POI = 0;
    public int LIST_DEPARTMENT = 1;

    public Datafile(String rootDirectory)
    {
        // === Set Initialization Variables ====================
        // Get the root directory
        String root = rootDirectory + "\\";

        // Set the constants
        DIR_ICON = Constants.GetIconDirectory(root);
        DIR_TEXT = Constants.GetTextDirectory(root);
        DIR_IMAGE = Constants.GetImageDirectory(root);
        DIR_VIDEO = Constants.GetVideoDirectory(root);
        DIR_ROOT = root;

        Intialize();
    }

    /// <summary>
    /// This method loads the datafiles into the database's memory if they exist. If
    /// the datafiles are missing, it just leaves the list represented by that database
    /// empty. Note that the databases should ALWAYS exist, and no guarantee is made
    /// that the datafile will work if one or more datafiles is missing.
    /// </summary>
    private void Intialize()
    {
        // === Load the Media Database =========================
        // First, make sure that the file exists
        if (File.Exists(DIR_ROOT + "media.vtd"))
        {
            StreamReader fr = new StreamReader(DIR_ROOT + "media.vtd");
            String curLine = fr.ReadLine();
            MediaFile[] temp = { new Text(), new Image(), new Video() };
            int curType = 0;

            while (curLine != null)
            {
                // Determine the type of the current Media File
                if (curLine.CompareTo("<" + Constants.TAG_TEXT + ">") == 0) { curType = 0; }
                else if (curLine.CompareTo("<" + Constants.TAG_IMAGE + ">") == 0) { curType = 1; }
                else if (curLine.CompareTo("<" + Constants.TAG_VIDEO + ">") == 0) { curType = 2; }
                // Gather the general information
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_ID + ">") == 0)
                {
                    int id = int.Parse(curLine.Substring(5));
                    temp[curType].ID = id;
                }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_NAME + ">") == 0) { temp[curType].Name = curLine.Substring(5); }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_DESCRIPTION + ">") == 0) { temp[curType].Description = curLine.Substring(5); }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_FILENAME + ">") == 0) { temp[curType].FileName = curLine.Substring(5); }
                // Handle type ending notifier
                else if (curLine.CompareTo("</" + Constants.TAG_TEXT + ">") == 0)
                {
                    mediaList.Add(temp[curType]);
                    temp[curType] = new Text();
                }
                else if (curLine.CompareTo("</" + Constants.TAG_IMAGE + ">") == 0)
                {
                    mediaList.Add(temp[curType]);
                    temp[curType] = new Image();
                }
                else if (curLine.CompareTo("</" + Constants.TAG_VIDEO + ">") == 0)
                {
                    mediaList.Add(temp[curType]);
                    temp[curType] = new Video();
                }

                curLine = fr.ReadLine();
            }

            fr.Close();
        }

        // === Load the Department Database ====================
        if (File.Exists(DIR_ROOT + "department.vtd"))
        {
            StreamReader fr = new StreamReader(DIR_ROOT + "department.vtd");
            Department d = null;
            String curLine = fr.ReadLine();

            while (curLine != null)
            {
                if (curLine.Length < 3) { curLine = fr.ReadLine(); }
                else if (curLine.CompareTo("<" + Constants.TAG_DEPARTMENT + ">") == 0) { d = new Department(); }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_ID + ">") == 0)
                {
                    int id = int.Parse(curLine.Substring(5));
                    d.ID = id;
                }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_NAME + ">") == 0) { d.Name = curLine.Substring(5); }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_DESCRIPTION + ">") == 0) { d.Description = curLine.Substring(5); }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_TEXT + ">") == 0
                    || (curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_IMAGE + ">") == 0
                    || (curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_VIDEO + ">") == 0)
                {
                    int id = int.Parse(curLine.Substring(5));
                    MediaFile tempMedia = GetMedia(id);
                    if (tempMedia == null) { Console.Error.WriteLine("Error - no such media file exists."); }
                    d.AddMedia(tempMedia);
                }
                else if (curLine.CompareTo("</" + Constants.TAG_DEPARTMENT + ">") == 0) { deptList.Add(d); }

                curLine = fr.ReadLine();
            }

            fr.Close();
        }

        // === Load the Points of Interest Database ============
        if (File.Exists(DIR_ROOT + "poi.vtd"))
        {
            StreamReader fr = new StreamReader(DIR_ROOT + "poi.vtd");
            PoI p = null;
            String curLine = fr.ReadLine();

            while (curLine != null)
            {
                if (curLine.CompareTo("<" + Constants.TAG_POI + ">") == 0) { p = new PoI(); }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_ID + ">") == 0)
                {
                    int id = int.Parse(curLine.Substring(5));
                    p.ID = id;
                }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_NAME + ">") == 0) { p.Name = curLine.Substring(5); }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_DESCRIPTION + ">") == 0) { p.Description = curLine.Substring(5); }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_FILENAME + ">") == 0) { p.IconName = curLine.Substring(5); }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_LOCATION + ">") == 0)
                {
                    int comma = curLine.IndexOf(",");
                    int x = int.Parse(curLine.Substring(5, comma - 5));
                    int y = int.Parse(curLine.Substring(comma + 1));
                    p.Location =  new Point(x, y);
                }
                else if ((curLine.Substring(0, 5)).CompareTo("<" + Constants.TAG_DEPARTMENT + ">") == 0)
                {
                    int id = int.Parse(curLine.Substring(5));
                    Department tempDept = GetDepartment(id);
                    if (tempDept == null) { Console.Error.WriteLine("Error - no such department file exists."); }
                    p.AddDepartment(tempDept);
                }
                else if (curLine.CompareTo("</" + Constants.TAG_POI + ">") == 0) { poiList.Add(p); }

                curLine = fr.ReadLine();
            }

            fr.Close();
        }
    }

    // === Methods for Selecting Media Files ===============
    // =====================================================

    /// <summary>
    /// Provides the <c>Text</c> object of the indicated ID or returns null if it does not exist.
    /// </summary>
    /// <param name="textID">Represents the ID of the desired object.</param>
    /// <returns>Returns a <c>Text</c> if the desired object exists, null otherwise.</returns>
    public Text GetText(int textID)
    {
        for (int i = 0; i < mediaList.Count; i++) { if (mediaList[i].ID == textID && mediaList[i] is Text) { return (Text)mediaList[i]; } }
        return null;
    }

    /// <summary>
    /// Provides the <c>Image</c> object of the indicated ID or returns null if it does not exist.
    /// </summary>
    /// <param name="textID">Represents the ID of the desired object.</param>
    /// <returns>Returns a <c>Image</c> if the desired object exists, null otherwise.</returns>
    public Image GetImage(int imageID)
    {
        for (int i = 0; i < mediaList.Count; i++) { if (mediaList[i].ID == imageID && mediaList[i] is Image) { return (Image)mediaList[i]; } }
        return null;
    }

    /// <summary>
    /// Provides the <c>Video</c> object of the indicated ID or returns null if it does not exist.
    /// </summary>
    /// <param name="textID">Represents the ID of the desired object.</param>
    /// <returns>Returns a <c>Video</c> if the desired object exists, null otherwise.</returns>
    public Video GetVideo(int videoID)
    {
        for (int i = 0; i < mediaList.Count; i++) { if (mediaList[i].ID == videoID && mediaList[i] is Video) { return (Video)mediaList[i]; } }
        return null;
    }

    /// <summary>
    /// Provides the <c>MediaFile</c> object of the indicated ID or returns null if it does not exist.
    /// </summary>
    /// <param name="textID">Represents the ID of the desired object.</param>
    /// <returns>Returns a <c>MediaFile</c> if the desired object exists, null otherwise.</returns>
    public MediaFile GetMedia(int mediaID)
    {
        for (int i = 0; i < mediaList.Count; i++) { if (mediaList[i].ID == mediaID) { return mediaList[i]; } }
        return null;
    }

    // === Methods for Selecting Department Files ==========
    // =====================================================

    /// <summary>
    /// Provides the <c>Department</c> object of the indicated ID or returns null if it does not exist.
    /// </summary>
    /// <param name="textID">Represents the ID of the desired object.</param>
    /// <returns>Returns a <c>Department</c> if the desired object exists, null otherwise.</returns>
    public Department GetDepartment(int deptID)
    {
        for (int i = 0; i < deptList.Count; i++) { if (deptList[i].ID == deptID) { return deptList[i]; } }
        return null;
    }

    // === Methods for Selecting Point of Interest Files ===
    // =====================================================
    /// <summary>
    /// Provides the <c>PoI</c> object of the indicated ID or returns null if it does not exist.
    /// </summary>
    /// <param name="textID">Represents the ID of the desired object.</param>
    /// <returns>Returns a <c>PoI</c> if the desired object exists, null otherwise.</returns>
    public PoI GetPointOfInterest(int poiID)
    {
        for (int i = 0; i < poiList.Count; i++) { if (poiList[i].ID == poiID) { return poiList[i]; } }
        return null;
    }

    // === Filtering Methods ===============================
    // =====================================================

    /// <summary>
    /// Method provides a List object contianing all the Media Files which meet the
    /// conditions specified by the given MediaFilter.
    /// </summary>
    /// <param name="f">Indicates the MediaFilter to be used when filtering.</param>
    /// <returns>Returns a List object containing MediaFile objects.</returns>
    public List<MediaFile> FilterMedia(MediaFilter f)
    {
        List<MediaFile> filterList = new List<MediaFile>();

        for (int i = 0; i < mediaList.Count; i++) { if (f.IsValid(mediaList[i])) { filterList.Add(mediaList[i].Clone()); } }

        return filterList;
    }

    /// <summary>
    /// Method provides a List object contianing all the Department objects which meet the
    /// conditions specified by the given DepartmentFilter.
    /// </summary>
    /// <param name="f">Indicates the DepartmentFilter to be used when filtering.</param>
    /// <returns>Returns a List object containing Department objects.</returns>
    public List<Department> FilterDepartments(DepartmentFilter f)
    {
        List<Department> filterList = new List<Department>();

        for (int i = 0; i < deptList.Count; i++) { if (f.IsValid(deptList[i])) { filterList.Add(deptList[i].Clone()); } }

        return filterList;
    }

    /// <summary>
    /// Method provides a List object contianing all the PoI objects which meet the
    /// conditions specified by the given PoIFilter.
    /// </summary>
    /// <param name="f">Indicates the PoIFilter to be used when filtering.</param>
    /// <returns>Returns a List object containing PoIFilter objects.</returns>
    public List<PoI> FilterMedia(PoIFilter f)
    {
        List<PoI> filterList = new List<PoI>();

        for (int i = 0; i < poiList.Count; i++) { if (f.IsValid(poiList[i])) { filterList.Add(poiList[i].Clone()); } }

        return filterList;
    }

    // === Methods for Saving ==============================
    // =====================================================

    /// <summary>
    /// Method saves the database to the three .vtd files located at the "root" location
    /// specified upon initialization of the Datafile object.
    /// </summary>
    public void SaveDatabase()
    {
        // Get a new string builder to hold the save data as its collected
        StringBuilder[] s = new StringBuilder[3];

        // Collect the PoI save data
        s[0] = new StringBuilder();
        foreach (PoI p in poiList) { s[0].Append(p.GetSaveOutput()); }

        // Collect the Department save data
        s[1] = new StringBuilder();
        foreach (Department d in deptList) { s[1].Append(d.GetSaveOutput()); }

        // Collect the Media File save data
        s[2] = new StringBuilder();
        foreach (MediaFile m in mediaList) { s[2].Append(m.GetSaveOutput()); }

        // Save the data
        String[] path = { DIR_ROOT + "poi.vtd", DIR_ROOT + "department.vtd", DIR_ROOT + "media.vtd" };
        for (int i = 0; i < 3; i++)
        {
            if (!File.Exists(path[i])) { File.CreateText(path[i]); }
            StreamWriter writer = new StreamWriter(path[i]);
            writer.Write(s[i].ToString());
            writer.Close();
        }
    }

    /// <summary>
    /// Method saves the database to the three .xml files located at the "root" location
    /// specified upon initialization of the Datafile object. XML files use the XML tags
    /// defined in the XMLConstants internal class and follow the criteria of the XML
    /// schema associated with the database system.
    /// </summary>
    public void SaveDatabaseXML()
    {
        // Create an XML document
        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", Encoding.UTF8.WebName,"yes");
        doc.AppendChild(dec);

        // Create the database element
        XmlElement root = doc.CreateElement(XMLConstants.DATABASE);
        doc.AppendChild(root);

        // Get the XML elements
        XmlElement[] element = { GetXmlData(Constants.TYPE_POI, doc), GetXmlData(Constants.TYPE_DEPARTMENT, doc), GetXmlData(Constants.TYPE_MEDIA, doc) };

        // Add the elements to the document
        foreach (XmlElement e in element) { root.AppendChild(e); }

        // Stream the output to a String Builder
        StringBuilder s = new StringBuilder();
        StringWriter fileWriter = new StringWriter(s);
        doc.Save(fileWriter);

        // Print the result
        Console.Out.WriteLine(s.ToString());
        StreamWriter writer = new StreamWriter(DIR_ROOT + "database.xml");
        writer.Write(s.ToString());
        writer.Close();
    }

    // === Methods for Debugging ===========================
    // =====================================================
    public List<MediaFile> GetMediaList() { return mediaList; }

    public List<Department> GetDepartmentList() { return deptList; }

    public List<PoI> GetPOIList() { return poiList; }
}
