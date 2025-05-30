# 🌍 Blocked Countries API (.NET 8)

A .NET 8 Web API for managing blocked countries and IP validation using [ipapi.co](https://ipapi.co).  
This app **does not use a database**, and relies entirely on **in-memory storage**.

---

## 🚀 Features

- ✅ Add / remove permanently blocked countries
- ✅ Temporarily block countries for a specified duration
- ✅ Auto-remove expired temporary blocks via background service
- ✅ Lookup geolocation info by IP
- ✅ Check if caller's IP is from a blocked country
- ✅ Log blocked attempts (IP, user-agent, country, time)
- ✅ Search + pagination on blocked countries and logs
- ✅ Swagger API documentation

---

## 🔧 Technologies

- **.NET 8 Web API**
- **In-memory data** using `ConcurrentDictionary` / `List`
- **HttpClient** with `System.Text.Json`
- **HostedService** for background cleanup
- **Swagger** (Swashbuckle)
- No external database or ORM

---

## 📁 Endpoints Overview

### 🔐 Country Blocking

| Method | Endpoint                         | Description                        |
|--------|----------------------------------|------------------------------------|
| POST   | `/api/countries/block`           | Block a country (permanent)        |
| DELETE | `/api/countries/block/{code}`    | Unblock a country                  |
| GET    | `/api/countries/blocked`         | List blocked countries (with search/pagination) |
| POST   | `/api/countries/temporal-block`  | Temporarily block a country        |

### 🌐 IP Geolocation

| Method | Endpoint                         | Description                        |
|--------|----------------------------------|------------------------------------|
| GET    | `/api/ip/lookup?ipAddress={ip}`  | Get country info by IP             |
| GET    | `/api/ip/check-block`            | Check if caller's IP is blocked    |

### 📜 Logs

| Method | Endpoint                         | Description                        |
|--------|----------------------------------|------------------------------------|
| GET    | `/api/logs/blocked-attempts`     | View log of blocked IP attempts    |

---

## 🔁 Background Service

- Every **5 minutes**, expired temporary blocks are removed using a `BackgroundService`.

---

## 🔑 IP API Integration

Using [ipapi.co](https://ipapi.co) for geolocation:
- For development, **you can use it without an API key**
- Or sign up and store your key in `appsettings.json`:

```json
{
  "IpApiKey": "your_real_key_here"
}
