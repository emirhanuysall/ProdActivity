import sys
import cv2
import threading
import time
import json
import asyncio
from PyQt5.QtWidgets import QApplication, QStackedWidget
from ui.main_window import MainWindow
from ui.config_screen import ConfigScreen
from ui.camera_screen import CameraScreen
from detector import Detector
from server import WebSocketServer


class App:
    def __init__(self):
        self.ws_port = 8765
        self.detector = Detector()
        self.capture = None
        self.running = False
        self.ws_server = WebSocketServer()

        self.app = QApplication(sys.argv)
        self.stack = QStackedWidget()

        self.main_window = MainWindow(
            on_config=self.show_config,
            on_camera=self.start_camera
        )

        self.config_screen = ConfigScreen(
            on_back=self.show_main,
            on_config_changed=self.update_config,
            on_port_changed=self.update_ws_port,
            current_port=self.ws_port
        )

        self.camera_screen = CameraScreen(
            on_back=self.stop_camera
        )

        self.stack.addWidget(self.main_window)
        self.stack.addWidget(self.config_screen)
        self.stack.addWidget(self.camera_screen)
        self.stack.setCurrentWidget(self.main_window)

        threading.Thread(target=self.run_ws_server, daemon=True).start()

    def run_ws_server(self):
        print("[UI] WebSocket sunucusu başlatılıyor...")
        loop = asyncio.new_event_loop()
        asyncio.set_event_loop(loop)
        loop.run_until_complete(self.ws_server.start())

    def update_ws_port(self, new_port):
        print(f"[UI] Yeni WebSocket portu ayarlandı: {new_port}")
        self.ws_port = new_port

    def show_main(self):
        self.stack.setCurrentWidget(self.main_window)

    def show_config(self):
        self.stack.setCurrentWidget(self.config_screen)

    def update_config(self, selected_labels):
        print(f"[UI] Seçilen ürünler: {selected_labels}")
        self.detector.set_target_labels(selected_labels)
        self.stack.setCurrentWidget(self.main_window)

    def start_camera(self):
        print("[UI] Kamera başlatılıyor...")
        self.stack.setCurrentWidget(self.camera_screen)
        self.running = True
        self.capture = cv2.VideoCapture(0)
        threading.Thread(target=self.camera_loop, daemon=True).start()

    def stop_camera(self):
        print("[UI] Kamera durduruluyor...")
        self.running = False
        if self.capture:
            self.capture.release()
        self.stack.setCurrentWidget(self.main_window)

    def camera_loop(self):
        detection_states = {}  # key: obj_id, value: {"touched": bool, "sent": bool}

        while self.running:
            ret, frame = self.capture.read()
            if not ret:
                continue

            height, width, _ = frame.shape
            line_x = width // 2

            cv2.line(frame, (line_x, 0), (line_x, height), (0, 255, 255), 1)

            detections = self.detector.detect(frame)
            send_detections = []

            for det in detections:
                x1, y1, x2, y2 = det["bbox"]
                label = det["label"]
                conf = det["confidence"]

                cv2.rectangle(frame, (x1, y1), (x2, y2), (0, 255, 0), 2)
                cv2.putText(frame, f"{label} ({conf:.2f})", (x1, y1 - 10),
                            cv2.FONT_HERSHEY_SIMPLEX, 0.4, (0, 255, 0), 2)

                center_x = (x1 + x2) // 2
                center_y = (y1 + y2) // 2
                obj_id = f"{label}_{center_x // 20}_{center_y // 20}"

                state = detection_states.get(obj_id, {"touched": False, "sent": False})

                is_touching = x1 <= line_x <= x2

                if is_touching:
                    state["touched"] = True
                elif state["touched"] and not state["sent"]:
                    send_detections.append(det)
                    state["sent"] = True

                detection_states[obj_id] = state

            if send_detections:
                json_data = {
                    "type": "detection",
                    "timestamp": time.time(),
                    "detections": send_detections
                }
                print("[UI] Gönderilen JSON:")
                print(json.dumps(json_data, indent=2))
                asyncio.run(self.ws_server.broadcast(json.dumps(json_data)))

            self.camera_screen.update_frame(frame)
            self.camera_screen.update_detections(detections)

            time.sleep(0.03)

    def run(self):
        self.stack.setWindowTitle("ProdActivity")
        self.stack.resize(900, 500)
        self.stack.show()
        sys.exit(self.app.exec_())

if __name__ == "__main__":
    App().run()
