docker build -t nginx-balancer ../nginx-balancer
docker tag nginx-balancer:latest cr.yandex/crpkbb5djavf03s565sl/nginx-balancer:latest
docker push cr.yandex/crpkbb5djavf03s565sl/nginx-balancer:latest

yc serverless container revision deploy \
  --container-name nginx-balancer \
  --image cr.yandex/crpkbb5djavf03s565sl/nginx-balancer:latest \
  --service-account-id ajeq0stspndu7grcu5m0 \
  --cores 1 \
  --memory 1GB \
  --concurrency 1 \
  --execution-timeout 60s