#!/bin/bash

echo "Connecting to VPN..."

echo "c HOMESERVER" > /var/run/xl2tpd/l2tp-control

sleep 10

# To have all internet traffic routed through the VPN uncomment:
#ip route add default dev ppp0

# To only have a remote subnet routed through the VPN uncomment
# (this line assumes the remote subnet you want routed is 192.168.0.0/24 and the remote VPN end is 10.11.0.1:
ip route add 192.168.2.0/24 via <YOUR-IP-ADDRESS> dev ppp0 onlink
