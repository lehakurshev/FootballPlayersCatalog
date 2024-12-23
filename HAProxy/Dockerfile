# Используем официальный образ HAProxy
FROM haproxy:latest

# Копируем конфигурационный файл HAProxy в контейнер
COPY haproxy.cfg /usr/local/etc/haproxy/haproxy.cfg

# Открываем порт 80 для HTTP и 443 для HTTPS
EXPOSE 80
EXPOSE 443

# Запускаем HAProxy при старте контейнера
CMD ["haproxy", "-f", "/usr/local/etc/haproxy/haproxy.cfg"]
