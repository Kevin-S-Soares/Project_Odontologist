### Table of contents
[About the project](#about-the-project)

[Installation](#installation)

[Run](#Run)

---

### About the project

If you are here and want to check other projects:

[Project Odontologist Legacy](https://github.com/Kevin-S-Soares/Project_Odontologist_Legacy) - made in Next.js and C# web api.

[College Project](https://github.com/Kevin-S-Soares/Projeto_5S_T12) - made in Razor pages.

This project is made using Astro with Svelte components and C# web api. Data is stored using SQLite. Everything is containerized and syncronized using Docker Compose. It is running in a t3.small instance from AWS EC2.

Authentication is made using JWT asymmetric signature (RSA-SHA-4096).

Passwords are stored using bcrypt.

---

### Installation
#### Requirement
 - SendGrid Api Key
#### ubuntu

```
git clone https://github.com/Kevin-S-Soares/Project_Odontologist && cd Project_Odontologist
bash wizard.sh
```
---
### Run
```
sudo docker compose up -d
```

