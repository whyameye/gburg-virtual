/*
 * InfoScreen class 
 * extends Grid class
 * 
 * Author(s): 
 * Amanda Gower
 * Mike Shoolbraid
 * 
 * Gets added to instance of TrayArea class (which is then ultimately added to TrayFrame)
 * Aligned to left side of TrayArea (TrayTab instances go on right side)
 * 
 * 
 * Intended to eventually be extended for other classes for specific information screens
 * ex. WrittenInfo, MediaInfo, etc. 
 * 
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using libSMARTMultiTouch.Input;
using libSMARTMultiTouch.Controls;

using System.Windows;
using System.IO;

namespace GVirtual
{
    class InfoScreen : Grid
    {
        //Mainly for testing at this stage - to designate between info screens
        private TextBlock text;
        private ScrollViewer scroll;
        private bool scrollTop;
        private bool scrollBottom;
        private InteractiveBorder InfoUp;
        private InteractiveBorder InfoDown;
        private Grid info1Grid;

        private GVirtualCanvas canvas; // for debugging
        private TrayFrame frame;

        private List<String> pics;      //list of file names for pictures
        private List<String> videos;    //list of file names for videos
        private TouchImageButton view;  //to display current image in pic/video area

        private int numItems;  //the number of thumbnails
        private int numRows;  //the number of rows of thumbnails that exist
        private int firstItem;  //index of the first viewable item
        private int firstRow = 1;  //index of the first viewable row
        private BitmapImage currentMain;
        private MediaElement currentVideo;
        private Boolean isOnPicTab;
        //private BitmapImage currentMain = new BitmapImage(new Uri(@"1.bmp", UriKind.RelativeOrAbsolute)); //currently selected picture
        //mpeg-4 variable

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>Constructor</para></summary> 
        ///<param type ="double"> Height and width for screen 
        ///                      (recommended: same height as TrayArea height and width as porportion of TrayArea width </param>
        public InfoScreen(double height, double width, SolidColorBrush backgroundColor, GVirtualCanvas canvas, TrayFrame frame)
        {
            this.canvas = canvas;
            this.Height = height;
            this.Width = width;
            this.Background = backgroundColor;
            this.frame = frame;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            scroll = new ScrollViewer();
        }//end constructor

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function adds text for testing and debugging.</para></summary> 
        ///<param type ="String"> Text to print on screen </param>
        public void setText(String str)
        {
            text = new TextBlock();
            text.Height = Height / 6;
            text.Width = Width / 2;
            text.FontSize = 24;
            text.Background = new SolidColorBrush(Colors.White);
            text.Foreground = this.Background;
            text.TextAlignment = System.Windows.TextAlignment.Center;
            text.Text = str;
            Children.Clear();
            Children.Add(text);
        }//end method setText

        //author: Mike Shoolbraid
        //Sets up the list of thumbnails of videos/pictures (count should initially be set to 1 when this method is called)
        public void setPictureMatrix(int numItems, int count)
        {
            //thumbnail matrix (from a list of sequentially ordered pictures (1.bmp, 2.bmp, etc.))
            //figure out how many rows need to exist
            isOnPicTab = true;
            int numRows;
            int sbSize = (int)Width / 20;  //size of scrollbar

            if (numItems > 3)
            {
                if (numItems % 4 == 0)
                {
                    numRows = numItems / 4;
                }
                else
                {
                    numRows = (numItems / 4) + 1;
                }
            }
            else
            {
                numRows = 1;
            }

            this.numRows = numRows;
            this.numItems = numItems;

            for (int j = 0; j < 2; j++)  //for two rows of thumbnails
            {
                for (int i = 0; i < 4; i++)  //for four thumbnails per row
                {
                    if (count > numItems)
                    {
                        break;
                    }


                    BitmapImage bi = new BitmapImage();

                    //scale the image so that it is as wide as the TouchImageButton and as high as the aspect ratio of the picture allows
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"images/" + pics.ElementAt(count - 1), UriKind.RelativeOrAbsolute);
                    bi.DecodePixelWidth = (int)(Width / 6) - (sbSize / 4);
                    bi.EndInit();

                    TouchImageButton tib = new TouchImageButton(bi);
                    tib.Height = Height / 2;
                    tib.Width = (Width / 6) - (sbSize / 4);
                    tib.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    tib.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    TouchInputManager.AddTouchContactDownHandler(tib, new TouchContactEventHandler(chooseThumbnail));

                    TransformGroup tgPic = new TransformGroup();
                    tgPic.Children.Add(new TranslateTransform((Width / 3) + (i * ((Width / 6) - (sbSize / 4))), (Height / 2) * j));
                    tib.RenderTransform = tgPic;

                    Children.Add(tib);
                    count++;
                }
            }

            this.firstItem = (this.firstRow * 4) - 3;
            setMainArea(this.currentMain);
            setScrollbar();
        }

        public void setVideoMatrix(int numItems, int count)
        {
            //thumbnail matrix (from a list of sequentially ordered pictures (1.bmp, 2.bmp, etc.))
            //figure out how many rows need to exist
            isOnPicTab = false;
            int numRows;
            int sbSize = (int)Width / 20;  //size of scrollbar

            if (numItems > 2)
            {
                if (numItems % 3 == 0)
                {
                    numRows = numItems / 3;
                }
                else
                {
                    numRows = (numItems / 3) + 1;
                }
            }
            else
            {
                numRows = 1;
            }

            this.numRows = numRows;
            this.numItems = numItems;

            for (int j = 0; j < 2; j++)  //for two rows of thumbnails
            {
                for (int i = 0; i < 3; i++)  //for three thumbnails per row
                {
                    if (count > numItems)
                    {
                        break;
                    }

                    TextBlock tb = new TextBlock();
                    MediaElement temp = new MediaElement();
                    temp.Source = new Uri(@"videos/" + videos.ElementAt(count - 1), UriKind.RelativeOrAbsolute);

                    String s = temp.Source.ToString();
                    int subLength = (s.IndexOf('.')) - 7;
                    String ss = s.Substring(7, subLength);
                    tb.Text = ss;
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.Background = new SolidColorBrush(Colors.White);

                    if (count == 1)
                    {
                        tb.Background = new SolidColorBrush(Colors.LightGray);
                    }

                    tb.TextAlignment = TextAlignment.Center;
                    tb.Height = Height / 2;
                    tb.Width = (Width / 6) - (sbSize / 4);
                    tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    tb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    tb.Name = "id" + count.ToString();
                    TouchInputManager.AddTouchContactDownHandler(tb, new TouchContactEventHandler(chooseVideoThumbnail));

                    TransformGroup tgVid = new TransformGroup();
                    tgVid.Children.Add(new TranslateTransform((Width / 2) + (i * ((Width / 6) - (sbSize / 3))), (Height / 2) * j));
                    tb.RenderTransform = tgVid;


                    //RenderTargetBitmap rtb = new RenderTargetBitmap((int)Height / 2, (int)(Width / 8) - (sbSize / 4), 96, 96, PixelFormats.Pbgra32);
                    //MediaElement temp = new MediaElement();
                    //temp.Source = new Uri(@"videos/" + videos.ElementAt(count - 1), UriKind.RelativeOrAbsolute);
                    //rtb.Render(temp);
                    //BitmapSource bs = rtb;
                    //BitmapImage bi = new BitmapImage();

                    //scale the image so that it is as wide as the TouchImageButton and as high as the aspect ratio of the picture allows
                    //bi.BeginInit();
                    //bi.UriSource = new Uri(@"" + count + ".bmp", UriKind.RelativeOrAbsolute);
                    //bi.DecodePixelWidth = (int)(Width / 8) - (sbSize / 4);
                    //bi.EndInit();

                    //TouchImageButton tib = new TouchImageButton(bi);
                    //tib.Height = Height / 2;
                    //tib.Width = (Width / 8) - (sbSize / 4);
                    //tib.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    //tib.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    //tib.Name = "id" + count.ToString();
                    //TouchInputManager.AddTouchContactDownHandler(tib, new TouchContactEventHandler(chooseVideoThumbnail));

                    //TransformGroup tgPic = new TransformGroup();
                    //tgPic.Children.Add(new TranslateTransform((Width / 2) + (i * ((Width / 8) - (sbSize / 4))), (Height / 2) * j));
                    //tib.RenderTransform = tgPic;

                    //Children.Add(tib);
                    Children.Add(tb);
                    count++;
                }
            }

            this.firstItem = (this.firstRow * 3) - 2;
            setMainArea(this.currentVideo);
            setScrollbar();
        }

        //author: Mike Shoolbraid
        //Sets the main picture area (is set as the first item in the list, then to whichever one is clicked on)
        public void setMainArea(BitmapImage img)
        {
            //scale the image
            BitmapImage temp = new BitmapImage();
            temp.BeginInit();
            temp.DecodePixelWidth = (int)Width / 3;
            temp.UriSource = img.UriSource;
            temp.EndInit();

            Children.Remove(view);
            view = new TouchImageButton(temp);

            view.Height = Height;
            view.Width = Width / 3;
            view.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            view.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            Children.Add(view);
        }

        //author: Mike Shoolbraid
        //Sets the main video area (is set as the first item in the list, then to whichever one is clicked on)
        public void setMainArea(MediaElement video)
        {
            //scale the video
            //MediaPlayer temp = new MediaPlayer();

            //MediaElement view = new MediaElement();
            Children.Remove(view);
            Children.Remove(currentVideo);
            this.currentVideo = video;
            currentVideo.LoadedBehavior = MediaState.Pause;
            currentVideo.Height = Height;
            currentVideo.Width = Width / 2;
            currentVideo.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            currentVideo.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            TouchInputManager.AddTouchContactDownHandler(currentVideo, new TouchContactEventHandler(playPause));
            Children.Add(currentVideo);
            
            
        }

        //author: Mike Shoolbraid
        //Sets the scrollbar; two boxes for up/down buttons and a bar in between
        public void setScrollbar()
        {
            //initialize the up button
            TextBlock top = new TextBlock();
            top.Height = Height / 8;
            top.Width = Width / 20;

            //determines color (enabled/disabled)
            if (firstRow == 1)
            {
                top.Background = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                top.Background = new SolidColorBrush(Colors.White);
            }

            top.Foreground = this.Background;
            top.Text = "^";
            top.TextAlignment = System.Windows.TextAlignment.Center;
            top.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            top.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            TouchInputManager.AddTouchContactDownHandler(top, new TouchContactEventHandler(scrollUp));
            Children.Add(top);

            //initialize the down button
            TextBlock bot = new TextBlock();
            bot.Height = Height / 8;
            bot.Width = Width / 20;

            //determines color (enabled/disabled)
            if (firstRow == numRows - 1 || numRows < 3)
            {
                bot.Background = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                bot.Background = new SolidColorBrush(Colors.White);
            }

            bot.Foreground = this.Background;
            bot.Text = "v";
            bot.TextAlignment = System.Windows.TextAlignment.Center;
            bot.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            bot.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            TouchInputManager.AddTouchContactDownHandler(bot, new TouchContactEventHandler(scrollDown));
            Children.Add(bot);

            //initialize the middle bar
            TextBlock bar = new TextBlock();

            bar.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            bar.Width = Width / 20;
            bar.Height = Height - (Height / 4);

            //determine size and position of bar when there are more than 2 rows
            if (numRows > 2)
            {
                bar.Background = new SolidColorBrush(Colors.White);
                bar.Height = bar.Height / (numRows - 1);
                bar.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                //set the position of the bar depending on which rows are currently being viewed
                TransformGroup tgBar = new TransformGroup();
                tgBar.Children.Add(new TranslateTransform(0, (Height / 8) + (bar.Height * (firstRow - 1))));
                bar.RenderTransform = tgBar;
            }
            else
            {
                bar.Background = new SolidColorBrush(Colors.LightGray);
            }

            Children.Add(bar);
        }

        //author: Mike Shoolbraid
        //Action taken when a thumbnail is chosen
        private void chooseThumbnail(object sender, TouchContactEventArgs e)
        {
            TouchImageButton t = sender as TouchImageButton;
            BitmapImage b = t.UpImage;
            this.currentMain = b;
            setMainArea(currentMain);
        }

        //author: Mike Shoolbraid
        //Action taken when a thumbnail is chosen
        private void playPause(object sender, TouchContactEventArgs e)
        {
            if (currentVideo.LoadedBehavior == MediaState.Play)
            {
                currentVideo.LoadedBehavior = MediaState.Pause;
            }
            else
            {
                currentVideo.LoadedBehavior = MediaState.Play;
            }
        }

        private void chooseVideoThumbnail(object sender, TouchContactEventArgs e)
        {
            currentVideo.LoadedBehavior = MediaState.Stop;
            TextBlock t = sender as TextBlock;
            t.Background = new SolidColorBrush(Colors.LightGray);
            String id = t.Name;
            int index = 0;
            for (int i = 1; i <= videos.Count; i++)
            {
                if (id.Equals("id" + i))
                    index = i - 1;
            }

            MediaElement vid = new MediaElement();
            vid.Source = new Uri(@"videos/" + videos.ElementAt(index), UriKind.RelativeOrAbsolute);
            TouchInputManager.AddTouchContactDownHandler(vid, new TouchContactEventHandler(playPause));
            vid.LoadedBehavior = MediaState.Manual;
            this.currentVideo = vid;
            setMainArea(vid);
        }

        //author: Mike Shoolbraid
        //Action taken when the user scrolls up
        private void scrollUp(object sender, TouchContactEventArgs e)
        {
            TextBlock t = sender as TextBlock;

            if (firstRow != 1)
            {
                if (isOnPicTab)
                {
                    firstItem = firstItem - 4;
                }
                else
                {
                    firstItem = firstItem - 3;
                }
                firstRow--;
                Children.Clear();

                if (isOnPicTab)
                {
                    setPictureMatrix(numItems, firstItem);
                }
                else
                {
                    setVideoMatrix(numItems, firstItem);
                }
            }
        }

        //author: Mike Shoolbraid
        //Action taken when the user scrolls down
        private void scrollDown(object sender, TouchContactEventArgs e)
        {
            TextBlock t = sender as TextBlock;
            
            if (firstRow != numRows - 1 && numRows > 2)
            {
                if (isOnPicTab)
                {
                    firstItem = firstItem + 4;
                }
                else
                {
                    firstItem = firstItem + 3;
                }
                firstRow++;
                Children.Clear();

                if (isOnPicTab)
                {
                    setPictureMatrix(numItems, firstItem);
                }
                else
                {
                    setVideoMatrix(numItems, firstItem);
                }
            }
        }

        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function adds text for testing and debugging.</para></summary> 
        ///<param type ="String"> Text to prtorint on screen </param>
        public void setInfoText(String str)
        {
            //canvas.Trace("SETUP INFO TEXT IN INFOSCREEN");

            info1Grid = new Grid();
            info1Grid.Height = .95 * Height;
            info1Grid.Width = .95 * Width;

            scroll = new ScrollViewer();
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scroll.Height = info1Grid.Height;
            scroll.Width = .94 * info1Grid.Width;
            scroll.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            text = new TextBlock();
            text.Padding = new System.Windows.Thickness(15);
            text.FontSize = 10;
            text.Background = new SolidColorBrush(Colors.White);
            text.Foreground = this.Background;
            text.TextAlignment = System.Windows.TextAlignment.Left;
            text.TextWrapping = System.Windows.TextWrapping.Wrap;
            text.Text = str;
            scroll.Content = text;
            info1Grid.Children.Add(scroll);

            //buttons to allow touch to work
            InfoUp = new InteractiveBorder();
            InfoUp.Width = info1Grid.Width * .05;
            InfoUp.Height = InfoUp.Width / 2;
            InfoUp.Background = new SolidColorBrush(Colors.LightGray);
            InfoUp.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            InfoUp.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            TextBlock tb = new TextBlock();
            tb.Background = new SolidColorBrush(Colors.Transparent);
            tb.Foreground = frame.getPrimaryColor();
            tb.Text = "^";
            tb.Height = InfoUp.Height;
            tb.Width = InfoUp.Width;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.TextAlignment = TextAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.FontSize = 10;
            InfoUp.Child = tb;
            info1Grid.Children.Add(InfoUp);
            scrollTop = true;

            InfoDown = new InteractiveBorder();
            InfoDown.Width = info1Grid.Width * .05;
            InfoDown.Height = InfoDown.Width / 2;
            InfoDown.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            InfoDown.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;

            TextBlock tb2 = new TextBlock();
            tb2.Background = new SolidColorBrush(Colors.Transparent);
            tb2.Foreground = frame.getPrimaryColor();
            tb2.Text = "v";
            tb2.Height = InfoDown.Height;
            tb2.Width = InfoDown.Width;
            tb2.HorizontalAlignment = HorizontalAlignment.Center;
            tb2.TextAlignment = TextAlignment.Center;
            tb2.VerticalAlignment = VerticalAlignment.Center;
            tb2.FontSize = 8;
            InfoDown.Child = tb2;
            info1Grid.Children.Add(InfoDown);

            TouchInputManager.AddTouchContactDownHandler(InfoDown, new TouchContactEventHandler(ScrollDown));
            InfoDown.Background = new SolidColorBrush(Colors.White);
            scrollBottom = false;

            Children.Clear();
            Children.Add(info1Grid);

        }//end method setInfoText


        /// <summary><para>@author: Amanda Gower</para>
        ///<para>This function sets picture ArrayList and calls the code to setup area</para></summary> 
        ///<param type ="List<String>"> List of file names </param>
        public void setInfoPics(List<String> pics)
        {
            this.pics = pics;
            this.currentMain = new BitmapImage(new Uri(@"images/" + pics.ElementAt(0), UriKind.RelativeOrAbsolute));
            setPictureMatrix(pics.Count, 1);
        }//end method setInfoPics

        /// <summary><para>@author: Amanda Gower/ Fumba Chibaka</para>
        ///<para>This function sets video ArrayList and calls the code to setup area</para></summary> 
        ///<param type ="List<String>"> List of file names </param>
        public void setInfoVids(List<String> videos)
        {
            this.videos = videos;
            this.currentVideo = new MediaElement();
            currentVideo.Source = new Uri(@"videos/" + videos.ElementAt(0), UriKind.RelativeOrAbsolute);
            setVideoMatrix(videos.Count, 1);
            //showVideos();
        }//end method setInfoVids



        /// <summary><para>@author: Fumbani Chibaka</para>
        ///<para>This function adds pictures associated with the selected buildings </para></summary> 
        ///<param type ="String"> Text to print on screen </param>
        /*public void showVideos()
        {
            Video videoElement = new Video("videos/test.WMV");
            DraggableVideo video = new DraggableVideo(10, 10, 0, videoElement, false, 0, 0);

            Children.Add(video);
            videoElement.BufferVideo();

            //Activate Click on video
            TouchInputManager.AddTouchContactDownHandler(video, new TouchContactEventHandler(playVideo));

       
        }//end method setInfoPictures
         */


        /// <summary><para>@author: Fumbani CHibaka </para>
        ///<para>This funtion plays the video</para></summary> 
        private void playVideo(object sender, TouchContactEventArgs e)
        {
            DraggableVideo border = (DraggableVideo)sender;

            MediaElement video = (MediaElement)border.GetMediaElement;
            border.IsTouchBounceEnabled = true;

            video.Play();
        }




        /// <summary><para>@author: Amanda Gower </para>
        ///<para>Gets the text on the info screen </para></summary> 
        public TextBlock Text
        {
            get { return text; }
        }

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method scrolls down for info screen</para></summary> 
        private void ScrollUp(object sender, TouchContactEventArgs e)
        {
            scroll.PageUp();
            if (scrollBottom)
            {
                scrollBottom = false;
                TouchInputManager.AddTouchContactDownHandler(InfoDown, new TouchContactEventHandler(ScrollDown));
                InfoDown.Background = new SolidColorBrush(Colors.White);
            }
            if (scroll.VerticalOffset == 0)
            {
                scrollTop = true;
                TouchInputManager.RemoveTouchContactDownHandler(InfoUp, new TouchContactEventHandler(ScrollUp));
                InfoUp.Background = new SolidColorBrush(Colors.LightGray);
            }
        }//end method ScrollUp

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method scrolls down for info screen</para></summary> 
        private void ScrollDown(object sender, TouchContactEventArgs e)
        {
            scroll.PageDown();
            if (scrollTop)
            {
                scrollTop = false;
                TouchInputManager.AddTouchContactDownHandler(InfoUp, new TouchContactEventHandler(ScrollUp));
                InfoUp.Background = new SolidColorBrush(Colors.White);
            }
            if (scroll.VerticalOffset == scroll.ScrollableHeight)
            {
                scrollBottom = true;
                TouchInputManager.RemoveTouchContactDownHandler(InfoDown, new TouchContactEventHandler(ScrollDown));
                InfoDown.Background = new SolidColorBrush(Colors.LightGray);
            }
        }//end method ScrollDown

        /// <summary><para>@author: Amanda Gower </para>
        ///<para>This method gets the text scroll area from this object</para></summary> 
        public ScrollViewer getScroll()
        {
            return scroll;
        }//end method getScroll




    }//end class InfoScreen
}
