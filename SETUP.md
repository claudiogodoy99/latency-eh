# Setup

Comandos para criar uma VM com um interface acelerada.

```

az network vnet create --resource-group [RGNAME] --name [VNET-NAME] --address-prefix 192.168.0.0/16 --subnet-name default --subnet-prefix 192.168.1.0/24

az network nsg create --resource-group [RGNAME] --name [NSG-NAME]

az network nsg rule create --resource-group [RGNAME] --nsg-name [NSG-NAME] --name Allow-SSH-Internet --access Allow --protocol Tcp --direction Inbound --priority 100 --source-address-prefix Internet --source-port-range "*" --destination-address-prefix "*" --destination-port-range 22


az network public-ip create --name [PUBLIC-IP-NAME] --resource-group Rg-B3-Godoy

az network nic create --resource-group Rg-B3-Godoy --name [INTERFACE-NAME] --vnet-name [VN-NAME] --subnet default --accelerated-networking true --public-ip-address [PUBLIC-IP-NAME] --network-security-group [NSG-NAME]

az vm create --resource-group [RGNAME] --name [VM-NAME] --image UbuntuLTS --size Standard_DS4_v2 --admin-username [ADM-NAME] --generate-ssh-keys --nics [INTERFACE-NAME] --admin-password [PASSWORD]
```

Instalar .NET:

```
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-6.0
```

Instalar GitHub:

```ssh
sudo apt-get install git-all
```
