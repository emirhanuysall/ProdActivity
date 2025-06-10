from PyQt5.QtWidgets import QWidget, QPushButton, QVBoxLayout
from PyQt5.QtCore import Qt

class MainWindow(QWidget):
    def __init__(self, on_config, on_camera):
        super().__init__()
        self.setStyleSheet("""
            QWidget {
                background-color:#2e2e2e;
                color: white;
            }
            QPushButton {
                background-color: #5aaaff;
                color: white;
                font-size: 20px;
                padding: 20px;
                border-radius: 12px;
                margin: 20px;
            }
            QPushButton:hover {
                background-color: #4a90e2;
            }
        """)

        layout = QVBoxLayout()
        layout.setAlignment(Qt.AlignCenter)

        self.config_btn = QPushButton("⚙️ Konfigürasyon")
        self.config_btn.clicked.connect(on_config)

        self.camera_btn = QPushButton("🎥 Ürün Takibi")
        self.camera_btn.clicked.connect(on_camera)

        layout.addWidget(self.config_btn)
        layout.addWidget(self.camera_btn)

        self.setLayout(layout)
