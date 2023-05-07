#!/usr/bin/env bash

docker run --env RUST_LOG=info --env-file ./.env registry.willemserruys.com/weather-microservice:latest &> logs.txt 
