/*
 * TrayMenu class 
 * extends Grid class
 * 
 * Author(s): 
 * Fumbani Chibaka
 * Amanda Gower
 * 
 *
 * Gets added to instance of TrayFrame class
 * Aligned to left side of tray (TrayArea object is on right side)
 * Contains instances of TrayFilter and POIDropArea classes
 * 
 * Has a grid as a frame to organize layout
 * IteractiveBorder is layered on top of the grid to allow for user touches
 * 
 * 
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using libSMARTMultiTouch.Controls;
using System.Windows;

namespace GVirtual
{
    class TrayMenu : Grid
    {
        private TrayFilter filter;
        private POIDropArea drop;
        private SolidColorBrush backgroundColor = new SolidColorBrush(Colors.White);

        //link to overall tray frame
        private TrayFrame frame;

        //keep this for resizing filter
        private double initialWidth;

        private GVirtualCanvas canvas;

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Constructor</para></summary> 
        ///<param type ="double"> Height and width for menu 
        ///                      (recommended: same height as TrayFrame height and width as porportion of TrayFrame width </param>
        ///<param type ="TrayFrame"> TrayFrame objects that contains this object </param>
        public TrayMenu(double height, double width, TrayFrame frame, GVirtualCanvas canvas)
        {
            this.canvas = canvas;
            this.Height = height;
            this.Width = width;
            this.initialWidth = width;
            this.Background = backgroundColor;
            this.frame = frame;

            drop = new POIDropArea(this.Height, 7*this.Width / 8, this,frame); 
            drop.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

            filter = new TrayFilter(this.Height, this.Width / 8, this, canvas);
            filter.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            
            Children.Add(drop);
            Children.Add(filter);

            //<< set the position of the POIDropArea >>
            // << What is the Precise Position of the POIDropArea  (Group 2???) >>
            drop.Position = new Point(0, 0);

        }//end constructor

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Getter function for TrayFrame object </para></summary> 
        public TrayFrame getFrame()
        {
            return frame;
        }//end method getFrame

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Expands filter incrementally</para></summary> 
        public void expandFilter()
        {
            //tray is fully retracted
            if (filter.getExpandFactor() == filter.getMinExpand())
            {
                this.Width = frame.Width / filter.getExpansionLevel();
            }

            //increase width incrementally
            else
            {
                this.Width += frame.Width / filter.getExpansionLevel();
            }

            if (Children.Contains(filter))
            {
                Children.Remove(filter);
            }
            filter.expand();
            Children.Add(filter);
        }//end method expandFilter

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Retracts filter incrementally</para></summary>  
        public void retractFilter()
        {
            //tray will end up fully retracted
            if (filter.getExpandFactor() == filter.getMinExpand()+1)
            {
                this.Width = initialWidth;
            }
            else
            {
                this.Width -= frame.Width / filter.getExpansionLevel();
            }

            if (Children.Contains(filter))
            {
                Children.Remove(filter);
            }
            filter.retract();
            Children.Add(filter);
        }//end method retractFilter

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>Getter function for this TrayMenu object </para></summary> 
        ///
        public POIDropArea POIDropArea
        {
            get { return drop; }
        }//end function TrayMenu

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Returns the filter for this object </para></summary> 
        ///
        public TrayFilter Filter
        {
            get { return filter; }
        }//end function Filter


       
    }//end class 
}
