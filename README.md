# FootballPlayersCatalog

docker build -t frontend ./catalog-of-football-players-frontend
docker build -t backend ./CatalogOfFootballPlayersBackend

yc init
docker tag backend:latest cr.yandex/crpkbb5djavf03s565sl/backend:latest
docker push cr.yandex/crpkbb5djavf03s565sl/backend:latest

docker tag frontend:latest cr.yandex/crpkbb5djavf03s565sl/frontend:latest
docker push cr.yandex/crpkbb5djavf03s565sl/frontend:latest

https://console.yandex.cloud/folders/b1ge5opkkambmqd2dpk6/container-registry/registries/crpkbb5djavf03s565sl/overview/backend/image





openapi: 3.0.0
info:
  title: Sample API
  version: 1.0.0
servers:
- url: https://d5dm4l253eakc4lhk1im.apigw.yandexcloud.net
paths:
  /:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bbagg8nvbr0vd4g8hdo9
  /{path}:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bbagg8nvbr0vd4g8hdo9
      parameters:
        - name: path
          in: path
          required: true
          schema:
            type: string
  /swagger/v1/{path}:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bbagg8nvbr0vd4g8hdo9
      parameters:
        - name: path
          in: path
          required: true
          schema:
            type: string
  /api/1.0/FootballPlayer:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bbagg8nvbr0vd4g8hdo9
    post:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bbagg8nvbr0vd4g8hdo9
    put:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bbagg8nvbr0vd4g8hdo9
  /api/1.0/FootballPlayer/{id}:
    parameters:
      - name: id
        in: path
        required: true
        schema:
          type: string
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bbagg8nvbr0vd4g8hdo9
    delete:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bbagg8nvbr0vd4g8hdo9
  /:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bba95ptk878l5c8koe8e
  /{path}:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bba95ptk878l5c8koe8e
      parameters:
        - name: path
          in: path
          required: true
          schema:
            type: string
  /swagger/v2/{path}:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bba95ptk878l5c8koe8e
      parameters:
        - name: path
          in: path
          required: true
          schema:
            type: string
  /api/2.0/FootballPlayer:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bba95ptk878l5c8koe8e
    post:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bba95ptk878l5c8koe8e
    put:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bba95ptk878l5c8koe8e
  /api/2.0/FootballPlayer/{id}:
    parameters:
      - name: id
        in: path
        required: true
        schema:
          type: string
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bba95ptk878l5c8koe8e
    delete:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bba95ptk878l5c8koe8e



openapi: 3.0.0
info:
  title: Sample API
  version: 1.0.0
servers:
- url: https://d5dm4l253eakc4lhk1im.apigw.yandexcloud.net
paths:
  /:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bbagg8nvbr0vd4g8hdo9
  /:
    get:
      x-yc-apigateway-integration:
        type: serverless_containers
        service_account_id: ajeq0stspndu7grcu5m0
        container_id: bba95ptk878l5c8koe8e
