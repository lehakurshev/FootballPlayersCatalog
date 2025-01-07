FROM postgres:16

# Установка необходимых пакетов для генерации сертификатов
RUN apt-get update && apt-get install -y openssl && apt-get clean

# Создание директории для хранения сертификатов
RUN mkdir /var/lib/postgresql/certificates

# Генерация самоподписанного сертификата и ключа
RUN openssl req -new -newkey rsa:2048 -days 365 -nodes \
    -x509 -subj "/C=US/ST=State/L=City/O=Organization/CN=localhost" \
    -keyout /var/lib/postgresql/certificates/server.key \
    -out /var/lib/postgresql/certificates/server.crt && \
    chmod 600 /var/lib/postgresql/certificates/server.key && \
    chown postgres:postgres /var/lib/postgresql/certificates/server.*

# Копирование конфигурационного файла PostgreSQL
COPY postgres.conf /etc/postgresql/postgresql.conf

ENV POSTGRES_DB=footballplayersdb
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=postgres

ENV PGPORT=8080

EXPOSE 8080

# Запуск PostgreSQL с конфигурацией для использования SSL
CMD ["postgres", "-c", "config_file=/etc/postgresql/postgresql.conf"]
