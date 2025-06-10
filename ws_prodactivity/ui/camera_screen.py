from PyQt5.QtWidgets import QWidget, QLabel, QVBoxLayout, QPushButton, QListWidget, QHBoxLayout, QListWidgetItem
from PyQt5.QtGui import QImage, QPixmap, QFont, QColor, QBrush
from PyQt5.QtCore import Qt, QSize
import cv2

class CameraScreen(QWidget):
    def __init__(self, on_back):
        super().__init__()
        self.setStyleSheet("""
            QWidget {
                background-color: #2e2e2e;
                color: white;
                font-size: 16px;
            }
            QPushButton {
                padding: 14px;
                font-weight: bold;
                border-radius: 6px;
                font-size: 18px;
            }
            QLabel {
                font-weight: bold;
                font-size: 18px;
            }
            QListWidget {
                background-color: #3a3a3a;
                border-radius: 8px;
                padding: 8px;
                font-size: 16px;
            }
            QListWidget::item {
                padding: 10px;
                margin: 5px 0;
                border-radius: 4px;
                border-bottom: 1px solid #444;
            }
            QListWidget::item:selected {
                background-color: #5aaaff;
            }
        """)
        self.setWindowTitle("Kamera Önizlemesi")

        self.setMinimumSize(1200, 700)

        main_layout = QHBoxLayout()
        left_layout = QVBoxLayout()
        right_layout = QVBoxLayout()

        self.video_label = QLabel("Kamera başlatılıyor...")
        self.video_label.setFixedSize(800, 600)
        self.video_label.setAlignment(Qt.AlignCenter)
        self.video_label.setStyleSheet("border: 3px solid #5aaaff; border-radius: 10px; background-color: #222;")
        left_layout.addWidget(self.video_label)

        buttons_layout = QHBoxLayout()

        self.back_btn = QPushButton("🔙 Geri")
        self.exit_btn = QPushButton("❌ Çıkış")

        self.back_btn.setStyleSheet("background-color: #5aaaff; color: white;")
        self.exit_btn.setStyleSheet("background-color: #ff5a5a; color: white;")

        self.back_btn.clicked.connect(on_back)
        self.exit_btn.clicked.connect(lambda: exit(0))

        buttons_layout.addWidget(self.back_btn)
        buttons_layout.addWidget(self.exit_btn)
        left_layout.addLayout(buttons_layout)

        list_title = QLabel("🔍 Tespit Edilen Ürünler")
        list_title.setAlignment(Qt.AlignCenter)
        list_title.setStyleSheet("color: white; margin-bottom: 10px; font-size: 20px;")

        self.detection_list = QListWidget()
        self.detection_list.setMinimumWidth(300)
        self.detection_list.setStyleSheet("""
            QListWidget {
                background-color: #3a3a3a;
                border: 2px solid #444;
                border-radius: 8px;
                font-size: 18px;
            }
            QListWidget::item {
                border-bottom: 1px solid #444;
                color: white;
                padding: 12px 8px;
            }
        """)

        right_layout.addWidget(list_title)
        right_layout.addWidget(self.detection_list)

        main_layout.addLayout(left_layout, 7)
        main_layout.addLayout(right_layout, 3)
        self.setLayout(main_layout)

    def update_frame(self, frame):
        rgb_image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        h, w, ch = rgb_image.shape
        bytes_per_line = ch * w
        qt_image = QImage(rgb_image.data, w, h, bytes_per_line, QImage.Format_RGB888)
        pixmap = QPixmap.fromImage(qt_image)
        pixmap = pixmap.scaled(self.video_label.size(), Qt.KeepAspectRatio, Qt.SmoothTransformation)
        self.video_label.setPixmap(pixmap)

    def draw_detection_boxes(self, frame, detections):
        result_frame = frame.copy()
        for det in detections:
            if 'bbox' in det:
                x, y, w, h = det['bbox']
                cv2.rectangle(result_frame, (x, y), (x + w, y + h), (0, 255, 0), 3)
                cv2.putText(result_frame, f"{det['label']}", (x, y - 10),
                            cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0, 255, 0), 2)
        return result_frame

    def update_detections(self, detections):
        self.detection_list.clear()

        icons = {
            "bottle": "🥤",
            "scissors": "✂️",
            "mouse": "🖱️",
            "keyboard": "⌨️",
        }

        for d in detections:
            label = d['label']
            confidence = d['confidence']

            icon = icons.get(label.lower(), "📦")

            if confidence >= 0.7:
                confidence_indicator = "🟢"
            elif confidence >= 0.5:
                confidence_indicator = "🟡"
            else:
                confidence_indicator = "🔴"

            item = QListWidgetItem(f"{icon} {label} - {confidence_indicator} {confidence:.2f}")
            item.setData(Qt.UserRole, d)

            self.detection_list.addItem(item)

    def update_frame_with_detections(self, frame, detections):
        result_frame = self.draw_detection_boxes(frame, detections)
        self.update_frame(result_frame)
        self.update_detections(detections)