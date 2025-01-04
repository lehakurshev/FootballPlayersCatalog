docker build -t frontend ../catalog-of-football-players-frontend
docker tag frontend:latest cr.yandex/crpkbb5djavf03s565sl/frontend:latest
docker push cr.yandex/crpkbb5djavf03s565sl/frontend:latest

