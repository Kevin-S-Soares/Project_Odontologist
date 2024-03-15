#!/bin/sh

echo "updating and upgrading"
sudo apt update -y && sudo apt upgrade -y

var="which sqlite3"
if [ $A != "/usr/bin/sqlite3" ]
then
    echo "installing sqlite3"
    sudo apt install sqlite3 -y
fi

var="which curl"
if [ $A != "/usr/bin/curl" ]
then
    echo "installing curl"
    sudo apt install curl -y
fi

var=`sudo which docker`
if [ $A != "/usr/bin/docker" ]
then
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

read -p '[SERVER][CONTAINER] where is the database located? ' var
echo "CONNECTION_STRING=\"Data Source=$var\"" > .env

read -p '[SERVER] are you using a Send Grid API Key (y/n)? ' var
if [ $var = "y" ]
then    
    read -p '[SERVER] please enter your API Key: ' var
    echo "SENDGRID_API_KEY=\"$var\"" >> .env
else
    echo '[SERVER] Email service is disabled. You must configure your own email service.'
    echo "SENDGRID_API_KEY=\"\"" >> .env
fi

read -p '[SERVER] what is the domain or client? ' var
echo "CLIENT=\"$var\"" >> .env

read -p '[CLIENT] what is the path to ssl certificate directory? ' var
echo "PATH_TO_SSL=\"$var\"" >> .env

read -p '[CLIENT] what is the path to ssl certificate key? ' var
echo "SERVER_KEY_PATH=\"$var\"" >> .env

read -p '[CLIENT] what is the path to ssl certificate? ' var
echo "SERVER_CERT_PATH=\"$var\"" >> .env

read -p '[SERVER] what is the RSA-SHA-4096 private key? ' var
echo "PRIVATE_KEY=\"$var\"" >> .env

read -p '[CLIENT] what is the RSA-SHA-4096 public key? ' var
echo "PUBLIC_KEY=\"$var\"" >> .env






