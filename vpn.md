# VPN setup

## OS

I used a raspberry pi with Raspberry PI OS lite on it.

>Best to use Raspberry Pi Imager to flash image onto SD card. This flasher allows you to enable SSH and configure WLAN during flashing

After that, I followed [this tutorial](https://pimylifeup.com/raspberry-pi-wireguard/)

Notes on the tutorial:

- Working with a no-ip hostname did not work. Probably because I did not wait long enough after configuring the hostname? For now, I have configured an IP address, but this is not ideal, as the IP address can change.
- After configuring the pivpn, best to run `pivpn -d` to fix an iptables issue (see [faq](https://docs.pivpn.io/faq/))

## Install docker & docker compose

See [this](https://www.jfrog.com/connect/post/install-docker-compose-on-raspberry-pi/)