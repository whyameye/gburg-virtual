/*
 * TrayArea class 
 * extends Grid class
 * 
 * Author(s): 
 * Amanda Gower
 * Fumbani Chibaka
 * 
 * Gets added to instance of TrayFrame class
 * Aligned to right side of tray (TrayMenu object is on left side)
 * Contains instances of InfoScreen and TrayTab classes
 * 
 * 
 * Intended to display information to user (written, media)
 * Linked to a specific TrayTab for a building
 * User should be able to switch between viewing information in this area for different buildings by selecting TrayTabs
 * Selecting a new TrayTab will display a different instance of InfoScreen
 * 
 */

using System;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections;
using libSMARTMultiTouch.Controls;

namespace GVirtual

{
    class TrayArea : Grid
    {
        //grid to organize tabs
        private Grid tabs;

        //number of tabs needed
        private int numTabs;

        //list of tabs
        private ArrayList tabList;

        //the frame and current info screen (and link to current tab) to be displayed
        private InfoScreen currentInfo;
        private TrayFrame frame;
        private GVirtualCanvas canvas;

         InfoScreen info1; // for text
         InfoScreen info2; // for pictures
         InfoScreen info3; // for videos


         TrayTab tab1;
         TrayTab tab2;
         TrayTab tab3;


        private SolidColorBrush backgroundColor;

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Constructor</para></summary> 
        ///<param type ="double"> Height and width for screen 
        ///                      (recommended: same height as TrayFrame height and width as porportion of TrayFrame width </param>
        ///<param type ="int"> Given number of tabs that will be associated with building this specific object displays info about
        ///                    Allows for tabs to be created with correct sizing and spacing </param>
        public TrayArea(double height, double width, int numTabs, TrayFrame frame, GVirtualCanvas canvas)
        {
            this.canvas = canvas;
            this.Height = height;
            this.Width = width;
            this.numTabs = numTabs;
            this.frame = frame;
            this.Background = frame.getPrimaryColor();
            tabList = new ArrayList(numTabs);

            tabs = new Grid();
            tabs.Height = height;
            tabs.Width = width / 6;


            //remainder is mainly for testing at this stage
            info1 = new InfoScreen(height, 5 * width / 6, frame.getPrimaryColor(), canvas, frame);
            //info1.setText("Written Information");

            tab1 = new TrayTab(this.frame, this, height / numTabs, width / 6, System.Windows.VerticalAlignment.Top, info1, 1);
            tab1.setText("Info");
            tabs.Children.Add(tab1);
            tabList.Add(tab1);

            info2 = new InfoScreen(height, 5 * width / 6, frame.getPrimaryColor(), canvas, frame);
            //info2.setText("Pictures");
            //info2.setMatrix(9, 1);

            tab2 = new TrayTab(this.frame, this, height / numTabs, width / 6, System.Windows.VerticalAlignment.Center, info2, 2);
            tab2.setText("Pics");
            tabs.Children.Add(tab2);
            tabList.Add(tab2);

            info3 = new InfoScreen(height, 5 * width / 6, frame.getPrimaryColor(), canvas, frame);
            //info3.setText("Videos");
            //info3.setMatrix(8, 1);

            tab3 = new TrayTab(this.frame, this, height / numTabs, width / 6, System.Windows.VerticalAlignment.Bottom, info3, 3);
            tab3.setText("Vids");
            tabs.Children.Add(tab3);
            tabList.Add(tab3);
            
            tabs.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            setCurrentTab(tab1);

            Children.Add(tabs);
        }//end constructor

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This function sets the given tab to current, sets all others so they are not current</para></summary> 
        public void setCurrentTab(TrayTab tab)
        {
            for (int i = 0; i < numTabs; i++)
            {
                TrayTab temp = (TrayTab)tabList[i];
                if (temp.getID() == tab.getID())
                {
                    temp.setToCurrent();
                    if (Children.Contains(currentInfo))
                    {
                        Children.Remove(currentInfo);
                    }
                    currentInfo = temp.getInfoScreen();
                    Children.Add(currentInfo);
                }
                else
                {
                    temp.setToNotCurrent();
                }
            }
        }//end method setCurrentTab


        /// <summary><para>@author: Fumbani Chibaka </para>
        ///<para>Used for Debbuging: Prints text on the Info Screen </para></summary> 
        public String Info1Text 
        {
            set { info1.setText(value); }
        }


        /// <summary><para>@author: Amanda Gower </para>
        ///<para>Gets the current info screen </para></summary> 
        public InfoScreen CurrentInfoScreen
        {
            get { return currentInfo; }
        }

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>Gets the current info screen </para></summary> 
        public InfoScreen Info1Screen
        {
            get { return info1; }
        }

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>Gets the current info screen </para></summary> 
        public InfoScreen Info2Screen
        {
            get { return info2; }
        }

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>Gets the current info screen </para></summary> 
        public InfoScreen Info3Screen
        {
            get { return info3; }
        }

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>Gets the tray frame this object belongs to </para></summary> 
        public TrayFrame Frame
        {
            get { return frame; }
        }
   
    }//end class TrayArea
}
