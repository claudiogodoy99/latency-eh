# Setup

Comandos para criar uma VM com um interface acelerada.

```
az network public-ip create --name pip-accelerated-1 --resource-group Rg-B3-Godoy

az network nic create --resource-group Rg-B3-Godoy --name nic-accelerated-1 --vnet-name vm-eastus-godoy-vnet --subnet default --accelerated-networking true --public-ip-address pip-accelerated-1 --network-security-group vm-eastus-godoy-nsg

az vm create --resource-group Rg-B3-Godoy --name vm-producer-linux --image UbuntuLTS --size Standard_DS4_v2 --admin-username claudio --generate-ssh-keys --nics nic-accelerated-1 --admin-password Carroloco99@

When specifying an existing NIC, do not specify accelerated networking. Ignore --accelerated-networking now. This will trigger an error instead of a warning in future releases.



```

Conectar:

```dotnetcli

ssh claudio@20.228.150.72|4.246.158.114
```

Instalar .NET:

```
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-6.0
```