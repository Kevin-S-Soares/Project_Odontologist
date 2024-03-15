# Table of contents
[About the project](#about-the-project)
[Installation](#installation)

---

## About the project 

If you are here and want to check complete projects:

[Project Odontologist Legacy](https://github.com/Kevin-S-Soares/Project_Odontologist_Legacy) - made in React and C# web api.

[College Project](https://github.com/Kevin-S-Soares/Projeto_5S_T12) - made in Razor pages.

This project is incomplete. I'm using it to learn:
 - SEO / Acessibility / PWA
 - Performance
 - Security
 - Cost efficiency
 - SSO and federated authentication
 - Internatiolization
 - And many other things


This project is made using Astro with Svelte components and C# web api. Data is stored using SQLite. Everything is containerized and syncronized using Docker Compose. It is running in a t2.small instance from AWS EC2.

Currently, the only thing you can do is register, sign-in, sign-out, forget password and reset password. 

Authentication is made using JWT asymmetric signature (RSA-SHA-4096).

Passwords are stored using bcrypt.

---

## Installation

#### ubuntu

```
git clone https://github.com/Kevin-S-Soares/Project_Odontologist
cd Project_Odontologist
bash wizard.sh
bash migration.sh
```