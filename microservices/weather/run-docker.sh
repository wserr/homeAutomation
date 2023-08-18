#!/usr/bin/env bash
file=/home/willem/repos/homeAutomation/microservices/weather/logs.$(date "+%F").txt

docker run --env RUST_LOG=info --env-file /home/willem/repos/homeAutomation/microservices/weather/.env registry.willemserruys.com/weather-microservice:latest &>> $file
