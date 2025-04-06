# 🧩 Git Submodules Management

This repository uses **Git submodules** to include external repositories (modules) within the main project.

To simplify working with submodules, two shell scripts are provided inside the `module-actions/` folder:

- `update-submodules.sh` – updates submodules to their latest commits from `origin/main`.
- `commit-submodule-updates.sh` – commits the new submodule references to the main repository.

---

## ➕ How to Add a New Submodule

To add a submodule to your project, run the following command from the root of the repository:

```bash
git submodule add https://github.com/e-savca/UrbanWatch.FE.git frontend
```

Then commit the changes:

```bash
git add .gitmodules frontend
git commit -m "Add submodule: module-repo"
git push
```

---

## 🔄 How to Update All Submodules

Navigate to the root of the repository and run the two scripts in order:

### 1. Make sure the scripts are executable:

```bash
chmod +x module-actions/update-submodules.sh module-actions/commit-submodule-updates.sh
```

### 2. Run the update script:

```bash
./module-actions/update-submodules.sh
```

This script:

- Initializes submodules (if needed)
- Pulls the latest changes from `origin/main` in each submodule

### 3. Commit the updated submodule references:

```bash
./module-actions/commit-submodule-updates.sh
```

---

## 📦 Notes

- These scripts assume that the default branch of each submodule is `main`. If your modules use a different branch, you’ll need to adjust the scripts.
- If you clone this repository for the first time, make sure to run:

```bash
git submodule update --init --recursive
```

---

## ✅ Example Workflow

```bash
git pull
./module-actions/update-submodules.sh
# Optional: make additional changes
./module-actions/commit-submodule-updates.sh
```

---

Enjoy working with modular repositories! 🛠️
