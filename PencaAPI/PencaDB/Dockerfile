FROM postgres:16-alpine

ENV POSTGRES_DB=pencadb
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=postgres

# El directorio de docker donde se ejectuan los script de postgres al iniciar
WORKDIR /docker-entrypoint-initdb.d

COPY ./init.sql /docker-entrypoint-initdb.d/

