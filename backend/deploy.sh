#!bin/bash

# Builds docker image in amd64 and pushs it to docker hub.
docker buildx build --platform linux/amd64 -t moomoo02/waifu:amd64 .
docker push moomoo02/waifu:amd64