FROM ghcr.io/home-assistant/home-assistant:stable

COPY ./configurations/home-assistant/configuration-production.yaml /config/configuration.yaml
