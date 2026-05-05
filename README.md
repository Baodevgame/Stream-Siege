# 🎮 Stream Siege

**Stream Siege** là một game **top-down interactive** dành cho livestream, nơi người xem có thể tác động trực tiếp vào gameplay thông qua donate hoặc chat.

Người chơi (streamer) sẽ phải sinh tồn trước làn sóng hỗn loạn do chính khán giả tạo ra — khi một bên hỗ trợ và bên còn lại cố gắng phá game.

---

## 🚀 Core Concept

* 🎥 Tích hợp với livestream (Twitch / YouTube / TikTok)

* 👥 Viewer chia thành 2 phe:

  * 🟢 **Support Team**: hỗ trợ streamer bằng skill, buff
  * 🔴 **Enemy Team**: spawn quái, boss để phá

* 💰 Donate hoặc chat → chuyển thành **Energy**

* ⚡ Energy → dùng để kích hoạt hành động trong game

---

## 🎮 Gameplay

* Game góc nhìn **top-down**
* Streamer điều khiển 1 nhân vật chính
* Viewer tương tác real-time:

  * Spawn quái
  * Buff / heal
  * Kích hoạt skill đặc biệt

### 🔁 Gameplay Loop

1. Viewer donate / chat
2. Server nhận event
3. Chuyển thành Energy
4. Viewer hoặc hệ thống dùng Energy
5. Spawn quái hoặc kích hoạt skill
6. Streamer cố gắng sống sót

---

## ⚖️ Core Mechanics

### 🔋 Energy System

* Donate không trigger trực tiếp
* Mỗi hành động cần tiêu hao Energy

### ⏱️ Cooldown

* Boss / skill mạnh có cooldown
* Tránh spam phá game

### 🧠 Balance

* Hệ thống giới hạn spawn
* Scaling theo thời gian

---

## 🌐 System Architecture

```text
[ Livestream Platform ]
          ↓
[ Backend Server (Node.js / Python) ]
          ↓ (WebSocket)
[ Unity Game Client ]
```

### 🧩 Components

* **Livestream API**

  * Nhận donate / chat event

* **Server**

  * Xử lý event
  * Convert → game data

* **Unity Client**

  * Nhận dữ liệu realtime
  * Spawn / trigger gameplay

---

## 🛠️ Tech Stack

* 🎮 Unity (Top-down 2D/3D)
* 🌐 WebSocket (Realtime communication)
* 🧠 Node.js / Python (Backend)
* 🔗 API integration (Twitch / YouTube / TikTok)

---

## 📦 Features (MVP)

* [ ] Player movement (top-down)
* [ ] Spawn enemy system
* [ ] Skill system
* [ ] Energy system
* [ ] Fake event simulation
* [ ] Livestream integration
* [ ] Chat command system
* [ ] UI energy display
* [ ] Boss mechanics

---

## 🔥 Future Plans (Dự định)

* ⚔️ PvP giữa các streamer
* 🤝 Co-op nhiều streamer
* 🏆 Ranking viewer (top supporter / top enemy)
* 🎲 Random events (chaos mode)
* 🧠 AI auto-balance system

---

## 🎯 Vision

Tạo ra một game mà:

> **Viewer không chỉ xem — mà là một phần của gameplay**

---

## ❤️ Credits

Developed by: Phạm Bảo (Xenos)
Idea: Interactive Stream Game Concept

---
