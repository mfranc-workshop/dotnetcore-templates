FROM microsoft/dotnet:latest

RUN mkdir -p /usr/src/microservice
WORKDIR /usr/src/microservice

COPY . /usr/src/microservice
RUN dotnet restore

ENTRYPOINT [ "dotnet", "run" ]

CMD [ "5000" ]
