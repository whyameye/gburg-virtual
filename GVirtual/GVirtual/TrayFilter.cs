/*
 * TrayFilter class 
 * extends Grid class
 * 
 * Author(s): 
 * Amanda Gower
 * 
 * Gets added to instance of TrayMenu class (which is then ultimately added to TrayFrame)
 * Aligned to left side of TrayMenu (POIDropArea object goes on right side)
 * 
 * 
 * Begins as a small "button" on left side of tray to expand
 * When retraction is possible (filter already extended) another button is available for this option
 * When fully expanded, expand button is temporarily not available
 * 
 * Can be incrementally expanded for 4 different levels of menu/filters to allow user to find desired information
 * Can be expanded up to four levels, retracted back to original "button"
 * 
 * When expanded, filer covers remainder of tray (information displayed there is retained)
 * When retracted, the tray and information displayed once again
 * 
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using System.Collections;
using libSMARTMultiTouch.Input;
using libSMARTMultiTouch.Controls;

namespace GVirtual
{
    class TrayFilter : Grid
    {
        //for testing - arrays to populate buttons in filter expansion
        private static ArrayList firstExp = new ArrayList(new String[6] { "Departments", "Academic Buildings", "Dining", "Resources", "Student Life", "Parking Lots" }); //TODO : Ask Amanda how this works
        private ArrayList secondExp;

        private static ArrayList deptOptions = new ArrayList(new String[3] { "Information", "Show Location of Associated Buildings", "Department Contact Info" });
        private static ArrayList options = new ArrayList(new String[2] { "Show Location", "Information" });

        private List<PoI> POIs;

        private ArrayList depts; //list of departments
        private ArrayList academic; //list of academic buildings
        private ArrayList resources; //resources
        private ArrayList dining;
        private ArrayList stuLife; //student life
        private ArrayList lots; //parking lots

        private SolidColorBrush backgroundColor;

        //need grid to hold expansions of the filter
        private Grid expansions;

        //to keep uniform sizing
        private double buttonWidth = 25.0;
        private double initialWidth;

        //keeps track of degree of tray expansion
        private int expandFactor;
        private int minExpand = 0;
        private int maxExpand = 3;

        //total number of levels that can be expanded
        private int totalExpandLevels;

        //link to TrayMenu/Canvas that holds object
        private TrayMenu menu;
        private TrayArea area;
        private GVirtualCanvas canvas;

        //buttons to expand/retract filter
        private InteractiveBorder expandButton;
        private InteractiveBorder retractButton;
        private InteractiveBorder ScrollUpButton;
        private InteractiveBorder ScrollDownButton;

        //keep track of button pressed to cause expansion
        private InteractiveBorder firstButtonPressed;
        private InteractiveBorder secondButtonPressed;

        //for each of three expansions, need grids to hold buttons
        private Grid firstTop;
        private Grid firstBottom;
        private Grid secondTop;
        private Grid secondBottom;
        private Grid thirdTop;
        private Grid thirdBottom;

        //maintain what list/where in the list the second expansion is 
        private ArrayList curSecondLabels;
        private int curSecondTopIndex;

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Constructor</para></summary> 
        ///<param type ="double"> Height and width for initial "button" 
        ///                      (recommended: same height as TrayMenu height and width as porportion of TrayMenu width </param>
        ///<param type ="TrayMenu"> TrayMenu instance that holds this POIDropArea object </param>
        public TrayFilter(double height, double width, TrayMenu menu, GVirtualCanvas canvas)
        {
            this.Height = height;
            this.Width = width;
            this.initialWidth = width;
            this.menu = menu;
            this.canvas = canvas;
            this.Background = menu.getFrame().getSecondaryColor();

            //initially completely retracted
            this.expandFactor = minExpand;
            this.totalExpandLevels = maxExpand - minExpand;

            expandButton = makeButton(this.Height, buttonWidth, HorizontalAlignment.Right, VerticalAlignment.Top, "Expand Menu", 270);
            retractButton = makeButton(this.Height, buttonWidth, HorizontalAlignment.Right, VerticalAlignment.Top, "Retract Menu", 270);

            //to recognize touches to expand/retract
            TouchInputManager.AddTouchContactDownHandler(expandButton, new TouchContactEventHandler(ExpandTouch));
            TouchInputManager.AddTouchContactDownHandler(expandButton, new TouchContactEventHandler(PopulateTouch));
            TouchInputManager.AddTouchContactDownHandler(retractButton, new TouchContactEventHandler(RetractTouch));

            //add expand button - retract button is not added yet
            Children.Add(expandButton);

            /*Database db = new Database(canvas.Console);

            this.depts = db.ExtractDepartmentsList();
            this.academic = db.ListBuildings("Academic");
            this.resources = db.ListBuildings("Resource");
            this.dining = db.ListBuildings("Dining");
            this.stuLife = db.ListBuildings("Student Life");
            this.lots = db.ListBuildings("Parking Lot");*/


            POIs = canvas.getData().GetPOIList();
            
            List<Department> temp = canvas.getData().GetDepartmentList();
            depts = new ArrayList();
            foreach (Department d in temp)
            {
                depts.Add(d.Name);
            }

            academic = new ArrayList();
            resources = new ArrayList();
            dining = new ArrayList();
            stuLife = new ArrayList();
            lots = new ArrayList();
            foreach (PoI p in POIs)
            {
                List<String> tags = p.Tags;
                if (tags.Contains("academic"))
                {
                    academic.Add(p.Name);
                }
                if (tags.Contains("resource"))
                {
                    resources.Add(p.Name);
                }
                if (tags.Contains("dining"))
                {
                    dining.Add(p.Name);
                }
                if (tags.Contains("student"))
                {
                    stuLife.Add(p.Name);
                }
                if (tags.Contains("parking"))
                {
                    lots.Add(p.Name);
                }

            }
            this.secondExp = new ArrayList(new ArrayList[6] { depts, academic, dining, resources, stuLife, lots });


        }//end constructor

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method creates a "button" to be added to the filter
        ///         It returns the "button" according to the specifications of the parameters</para></summary> 
        ///<param type ="double"> Values for height and width of border </param>
        ///<param type ="HorizontalAlignment"> How the button should be aligned on grid/canvas </param> 
        ///<param type ="VerticalAlignment"> How the button should be aligned on grid/canvas </param>
        ///<param type ="String"> Text to be displayed as button label </param>
        ///<param type ="int"> Degrees that the text should be rotated to fit on "button"
        ///                     For vertical buttons: use 270 </param>
        private InteractiveBorder makeButton(double height, double width, HorizontalAlignment ha, VerticalAlignment va, String text, int rotate)
        {
            InteractiveBorder button = new InteractiveBorder();
            button.Height = height;
            button.Width = width;
            button.HorizontalAlignment = ha;
            button.VerticalAlignment = va;
            button.Background = new SolidColorBrush(Colors.Transparent);
            setButtonText(text, button, rotate);
            return button;
        }//end method makeButton

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method sets a "button" to display text with proper alignment and sizing
        ///         Spaces text to be centered, take 2/3 of border's height and 9/10 of width
        ///         Text size 14, color blue, background white (not given as parameters to ensure uniform look and feel)</para></summary> 
        ///<param type ="String"> String to be displayed </param>
        ///<param type ="InteractiveButton"> "button" on which text should be displayed </param>
        ///<param type ="int"> Degrees that the text should be rotated to fit on "button"
        ///                             For vertical buttons: use 270 </param>
        private void setButtonText(String str, InteractiveBorder button, int rotate)
        {
            TextBlock text = new TextBlock();
            text.Background = new SolidColorBrush(Colors.White);
            text.Foreground = menu.getFrame().getPrimaryColor();
            text.Text = str;
            //if this is a vertical button
            if (rotate != 0)
            {
                text.Height = 4 * button.Width / 5;
                text.Width = 9 * button.Height / 10;
            }
            else
            {
                text.Height = 9 * button.Height / 10;
                text.Width = button.Width;
            }
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.TextAlignment = TextAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.LayoutTransform = new RotateTransform(rotate);
            text.FontSize = 14;
            button.Child = text;
        }//end method setButtonText

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method makes the Grid structure to hold buttons for next expansion</para></summary> 
        ///<param type ="Grid"> Two grids (top and bottom) to organize buttons </param>
        ///<param type ="HorizontalAlignment"> Where is designated Grid the "button" should be placed </param>
        private void newExpansion(Grid top, Grid bottom, HorizontalAlignment ha)
        {
            top.VerticalAlignment = VerticalAlignment.Top;
            top.HorizontalAlignment = ha;
            top.Height = this.Height / 2;

            bottom.VerticalAlignment = VerticalAlignment.Bottom;
            bottom.HorizontalAlignment = ha;
            bottom.Height = this.Height / 2;

            if (expandFactor == 1)
            {
                top.Width = expansions.Width;
                bottom.Width = expansions.Width;
            }

            else if (expandFactor == 2)
            {
                top.Width = expansions.Width / 2;
                bottom.Width = expansions.Width / 2;
            }

            else
            {
                top.Width = expansions.Width / 3;
                bottom.Width = expansions.Width / 3;
            }
        }//end method newExpansion

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method determines which grids and what array list of strings should be used</para></summary> 
        private void populateFilterExpansion(InteractiveBorder buttonPressed)
        {
            Grid top;
            Grid bottom;
            ArrayList labels = new ArrayList();
            //initial expansion - expand factor == 1
            if (buttonPressed == null)
            {
                firstTop = new Grid();
                firstBottom = new Grid();
                newExpansion(firstTop, firstBottom, HorizontalAlignment.Left);
                top = firstTop;
                bottom = firstBottom;
                labels = firstExp;
            }

            //if the button pressed was in the first expansion
            else if (firstTop.Children.Contains(buttonPressed) || firstBottom.Children.Contains(buttonPressed))
            {
                //need to retract back to first expansion level
                if (expandFactor == 3)
                {
                    menu.getFrame().retractMenu();
                    expansions.Children.Remove(secondTop);
                    expansions.Children.Remove(secondBottom);
                    expansions.Children.Remove(thirdTop);
                    expansions.Children.Remove(thirdBottom);
                }
                else if (expandFactor == 2)
                {
                    expansions.Children.Remove(secondTop);
                    expansions.Children.Remove(secondBottom);
                }
                secondTop = new Grid();
                secondBottom = new Grid();
                newExpansion(secondTop, secondBottom, HorizontalAlignment.Right);
                firstButtonPressed = buttonPressed;

                removeExpand(firstTop, firstBottom);
                highlightSelectedButton(firstTop, firstBottom);

                top = secondTop;
                bottom = secondBottom;
                labels = (ArrayList)secondExp[firstExp.IndexOf(((TextBlock)firstButtonPressed.Child).Text)];
                curSecondLabels = (ArrayList)secondExp[firstExp.IndexOf(((TextBlock)firstButtonPressed.Child).Text)];
            }
            //need to retract back to previous expansion level
            else
            {
                //need to retract back to first expansion level
                if (expandFactor == 3)
                {
                    expansions.Children.Remove(thirdTop);
                    expansions.Children.Remove(thirdBottom);
                }

                thirdTop = new Grid();
                thirdBottom = new Grid();
                newExpansion(thirdTop, thirdBottom, HorizontalAlignment.Right);
                secondTop.HorizontalAlignment = HorizontalAlignment.Center;
                secondBottom.HorizontalAlignment = HorizontalAlignment.Center;
                secondButtonPressed = buttonPressed;

                removeExpand(secondTop, secondBottom);
                highlightSelectedButton(secondTop, secondBottom);

                top = thirdTop;
                bottom = thirdBottom;
                //different options if selected button is a department
                if (depts.Contains(((TextBlock)secondButtonPressed.Child).Text))
                    labels = deptOptions;
                else
                    labels = options;
            }

            if (labels.Count != 0)
                populateGrids(top, bottom, labels);

            expansions.Children.Add(top);
            expansions.Children.Add(bottom);
        }//end method populateFilterExpand

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method makes and inserts the "buttons" into the Grid structure for next expansion
        ///         It also adds scroll buttons to the second expansion if they are needed</para></summary> 
        ///<param type ="Grid"> Two grids (top and bottom) to organize buttons </param>
        ///<param type ="ArrayList">List of strings for labels on "buttons" in current expansion </param>
        private void populateGrids(Grid top, Grid bottom, ArrayList labels)
        {
            Grid grid;
            InteractiveBorder button;

            double tempWidth;
            if (expandFactor == 2 && curSecondLabels.Count > 6)
            {
                ScrollUpButton = makeButton(buttonWidth / 2, buttonWidth * .9, HorizontalAlignment.Right, VerticalAlignment.Top, "^", 0);
                ((TextBlock)ScrollUpButton.Child).FontSize = 8;
                TouchInputManager.AddTouchContactDownHandler(ScrollUpButton, new TouchContactEventHandler(ScrollUpTouch));
                //top.Children.Add(ScrollUpButton);

                ScrollDownButton = makeButton(buttonWidth / 2, buttonWidth * .9, HorizontalAlignment.Right, VerticalAlignment.Bottom, "v", 0);
                ((TextBlock)ScrollDownButton.Child).FontSize = 8;
                TouchInputManager.AddTouchContactDownHandler(ScrollDownButton, new TouchContactEventHandler(ScrollDownTouch));
                bottom.Children.Add(ScrollDownButton);
                curSecondTopIndex = 0;
                tempWidth = top.Width - buttonWidth;
            }
            else
                tempWidth = top.Width;
            //for initial expansion
            for (int i = 0; (i < 6) && (i < labels.Count); i++)
            {
                if (i < 3)
                    grid = top;
                else
                    grid = bottom;

                if (i % 3 == 0)
                    button = makeButton(grid.Height / 3, tempWidth, HorizontalAlignment.Left, VerticalAlignment.Top, (String)labels[i], 0);
                else if (i % 3 == 1)
                    button = makeButton(grid.Height / 3, tempWidth, HorizontalAlignment.Left, VerticalAlignment.Center, (String)labels[i], 0);
                else
                    button = makeButton(grid.Height / 3, tempWidth, HorizontalAlignment.Left, VerticalAlignment.Bottom, (String)labels[i], 0);

                if (expandFactor != maxExpand)
                {
                    TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(ExpandTouch));
                    TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(PopulateTouch));
                }
                else
                {
                    if (((String)labels[i]).Equals("Information"))
                        TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(DisplayInfo));
                    else if (((String)labels[i]).Equals("Show Location of Associated Buildings") ||
                                ((String)labels[i]).Equals("Show Location"))
                        TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(HighlightPOITouch));
                    else if (((String)labels[i]).Equals("Department Contact Info"))
                        TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(DisplayContact));
                    TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(CloseFilterTouch));
                }
                grid.Children.Add(button);
            }
        }//end method populateGrids

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method traverses the grids and ensures that the selected button has colors that
        ///         are the reverse of all other buttons</para></summary> 
        ///<param type ="Grid"> Two grids (top and bottom) to organize buttons </param>
        private void highlightSelectedButton(Grid top, Grid bottom)
        {
            InteractiveBorder button;
            TextBlock text;
            for (int i = 0; i < top.Children.Count; i++)
            {
                button = (InteractiveBorder)top.Children[i];
                text = (TextBlock)button.Child;
                if (isFirstSelected(button) || isSecondSelected(button))
                {
                    text.Background = menu.getFrame().getPrimaryColor();
                    text.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    text.Background = new SolidColorBrush(Colors.White);
                    text.Foreground = menu.getFrame().getPrimaryColor();
                }
                text.Width = button.Width;
            }
            for (int i = 0; i < bottom.Children.Count; i++)
            {
                button = (InteractiveBorder)bottom.Children[i];
                text = (TextBlock)button.Child;
                if (isFirstSelected(button) || isSecondSelected(button))
                {
                    text.Background = menu.getFrame().getPrimaryColor();
                    text.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    text.Background = new SolidColorBrush(Colors.White);
                    text.Foreground = menu.getFrame().getPrimaryColor();
                }
                text.Width = button.Width;
            }
        }//end method highlightSelectedButton

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method prevents buttons from being able to expand once expanded beyond their level</para></summary> 
        ///<param type ="Grid"> Two grids (top and bottom) to organize buttons </param>
        private void removeExpand(Grid top, Grid bottom)
        {
            //dim color and remove expand ability from previous buttons
            InteractiveBorder button;
            //TextBlock text;
            for (int i = 0; i < top.Children.Count; i++)
            {
                button = (InteractiveBorder)top.Children[i];
                TouchInputManager.RemoveTouchContactDownHandler(button, new TouchContactEventHandler(ExpandTouch));
            }
            for (int i = 0; i < bottom.Children.Count; i++)
            {
                button = (InteractiveBorder)bottom.Children[i];
                TouchInputManager.RemoveTouchContactDownHandler(button, new TouchContactEventHandler(ExpandTouch));
            }
        }//end method removeExpand

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method allows buttons to be "reactivated" once menu has been retracted back to their level</para></summary> 
        ///<param type ="Grid"> Two grids (top and bottom) to organize buttons </param>
        private void replaceExpand(Grid top, Grid bottom)
        {
            //dim color and remove expand ability from previous buttons
            InteractiveBorder button;
            TextBlock text;
            for (int i = 0; i < top.Children.Count; i++)
            {
                button = (InteractiveBorder)top.Children[i];
                if (!button.Equals(ScrollUpButton))
                {
                    //to ensure proper order of actions, remove populate touch, add again after expand touch
                    TouchInputManager.RemoveTouchContactDownHandler(button, new TouchContactEventHandler(PopulateTouch));

                    TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(ExpandTouch));
                    TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(PopulateTouch));
                }
                text = (TextBlock)button.Child;
                text.Background = new SolidColorBrush(Colors.White);
                text.Foreground = menu.getFrame().getPrimaryColor();
                text.Width = button.Width;

            }
            for (int i = 0; i < bottom.Children.Count; i++)
            {
                button = (InteractiveBorder)bottom.Children[i];
                if (!button.Equals(ScrollDownButton))
                {
                    //to ensure proper order of actions, remove populate touch, add again after expand touch
                    TouchInputManager.RemoveTouchContactDownHandler(button, new TouchContactEventHandler(PopulateTouch));

                    TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(ExpandTouch));
                    TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(PopulateTouch));
                }
                text = (TextBlock)button.Child;
                text.Background = new SolidColorBrush(Colors.White);
                text.Foreground = menu.getFrame().getPrimaryColor();
                text.Width = button.Width;
            }
        }//end method replaceExpand

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method repopulates the filter when a new button is selected</para></summary> 
        private void PopulateTouch(object sender, TouchContactEventArgs e)
        {
            InteractiveBorder buttonPressed;

            //if this is the first expansion, the button pressed was "expand" and is irrelevant
            if (expandFactor == 1)
                buttonPressed = null;
            else
                buttonPressed = (InteractiveBorder)sender;
            populateFilterExpansion(buttonPressed);
        }//end method PopulateTouch

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method is called when expand button is touched - calls for TrayFrame to expand TrayMenu</para></summary> 
        private void ExpandTouch(object sender, TouchContactEventArgs e)
        {
            if (expandFactor != maxExpand)
            {
                menu.getFrame().expandMenu();
            }
        }//end method ExpandTouch

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method is called when retract button is touched - calls for TrayFrame to retract TrayMenu</para></summary> 
        private void RetractTouch(object sender, TouchContactEventArgs e)
        {
            if (expandFactor != minExpand)
            {
                menu.getFrame().retractMenu();
            }
        }//end method RetractTouch

        //TODO integrate database
        /// <summary><para>@author: Amanda Gower, Fumbani Chibaka </para>
        ///<para>This method displays the info screen for the button selected</para></summary> 
        public void DisplayInfo(object sender, TouchContactEventArgs e)
        {
            area.Info1Screen.Children.Clear();
            area.Info2Screen.Children.Clear();
            area.Info3Screen.Children.Clear();

            String first = ((TextBlock)firstButtonPressed.Child).Text;
            String button = ((TextBlock)secondButtonPressed.Child).Text;


            //Sends correct button pushed to drop area Author:Adam Hartman
            POIDropArea dropArea = menu.POIDropArea;
            dropArea.ActivateFilter(button);
         

            //TODO: Amanda [?] Text is only added to departments?
            //if (first.Equals("Departments"))
            //{
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

                    area.Info1Screen.setInfoText(readIn);
                }
                if(pics.Count != 0)
                    area.Info2Screen.setInfoPics(pics);
                if(videos.Count != 0)
                    area.Info3Screen.setInfoVids(videos);

                    //TODO (Fumba): USE DATABASE TO TAKE IN PICTURES (return ArrayList)
                    //ArrayList pictures = new ArrayList();
                    //pictures.Add("dropImages/glat.jpg");
                    //pictures.Add("dropImages/fumbatest/anthro.jpeg");
                    //pictures.Add("dropImages/fumbatest/hass.jpg");

                
            //}

            /*else
            {
                area.Info1Screen.setText(button);
            }*/

        }//end method DisplayInfo

        /// <summary><para>@author: Amanda Gower, Fumbani Chibaka </para>
        ///<para>This method displays the contact info screen for the button selected</para></summary> 
        public void DisplayContact(object sender, TouchContactEventArgs e)
        {
            String first = ((TextBlock)firstButtonPressed.Child).Text;
            String button = ((TextBlock)secondButtonPressed.Child).Text;

            POIDropArea dropArea = menu.POIDropArea;
            dropArea.ActivateFilter(button);

            //TODO: Amanda [?] Text is only added to departments?
            if (first.Equals("Departments"))
            {
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

                    area.Info1Screen.setInfoText(readIn);
                }
                    area.Info2Screen.Children.Clear();
                    area.Info3Screen.Children.Clear();

                    //TODO (Fumba): USE DATABASE TO TAKE IN PICTURES (return ArrayList)
                    //ArrayList pictures = new ArrayList();
                    //pictures.Add("dropImages/glat.jpg");
                    //pictures.Add("dropImages/fumbatest/anthro.jpeg");
                    //pictures.Add("dropImages/fumbatest/hass.jpg");
                    //area.Info2Screen.setInfoPictures(pictures);
                
            }

            else
            {
                area.Info1Screen.setText(button);
            }

        }//end method DisplayContact


        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method determines if given button is selected in either first or second expansion</para></summary> 
        ///<param type ="InteractiveBorder"> Button to examine</param>
        private bool isSelected(InteractiveBorder button)
        {
            if (isFirstSelected(button) || isSecondSelected(button))

                return true;
            else
                return false;
        }//end method isSelected

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method determines if given button is selected in first expansion</para></summary> 
        ///<param type ="InteractiveBorder"> Button to examine</param>
        private bool isFirstSelected(InteractiveBorder button)
        {
            if (firstButtonPressed == null)
                return false;
            else if (((TextBlock)button.Child).Text == ((TextBlock)firstButtonPressed.Child).Text)
                return true;
            else
                return false;
        }//end method isFirstSelected

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method determines if given button is selected in second expansion</para></summary> 
        ///<param type ="InteractiveBorder"> Button to examine</param>
        private bool isSecondSelected(InteractiveBorder button)
        {
            if (secondButtonPressed == null)
                return false;
            else if (((TextBlock)button.Child).Text == ((TextBlock)secondButtonPressed.Child).Text)
                return true;
            else
                return false;
        }//end method isSecondSelected

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method highlights select building</para></summary> 
        public void HighlightPOITouch(object sender, TouchContactEventArgs e)
        {
            String collectionName = ((TextBlock)secondButtonPressed.Child).Text;
            if (collectionName.Equals("Ike's") || collectionName.Equals("Bullet Hole") || collectionName.Equals("The Commons"))
                canvas.HighlightBuildings("College Union Building", menu.getFrame());
            else if(collectionName.Equals("The Dive"))
                canvas.HighlightBuildings("Center of Athletics", menu.getFrame());
            else if ((((TextBlock)firstButtonPressed.Child).Text).Equals("Departments"))
            {
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

            area.Info1Screen.Children.Clear();
            area.Info2Screen.Children.Clear();
            area.Info3Screen.Children.Clear();
        }//end method HighlightBuildings

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method "scrolls" up through buttons in filter if there are too many to display at once
        ///                 Only needed for second level of expansion</para></summary> 
        private void ScrollUpTouch(object sender, TouchContactEventArgs e)
        {
            curSecondTopIndex--;
            secondTop.Children.Clear();
            secondBottom.Children.Clear();

            Grid grid;
            InteractiveBorder button;
            for (int i = 0; (i < 6) && (i + curSecondTopIndex < curSecondLabels.Count); i++)
            {
                if (i < 3)
                    grid = secondTop;
                else
                    grid = secondBottom;

                VerticalAlignment va;

                if (i % 3 == 0)
                    va = VerticalAlignment.Top;
                else if (i % 3 == 1)
                    va = VerticalAlignment.Center;
                else
                    va = VerticalAlignment.Bottom;

                button = makeButton(grid.Height / 3, grid.Width - buttonWidth * .9, HorizontalAlignment.Left, va, (String)curSecondLabels[i + curSecondTopIndex], 0);
                if (isSelected(button))
                {
                    ((TextBlock)button.Child).Background = menu.getFrame().getPrimaryColor();
                    ((TextBlock)button.Child).Foreground = new SolidColorBrush(Colors.White);
                }

                TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(ExpandTouch));
                TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(PopulateTouch));
                grid.Children.Add(button);
            }
            if (curSecondTopIndex > 0)
            {
                secondTop.Children.Add(ScrollUpButton);
            }
            secondBottom.Children.Add(ScrollDownButton);

        }//end method scrollUpTouch

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method "scrolls" down through buttons in filter if there are too many to display at once
        ///                 Only needed for second level of expansion</para></summary> 
        private void ScrollDownTouch(object sender, TouchContactEventArgs e)
        {
            curSecondTopIndex++;
            secondTop.Children.Clear();
            secondBottom.Children.Clear();

            Grid grid;
            InteractiveBorder button;
            for (int i = 0; (i < 6) && (i + curSecondTopIndex < curSecondLabels.Count); i++)
            {
                if (i < 3)
                    grid = secondTop;
                else
                    grid = secondBottom;

                VerticalAlignment va;

                if (i % 3 == 0)
                    va = VerticalAlignment.Top;
                else if (i % 3 == 1)
                    va = VerticalAlignment.Center;
                else
                    va = VerticalAlignment.Bottom;

                button = makeButton(grid.Height / 3, grid.Width - buttonWidth * .9, HorizontalAlignment.Left, va, (String)curSecondLabels[i + curSecondTopIndex], 0);
                if (isSelected(button))
                {
                    ((TextBlock)button.Child).Background = menu.getFrame().getPrimaryColor();
                    ((TextBlock)button.Child).Foreground = new SolidColorBrush(Colors.White);
                }

                TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(ExpandTouch));
                TouchInputManager.AddTouchContactDownHandler(button, new TouchContactEventHandler(PopulateTouch));
                grid.Children.Add(button);
            }
            if (curSecondTopIndex + 6 < curSecondLabels.Count)
            {
                secondBottom.Children.Add(ScrollDownButton);
            }

            secondTop.Children.Add(ScrollUpButton);
        }//end method scrollDownTouch

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method fully retracts the filter when a final option has been selected</para></summary> 
        private void closeFilter()
        {
            while (expandFactor != minExpand)
            {
                menu.getFrame().retractMenu();
            }
        }//end method closeFilter

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method fully retracts the filter when a final option has been selected</para></summary> 
        public void CloseFilterTouch(object sender, TouchContactEventArgs e)
        {
            closeFilter();
        }//end method CloseFilterTouch

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method expands the filter to next level
        ///      Called as a part of TrayMenu's expand method (which is called when expand button is touched)</para></summary> 
        public void expand()
        {
            expandFactor++;
            this.Width = menu.Width;
            if (((TextBlock)menu.getFrame().TrayArea.Info1Screen.getScroll().Content) != null)
                ((TextBlock)menu.getFrame().TrayArea.Info1Screen.getScroll().Content).Background = new SolidColorBrush(Colors.LightGray);

            //if this is first expansion, take away expand button and add retract button
            if (expandFactor == 1)
            {
                expansions = new Grid();
                expansions.Height = this.Height;
                expansions.HorizontalAlignment = HorizontalAlignment.Left;
                Children.Add(expansions);

                Children.Remove(expandButton);
                Children.Add(retractButton);
            }
            expansions.Width = this.Width - buttonWidth;
        }//end method expand

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method retracts the filter to previous level
        ///      Called as a part of TrayMenu's retract method (which is called when retract button is touched)</para></summary> 
        public void retract()
        {
            expandFactor--;

            //retracting if factor is now 0 would mean only button is left, add expand button back
            if (expandFactor == 0)
            {
                this.Width = this.initialWidth;
                Children.Clear();
                firstButtonPressed = null;
                secondButtonPressed = null;
                Children.Add(expandButton);
                expansions.Children.Clear();
                if (((TextBlock)menu.getFrame().TrayArea.Info1Screen.getScroll().Content) != null)
                    ((TextBlock)menu.getFrame().TrayArea.Info1Screen.getScroll().Content).Background = new SolidColorBrush(Colors.White);
            }
            else if (expandFactor == 1)
            {
                this.Width = menu.Width;
                expansions.Width = this.Width - buttonWidth;
                expansions.Children.Remove(secondTop);
                expansions.Children.Remove(secondBottom);
                firstButtonPressed = null;
                secondButtonPressed = null;
                replaceExpand(firstTop, firstBottom);
            }
            else
            {
                this.Width = menu.Width;
                expansions.Width = this.Width - buttonWidth;
                expansions.Children.Remove(thirdTop);
                expansions.Children.Remove(thirdBottom);
                secondButtonPressed = null;
                secondTop.HorizontalAlignment = HorizontalAlignment.Right;
                secondBottom.HorizontalAlignment = HorizontalAlignment.Right;
                replaceExpand(secondTop, secondBottom);
            }
        }//end method retract

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method returns int to represent expansion factor of filter
        ///         Factor of 0 means it is fully retracted 
        ///         Factor of 4 means it is expanded over the extire tray </para></summary> 
        public int getExpandFactor()
        {
            return expandFactor;
        }//end method getExpandFactor

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method returns the minimum expansion factor (for resizing menu)</para></summary> 
        public int getMinExpand()
        {
            return minExpand;
        }//end method getMinExpand

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method returns the maximum expansion factor (for resizing menu)</para></summary> 
        public int getMaxExpand()
        {
            return maxExpand;
        }//end method getMaxExpand

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method returns the total number of levels that can be expanded (for resizing menu)</para></summary> 
        public int getExpansionLevel()
        {
            return totalExpandLevels;
        }//end method getExpansionLevel

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method sets the TrayArea to the area of the frame that holds this object</para></summary> 
        public void setArea(TrayArea area)
        {
            this.area = area;
        }//end method setArea

        /// <summary><para>@author: Adam Hartman </para>
        ///<para>This method returns dept array list</para></summary> 
        public ArrayList Dept()
        {
            return depts;
        }//end method 

    }//end class TrayFilter
}
