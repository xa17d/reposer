# compile server
dotnet build ./server/ --configuration Release --output ./../docker/build/app

# build docker image
docker build -t xa17d/reposer ./docker/