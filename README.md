
# EV Car UI
 Front end UI for an EV autocross car
![](https://github.com/Hussain-Aziz/EV-Car-UI/blob/main/EV%20Car%20UI/Assets/UIExample.gif)

 ## How to Build
- have .net6 and avalonia installed (see [avalonia docs](https://docs.avaloniaui.net/docs/getting-started))
- run the `publish.bat`
This will create 4 zips in the output folder,
- `windows.zip`: for running on windows
- `linux.zip`: for running on a VM
- `rasberrypi-64bit.zip`: for running on rasberry pi with 64 bit OS installed
- `rasberrypi-32bit.zip`: for running on rasberry pi with 32 bit OS installed
### Alternate instructions for Linux:
- Have .net6 and run `dotnet build -r linux-x64`  in the `EV Car UI` folder. The `bin/Debug/net6.0/linux-x64` folder should contain the executable called `EV Car UI`.
## How to run on Raspberry Pi
- Transfer the correct .zip from the output directory to the rasberry pi or go to github and download the latest artifact.
- unzip the transfered file to a directory and open that directory in a terminal window
- run the UI by typing `./'EV Car UI'`
Note: if you transfer the zip from the output folder, you will need to change the mode manually using `chmod +x 'EV Car UI'`. If you get the zip from github artifacts, it should already be an executable.

## Automatically send to Raspberry Pi
- Set up SSH and a static IP address for the Pi. The IP should be static on a network both your computer and the PI can connect to.
- (Optional) Set up an RSA key to avoid needing to input SSH password.
- Configure `run.sh`. Set the Pi's IP and SSH hostname.
- run `run.sh` in a linux environment while on the same network as the Pi. If you do not have an RSA key set up, it should prompt for a password 3 times. Once done, the pi will run the UI, which will be displayed on your computer.

## How to setup app to run on startup
The [original source](https://learn.sparkfun.com/tutorials/how-to-run-a-raspberry-pi-program-on-startup/all)
- Create a folder on desktop named `Car UI` and place the `EV Car UI` execuatable, `run.sh`, `libSkiaSharp.so`, and `libHarfBuzzSharp.so` in that folder.
- Make sure these are set up correctly
  - autologin is enabled, do that by going to preferences -> Rasberry Pi Configuration -> System.
  - `run.sh` and `EV Car UI` is executable. This should already be done if downloading from github.
- Create the directory where autostart looks for .desktop scripts using `mkdir ~/.config/autostart`
- Create a .desktop file in that directory `nano ~/.config/autostart/EVCarUI.desktop`
- Add the following lines to that file
```
[Desktop Entry]
Type=Application
Name=EVCarUI
Exec=/bin/sh /home/user/Desktop/Car\ UI/run.sh
```
- Save the file, reboot and test if it works.

> Note: The backslash in `Car\ UI` and `EV\ Car\ UI` is used to escape the space. It is done to preserve the literal value of the next character (the space).

> Note: Replace the `/home/user/Desktop/Car\ UI/` with the path of the app and replace the `/home/user/Desktop/Car\ UI/run.sh` with the correct path of the shell script.
