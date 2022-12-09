# EV-Car-UI
 Front end UI for an EV autocross car

 ## How to build (Windows)
- have .net6 and avalonia installed (see [docs](https://docs.avaloniaui.net/docs/getting-started))
- run the `publish.bat`
This will place 2 zips in the output folder next to the .sln, `windows.zip` and `rasberrypi.zip`. The windows zip is for local testing and the rasberrypi zip is for running the app on rasberry pi.

## How to change the publish.bat for different rasberry pi versions
For a 64 bit pi os: Change the runtime identifier (after the -r) to linux-arm64 (and change all folder references for the copying and the ziping)  
For a 32 bit pi os: Change the runtime identifier (after the -r) to linux-arm (and change all folder references for the copying and the ziping)

## How to run on rasberry pi
- Transfer `output/rasberrypi.zip` to the rasberry pi
- unzip it in a dir
- open that dir in terminal
- run the command `sh run.sh` or `bash run.sh`
- enjoy