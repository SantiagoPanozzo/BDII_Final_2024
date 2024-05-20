# Backend de la Penca

Hay dos formas de correr el programa. Un Docker Compose para la base de datos y otro para
la API. O un Docker Compose que corre las dos cosas.
Si se corre la api y la bd por separado, se puede reiniciar la api sin que se borre la bd.
Si se corren las dos cosas juntas, si se reinicia la api se borra la bd.

## Para correr:
### Base de datos (Postgres):

`docker compose -f docker-compose-db.yml up --build`

### API (.NET):

`docker compose -f docker-compose-api.yml up --build`

### Las dos cosas:
Si reinicias este se borra la base de datos también.

`docker compose -f docker-compose-all.yml up --build`
