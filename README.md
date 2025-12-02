### Table of contents
[About the project](#about-the-project)

[Installation](#installation)

[Run](#Run)

---

### About the project

If you are here and want to check other projects:

[Project Odontologist Legacy](https://github.com/Kevin-S-Soares/Project_Odontologist_Legacy) - made in Next.js and C# web api.

[College Project](https://github.com/Kevin-S-Soares/Projeto_5S_T12) - made in Razor pages.

Control Panel is a project intended to assist in the management of appointments.
You can verify, add, reschedule and cancel appointments based on the odontologist's schedule.
All appointments are verified, meaning that you do not have to worry about appointments overlapping.

This project is made using Astro with Svelte components and C# web api. Data is stored using SQLite. Everything is containerized and syncronized using Docker Compose. It is running in a t2.micro instance from AWS EC2.

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

