FROM prom/prometheus

# Копируем файл prometheus.yml в образ
COPY prometheus.yml /etc/prometheus/prometheus.yml

# Указываем, что этот файл должен быть доступен при запуске
CMD [ "--config.file=/etc/prometheus/prometheus.yml" ]