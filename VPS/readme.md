# Server setup

## Installation docker

- See [Official Website](https://docs.docker.com/engine/install/ubuntu/)

## Installation docker compose

- See [This Website](https://nextgentips.com/2022/05/06/how-to-install-docker-compose-v2-on-ubuntu-22-04/)

## Setup VPN client

Follow [this tutorial](https://docs.pivpn.io/wireguard/)

NOTE: on ubuntu, you need to add a symbolic link in order to enable the VPN ([source](https://superuser.com/a/1544697))

```bash
ln -s /usr/bin/resolvectl /usr/local/bin/resolvconf
```

## To Do

- Start the VPN client automatically after reboot

