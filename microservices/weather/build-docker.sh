#!/usr/bin/env bash

docker build . -t weather-microservice:latest
docker tag weather-microservice:latest registry.willemserruys.com/weather-microservice:latest
docker push registry.willemserruys.com/weather-microservice:latest
