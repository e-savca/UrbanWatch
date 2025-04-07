# Running Docker Compose Environments

## 🔧 Usage

Run the appropriate `docker-compose` command based on your desired environment.

### ✅ Available Compose Environments

#### 🏭 Production (default)
```sh
docker-compose up --build
```

#### 🧪 Development with Portainer
```sh
docker-compose -f docker-compose.dev.portainer.yml up --build
```

#### 💻 Development on Localhost
```sh
docker-compose -f docker-compose.dev.local.yml up --build
```

## 📌 Notes

- Make sure Docker and Docker Compose are installed.
- Use `--build` to ensure fresh images are created when services are started.
