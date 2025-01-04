# FootballPlayersCatalog

docker build -t frontend ./catalog-of-football-players-frontend
docker build -t backend ./CatalogOfFootballPlayersBackend

yc init
1
y0_AgAAAAB4...6pl_F_tWQV0Do0RjA
1
Y
1
docker tag backend:latest cr.yandex/crpkbb5djavf03s565sl/backend:latest
docker push cr.yandex/crpkbb5djavf03s565sl/backend:latest

docker tag frontend:latest cr.yandex/crpkbb5djavf03s565sl/frontend:latest
docker push cr.yandex/crpkbb5djavf03s565sl/frontend:latest

https://console.yandex.cloud/folders/b1ge5opkkambmqd2dpk6/container-registry/registries/crpkbb5djavf03s565sl/overview/backend/image


-----------------------------------------------------

docker build -t nginx-balancer .
docker run -d -p 80:80 -p 3000:3000 -p 5000:5000 nginx-balancer
