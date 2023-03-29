# EV Car UI
 Front end UI for an EV autocross car  
![](https://github.com/Hussain-Aziz/EV-Car-UI/blob/main/EV%20Car%20UI/Assets/UIExample.gif)  

 ## How to build (Windows)
- have .net6 and avalonia installed (see [avalonia docs](https://docs.avaloniaui.net/docs/getting-started))
- run the `publish.bat`  
This will create 4 zips in the output folder, 
- `windows.zip`: for running on windows
- `linux.zip`: for running on a VM
- `rasberrypi-64bit.zip`: for running on rasberry pi with 64 bit OS installed
- `rasberrypi-32bit.zip`: for running on rasberry pi with 32 bit OS installed

## How to run on rasberry pi
- Transfer the correct .zip from the output directory to the rasberry pi or go to github and download the latest artifact.
- unzip the transfered file to a directory and open that directory in a terminal window
- run the UI by typing `./'EV Car UI'`
Note: if you transfer the zip from the output folder, you will need to change the mode manually using `chmod +x 'EV Car UI'`. If you get the zip from github artifacts, it should already be an executable.