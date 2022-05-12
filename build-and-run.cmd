docker build -f DockerfileApi . -t ridelytask-api
docker build -f DockerfileWorker . -t ridelytask-worker
docker-compose -f ridelytask-infra.yml up -d
docker compose -f ridelytask-app.yml up -d