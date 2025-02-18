/*
 * 
 * G- Virtual Campus Tour Canvas
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.IO;

using libSMARTMultiTouch.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging; //BitmapImage
using libSMARTMultiTouch.Controls;
using System.Diagnostics;
//using System.Drawing.Image;  doesn't work?
//using System.Drawing.Drawing2D;  doesn't work?

//using System.Windows.Forms;

namespace GVirtual
{

    class GVirtualCanvas : Canvas
    {
        private Datafile data = new Datafile(Directory.GetCurrentDirectory() + "\\");
        private Boolean trayDebbugMode = true; //Set to false to remove Group1's tray Building-Tray interactions

        private Dictionary<String, Building> buildingsHM = new Dictionary<String, Building>(); //collection of buildings on the canvas
        private double windowHeight = 768, windowWidth = 1024;   //canvas height and width
        private TextBox console;     //console that displays on canvas: used for debugging purposes

        private TrayFrame leftTrayFrame; //frame that contains left tray elements
        private TrayFrame rightTrayFrame; //frame that contains right tray elements

        private Grid screen; //grid that organises all the elements on the canvas 
        //private Database db; //initiates database

        //proportion of screen to be occupied by one tray object 
        //ex.   6 = 1/6 of frame is one tray
        private int TraySection = 6;

        //number necessary for moving tray into correct location after being rotated to fit on screen
        private double TrayShift = 3.4;

        //animations
        private DoubleAnimation myDoubleAnimation;
        //private Storyboard myStoryboard;

        private ArrayList buildingsCollection;
        private List<PoI> PoICollection;

        private Boolean CanRegisterName = true;


        public GVirtualCanvas()
        {
            //Add all [graphic] elements to the canvas
            AddElementsToCanvas();

        }//end constructor

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>This function adds elements to the canvas. Note: The order in which the elements
        ///are added in important.</para></summary>  
        void AddElementsToCanvas()
        {

            //prepare the canvas
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Width = windowWidth;
            this.Height = windowHeight;

            //prepare the screen (Grid)
            screen = new Grid();
            screen.Height = windowHeight;
            screen.Width = windowWidth;
            screen.Background = new SolidColorBrush(Colors.Transparent);
            this.Children.Add(screen);

            // 1. GBurg Map
            AddMap("images/campusmap2.jpg");

            // 2. Console window for debugging
            AddConsoleWindow();

            //3. Initiate the database (once debug console window is ready for use)
            //db = new Database(console);

            // 2.Trays
            AddTrays();

            //4. Campus Buildings
            AddBuildings();


            //this.Children.Add(console);

            //5 Reset button
            AddResetBtn();



        }//end method AddElementsToCanvas

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>This function adds a console window that is used for printing text for debugging purposes.</para></summary> 
        ///<param type ="Boolean"> Optional to include the console on the window </param>
        /// <returns>void</returns> 
        void AddConsoleWindow()
        {
            this.console = new TextBox();
            Canvas.SetTop(console, 0);
            Canvas.SetLeft(console, windowWidth / 6);
            this.console.Height = 200;
            this.console.Width = 680;
            this.console.Background = new SolidColorBrush(Colors.Black);
            this.console.Foreground = new SolidColorBrush(Colors.White);
            this.console.FontFamily = new FontFamily("Courier");
            this.console.FontSize = 9;
        }


        /// <summary><para>@authors: Fumbani Chibaka, Walter </para>
        ///<para>This function adds a map image to the canvas.</para></summary> 
        ///<param type ="String"> Filepath for the map image</param>
        void AddMap(String FilePath)
        {
            //BitmapImage map = new BitmapImage(new Uri(FilePath, UriKind.Relative));
            BitmapImage map = new BitmapImage();
            map.BeginInit();
            map.UriSource = new Uri(FilePath, UriKind.Relative);
            map.DecodePixelWidth = ((1024 * 2) / 3);
            map.EndInit();
            TouchImageButton mapBtn = new TouchImageButton(map);
            mapBtn.HorizontalAlignment = HorizontalAlignment.Center;
            mapBtn.VerticalAlignment = VerticalAlignment.Bottom;

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("images/gvirtuallogo.png", UriKind.Relative);
            logo.DecodePixelWidth = ((1024 * 2) / 3);
            logo.EndInit();
            TouchImageButton logoBtn = new TouchImageButton(logo);
            logoBtn.HorizontalAlignment = HorizontalAlignment.Center;
            logoBtn.VerticalAlignment = VerticalAlignment.Top;

            screen.Children.Add(mapBtn);
            screen.Children.Add(logoBtn);
        }


        /// <summary><para>@authors: Amanda Gower, Fumbani Chibaka </para>
        ///<para>This function creates and adds two grids that represents the trays.</para></summary> 
        ///<param type ="String"> String </param>
        /// <returns>void</returns>  
        void AddTrays()
        {


            leftTrayFrame = new TrayFrame(windowWidth / 6, windowHeight, "left", this);
            rightTrayFrame = new TrayFrame(windowWidth / TraySection, windowHeight, "right", this);

            //get left tray, rotate and move to fit at side
            InteractiveBorder leftTray = leftTrayFrame.Tray;
            leftTray.HorizontalAlignment = HorizontalAlignment.Left;
            leftTray.VerticalAlignment = VerticalAlignment.Center;

            TransformGroup tgLeft = new TransformGroup();
            tgLeft.Children.Add(new RotateTransform(90));
            tgLeft.Children.Add(new TranslateTransform((-windowWidth / TrayShift)+(.05*leftTray.Height), 0));
            leftTray.RenderTransform = tgLeft;

            screen.Children.Add(leftTray);

            //get right tray, rotate and move to fit at side
            InteractiveBorder rightTray = rightTrayFrame.Tray;
            rightTray.HorizontalAlignment = HorizontalAlignment.Right;
            rightTray.VerticalAlignment = VerticalAlignment.Center;

            TransformGroup tgRight = new TransformGroup();
            tgRight.Children.Add(new RotateTransform(270));
            tgRight.Children.Add(new TranslateTransform(windowWidth / TrayShift, 0));
            rightTray.RenderTransform = tgRight;

            screen.Children.Add(rightTray);
        }


        /// <summary><para>@authors: DJ , Fumbani Chibaka, Walter </para>
        ///<para>This function adds buildings on the canvas. Note: Buildings are added at the specified points according to dictionary
        ///</para></summary> 
        private void AddBuildings()
        {
            buildingsCollection = new ArrayList();
            PoICollection = data.GetPOIList();
            Building b = new Building();
            this.Children.Add(b);
            this.AddListeners(b);

            /*
            foreach (PoI p in PoICollection)
            {
                //prevents dining areas inside CUB from being added to map
                if (!(p.Icon.X == 0 && p.Icon.Y == 0))
                {
                    Building b = new Building(p.ID.ToString(), p.Name, p.Icon.FileName, new Point(p.Icon.X, p.Icon.Y), p.Icon.ScaledDimension);
                    buildingsCollection.Add(b);
                    b.Parent = this;
                    this.buildingsHM.Add(b.FullName, b);
                    this.Children.Add(b);
                    this.AddListeners(b);
                }
                //TODO:  this.AnimateBuilding(building, true);
            }
            */
        }//end method AddBuildings


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>  Add Listeners to the building </para></summary> 
        private void AddListeners(Building building)
        {
            building.TouchDown += new TouchContactEventHandler(BuildingTouchDown);
            building.TouchUp += new TouchContactEventHandler(BuildingTouchUp);
            building.TouchMove += new TouchContactEventHandler(BuildingTouchMove);
        }



        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>  Animates the building of interest </para></summary> 
        public void AnimateBuilding(Building building, Boolean registerName)
        {
            Application.Current.MainWindow.RegisterName(building.Name, building);

            if (registerName)
            {
                myDoubleAnimation = new DoubleAnimation();
                myDoubleAnimation.From = 1.0;
                myDoubleAnimation.To = 0.3;
                myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                myDoubleAnimation.AutoReverse = true;
                myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(myDoubleAnimation);
                Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Building.OpacityProperty));

            }

            //set target
            Storyboard.SetTargetName(myDoubleAnimation, building.Name);

            //myStoryboard.Begin(Application.Current.MainWindow);
        }


        /// <summary><para>@author: Fumbani Chibaka, DJ, Walter</para>
        ///<para>Touch Down event listener attached to buildings</para></summary> 
        
        void BuildingTouchDown(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {

            Building b = (Building)sender;
            Debug.WriteLine("DOWN id: " + e.TouchContact.ID + "X: " + e.TouchContact.Position.X + " Y: " + e.TouchContact.Position.Y);

            //Debug.WriteLine("" + b.HasTouch);
            /*
            if (b.HasTouch == false)
            {
                b.BitmapEffect = null; //Cancel out effect

                //add new Building object for multiuser interaction
                Building newBuilding = new Building(b.Name, b.FullName, b.Url, b.OriginalPosition, b.ScaledWidth);
                this.AddListeners(newBuilding);
                this.Children.Add(newBuilding);


                // Replace active building in Hashmap

                Boolean removed = this.buildingsHM.Remove(b.FullName);
                //  Trace(b.FullName + " removed from Hashmap : " + removed);
                this.buildingsHM.Add(b.FullName, newBuilding);


                //this.PrintHashMap();
                //unregister name once the building is selected

                //Application.Current.MainWindow.UnregisterName(b.Name);
                //this.AnimateBuilding(newBuilding, false);


                //if (this.trayDebbugMode)
                //    DebugTools.Tray_Building_DropInteractions(2, new Point(), building, leftTrayFrame,rightTrayFrame); //@parameters: mode, touch point, building, trays

                //Clear Touch Spot 
                e.TouchContact.Release();
                b.HasTouch = true;
                b.TouchID = e.TouchContact.ID;
            }

            else
            {
                e.TouchContact.Release();
            }

            */
        }//end method buildingTouchDown
        

        /// <summary><para>@author: Fumbani Chibaka, DJ</para>
        ///<para>Touch Down event listener attached to buildings</para></summary> 
        void BuildingTouchDownOff(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {
            Debug.WriteLine("OFF id: " + e.TouchContact.ID + "X: " + e.TouchContact.Position.X + " Y: " + e.TouchContact.Position.Y);
        }

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>Touch Move event listener attached to buildings</para></summary>
        void BuildingTouchMove(object sender, TouchContactEventArgs e)
        {
            //Debug.WriteLine("MOVE id: " + e.TouchContact.ID + "X: " + e.TouchContact.Position.X + " Y: " + e.TouchContact.Position.Y);
            /*
            Building building = (Building)sender;
            
            if (building.HasTouch == false)
            {
                
                //Trace( "Touch Move : "+ building.Name);
                Point hoverPoint = e.TouchContact.Position;

                building.Position = hoverPoint;

            }

            else if (e.TouchContact.ID != building.TouchID)
            {
                e.TouchContact.Release();
            }
            */
        }//end method buildingTouchMove

        /// <summary><para>@author: Fumbani Chibaka, Walter</para>
        ///<para>Touch up event listener attached to buildings</para></summary> 
        void BuildingTouchUp(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {
            Debug.WriteLine("UP id: " + e.TouchContact.ID + "X: " + e.TouchContact.Position.X + " Y: " + e.TouchContact.Position.Y);
/*

            Building building = (Building)sender;
            if (building.HasTouch && e.TouchContact.ID == building.TouchID)
            {
                // Trace("Dropped: " + building.Name + " at " + e.TouchContact.Position);
                Point point = e.TouchContact.Position;

                // Remove building object from Canvas
                this.Children.Remove(building);

                if (this.trayDebbugMode)
                    DebugTools.Tray_Building_DropInteractions(1, point, building, leftTrayFrame, rightTrayFrame); //@parameters: mode, touch point, building, trays

                //Clear Touch Spot 
                e.TouchContact.Release();
            }
            else
            {
                e.TouchContact.Release();
            }


            //if when the building is removed, there are less buildings on the map than in the hash table, call Refresh()
            if (this.Children.Count < buildingsCollection.Count + 2) { }
                //this.Refresh();
                */
        }//end method buildingTouchUp



        /// <summary><para>@author: Fumbani Chibaka, Walter</para>
        ///<para>This function highlight POI on map [ called in TrayFilter.cs] </para></summary> 
        public void HighlightBuildings(String collectionName, TrayFrame frame)
        {
            // Trace(" Highlight Action on > : " + collectionName);

            Building target;
            buildingsHM.TryGetValue(collectionName, out target);

            if (target != null)
            {
                if (frame.isBuildingHighlighted())
                    frame.getBuildingHighlighted().AddVisualEffects();
                //Trace(" worked: " + target.Name);
                target.Highlight(frame);
                frame.setBuildingHighlighted(target);
            }
            else { }
                //Trace(collectionName + " NOT FOUND IN HASHMAP!!!!");
        }//end method HighlightBuildings


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>This function writes on the debugging console
        ///</para></summary> 
        public TextBox Console
        {
            get { return console; }
        }

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>This function writes on the debugging console
        ///</para></summary> 
        public void Trace(String input)
        {
            console.Text += "\n" + input;
            console.ScrollToEnd();
        }//end method Trace


        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>This function prints the contents of the Dictionary for debugging purposes
        ///</para></summary> 
        /*public void PrintHashMap()
        {
            Trace("______hashmap__________________________");
            List<string> keys = new List<string>(buildingsHM.Keys);
            foreach (String key in keys)
            {
                Trace(key + " > [" + buildingsHM[key] + "]");
            }
            Trace("________________________________________");

        }//end method PrintHashMap*/

        /// <summary><para>@author: Walter</para>
        ///<para>Iterates through and adds a building if its missing</para></summary>
        /*
        void Refresh()
        {
            for (int i = 0; i < buildingsCollection.Count; i++)
            {
                if (!this.Children.Contains((UIElement)buildingsCollection[i]))
                {
                    Building b = (Building)buildingsCollection[i];
                    Building refreshed = new Building(b.Name, b.FullName, b.Url, b.OriginalPosition, b.ScaledWidth);
                    this.AddListeners(refreshed);
                    this.Children.Add(refreshed);
                    this.buildingsHM.Add(b.FullName, refreshed);
                }

            }
        }
        */
        //Fumbani Chibaka - RESET the Canvas
        public void AddResetBtn()
        {

            BitmapImage reset = new BitmapImage();
            reset.BeginInit();
            reset.UriSource = new Uri("images/reset.png", UriKind.Relative);
            reset.DecodePixelWidth = 50;
            reset.EndInit();

            TouchImageButton logoBtn = new TouchImageButton(reset);
            Canvas.SetTop(logoBtn, 90);
            Canvas.SetRight(logoBtn, windowWidth / 2);
            this.Children.Add(logoBtn);

            TouchInputManager.AddTouchContactDownHandler(logoBtn, new TouchContactEventHandler(Restart));

            //Only Register the name of the button once
            // if (CanRegisterName)
            // {
            Application.Current.MainWindow.RegisterName("Reset_Button", logoBtn);
            CanRegisterName = false;
            //}

            //Animate reset button
            myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1.0;
            myDoubleAnimation.To = 0.0;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(3));
            myDoubleAnimation.AutoReverse = true;
            myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Building.OpacityProperty));

            //set target
            Storyboard.SetTargetName(myDoubleAnimation, "Reset_Button");

            myStoryboard.Begin(Application.Current.MainWindow);

        }

        /// <summary><para>@author: Fumbani CHibaka </para>
        ///<para>This method is called when expand button is touched - calls for TrayFrame to expand TrayMenu</para></summary> 
        private void Restart(object sender, TouchContactEventArgs e)
        {

            //Clear Touch Spot 
           e.TouchContact.Release();


            buildingsHM = new Dictionary<String, Building>(); //collection of buildings on the canvas
            console = new TextBox();     //console that displays on canvas: used for debugging purposes
            buildingsCollection = new ArrayList();
            PoICollection = new List<PoI>();

            this.Children.Clear();
            Application.Current.MainWindow.UnregisterName("Reset_Button");


            AddElementsToCanvas();



        }//end method ExpandTouch



        public Datafile getData()
        {
            return data;
        }

    }//end class GVirtualCanvas
}