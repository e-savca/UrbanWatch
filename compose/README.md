# Running Docker Compose Environments

## 🔧 Usage

Run the appropriate `docker-compose` command based on your desired environment.

### ✅ Available Compose Environments

| Command                    | Description                        |
|----------------------------|------------------------------------|
| `docker-compose -f docker-compose.dev.yml up --build` | Base development environment |
| `docker-compose -f docker-compose.dev.api.yml up --build` | API-only setup              |
| `docker-compose -f docker-compose.dev.api-worker.yml up --build` | API with worker services    |
| `docker-compose up --build` | Default Compose setup (`docker-compose.yml`) |

## 📝 Example

```bash
# For API with worker setup:
docker-compose -f docker-compose.dev.api-worker.yml up --build
```

## 📌 Notes

- Make sure Docker and Docker Compose are installed.
- Use `--build` to ensure fresh images are created when services are started.
