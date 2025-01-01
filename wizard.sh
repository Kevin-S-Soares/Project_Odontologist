#!/bin/sh

echo "updating and upgrading"
sudo apt update -y && sudo apt upgrade -y

var=$(which sqlite3)
if [[ $var != "/usr/bin/sqlite3" ]] ; then
    echo "installing sqlite3"
    sudo apt install sqlite3 -y
fi

var=$(which curl)
if [[ $var != "/usr/bin/curl" ]] ; then
    echo "installing curl"
    sudo apt install curl -y
fi

var=$(which openssl)
if [[ $var != "/usr/bin/openssl" ]] ; then
    echo "installing openssl"
    sudo apt install openssl -y
fi

var=$(sudo which docker)
if [[ $var != "/usr/bin/docker" ]] ; then
    echo "installing docker"
    sudo apt-get update -y
    sudo apt-get install ca-certificates curl gnupg -y
    sudo install -m 0755 -d /etc/apt/keyrings
    curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
    sudo chmod a+r /etc/apt/keyrings/docker.gpg

    echo \
    "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu \
    $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
    sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
    sudo apt-get update -y

    sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin -y
fi

echo "CONNECTION_STRING=\"Data Source=/tmp/data.db\"" > .env

read -p 'Please enter your API Key (press enter to skip): ' var
echo "SENDGRID_API_KEY=\"$var\"" >> .env

echo "SERVER=\"http://localhost:8080\"" >> .env

while [ 1 ] ; do
    read -p 'Is there a domain (y/n): ' isDomain
    if [ "$isDomain" == "y" ] || [ "$isDomain" == "n" ] ; then
        break
    fi
done

if [[ "$isDomain" == "y" ]]
then
    read -p 'what is the domain: ' domain
    echo "CLIENT=\"$domain\"" >> .env
    var="which certbot"
    if [[ "$var" != "/usr/bin/certbot" ]] ; then
        echo "installing /usr/bin/certbot"
        sudo snap install --classic certbot
    fi
    sudo certbot certonly --standalone -d $domain
    mkdir ssl
    key_path=$(sudo certbot certificates | grep 'privkey' | cut -b 23-)
    cert_path=$(sudo certbot certificates | grep 'fullchain' | cut -b 23-)
    sudo cp $key_path ./ssl
    sudo cp $cert_path ./ssl
    current_path=$(pwd)
    echo "PATH_TO_SSL=\"$current_path/ssl\"" >> .env
    echo "SERVER_KEY_PATH=\"/app/ssl/privkey.pem\"" >> .env
    echo "SERVER_CERT_PATH=\"/app/ssl/fullchain.pem\"" >> .env
else
    echo "CLIENT=\"http://localhost:443\"" >> .env
    echo "PATH_TO_SSL=\"\"" >> .env
    echo "SERVER_KEY_PATH=\"\"" >> .env
    echo "SERVER_CERT_PATH=\"\"" >> .env
fi

openssl genrsa -out private.pem 4096
openssl rsa -in private.pem -pubout -out public.pem

public_key=$(cat public.pem)
echo "PUBLIC_KEY=\"$public_key\"" >> .env

private_key=$(cat private.pem)
echo "PRIVATE_KEY=\"$private_key\"" >> .env

sudo docker volume create database
sudo bash migration.sh
