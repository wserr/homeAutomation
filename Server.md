# Server setup

## Installation docker

- See [Official Website](https://docs.docker.com/engine/install/ubuntu/)

## Setup VPN client

Follow [this tutorial](https://docs.pivpn.io/wireguard/)

NOTE: on ubuntu, you need to add a symbolic link in order to enable the VPN ([source](https://superuser.com/a/1544697))

```bash
ln -s /usr/bin/resolvectl /usr/local/bin/resolvconf
```

## Setup Home Assistant

```bash
sudo docker run -d \
  --name homeassistant \
  --privileged \
  --restart=unless-stopped \
  -e TZ=Europe/Brussels \
  -v /home/willem/homeassistant:/config \
  --network=host \
  ghcr.io/home-assistant/home-assistant:stable
```

Now, home assistant can be accessed at http://<host>:8123

## To Do

- Now, you can only connect to the home assistant server when you are connected to the VPN - is this OK or should this be changed?
- Start the VPN client automatically after reboot

