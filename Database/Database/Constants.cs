using System;
using System.Text;

// Constants Static Class
// Author: Kyle McCarty

/// <summary>
/// This class provides reference variables for the legacy method of saving
/// the database which predated the XML method. It also provides references
/// for the file hierarchy structure that the database uses in addition to
/// reference variables for Media types and data types.
/// </summary>
static class Constants
{
    // === Private Class Variables ==========================================
    // ======================================================================

    // File path holders
    private static String[] dir = { "icons\\", "text\\", "images\\", "videos\\" };
    private static String[] source = { "poi.vtd", "department.vtd", "media.vtd" };

    // === Type Reference Varaibles =========================================
    // ======================================================================

    // Reference integers for Media types
    internal static int MEDIA_ICON = 0;
    internal static int MEDIA_TEXT = 1;
    internal static int MEDIA_IMAGE = 2;
    internal static int MEDIA_VIDEO = 3;

    // Database type variables
    internal static int TYPE_POI = 0;
    internal static int TYPE_DEPARTMENT = 1;
    internal static int TYPE_MEDIA = 2;

    // === Legacy Save Tag Reference Variables ==============================
    // ======================================================================

    // Media Type Tags
    internal static String TAG_TEXT = "txt";
    internal static String TAG_IMAGE = "img";
    internal static String TAG_VIDEO = "vid";
    // Data Type Tags
    internal static String TAG_POI = "poi";
    internal static String TAG_DEPARTMENT = "dpt";
    internal static String[] TAG_MEDIA = { TAG_POI, TAG_TEXT, TAG_IMAGE, TAG_VIDEO };
    // Data Type Attribute Tags
    internal static String TAG_ID = "idn";
    internal static String TAG_NAME = "nme";
    internal static String TAG_DESCRIPTION = "dis";
    internal static String TAG_FILENAME = "fnm";
    internal static String TAG_LOCATION = "loc";
    // File Termination Tag
    internal static String TAG_FILEEND = "end";

    // === Methods for Getting Media Directories ============================
    // ======================================================================

    // Data directory selectors
    internal static String GetVideoDirectory(String root) { return root + dir[MEDIA_VIDEO]; }

    internal static String GetImageDirectory(String root) { return root + dir[MEDIA_IMAGE]; }

    internal static String GetTextDirectory(String root) { return root + dir[MEDIA_TEXT]; }

    internal static String GetIconDirectory(String root) { return root + dir[MEDIA_ICON]; }
}
