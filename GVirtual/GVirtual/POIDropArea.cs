/*
 * POIDropArea class 
 * extends Grid class
 * 
 * Author(s): 
 * Adam Hartman
 * Fumbani Chibaka
 * Amanda Gower
 * 
 * 
 * Get added to instance of TrayMenu class (which is then ultimately added to TrayFrame)
 * Aligned to right side of TrayMenu (TrayFilter instance goes on left side)
 * 
 * 
 * Intended to contain pictures of POIs (points of interest) that have been added to tray by user
 * User should be able to switch between viewing information for different buildings by selecting them from this area
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using libSMARTMultiTouch.Controls;
using libSMARTMultiTouch.Input;
using libSMARTMultiTouch.Extensions;
using libSMARTMultiTouch.Resources;
using System.Windows.Navigation;
using System.Collections;
using System.IO;



namespace GVirtual
{
    class POIDropArea : Grid
    {

        //Datafile db = new Datafile(Directory.GetCurrentDirectory() + "\\");  //list root directory for database directory
        private TrayMenu menu;
        private TrayFrame trayFrame;
        private Point position; //position on the main canvas
        private TextBlock clearButton;
        private TextBlock filterOpt;
        private TouchImageButton picWindow;
        private TextBlock mainButton;
        private Grid mainArea;
        private bool clear;
        private Building currentBuilding;
        private BitmapImage img;
        private String buildingName;
        private static ArrayList deptOptions = new ArrayList(new String[3] { "Information", "Show Location of Associated Buildings", "Department Contact Info" });
        private static ArrayList options = new ArrayList(new String[1] { "Show Location" });
        private TrayFilter filter;
        private ArrayList depts;
        private GVirtualCanvas canvas;

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Constructor</para></summary> 
        ///<param type ="double"> Height and width for screen 
        ///                      (recommended: same height as TrayMenu height and width as porportion of TrayMenu width </param>
        ///<param type ="TrayMenu"> TrayMenu instance that holds this POIDropArea object </param>
        public POIDropArea(double height, double width, TrayMenu menu, TrayFrame trayFrame)
        {

            this.Height = height;
            this.Width = width;
            this.menu = menu;
            this.trayFrame = trayFrame;
            this.Background = trayFrame.getSecondaryColor();
            this.clear = true;
            this.filter = menu.Filter;

            canvas = trayFrame.Canvas;
            List<Department> temp = canvas.getData().GetDepartmentList();
            depts = new ArrayList();
            foreach (Department d in temp)
            {
                depts.Add(d.Name);
            }


            setupButtons(height, width);

            //TouchInputManager.AddTouchContactDownHandler(this, new TouchContactEventHandler(DropInTray));           
        }//end constructor


        /// <summary><para>@author: Adam Hartman/  ???? </para>
        ///<para> Sets up buttons on drop area
        private void setupButtons(double height, double width)
        {

            //clear button

            clearButton = new TextBlock();
            clearButton.Height = height / 8;
            clearButton.Width = width;
            clearButton.Background = new SolidColorBrush(Colors.LightGray);
            clearButton.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            clearButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            clearButton.Text = "clear";
            clearButton.FontSize = 15;
            clearButton.TextAlignment = System.Windows.TextAlignment.Center;
            TouchInputManager.AddTouchContactDownHandler(clearButton, new TouchContactEventHandler(Clear_TouchContactDown));
            TouchInputManager.AddTouchContactUpHandler(clearButton, new TouchContactEventHandler(Clear_TouchContactUp));
            this.Children.Add(clearButton);



            //filter button
            filterOpt = new TextBlock();
            filterOpt.Height = height / 7;
            filterOpt.Width = width / 2;
            filterOpt.Text = "Filter";
            filterOpt.Foreground = new SolidColorBrush(Colors.Ivory);
            filterOpt.FontSize = 10;
            filterOpt.TextAlignment = System.Windows.TextAlignment.Center;
            filterOpt.Background = trayFrame.getPrimaryColor();
            filterOpt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            filterOpt.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            TouchInputManager.AddTouchContactDownHandler(filterOpt, new TouchContactEventHandler(Filter_TouchContactDown));
            this.Children.Add(filterOpt);




            mainButton = new TextBlock();
            mainButton.Height = height / 7;
            mainButton.Width = width / 4;
            mainButton.Text = "Main";
            mainButton.Foreground = new SolidColorBrush(Colors.Ivory);
            mainButton.FontSize = 10;
            mainButton.TextAlignment = System.Windows.TextAlignment.Center;
            mainButton.Background = trayFrame.getSecondaryColor();
            mainButton.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            mainButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            TouchInputManager.AddTouchContactDownHandler(mainButton, new TouchContactEventHandler(Main_TouchContactDown));
            this.Children.Add(mainButton);


            //picture with correct building
            //first set up bitmap for no image (no building selected)
            img = new BitmapImage(new Uri(@"dropimages/clear.jpg", UriKind.RelativeOrAbsolute));
            picWindow = new TouchImageButton(img, height / 4.5, width / 6);
            picWindow.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            picWindow.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.Children.Add(picWindow);

            //main area of drop area where filter buttons go
            mainArea = new Grid();
            mainArea.Height = height / 1.37;
            mainArea.Width = width;
            mainArea.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            mainArea.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            setMainAreaClear();
            TransformGroup tg = new TransformGroup();
            tg.Children.Add(new TranslateTransform(0, 3));
            mainArea.RenderTransform = tg;
            mainArea.Background = new SolidColorBrush(Colors.Transparent);
            this.Children.Add(mainArea);

        }

        /// <summary><para>@author: Adam Hartman/  ???? </para>
        ///<para> touch down event handler for clear button down
        private void Clear_TouchContactDown(object sender, TouchContactEventArgs e)
        {
            if (getClear())
            {
                TextBlock block = sender as TextBlock;
                trayFrame.TrayArea.Info1Screen.Children.Clear();
                trayFrame.TrayArea.Info2Screen.Children.Clear();
                trayFrame.TrayArea.Info3Screen.Children.Clear();
                mainAreaType("clear");
                setClearInactive();
            }
        }

        /// <summary><para>@author: Adam Hartman/  ???? </para>
        ///<para> touch down event handler for clear button up
        private void Clear_TouchContactUp(object sender, TouchContactEventArgs e)
        {

        }


        /// <summary><para>@author: Adam Hartman/  ???? </para>
        ///<para> touch down event handler for filter button
        private void Filter_TouchContactDown(object sender, TouchContactEventArgs e)
        {
       
            mainAreaType("filter");
           

        }

        /// <summary><para>@author: Adam Hartman/  ???? </para>
        ///<para> touch down event handler for main button
        private void Main_TouchContactDown(object sender, TouchContactEventArgs e)
        {
            mainAreaType("main");


        }
        /// <summary><para>@author: Adam Hartman/  ???? </para>
        ///<para>method that changes main area depending on button selection
        private void mainAreaType(String type)
        {
            if (getClear())
            {
                if (type.Equals("filter"))
                {
                    mainArea.Children.Clear();
                    filterOpt.Background = trayFrame.getSecondaryColor();
                    mainArea.Background = new SolidColorBrush(Colors.Transparent);
                    mainButton.Background = trayFrame.getPrimaryColor();

                    //configure filter area off of current building selected

                    configureFilter();


                }
                else if (type.Equals("main"))
                {
                    mainButton.Background = trayFrame.getSecondaryColor();
                    mainArea.Background = new SolidColorBrush(Colors.Transparent);
                    filterOpt.Background = trayFrame.getPrimaryColor();
                    mainArea.Children.Clear();
                    configureMain();
                }

                else if (type.Equals("clear"))
                {
                    img = new BitmapImage(new Uri(@"dropimages/clear.jpg", UriKind.RelativeOrAbsolute));
                    picWindow = new TouchImageButton(img, this.Height / 4.5, this.Width / 6);
                    picWindow.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    picWindow.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    mainAreaType("main");
                    setMainAreaClear();
                    this.Children.Add(picWindow);
                    trayFrame.clearBuildingList();
                    setClearInactive();


                }
            }

        }

        /// <summary><para>@author: Adam Hartman/  ???? </para>
        ///Sets main area back to clear
        private void setMainAreaClear()
        {

            mainArea.Children.Clear();
            TextBlock tb = new TextBlock();
            tb.Text = "Drop a building to begin";
            tb.Foreground = new SolidColorBrush(Colors.Ivory);
            tb.FontSize = 13;
            tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            mainArea.Children.Add(tb);

        }

        /// <summary><para>@author: Adam Hartman/  ???? </para>
        ///method that checks to see if tray has been cleared
        private bool getClear()
        {

            return clear;

        }

        /// <summary><para>@author: Adam Hartman/  ???? </para>
        //method that configures complete drop area when a building is dropped on tray( sets pic and then calls filter and main setup methods
        private void configureDropArea(String poi)
        {


            setClearActive();
            mainArea.Children.Clear();
            configureMain();


            String poiName = buildingName;

            if (poiName == "Pennsylvania Hall/ Financial Aid")
            {
                poiName = "Pennsylvania Hall";
            }
            else if (poiName == "Servo/ Dining Hall")
            {
                poiName = "Servo";
            }
            else if (poiName == "Center of Athletics, Recreation and Fitness")
            {
                poiName = "Center";
            }



                img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(@"dropimages/" + poiName + ".jpg", UriKind.RelativeOrAbsolute);
                int h = Convert.ToInt32(Height / 4.5);
                img.DecodePixelHeight = (int)h;
                img.EndInit();
                picWindow = new TouchImageButton(img, this.Height / 4.5, this.Width / 6);
                picWindow.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                picWindow.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                this.Children.Add(picWindow);
                //configureFilter();

          


        }

        /// <summary><para>@author: Adam Hartman/  ???? </para>
        //method that displays appropriate filter info for current POI
        private void configureMain()
        {


        

            TextBlock curr;
            String poiName = buildingName;

            curr = new TextBlock();
            if (poiName.Length > 30)
            {
                curr.Height = this.Height / 7;
            }
            else
            {
                curr.Height = this.Height / 9;
            }
            curr.Width = this.Width;
            curr.Text = poiName;
            curr.FontSize = 10;
            curr.TextWrapping = TextWrapping.Wrap;
            curr.TextAlignment = System.Windows.TextAlignment.Center;




            curr.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            curr.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            curr.Background = new SolidColorBrush(Colors.White);
            TransformGroup tg = new TransformGroup();
            tg.Children.Add(new TranslateTransform(0, (this.Height / 9) * 0));
            curr.RenderTransform = tg;
            TouchInputManager.AddTouchContactDownHandler(curr, new TouchContactEventHandler(Main_TouchContactDown));
            mainArea.Children.Add(curr);

        }



        /// <summary><para>@author: Adam Hartman/  ???? </para>
        //method that displays appropriate filter info for current POI
        private void configureFilter()
        {


            String poi = buildingName;
            ArrayList filterCategories = new ArrayList();
            



            if (depts.Contains(poi))
                filterCategories = deptOptions;
            else
                filterCategories = options;




            Boolean isWrap = false;

            for (int i = 0; i < filterCategories.Count; i++)
            {
                
                TextBlock current = new TextBlock();
                String text = filterCategories[i] as String;
                if (text.Length > 25)
                {
                    current.Height = this.Height / 7;
                    isWrap = true;
                }
                else
                {
                    current.Height = this.Height / 9;
                }

                current.TextWrapping = TextWrapping.Wrap;
      
                current.Width = this.Width;
                current.Text = text;
                current.FontSize = 10;
                current.TextAlignment = System.Windows.TextAlignment.Center;

                current.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                current.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                current.Background = new SolidColorBrush(Colors.White);
                TransformGroup tg = new TransformGroup();
                if (isWrap && i == 2)
                {


                    tg.Children.Add(new TranslateTransform(0, (this.Height / 7.8) * i));
                    current.RenderTransform = tg;
                }
                else
                {
                    tg.Children.Add(new TranslateTransform(0, (this.Height / 9) * i));
                    current.RenderTransform = tg;
                }
                    
              
                mainArea.Children.Add(current);

                if (((String)filterCategories[i]).Equals("Information"))
                    TouchInputManager.AddTouchContactDownHandler(current, new TouchContactEventHandler(DisplayInfo));
                else if (((String)filterCategories[i]).Equals("Show Location of Associated Buildings") ||
                               ((String)filterCategories[i]).Equals("Show Location"))
                    TouchInputManager.AddTouchContactDownHandler(current, new TouchContactEventHandler(HighlightPOITouch));
                else if (((String)filterCategories[i]).Equals("Department Contact Info"))
                    TouchInputManager.AddTouchContactDownHandler(current, new TouchContactEventHandler(DisplayContact));
               

            }




        }

        /// <summary><para>@author: Adam Hartman, Amanda Gower, Fumbani Chibaka </para>
        ///<para>This method displays the info screen for the current POI (edited method in TrayFilter Class</para></summary> 
        private void DisplayInfo(object sender, TouchContactEventArgs e)
        {
            trayFrame.TrayArea.Info1Screen.Children.Clear();
            trayFrame.TrayArea.Info2Screen.Children.Clear();
            trayFrame.TrayArea.Info3Screen.Children.Clear();

            //String first = ((TextBlock)firstButtonPressed.Child).Text;
            String button = buildingName;


         

                List<MediaFile> files = canvas.getData().GetMediaList();
                String file = "";
                List<String> pics = new List<String>();
                List<String> videos = new List<String>();
                
                foreach (MediaFile f in files)
                {
                    if (f.FileName.Equals(button + ".txt"))
                    {
                        file = button + ".txt";
                    }
                    else if (f.Tags.Contains(button.ToLower()) && f.Tags.Contains("pic"))
                    {
                        pics.Add(f.FileName);
                    }
                    else if (f.Tags.Contains(button.ToLower()) && f.Tags.Contains("video"))
                    {
                        videos.Add(f.FileName);
                    }
                }
                if (!file.Equals(""))
                {
                    TextReader tr = new StreamReader(Directory.GetCurrentDirectory() + "\\text\\" + file);
                    String readIn = tr.ReadToEnd();
                    tr.Close();

                    trayFrame.TrayArea.Info1Screen.setInfoText(readIn);
                }
                if(pics.Count != 0)
                    trayFrame.TrayArea.Info2Screen.setInfoPics(pics);
                if(videos.Count != 0)
                   trayFrame.TrayArea.Info3Screen.setInfoVids(videos);

           
        }//end method DisplayInfo

        


         /// <summary><para>@author: Adam Hartman, Amanda Gower </para>
        ///<para>This method highlights select building (edited method in tray filter class)</para></summary> 
        private void HighlightPOITouch(object sender, TouchContactEventArgs e)
        {

             ArrayList depts;
            List<Department> temp = canvas.getData().GetDepartmentList();
            depts = new ArrayList();
            foreach (Department d in temp)
            {
                depts.Add(d.Name);
            }
                	
            

            String collectionName = buildingName;
            if (collectionName.Equals("Ike's") || collectionName.Equals("Bullet Hole") || collectionName.Equals("The Commons"))
                canvas.HighlightBuildings("College Union Building", menu.getFrame());
            else if (collectionName.Equals("The Dive"))
                canvas.HighlightBuildings("Center of Athletics", menu.getFrame());
            else if (depts.Contains(collectionName))
            {
                List<PoI> POIs = canvas.getData().GetPOIList();
                foreach (PoI p in POIs)
                {
                    foreach (Department d in p.GetDepartments())
                    {
                        if (d.Name == collectionName)
                            canvas.HighlightBuildings(p.Name, menu.getFrame());
                    }
                }
            }
            else
                canvas.HighlightBuildings(collectionName, menu.getFrame());

        }

        /// <summary><para>@author: Adam Hartman,Amanda Gower, Fumbani Chibakan</para>
        ///<para>This method displays the contact info screen for the POI selected (Edited method from Tray FIlter class</para></summary> 
        public void DisplayContact(object sender, TouchContactEventArgs e)
        {
          
            String button = buildingName;

           

            
           
                List<MediaFile> files = canvas.getData().GetMediaList();
                String file = "";
                foreach (MediaFile f in files)
                {
                    if (f.FileName.Equals(button + ".txt"))
                    {
                        file = button + " Contact.txt";
                    }
                }
                if (!file.Equals(""))
                {
                    TextReader tr = new StreamReader(Directory.GetCurrentDirectory() + "\\text\\" + file);
                    String readIn = tr.ReadToEnd();
                    tr.Close();

                    trayFrame.TrayArea.Info1Screen.setInfoText(readIn);
                }
                trayFrame.TrayArea.Info2Screen.Children.Clear();
                trayFrame.TrayArea.Info3Screen.Children.Clear();

                
            

        
        }




        // <summary><para>@author:Adam Hartman  ???? </para>
        ///<para> makes clear button inactive</para></summary
        public void setClearInactive()
        {

            clear = false;

            clearButton.Background = new SolidColorBrush(Colors.LightGray);

        }

        // <summary><para>@author:Adam Hartman  ???? </para>
        ///<para>makes clear button active</para></summary
        public void setClearActive()
        {
            clear = true;
            clearButton.Background = new SolidColorBrush(Colors.White);

        }



        /// <summary><para>@author: Fumbani Chibaka & Adam Hartman  ???? </para>
        ///<para> Used for Debugging : This function activates the POIDropArea once a building is dropped in its containing tray</para></summary> 
        public void Activate(Building building, Point point)
        {
            this.currentBuilding = building;
            buildingName = currentBuilding.FullName;
            configureDropArea(buildingName);

        }//end method Activate



        /// <summary><para>@author:Adam Hartman  ???? </para>
        ///<para> Activates drop area on filter selection</para></summary> 
        public void ActivateFilter(String name)
        {
            buildingName = name;
            setClearActive();
            mainArea.Children.Clear();
            configureMain();

        }//end method Activate


        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This function returns the TrayFrame that holds this POIDropArea object</para></summary> 
        public TrayFrame getTray()
        {
            return menu.getFrame();
        }


        /// <summary><para>@author: Fumbani Chibaka/  ???? </para>
        ///<para>User Interface: This function instructs/ stops animations one user drops building in tray whenever a building has been picked</para></summary> 
        public void StopAnimations()
        {
            this.Background = new SolidColorBrush(Colors.Orange);
        }//end method buildingTouchDown


        /// <summary><para>@author: Fumbani Chibaka/  ???? </para>
        ///<para>This function defines the active area of the POIDropArea on the tray.
        /// Used by Building object (>touchUp) to check  if a building object has been dragged
        /// dragged into the tray</para></summary> 
        ///<param type ="Point">  Point   </param> <param type ="String"> String </param>
        /// <returns>void</returns>  
        public Boolean ActiveArea(Point point)
        {
            Rect activeArea = new Rect(position.X, position.Y, this.Width, this.Height);
            return activeArea.Contains(point);
        }


        /// <summary><para>@author: Fumbani Chibaka/ ???? </para>
        ///<para>Setter and getter for the current location of the tray on the main canvas </para></summary> 
        public Point Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }//end function Position


    }
}
