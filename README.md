🦷 Control Panel – Sistema de Gerenciamento de Consultas

Aplicação completa para gestão de consultas odontológicas, com verificação automática de conflitos, autenticação segura e deploy utilizando Docker.

---

## 📑 Sumário
- [Sobre o projeto](#📘-sobre-o-projeto)
- [Características](#✨-características)
- [Tech Stack](#🛠️-tech-stack)
- [Instalação](#📦-instalação)
- [Executar](#💻-executar)

---
## 📘 Sobre o projeto

**Control Panel** é um sistema desenvolvido para auxiliar odontologistas no gerenciamento de consultas.  
A aplicação permite:

- Visualizar agendamentos
- Criar novas consultas
- Reagendar
- Cancelar
- Garantir que *nenhum horário sobreponha outro*

O projeto utiliza **Astro + Svelte** no frontend e **C# Web API** no backend.  
Os dados são armazenados em **SQLite**, e toda a aplicação é **containerizada com Docker Compose**, em execução em uma instância **AWS EC2 t2.micro**.

Autenticação utiliza **JWT com assinatura assimétrica RSA-SHA-4096**, e senhas são protegidas com **bcrypt**.

### 🔗 Outros projetos
- **Project Odontologist Legacy** – Next.js + C# Web API  
  https://github.com/Kevin-S-Soares/Project_Odontologist_Legacy

- **College Project** – Razor Pages  
  https://github.com/Kevin-S-Soares/Projeto_5S_T12

---

## ✨ Características 
- ✔️ CRUD completo de consultas  
- ✔️ Verificação automática de conflito de horários  
- ✔️ Login seguro com JWT (RSA 4096)  
- ✔️ Hash de senha com bcrypt  
- ✔️ Frontend moderno com Astro + Svelte  
- ✔️ Backend em C# Web API  
- ✔️ Deploy via Docker Compose  
- ✔️ Banco de dados leve com SQLite  

---

## 🛠️ Tech Stack
**Frontend:** Astro, Svelte  
**Backend:** C# (.NET) Web API  
**Database:** SQLite  
**Auth:** JWT (RSA 4096) + bcrypt  
**DevOps:** Docker, Docker Compose, AWS EC2  
**Email:** SendGrid  

---

## 📦 Instalação

### Requerimentos
- SendGrid API Key  

### Ubuntu
```bash
git clone https://github.com/Kevin-S-Soares/Project_Odontologist
cd Project_Odontologist
bash wizard.sh
```

---

## 💻 Executar

```bash
sudo docker compose up -d
```
