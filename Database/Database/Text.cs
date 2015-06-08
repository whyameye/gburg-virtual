using System;

// Text Class
// Author: Kyle McCarty

/// <summary>
/// This class represents a text file. It holds information concerning
/// the text file's ID, name, description, file name, and file path as
/// well as the methods necessary to manipulate these. A text file
/// is also a MediaFile. A text file is part of the Media data class
/// for the purposes of determining when IDs overlap.
/// </summary>
public class Text : MediaFile
{
    /// <summary>
    /// Creates an empty Text object. Note that the ID fields must be set
    /// before the Text can be safely added to the database.
    /// </summary>
    public Text() { }

    /// <summary>
    /// Produces a Text object.
    /// </summary>
    /// <param name="textID">Represents the Text's ID.</param>
    public Text(int textID) { id = textID; }

    /// <summary>
    /// Produces a Text object.
    /// </summary>
    /// <param name="textID">Represents the Text's ID.</param>
    /// <param name="textName">Represents the Text's name.</param>
    /// <param name="textDescription">Represents a description of the Text.</param>
    /// <param name="textFileName">Represents the Text's file name.</param>
    public Text(int textID, String textName, String textDescription, String textFileName)
    {
        id = textID;
        name = textName;
        desc = textDescription;
        fileName = textFileName;
    }

    /// <summary>
    /// Methods reutrns a new Text object with the same properties as the calling object. Method
    /// replaces the general Clone method of abstract class DataType.
    /// </summary>
    /// <returns>Returns a new Text object that is an exact replica of the calling object.</returns>
    new public Text Clone()
    {
        Text t = new Text();
        t.Description = desc;
        t.FileName = fileName;
        t.ID = id;
        t.Name = name;

        return t;
    }
}
