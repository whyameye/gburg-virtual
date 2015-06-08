using System;

// Image Class
// Author: Kyle McCarty

/// <summary>
/// This class represents a image file. It holds information concerning
/// the image file's ID, name, description, file name, and file path as
/// well as the methods necessary to manipulate these. An image file
/// is also a MediaFile. An image file is part of the Media data class
/// for the purposes of determining when IDs overlap.
/// </summary>
public class Image : MediaFile
{
    /// <summary>
    /// Creates an empty Image object. Note that the ID fields
    /// must be set before the Image can be safely added to the
    /// database.
    /// </summary>
    public Image() { }

    /// <summary>
    /// Produces an Image object.
    /// </summary>
    /// <param name="imageID">Represents the Image's ID.</param>
    public Image(int imageID) { id = imageID; }

    /// <summary>
    /// Produces an Image object.
    /// </summary>
    /// <param name="imageID">Represents the Image's ID.</param>
    /// <param name="imageName">Represents the Image's name.</param>
    /// <param name="imageDescription">Represents a description of the Image.</param>
    /// <param name="imageFileName">Represents the Image's file name.</param>
    public Image(int imageID, String imageName, String imageDescription, String imageFileName)
    {
        id = imageID;
        name = imageName;
        desc = imageDescription;
        fileName = imageFileName;
    }

    /// <summary>
    /// Methods reutrns a new Image object with the same properties as the calling object. Method
    /// replaces the general Clone method of abstract class DataType.
    /// </summary>
    /// <returns>Returns a new Image object that is an exact replica of the calling object.</returns>
    new public Image Clone()
    {
        Image i = new Image();
        i.Description = desc;
        i.FileName = fileName;
        i.ID = id;
        i.Name = name;

        return i;
    }
}
