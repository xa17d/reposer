FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY build/app bin
COPY start.sh bin/start.sh
COPY config-default config-default
COPY webroot webroot

RUN apt-get update && \
    apt-get install -y git && \
    apt-get clean && \
    chmod +x bin/start.sh

##
## Volumes
##
VOLUME [ "/app/config" ]
EXPOSE 80
ENTRYPOINT ["bin/start.sh"]