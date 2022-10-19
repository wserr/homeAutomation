# VPN setup

## Intro

This is a manual to set up the Unify VPN server on an ubuntu machine. Note that this is quite difficult to set up, and I was not able to complete the set up on my VPN. This is why I still use a WireGuard VPN next to this one.

The advantages of this VPN however are that Ubiquity has quite a nice app (android/iOS/windows) to connect to the local network. That is why the main use of this VPN will be to connect to the home network from the outside.

## Installation on linux

See [this github thread](https://gist.github.com/nahall/6b23603ce9df2500a4053b280071d1ad)

- In Debian install the "xl2tpd" and "strongswan" packages.

- Edit /etc/ipsec.conf to add the connection:

    conn YOURVPNCONNECTIONNAME
         authby=secret
         pfs=no
         auto=start
         keyexchange=ikev1
         keyingtries=3
         dpddelay=15
         dpdtimeout=45
         dpdaction=clear
         rekey=no
         ikelifetime=3600
         keylife=3600
         type=transport
         left=%defaultroute
         leftprotoport=17/1701
    # Replace IP address with your VPN server's IP
         right=IPADDRESSOFVPNSERVER
         rightprotoport=17/%any
         ike=aes128-sha1-modp2048,aes256-sha1-modp4096,aes128-sha1-modp1536,aes256-sha1-modp2048,aes128-sha1-modp1024,aes256-sha1-modp1536,aes256-sha1-modp1024,3des-sha1-modp1024!
         esp=aes128-sha1-modp2048,aes256-sha1-modp4096,aes128-sha1-modp1536,aes256-sha1-modp2048,aes128-sha1-modp1024,aes256-sha1-modp1536,aes256-sha1-modp1024!


- Edit /etc/ipsec.secrets to add the secret key for this connection:

    IPADDRESSOFVPNSERVER : PSK "SECRETPRESHAREDKEY"
     

- Edit /etc/xl2tpd/xl2tpd.conf to add this connection:

    [lac YOURVPNCONNECTIONNAME]
    lns = IPADDRESSOFVPNSERVER
    ppp debug = yes
    pppoptfile = /etc/ppp/options.l2tpd.client-YOURVPNCONNECTIONNAME
    length bit = yes
    
    
- Create the file /etc/ppp/options.l2tpd.client-YOURVPNCONNECTIONNAME:

    ipcp-accept-local
    ipcp-accept-remote
    noccp
    refuse-eap
    refuse-chap
    noauth
    idle 1800
    mtu 1410
    mru 1410
    defaultroute

    # Uncomment if you want to use the DNS servers of the VPN host:
    #usepeerdns

    debug
    logfile /var/log/xl2tpd.log
    connect-delay 5000
    proxyarp
    name VPNUSERNAME
    password "VPNPASSWORD"
    
    
- Now to connect to the VPN create a script:

    #!/bin/bash

    echo "Connecting to VPN..."

    echo "c YOURVPNCONNECTIONNAME" > /var/run/xl2tpd/l2tp-control

    sleep 10

    # To have all internet traffic routed through the VPN uncomment:
    #ip route add default dev ppp0

    # To only have a remote subnet routed through the VPN uncomment
    # (this line assumes the remote subnet you want routed is 192.168.0.0/24 and the remote VPN end is 10.11.0.1:
    ip route add 192.168.0.0/24 via 10.11.0.1 dev ppp0 uplink

> Script above didn't work exacly. I had to add `onlink` at the end

```bash
/etc/init.d/ipsec restart
/etc/init.d/xl2tpd restart
```


- And to disconnect to the VPN create a script:

    #!/bin/bash

    ip route del default dev ppp0
    ip route del 192.168.0.0/24 dev ppp0

    echo "d YOURVPNCONNECTIONNAME" > /var/run/xl2tpd/l2tp-control
    service xl2tpd restart

