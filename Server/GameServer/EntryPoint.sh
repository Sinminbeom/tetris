#!/bin/bash

IP=$(hostname -I | awk '{print $1}')

sed -i "s/127.0.0.1/${IP}/g" /App/config.json
sed -i "s/152.67.197.127/infra-node-mariadb-mariadb-1/g" /App/config.json

exec "$@"
