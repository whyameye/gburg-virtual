
/*
 * Building Class: Representation of building objects on the canvas map
 * G-Virtual 
 * 
 * @Author: Fumbani Chibaka
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//manual imports
using libSMARTMultiTouch.Controls; //DraggableBorder
using System.Windows.Media.Imaging; //BitmapImage
using System.Windows.Media; //ImageBrush
using System.Windows; //Point,
using System.Windows.Media.Effects; 
using System.Windows.Controls; //Canvas.setTop.....
using System.Diagnostics;

using System.Windows.Media.Animation;
using System.IO;


namespace GVirtual
{
   
    ///
    class Building :  InteractiveBorder, ICloneable
    //class Building : DraggableBorder, ICloneable

    {
        private BitmapImage buildingBMP; //bitmap image of the building
        private Point currentPoint;
        private Point originalPoint; //original position of the building
        private GVirtualCanvas canvas;
        public String FullName;
        private String url;
        private int scaledWidth;
        private int touchID;

        private Boolean hastouch = false;

        public Building()
        {
            this.MinHeight = 300;
            this.MinWidth = 300;
            this.Height = 300;
            this.Width = 300;
            this.scaledWidth = 300;
            this.Background = new SolidColorBrush(Colors.Red);
            //this.Foreground = new SolidColorBrush(Colors.Red);
            Debug.WriteLine("building constructor");
            //this.IsScaleEnabled = false;
            //Visibility = Visibility.Visible;

            //this.IsTouchBounceEnabled = true;

            //Canvas.SetLeft(this,0);
            //Canvas.SetTop(this, 0);
        }

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>Constuctor for Class GVirtual </para></summary> 

        /*
        public Building(String ID, String name, String url, Point originalPoint, int scaledWidth)
        {
            this.Name = "Building"+ID;
            this.FullName = name;
        
            this.url = url;
            this.scaledWidth = scaledWidth;
            this.buildingBMP = new BitmapImage();
            this.buildingBMP.BeginInit();
            this.buildingBMP.UriSource = new Uri(Directory.GetCurrentDirectory() + "\\icons\\" + url, UriKind.Relative);
            this.buildingBMP.DecodePixelWidth = scaledWidth;
            this.buildingBMP.EndInit();
            ImageBrush paint = new ImageBrush(buildingBMP);

            //paint building as DraggableBorder background
            
            this.Background = paint;
            this.Width =  buildingBMP.PixelWidth;
            this.Height =  buildingBMP.PixelHeight;

            //position building on the map
            this.originalPoint = originalPoint;
            Canvas.SetLeft(this, originalPoint.X);
            Canvas.SetTop(this, originalPoint.Y);

            this.IsScaleEnabled = false;
            
            this.IsTouchBounceEnabled = true;
          

            AddVisualEffects();  //show building with a glowing border
        }//end constructor
        */

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para> get the original Positon of the building </para></summary> 
        public Point OriginalPosition
        {
            get { return originalPoint; }
        }//end function OriginalPosition

        /// <summary><para>@author: DJ</para>
        ///<para> get the image url of the building </para></summary> 
        public String Url
        {
            get { return url; }
        }//end function Url

        /// <summary><para>@author: DJ</para>
        ///<para> get the scaledWidth of the building </para></summary> 
        public int ScaledWidth
        {
            get { return scaledWidth; }
        }//end function ScaledWidth


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para> Manipulate Building Position</para></summary>
        public Point Position
        {
            set
            {
                this.currentPoint = value;
                this.UpdatePosition(currentPoint);
            }
            get { return currentPoint; }
        }//end method Position


        public Boolean HasTouch
        {
            set { this.hastouch = value; }
            get { return this.hastouch; }
        }



        public int TouchID
        {
            set { this.touchID = value; }
            get { return touchID; }
        }


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para> set the parent Canvas (container) of the building</para></summary> 
        public GVirtualCanvas Parent{
            set { this.canvas = value; }
        }


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>  Debbuing Use: Update the position of the building on the map</para></summary> 
        private void UpdatePosition(Point targetPoint)
        {
           // if (targetPoint.X % 10 == 0)
           //canvas.Trace(""+targetPoint);
        }

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>  //Add Visual Effects to Building Border</para></summary> 
        public void AddVisualEffects()
        {
            //Outer Glow Effect
            OuterGlowBitmapEffect outerGlow = new OuterGlowBitmapEffect();


            outerGlow.GlowColor = Colors.Green;
            outerGlow.GlowSize = 15;
            outerGlow.Opacity = 1;
            this.BitmapEffect = outerGlow; //obsolete yikes
        }//end method AddVisualEffects


        /// <summary><para>@author: Fumbani Chibaka, Walter</para>
        ///<para>  //Add Visual Effects to Building Border</para></summary> 
        public void Highlight(TrayFrame frame)
        {
            OuterGlowBitmapEffect outerGlow = new OuterGlowBitmapEffect();
            if (frame.isLeftTray())
                outerGlow.GlowColor = Colors.Orange;
            else
                outerGlow.GlowColor = Colors.Blue;
            outerGlow.GlowSize = 30;
            outerGlow.Opacity = 1;
            this.BitmapEffect = outerGlow; //obsolete yikes
        }//end method HighLight


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>Make a clone of the building object</para></summary>
       public object Clone () //ICloneable implementation
        {
            Building clone = this.MemberwiseClone() as Building;
            return clone;
        }//end method Clone

       public string ToString()
       {
           return this.FullName;
       }


    }//end class Building
}

