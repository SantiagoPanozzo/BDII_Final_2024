FROM postgres:16-bookworm

ENV POSTGRES_DB=pencadb
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=postgres

# El directorio de docker donde se ejectuan los script de postgres al iniciar
WORKDIR /docker-entrypoint-initdb.d

COPY ./*.sql /docker-entrypoint-initdb.d/
RUN apt-get update && apt-get install -y pgagent


