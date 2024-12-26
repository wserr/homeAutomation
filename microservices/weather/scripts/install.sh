#!/usr/bin/bash

# This script  should be run as root
# Build and install weather service

./build-docker.sh

ln -s $HOME/repos/homeAutomation/microservices/weather/run-docker.sh /usr/bin/weather-service

cp weather.service /etc/systemd/system/
cp weather.timer /etc/systemd/system/

systemctl daemon-reload

systemctl enable weather.service
systemctl enable weather.timer

systemctl start weather.service
systemctl start weather.timer


