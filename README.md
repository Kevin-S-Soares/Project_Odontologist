### Table of contents
[About the project](#about-the-project)

[Installation](#installation)

[Run](#Run)

---

### About the project

If you are here and want to check complete projects:

[Project Odontologist Legacy](https://github.com/Kevin-S-Soares/Project_Odontologist_Legacy) - made in Next.js and C# web api.

[College Project](https://github.com/Kevin-S-Soares/Projeto_5S_T12) - made in Razor pages.

This project is incomplete. I'm using it to learn:
 - SEO / Acessibility / PWA
 - Performance
 - Security
 - Cost efficiency
 - SSO and federated authentication
 - Internationalization
 - And many other things


This project is made using Astro with Svelte components and C# web api. Data is stored using SQLite. Everything is containerized and syncronized using Docker Compose. It is running in a t3.small instance from AWS EC2.

Currently, the only thing you can do is register, sign-in, sign-out, forget password, reset password, change password, change email, change account settings, change theme and delete account. 

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

