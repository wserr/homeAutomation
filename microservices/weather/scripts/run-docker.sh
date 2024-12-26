#!/usr/bin/env bash
# Make sure the weather-service-image exists locally (e.g. by running the build-docker script)
docker run --env RUST_LOG=info --env-file /home/pi/repos/homeAutomation/microservices/weather/environment-variables/.env.production weather-service:latest
