FROM postgres:16


ENV POSTGRES_DB footballplayersdb
ENV POSTGRES_USER postgres
ENV POSTGRES_PASSWORD postgres

EXPOSE 5432

CMD ["postgres"]

# docker build -t postgres-image .

# docker run -d --name postgres-container \
# -p 5432:5432 \
# -v postgres:/var/lib/postgresql/data \
# --network backend \
# --restart unless-stopped \
# my-postgres-image