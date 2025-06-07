FROM ghcr.io/home-assistant/home-assistant:stable

COPY ./configuration/configuration-production.yaml /config/configuration.yaml
