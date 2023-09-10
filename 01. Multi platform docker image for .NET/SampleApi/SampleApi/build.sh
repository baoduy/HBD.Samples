docker build --platform="linux/amd64" -f Dockerfile -t sampleapi-x64:latest ../
docker build --platform="linux/arm64" -f Dockerfile -t sampleapi-arm64:latest ../
#docker run -p 8080:80 -d --name sampleapi sampleapi:latest