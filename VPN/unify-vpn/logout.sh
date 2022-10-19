#!/bin/bash

ip route del default dev ppp0
ip route del 192.168.2.0/24 dev ppp0

echo "d HOMESERVER" > /var/run/xl2tpd/l2tp-control
service xl2tpd restart
