# reposer

A webserver that regularly polls a git repository containing a website in a markdown similar format, compiles it to a static website and publishes it over HTTP.

The whole thing is packaged into a docker image.

**WORK IN PROGRESS**

## Compile

Dependencies:

* .NET Core https://www.microsoft.com/net/download (`dotnet` command)
* Docker https://docs.docker.com/install/ (`docker` command)

Compile:

* run `make.sh`

## Configure

Docker image name is `xa17d/reposer`.
It exposes port `80`.
Can be configured by attaching a volume at `/app/config`.
The config folder can contain following files:

* **REQUIRED:** `git-repository`: contains the path to the git repository containing the website. E.g.:
    ```
    https://github.com/xa17d/website-repository.git
    ```
* `git-credentials`: contains git credentials. Each credential is stored on its own line as a URL like:
    ```
    https://user:pass@example.com
    ```
* `gitconfig`: Will be copied to `~/.gitconfig`.
* `id_rsa`: Private key used for SSH. Will be copied to `~/.ssh/id_rsa`.
* `id_rsa.pub`: Public key used for SSH. "Will be copied to `~/.ssh/id_rsa.pub`.