docker run -d --hostname rabbitmq --name rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3-management

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Qazplm27' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu