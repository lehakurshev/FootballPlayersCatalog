FROM postgres:16

ENV POSTGRES_DB=footballplayersdb
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=postgres

# Изменяем порт, который слушает PostgreSQL
ENV PGPORT=8080

EXPOSE 8080

# Меняем команду для запуска postgres, добавляя -p 443 для порта
CMD ["postgres", "-p", "8080"]

# docker build -t postgres-image .

# docker run -d --name postgres-container \
# -p 5432:5432 \
# -v postgres:/var/lib/postgresql/data \
# --network backend \
# --restart unless-stopped \
# my-postgres-image