using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace TestRun
{
    static class Program
    {
        // Create log variables
        static StringBuilder log = new StringBuilder();

        [STAThread]
        static void Main()
        {
            // Output starting demarcation lines
            println(); println(); println();
            // Locate the current root directory
            Out("Finding Root Directory: ");
            String root = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            Outln(Directory.GetCurrentDirectory());
            
            // Get the root directory
            String root = (new Constants()).getRootDirectory();
            
            // Run tests
            //runDirectoryTest();
            //runDatabaseTest();
            //runCreateFiles();

            //Environment.Exit(0);

            // Create datafile
            Constants c = new Constants();
            Datafile db = new Datafile();
            List<MediaFile> media = db.GetMediaList();
            for (int i = 0; i < media.Count; i++) {Outln(media[i].GetName() + "\tType: " + c.getType(media[i])); }
            List<Department> dept = db.GetDepartmentList();
            for (int i = 0; i < dept.Count; i++) { Outln(dept[i].GetName() + "\tID: " + dept[i].GetID()); }
            Outln(dept[0].GetSaveOutput());
            List<PoI> poi = db.GetPOIList();
            for (int i = 0; i < poi.Count; i++) { Outln(poi[i].GetName() + "\tID: " + poi[i].GetID()); }
            Outln(poi[0].GetSaveOutput());

            // Output ending demarcation lines
            println(); println(); println();

            // Output log files
            Console.WriteLine(log.ToString());

            writeFile(root + "out.log", log.ToString());
            if (!File.Exists(root + "out.log")) { File.CreateText(root + "out.log"); }
        }

        private static void runCreateFiles()
        {
            // Create constants object
            Constants c = new Constants();
            String root = c.getRootDirectory();

            // Create variables for the database create objects test
            Image[] image = new Image[4];
            Text[] text = new Text[4];
            String imageDir = root + "images\\";
            String textDir = root + "text\\";
            String[] names = { "Sphere", "Cylinder", "Pyramid", "Cube" };

            // Create database objects

            Outln("Initiating data object creation test.");
            for (int i = 0; i < 4; i++)
            {
                Outln("Creating files for " + names[i] + ":");
                image[i] = new Image();
                image[i].SetDiscription("Stuff about a " + names[i] + ".");
                image[i].SetFileName(names[i] + ".png");
                image[i].SetName(names[i]);
                image[i].SetID(2 * i);
                Outln(1, "Creating " + image[i].GetFilePath());

                text[i] = new Text();
                text[i].SetDiscription("Stuff about a " + names[i] + ".");
                text[i].SetFileName(names[i] + ".txt");
                text[i].SetName(names[i]);
                text[i].SetID(2 * i + 1);
                Outln(1, "Creating " + text[i].GetFilePath());
            }

            // Test to make sure the files exist
            Outln("Testing that files exist...");
            for (int i = 0; i < 4; i++)
            {
                bool[] exists = { File.Exists(image[i].GetFilePath()), File.Exists(text[i].GetFilePath()) };
                Outln(1, names[i] + ".png: " + exists[0]);
                Outln(1, names[i] + ".txt: " + exists[1]);
            }

            // Create a department and a point of interest which use these files
            Outln("Creating a new department.");
            Department d = new Department(0);
            d.SetDiscription("Geometry stuff!");
            d.SetName("Geometry Department");
            for (int i = 0; i < 4; i++)
            {
                Out(1, "Adding image " + (i + 1) + "... ");
                d.AddMedia(image[i]);
                Outln("[Success]");
                Out(1, "Adding text " + (i + 1) + "... ");
                d.AddMedia(text[i]);
                Outln("[Success]");
            }

            Outln("Creating a new PoI.");
            PoI p = new PoI();
            p.SetDiscription("A shapely point.");
            p.SetLocation(0, 0);
            p.SetName("The Geo Spot");
            p.SetID(0);
            Outln("Adding the department.");
            p.AddDepartment(d);

            // Generating database save output for viewing
            Outln("/nGenerating save output for files.");
            println();
            Outln("Outputting PoI data:");
            println();
            StringBuilder sPoi = new StringBuilder();
            sPoi.Append(getSaveOutput(p));
            Outln(sPoi.ToString());
            println();
            Outln("Outputting department data:");
            println();
            StringBuilder sDept = new StringBuilder();
            sDept.Append(getSaveOutput(d));
            Outln(sDept.ToString());
            println();
            Outln("Outputting media data:");
            println();
            StringBuilder sMedia = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                sMedia.Append(getSaveOutput(image[i]));
                sMedia.Append(getSaveOutput(text[i]));
            }
            Outln(sMedia.ToString() + "\n");
            println();

            // Save the data to a database file
            String end = (new Constants()).TAG_FILEEND;
            //sPoi.Append("</" + end + ">");
            //sDept.Append("</" + end + ">");
            //sMedia.Append("</" + end + ">");
            writeFile(root + "poi.vtd", sPoi.ToString());
            writeFile(root + "department.vtd", sDept.ToString());
            writeFile(root + "media.vdt", sMedia.ToString());
        }

        private static void writeFile(String filePath, String data)
        {
            if (!File.Exists(filePath)) { File.CreateText(filePath); }
            try
            {
                StreamWriter fw = new StreamWriter(filePath);
                fw.Write(data.ToString());
                fw.Close();
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("An error occurred while accessing the file.");
                Console.Error.WriteLine(e.Message);
            }
        }

        private static String getSaveOutput(Savable s) { return s.GetSaveOutput(); }

        private static void runDirectoryTest()
        {
            // Set up the variables for the directory check
            Constants c = new Constants();
            String[] dir = new String[4];
            String root = c.getRootDirectory();
            for (int i = 0; i < dir.Length; i++) { dir[i] = c.getMediaDirectory(i); }

            // Check to make sure that the needed directories exist
            Outln("\nInitiating directory check.");

            for (int i = 0; i < dir.Length; i++)
            {
                Out(1, "Checking for existance of directory \"" + dir[i] + "\"... ");
                bool exists = Directory.Exists(root + dir[i]);
                Outln("[" + exists + "]");

                if (!exists)
                {
                    Out(2, "Creating directory \"" + dir[i] + "\"... ");
                    Directory.CreateDirectory(root + dir[i]);
                    Outln("[" + Directory.Exists(root + dir[i]) + "]");
                }
            }
            Outln("Directory check complete.");
        }

        private static void runDatabaseTest()
        {
            // Create variables for database check
            Constants c = new Constants();
            String root = (new Constants()).getRootDirectory();
            String[] datum = new String[3];
            for (int i = 0; i < datum.Length; i++) { datum[i] = c.getDatabaseName(i); }

            // Check to make sure the necessary database files exist
            Outln("\nInitiating database check.");
            for (int i = 0; i < datum.Length; i++)
            {
                Out(1, "Checking for existance of database \"" + datum[i] + "\"... ");
                bool exists = File.Exists(root + datum[i]);
                Outln("[" + exists + "]");

                if (!exists)
                {
                    Out(2, "Creating database \"" + datum[i] + "\"... ");
                    File.CreateText(root + datum[i]);
                    Outln("[" + File.Exists(root + datum[i]) + "]");
                }
            }
            Outln("Database check complete.");
        }

        private static void runCreateDatabase()
        {

        }

        // Private helper methods
        private static void Outln() { Console.WriteLine(""); }

        private static void Out(Object output) { Out(0, output); }

        private static void Out(int level, Object output)
        {
            String indent = getIndent(level);
            log.Append(indent + output);
        }

        private static void Outln(Object output) { Outln(0, output); }

        private static void Outln(int level, Object output) { Out(level, output + "\n"); }

        private static String getIndent(int level)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < level; i++) { s.Append("\t"); }

            return s.ToString();
        }

        private static void println() { log.Append("=======================================================================\n"); }
    }
}
