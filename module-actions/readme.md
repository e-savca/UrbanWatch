## 🧩 Git Submodules – Quick Guide

This project uses **Git submodules** to manage external modules (like the Worker service).

### 🔁 Full Sync

Use the `full-sync.sh` script to update everything:

```bash
./full-sync.sh
```

This will:
- Pull the latest changes from the main repository
- Update all submodules to the latest commit on `origin/main`
- Commit updated submodule references

---

### ➕ Add a New Submodule

Use the helper script `add-submodule.sh`:

```bash
./module-actions/add-submodule.sh https://github.com/e-savca/UrbanWatch.Worker.git backend/worker
```

This will:
- Add the submodule at the given path
- Initialize it
- Commit the changes to `.gitmodules` and the main repo

---

### 🛠️ Manual Submodule Update (optional)

If needed, run:

```bash
./module-actions/update-submodules.sh
./module-actions/commit-submodule-updates.sh
```

---

✅ **Tip:** Run once if the scripts aren’t executable yet.
```
chmod +x *.sh
```
