import asyncio
import websockets
import logging
import json
from datetime import datetime


class WebSocketServer:
    def __init__(self):
        self.clients = set()
        logging.basicConfig(level=logging.INFO)
        self.logger = logging.getLogger('WebSocketServer')
        self.heartbeat_interval = 1

    async def handler(self, websocket):
        self.clients.add(websocket)
        print(f"[WebSocket] Yeni istemci bağlandı. Adres: {websocket.remote_address}")
        print(f"[WebSocket] Aktif bağlantı sayısı: {len(self.clients)}")

        try:
            heartbeat_task = asyncio.create_task(self.send_heartbeat(websocket))

            async for message in websocket:
                try:
                    data = json.loads(message)
                    print(f"[WebSocket] İstemciden mesaj alındı: {data}")

                    if "type" in data:
                        if data["type"] == "ping":
                            await websocket.send(json.dumps({"type": "pong"}))
                except json.JSONDecodeError:
                    print("[WebSocket] Geçersiz JSON formatı")

        except websockets.exceptions.ConnectionClosed:
            print(f"[WebSocket] İstemci bağlantısı kapandı: {websocket.remote_address}")
        except Exception as e:
            print(f"[WebSocket] İstemci hatası: {e}")
        finally:
            heartbeat_task.cancel()
            self.clients.remove(websocket)
            print(f"[WebSocket] İstemci ayrıldı. Kalan bağlantı sayısı: {len(self.clients)}")

    async def send_heartbeat(self, websocket):
        while True:
            try:
                await websocket.send(json.dumps({
                    "type": "heartbeat",
                    "timestamp": datetime.now().timestamp()
                }))
                await asyncio.sleep(self.heartbeat_interval)
            except:
                break

    async def start(self):
        try:
            print("[WebSocket] Sunucu başlatılıyor...")
            server = await websockets.serve(
                self.handler,
                "localhost",
                8765,
                ping_interval=30,
                ping_timeout=10,
                close_timeout=10
            )
            print("[WebSocket] Sunucu başlatıldı: ws://localhost:8765")
            await server.wait_closed()
        except Exception as e:
            print(f"[WebSocket] Sunucu başlatma hatası: {e}")
            raise

    async def broadcast(self, message):
        if not self.clients:
            print("[WebSocket] Aktif bağlı istemci yok, mesaj gönderilemedi")
            return

        print(f"[WebSocket] {len(self.clients)} istemciye mesaj gönderiliyor...")

        disconnected = set()
        for ws in list(self.clients):
            try:
                if isinstance(message, str):
                    await ws.send(message)
                else:
                    await ws.send(json.dumps(message))
                print(f"[WebSocket] Mesaj gönderildi -> {ws.remote_address}")
            except websockets.exceptions.ConnectionClosed:
                disconnected.add(ws)
                print(f"[WebSocket] Bağlantı kapandı: {ws.remote_address}")
            except Exception as e:
                print(f"[WebSocket] Gönderme hatası: {e}")
                disconnected.add(ws)

        if disconnected:
            self.clients -= disconnected
            print(f"[WebSocket] {len(disconnected)} istemci bağlantısı kaldırıldı")


def start_ws_server():
    loop = asyncio.new_event_loop()
    asyncio.set_event_loop(loop)
    loop.run_until_complete(ws_server.start())


ws_server = WebSocketServer()
