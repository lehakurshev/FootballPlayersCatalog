docker build -t backend ../CatalogOfFootballPlayersBackend
docker tag backend:latest cr.yandex/crpkbb5djavf03s565sl/backend:latest
docker push cr.yandex/crpkbb5djavf03s565sl/backend:latest
