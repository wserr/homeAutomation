#!/usr/bin/env bash
file=logs.$(date "+%F").txt

docker run --env RUST_LOG=info --env-file ./.env registry.willemserruys.com/weather-microservice:latest &> $file
