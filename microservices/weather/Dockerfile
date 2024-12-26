FROM rust:1.71 as builder
WORKDIR /usr/src/weather
COPY . .
RUN cargo install --path .

FROM debian:bullseye-slim as runtime-base
RUN apt-get update && apt-get install -y libssl1.1 ca-certificates libsasl2-dev && rm -rf /var/lib/apt/lists/*
COPY --from=builder /usr/local/cargo/bin/weather /usr/local/bin/weather
CMD ["weather"]
