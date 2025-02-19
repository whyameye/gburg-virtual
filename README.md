# This is probably useless but I've leaving here for posterity
Since the SDK for Smart Tech Multitouch Table 442i is no longer available I hacked this up to figure out how the SDK worked

If anybody ever has to reverse engineer the SDK again (doubtful) well here's this which might help (also doubtful).

[Final project](https://github.com/whyameye/TouchOSC)

## some notes (obviously but I wanted a header here so...)
### class "Building (Building.cs)" is what captures the touches
  - red box defines the touch area
  - constructor has this.opacity to set opacity

### set opacity of touches in GVirtualWindow.xaml.cs:
  - instance of TableControl has property Opacity
  - this also affects opacity of red box

### set opacity of primary window in GVirtualwindow.xaml
  - Background 1st 2 hex digits = opacity. Lower is more transparent
  - opacity of 00 creates click-through so use 01 as lowest setting

### remove/add cursor in "GVirtualWindow.xaml.cs"
  - this.Cursor = Cursors.None (comment or uncomment)
