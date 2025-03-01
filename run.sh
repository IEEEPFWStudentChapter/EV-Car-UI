#!/bin/bash
#/home/user/Desktop/Car\ UI/EV\ Car\ UI
pi_ip='192.168.12.245'
piname='carTest'
projname='EVCarUI'  #'CanTest'

#ccd 'CanTest'
dotnet build $projname -r linux-arm64 --self-contained
if [ $? -ne 0 ]; then
    echo 'build failed'
    exit
fi
cd $projname

ping -c 1 $pi_ip
if [ $? -ne 0 ]; then
    echo 'cannot connect to pi'
    exit
fi


echo 'pi says hi'
ssh ${piname}\@$pi_ip <<EOF
    mkdir -p Desktop/ui;
    rm -r Desktop/ui/*
EOF

scp -r bin/Debug/net6.0/  ${piname}\@$pi_ip:Desktop/ui
ssh -X ${piname}\@$pi_ip <<EOF
    pkill 'EVCarUI'
    ./Desktop/runUI.sh
EOF
