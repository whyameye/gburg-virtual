/*
 * TrayTab class 
 * extends InteractiveBorder class
 * 
 * Author(s): 
 * Amanda Gower
 * 
 * Gets added to instance of TrayArea class (which is then ultimately added to TrayFrame)
 * Aligned to right side of TrayArea (InfoScreen instances go on right side)
 * 
 * Linked to specific InfoScreen object to allow user to switch between info screens
 * Can either be current tab (its InfoScreen is currently being displayed)
 * Or not current (tab is still visible to be able to be selected, but its specfic InfoScreen is not displayed)
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using libSMARTMultiTouch.Controls;
using libSMARTMultiTouch.Input;

namespace GVirtual
{
    class TrayTab : InteractiveBorder
    {
        //is this the current tab/screen being displayed?
        private Boolean current;

        //unique ID for tab (for linking to info screen)
        private int ID;

        private TrayArea area;
        private InfoScreen info;
        private TrayFrame frame;
        
        private TextBlock text;

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Constructor</para></summary> 
        ///<param type ="TrayArea"> TrayArea that contains this object </param>
        ///<param type ="double"> Height and width for screen 
        ///                      (recommended: same height as TrayArea height and width as porportion of TrayArea width </param>
        ///<param type ="VerticalAlignment"> If tab is top, center or bottom tab in the TrayArea</param>
        ///<param type ="InfoScreen"> InfoScreen this tab links to </param>
        ///<param type ="int"> ID number </param>
        public TrayTab(TrayFrame frame, TrayArea area, double height, double width, System.Windows.VerticalAlignment position, InfoScreen info, int ID)
        {
            this.frame = frame;
            this.area = area;
            this.Height = height;
            this.Width = width;
            this.VerticalAlignment = position;
            this.info = info;
            this.ID = ID;
            this.Background = frame.getSecondaryColor();
            this.current = false;

            //to recognize touches to switch between tabs/screens
            TouchInputManager.AddTouchContactDownHandler(this, new TouchContactEventHandler(TabTouch));
            TouchInputManager.AddTouchContactDownHandler(this, new TouchContactEventHandler(frame.TrayMenu.Filter.CloseFilterTouch));
        }//end constructor

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function adds text for testing and debugging.</para></summary> 
        ///<param type ="String"> Text to print on screen </param>
        public void setText(String str)
        {
            text = new TextBlock();
            text.Height = Height / 4;
            text.Width = Width / 2;
            text.Background = new SolidColorBrush(Colors.White);
            text.Foreground = this.Background;
            text.Text = str;
            text.TextAlignment = System.Windows.TextAlignment.Center;
            Child = text;
        }//end method setText

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function returns if this tab is the one currently having InfoScreen displayed</para></summary> 
        public Boolean isCurrent()
        {
            return current;
        }//end method isCurrent

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function sets this tab to the current tab</para></summary> 
        public void setToCurrent()
        {
            this.Background = frame.getPrimaryColor();
            text.Foreground = frame.getPrimaryColor();
            current = true;
        }//end method setToCurrent

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function ensures this tab is not current tab when another tab becomes the current tab</para></summary> 
        public void setToNotCurrent()
        {
            this.Background = frame.getSecondaryColor();
            text.Foreground = frame.getSecondaryColor();
            current = false;
        }//end method setToNotCurrent

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function returns ID number</para></summary> 
        public int getID()
        {
            return ID;
        }//end method getID

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function returns designated info screen this tab links to</para></summary> 
        public InfoScreen getInfoScreen()
        {
            return info;
        }//end method getInfoScreen

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function switches this tab to be current tab when touched by user</para></summary> 
        private void TabTouch(object sender, TouchContactEventArgs e)
        {
            TrayTab touched = sender as TrayTab;
            touched.area.setCurrentTab(touched);
        }//end method TabTouch

    }//end class TrayTab
}
