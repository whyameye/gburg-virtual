using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

//manual import
using System.Xml.Linq;

namespace GVirtual
{
    class Database
    {
        private TextBox console;
        private XDocument db; //buildings database
        private XDocument departments; //departments database

        public Database(TextBox console)
        {
            this.console = console;
            console.Text = "[ database initiated ]";

            //Read the XML data from GVirtualDatabase.xml into XDocument (< replaces an XML document)
             db = XDocument.Load("database/GVirtualDatabase.xml");
             departments = XDocument.Load("database/Departments.xml");
        }//end constructor


        /// <summary>
        /// Fumbani Chibaka
        /// </summary>
        /// <returns> ArrayList of  Buildings</returns>
        /*
        public ArrayList ExtractBuildingData()
        {
            console.Text += ("\n Function: ExtractBuildingData ");

            ArrayList buildingsCollection = new ArrayList();
            // Do a simple query and print the results to the console
            var data = from item in db.Descendants("building") //look at all items in db that are "buildings"
                       select new
                       {
                            
                           Name = item.Element("fullName").Value,   //Full Name of Building
                           ID = item.Element("idName").Value, // Unique ID of Building
                           FileLocation = item.Element("fileLocation").Value, //Location of Image File
                           xValue = item.Element("position").Element("x").Value,
                           yValue = item.Element("position").Element("y").Value,
                           scaleValue = item.Element("position").Element("scale").Value
                       };

            // data is an arraylist of a set???
            foreach (var b in data)
            {
               //console.Text += ("\n" + (b.ToString()));
                Point point = new Point(System.Convert.ToInt32(b.xValue), System.Convert.ToInt32(b.yValue));
                int scale = System.Convert.ToInt32(b.scaleValue);

                Building building = new Building(b.ID, b.Name, b.FileLocation, point, scale);
                buildingsCollection.Add(building);
            }
            console.ScrollToEnd();

            return buildingsCollection;
        }//end method ExtractBuildingData

    */
        /// <summary>
        ///  Fumbani Chibaka
        /// </summary>
        /// <returns>ArrayList of Strings </returns>
        public ArrayList ExtractDepartmentsList()
        {
            console.Text += ("\n Function: ExtractDepartmentList ");
            ArrayList dptsCollection = new ArrayList();

            var data = from item in departments.Descendants("dpt") //look at all items in departments XML with tag <department>
                       select new
                       {
                           Name = item.Element("name").Value,   //Name of the Department
                       };

            foreach (var  d in data)
            {
                //console.Text += ("\n" + (d.ToString()));
                dptsCollection.Add(d.Name); //add name to ArrayList
            }
            console.ScrollToEnd();

            return dptsCollection;
        }//end method Extract DepartmentsList


        /// <summary>
        ///  Fumbani Chibaka
        /// </summary>
        /// <returns>ArrayList of Strings </returns>
        public ArrayList ListBuildings(String target)
        {
            console.Text += ("\n Function: ListAcademicBuildings > "+ target);
            ArrayList dptsCollection = new ArrayList();

            var data = from item in db.Descendants("building") //look at all items in departments XML with tag <department>
                       select new
                       {
                           Name = item.Element("fullName").Value,
                           Category = item.Element("category").Value,   //Name of the Department
                       };

            foreach (var d in data)
            {
                if (d.Category == target)
                {
                    console.Text += ("\n" + (d.ToString()));
                    dptsCollection.Add(d.Name); //add name to ArrayList
                }
            }
            console.Text += "\n\n";
            console.ScrollToEnd();
            
            return dptsCollection;
        }//end method Extract DepartmentsList


    }//end class
}
