FROM nginx:latest


RUN apt-get update && \
    apt-get install -y openssl && \
    apt-get clean

# Генерация самоподписанного сертификата
RUN openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
    -keyout /etc/ssl/private/nginx-selfsigned.key \
    -out /etc/ssl/certs/nginx-selfsigned.crt \
    -subj "/C=RU/ST=Moscow/L=Moscow/O=MyOrg/OU=MyUnit/CN=mydomain.com"

COPY nginx.conf /etc/nginx/nginx.conf