List all docker containers:
```
docker ps -a
```

Start and connect to an existing container:
```
docker start -a -i [DOCKER_ID]
```

Attach to running container bash:
```
docker exec -it [DOCKER_ID] /bin/bash
```

Save image to file:
```
docker save -o image_xa17d_reposer.tar xa17d/reposer
```

Load image from file:
```
docker load -i image_xa17d_reposer.tar
```