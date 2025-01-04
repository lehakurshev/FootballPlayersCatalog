FROM nginx:latest

# Определяем аргументы
ARG PORT1=localhost:4001
ARG PORT2=localhost:4002
ARG BALANCE_PORT=8080
ARG HOST1=bbau6qeh21r4lsravd9a.containers.yandexcloud.net
ARG HOST2=bbal3n8kt8j6thg4bvo8.containers.yandexcloud.net
ARG TARNSPORT=https

# Копируем шаблон конфигурации
COPY nginx.conf.template /etc/nginx/nginx.conf.template

# Генерируем конфигурацию на основе шаблона
RUN envsubst '$PORT1 $PORT2 $BALANCE_PORT $HOST1 $HOST2 $TARNSPORT' < /etc/nginx/nginx.conf.template > /etc/nginx/nginx.conf

# Указываем рабочий каталог
WORKDIR /usr/share/nginx/html