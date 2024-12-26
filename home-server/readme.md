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

## Start docker-compose

TODO add .sh script to start in production

## Tips

When database credentials are not updated after docker restart, consider using

```bash
docker compose rm db
```


## To Do

- Start the VPN client automatically after reboot

## Generate self signed certificates

https://stackoverflow.com/questions/10175812/how-to-generate-a-self-signed-ssl-certificate-using-openssl

> non-interactive and 10 years expiration

```bash
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -sha256 -days 3650 -nodes -subj "/C=homeassistant/ST=Belgium/L=Deerlijk/O=Willem/OU=Willem/CN=willem" -subj '/CN=homeassistant.com'
```
