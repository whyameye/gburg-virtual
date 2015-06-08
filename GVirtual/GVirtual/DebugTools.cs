/*
 * @ Author: Fumbani Chibaka
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Collections;

namespace GVirtual
{
    static class DebugTools
    {

        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>Debbuging Use: Interations on Building Drop into tray
        ///</para></summary> 
        public static void Tray_Building_DropInteractions(int mode, Point point, Building building, TrayFrame leftTrayFrame, TrayFrame rightTrayFrame)
        {

            //MODE = 1 [ ACTIVATED ON TOUCH-UP ] ___________________
            // 1. check if the building is dropped on tray active area
            // 2. add the bulding into tray's bulding collection
            // 3. create GFX activity
           
          
            if (mode == 1)
            {
                if (leftTrayFrame.ActiveArea(point))
                {
                    leftTrayFrame.AddBuilding(building);
                    //Debugging Purpose ? : Activate POIDropArea (Changes color to red)
                    leftTrayFrame.TrayMenu.POIDropArea.Activate(building, point);
                    //Debugging Purpose ?: Change text in Info Screen
                    //leftTrayFrame.TrayArea.Info1Text = building.FullName;
                    List<MediaFile> files = leftTrayFrame.Canvas.getData().GetMediaList();
                    String file = "";
                    List<String> pics = new List<String>();
                    List<String> videos = new List<String>();

                    leftTrayFrame.TrayArea.Info1Screen.Children.Clear();
                    leftTrayFrame.TrayArea.Info2Screen.Children.Clear();
                    leftTrayFrame.TrayArea.Info3Screen.Children.Clear();


                    foreach (MediaFile f in files)
                    {
                        if (f.FileName.Equals(building.FullName + ".txt"))
                        {
                            file = building.FullName + ".txt";
                        }
                        else if (f.Tags.Contains(building.FullName.ToLower()) && f.Tags.Contains("pic"))
                        {
                            pics.Add(f.FileName);
                        }
                        else if (f.Tags.Contains(building.FullName.ToLower()) && f.Tags.Contains("video"))
                        {
                            videos.Add(f.FileName);
                        }
                    }
                    if (!file.Equals(""))
                    {
                        TextReader tr = new StreamReader(Directory.GetCurrentDirectory() + "\\text\\" + file);
                        String readIn = tr.ReadToEnd();
                        tr.Close();

                        leftTrayFrame.TrayArea.Info1Screen.setInfoText(readIn);
                    }
                    if(pics.Count != 0)
                        leftTrayFrame.TrayArea.Info2Screen.setInfoPics(pics);
                    if(videos.Count != 0)
                        leftTrayFrame.TrayArea.Info3Screen.setInfoVids(videos);

                        //TODO (Fumba): USE DATABASE TO TAKE IN PICTURES (return ArrayList)
                        //ArrayList pictures = new ArrayList();
                        //pictures.Add("dropImages/glat.jpg");
                        //pictures.Add("dropImages/fumbatest/anthro.jpeg");
                        //pictures.Add("dropImages/fumbatest/hass.jpg");

                    }
                   


                    if (rightTrayFrame.ActiveArea(point))
                    {
                        rightTrayFrame.AddBuilding(building);
                        rightTrayFrame.TrayMenu.POIDropArea.Activate(building, point);
                        //rightTrayFrame.TrayArea.Info1Text = building.FullName;
                        List<MediaFile> files = rightTrayFrame.Canvas.getData().GetMediaList();
                        String file = "";
                        List<String> pics = new List<String>();
                        List<String> videos = new List<String>();

                        rightTrayFrame.TrayArea.Info1Screen.Children.Clear();
                        rightTrayFrame.TrayArea.Info2Screen.Children.Clear();
                        rightTrayFrame.TrayArea.Info3Screen.Children.Clear();

                        foreach (MediaFile f in files)
                        {
                            if (f.FileName.Equals(building.FullName + ".txt"))
                            {
                                file = building.FullName + ".txt";
                            }
                            else if (f.Tags.Contains(building.FullName.ToLower()) && f.Tags.Contains("pic"))
                            {
                                pics.Add(f.FileName);
                            }
                            else if (f.Tags.Contains(building.FullName.ToLower()) && f.Tags.Contains("video"))
                            {
                                videos.Add(f.FileName);
                            }
                        }
                        if (!file.Equals(""))
                        {
                            TextReader tr = new StreamReader(Directory.GetCurrentDirectory() + "\\text\\" + file);
                            String readIn = tr.ReadToEnd();
                            tr.Close();

                            rightTrayFrame.TrayArea.Info1Screen.setInfoText(readIn);
                        }
                        if(pics.Count != 0)
                            rightTrayFrame.TrayArea.Info2Screen.setInfoPics(pics);
                        if(videos.Count != 0)
                            rightTrayFrame.TrayArea.Info3Screen.setInfoVids(videos);

                            //TODO (Fumba): USE DATABASE TO TAKE IN PICTURES (return ArrayList)
                            //ArrayList pictures = new ArrayList();
                            //pictures.Add("dropImages/glat.jpg");
                            //pictures.Add("dropImages/fumbatest/anthro.jpeg");
                            //pictures.Add("dropImages/fumbatest/hass.jpg");
                            //rightTrayFrame.TrayArea.Info2Screen.setInfoPictures(pictures);
                        }
            }// END MODE 1


            //MODE= 2 [ACTIVATED ON TOUCH-DOWN___________________
            // 1. RESET : stop GFX activity once new building is picked
            if (mode == 2)
            {
                /*
                //Deactivate POIDropArea red color
                leftTrayFrame.TrayMenu.POIDropArea.StopAnimations();
                rightTrayFrame.TrayMenu.POIDropArea.StopAnimations();
                //Rename text fields on Info Screens
                leftTrayFrame.TrayArea.Info1Text = "Written Information";
                rightTrayFrame.TrayArea.Info1Text = "Written information";
                 * */
            }
        }//end function DebugTrays



    }
}
