using System;

// Video Class
// Author: Kyle McCarty

/// <summary>
/// This class represents a video file. It holds information concerning
/// the video file's ID, name, description, file name, and file path as
/// well as the methods necessary to manipulate these. A video file
/// is also a MediaFile. A video file is part of the Media data class
/// for the purposes of determining when IDs overlap.
/// </summary>
public class Video : MediaFile
{
    /// <summary>
    /// Creates an empty Video object. Note that the ID fields
    /// must be set before the Video can be safely added to the
    /// database.
    /// </summary>
    public Video() { }

    /// <summary>
    /// Produces an Video object.
    /// </summary>
    /// <param name="videoID">Represents the Video's ID.</param>
    public Video(int videoID) { id = videoID; }

    /// <summary>
    /// Produces an Video object.
    /// </summary>
    /// <param name="imageID">Represents the Video's ID.</param>
    /// <param name="imageName">Represents the Video's name.</param>
    /// <param name="imageDescription">Represents a description of the Video.</param>
    /// <param name="imageFileName">Represents the Video's file name.</param>
    public Video(int videoID, String videoName, String videoDescription, String videoFileName)
    {
        id = videoID;
        name = videoName;
        desc = videoDescription;
        fileName = videoFileName;
    }

    /// <summary>
    /// Methods reutrns a new Video object with the same properties as the calling object. Method
    /// replaces the general Clone method of abstract class DataType.
    /// </summary>
    /// <returns>Returns a new Video object that is an exact replica of the calling object.</returns>
    new public Video Clone()
    {
        Video v = new Video();
        v.Description = desc;
        v.FileName = fileName;
        v.ID = id;
        v.Name = name;

        return v;
    }
}