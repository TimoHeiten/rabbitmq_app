#!/usr/bin/env bash

docker container run -d --name local_rabbit --rm -p 5672:5672 -p 15673:15672 rabbitmq:3-management

# poll for start of rabbitmq
while true;do
    printf "."
    result=$(curl -f http://localhost:15673)
    if [[ $result == *"<!doctype html>"* ]]; then
        echo "is started"
        break
    fi
done

# run producer
dotnet run --project ./producer/producer.csproj
# run consumer
dotnet run --project ./consumer/consumer.csproj

dotnet container stop local_rabbit