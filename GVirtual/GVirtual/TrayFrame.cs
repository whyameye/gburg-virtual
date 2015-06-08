/*
 * TrayFrame class 
 * extends TouchEffectCanvas class
 * 
 * Author(s): 
 * Fumbani Chibaka
 * Amanda Gower
 * Adam Hartman
 * 
 * 
 * Gets added to overall canvas for application
 * Contains TrayMenu and TrayArea objects (TrayMenu aligned on left, TrayArea on right)
 * 
 * Has a grid as a frame to organize layout
 * IteractiveBorder is layered on top of the grid to allow for user touches
 * 
 * Can be identified using a string (expected to be "left" or "right")
 * Also provides methods to determine isLeftTray and isRightTray based on this expectation
 * 
 */

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using libSMARTMultiTouch.Controls;

namespace GVirtual
{
    class TrayFrame : TouchEffectCanvas
    {
        private Grid frame;
        private InteractiveBorder container;

        private TrayMenu menu;
        private TrayArea area;
        private String id;
        private SolidColorBrush backgroundColor = new SolidColorBrush(Colors.White);
        private SolidColorBrush frameColor = new SolidColorBrush(Colors.Transparent);
        private Point position; 
        private ArrayList buildingCollection = new ArrayList();
        private GVirtualCanvas canvas; //containing canvas

        private SolidColorBrush primaryColor;
        private SolidColorBrush secondaryColor;

        private bool highlighted;
        private Building highlightedBuilding;


        /// <summary><para>@author: Amanda Gower, Fumbani Chibaka </para>
        ///<para>Constructor</para></summary> 
        ///<param type ="double"> Height and width for tray (recommended: same height as and a proportion of width of SMART Table screen) </param>
        ///<param type ="String"> ID string (recommended: "left" or "right") </param>
        public TrayFrame(double height, double width, String id, GVirtualCanvas containingCanvas)
        {
            this.Height = height;
            this.Width = width;
            this.id = id;
            this.canvas = containingCanvas;
            highlighted = false;
            highlightedBuilding = null;

            //set the position of the frame according to its orientation on the canvas 
            this.position = new Point (0,0); //Default: position assuming left tray
            if (id == "right")
            {
                this.position = new Point(5.0/6.0*canvas.Width, 0);
                this.primaryColor = new SolidColorBrush(Colors.DarkBlue);
                this.secondaryColor = new SolidColorBrush(Colors.DarkOrange);
            }
            else
            {
                this.primaryColor = new SolidColorBrush(Colors.DarkOrange);
                this.secondaryColor = new SolidColorBrush(Colors.DarkBlue);
            }
            container = new InteractiveBorder();
            container.Width = this.Width;
            container.Height = this.Height;
            container.Background = backgroundColor;

            //grid to organzie elements
            frame = new Grid();
            frame.Height = this.Height;
            frame.Width = this.Width;
            frame.Background = frameColor;
            container.Child = frame;

            menu = new TrayMenu(this.Height, 2 * this.Width / 9, this, canvas);
            menu.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            area = new TrayArea(this.Height, 7 * this.Width / 9, 3, this,canvas);
            area.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            menu.Filter.setArea(area);

            frame.Children.Add(area);
            frame.Children.Add(menu);
        }//end constructor

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function returns id string </para></summary> 
        public String getID()
        {
            return id;
        }//end method getID

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function determines if object is left tray (based on id string) </para></summary> 
        public bool isLeftTray()
        {
            return id == "left";
        }//end method isLeftTray

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function determines if object is right tray (based on id string) </para></summary> 
        public bool isRightTray()
        {
            return id == "right";
        }//end method isRightTray

        /// <summary><para>@author: Amanda Gower, Fumbani Chibaka</para>
        ///<para>This function returns container that grid/frame is contained by </para></summary> 
        public InteractiveBorder Tray
        {
            get{ return  this.container; } 
        }//end method getTray

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function expands the TrayMenu object (which expands the filter) </para></summary> 
        public void expandMenu()
        {
            if (frame.Children.Contains(menu))
            {
                frame.Children.Remove(menu);
            }

            menu.expandFilter();
            frame.Children.Add(menu);
        }//end method expandMenu

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function retractss the TrayMenu object (which retractss the filter) </para></summary> 
        public void retractMenu()
        {
            if (frame.Children.Contains(menu))
            {
                frame.Children.Remove(menu);
            }
            menu.retractFilter();
            frame.Children.Add(menu);
        }//end method retractMenu

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Getter function for primary color of tray</para></summary> 
        ///
        public SolidColorBrush getPrimaryColor()
        {
            return primaryColor;
        }//end function getSecondaryColor

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Getter function for secondary color of tray</para></summary> 
        ///
        public SolidColorBrush getSecondaryColor()
        {
            return secondaryColor;
        }//end function getSecondaryColor

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>Getter function for TrayMenu object that is added to this frame</para></summary> 
        ///
        public TrayMenu TrayMenu
        {
            get { return menu; }
        }//end function TrayMenu

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>Getter function for TrayArea object that is added to this frame</para></summary> 
        ///
        public TrayArea TrayArea
        {
            get { return area; }
        }//end function TrayMenu

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>Getter function for canvas that holds this object</para></summary> 
        ///
        public GVirtualCanvas Canvas
        {
            get { return canvas; }
        }//end function Canvas

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>This function defines the active area of the POIDropArea on the tray.
        /// Used by Building object (>touchUp) to check  if a building object has been dragged
        /// dragged into the tray</para></summary> 
        ///<param type ="Point">  Point   </param> <param type ="String"> String </param>
        /// <returns>void</returns>  
        public Boolean ActiveArea(Point point)
        {
            Rect activeArea = new Rect( this.position.X, this.position.Y, this.Height, this.Width); 
            return activeArea.Contains(point);
        }//end function ActiveArea


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>This function checks to see if  user had dragged building into tray </para></summary> 
        public Boolean HasBuilding
        {
            get { return buildingCollection.Count != 0 ;} //check if  collection is empty
        }//end function HasBuilding


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>Add building to collection of buildings in tray, also calls drop area to update current state</para></summary> 
        public void AddBuilding( Building building)
        {
            this.buildingCollection.Add(building);
            
        }//end function AddBuilding


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para> Returns List of buildings currently placed in tray</para></summary> 
        public ArrayList BuildingsCollection
        {
            get { return this.buildingCollection; }
        }//end function GetBuildings

        /// <summary><para>@author: Adam Hartman</para>
        ///<para> Clears list of building in tray</para></summary> 
        public void clearBuildingList()
        {
            buildingCollection.Clear();
        }//end function GetBuildings

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Returns true if there is a building highlighted for this tray</para></summary> 
        public bool isBuildingHighlighted()
        {
            return highlighted;
        }//end function isBuildingHighlighted

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>sets the building highlighted for this tray</para></summary> 
        public void setBuildingHighlighted(Building b)
        {
            highlightedBuilding = b;
            highlighted = true;
        }//end function setBuildingHighlighted

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>gets the building highlighted for this tray</para></summary> 
        public Building getBuildingHighlighted()
        {
            return highlightedBuilding;
        }//end function setBuildingHighlighted

    }//end class TrayFrame
}
