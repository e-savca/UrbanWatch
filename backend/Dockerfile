FROM jenkins/inbound-agent:latest

USER root

RUN apt-get update && apt-get install -y \
    openjdk-17-jdk \
    curl unzip && \
    curl -fsSL https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -o packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    rm packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y dotnet-sdk-8.0 aspnetcore-runtime-8.0 && \
    rm -rf /var/lib/apt/lists/*

ENV JAVA_HOME=/usr/lib/jvm/java-17-openjdk-amd64
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="$PATH:$DOTNET_ROOT"

USER jenkins

EXPOSE 50000
ENTRYPOINT ["/usr/local/bin/jenkins-agent"]
