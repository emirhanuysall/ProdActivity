from PyQt5.QtWidgets import (
    QWidget, QVBoxLayout, QHBoxLayout, QLabel, QCheckBox,
    QPushButton, QTabWidget, QLineEdit
)
from PyQt5.QtCore import Qt

class ConfigScreen(QWidget):
    def __init__(self, on_back, on_config_changed, on_port_changed, current_port):
        super().__init__()
        self.on_back = on_back
        self.on_config_changed = on_config_changed
        self.on_port_changed = on_port_changed

        self.setStyleSheet("""
            QWidget {
                background-color: #2e2e2e;
                color: white;
                font-size: 16px;
            }
            QCheckBox {
                font-size: 20px;
                margin: 8px;
            }
            QPushButton {
                padding: 10px;
                font-weight: bold;
                margin-top: 20px;
            }
            QLineEdit {
                padding: 8px;
                font-size: 16px;
                background-color: #1e1e1e;
                color: white;
                border: 1px solid #555;
                border-radius: 5px;
            }
            QTabWidget::tab-bar {
                alignment: left;
            }
            QTabBar::tab {
                background-color: #1e1e1e;
                color: white;
                padding: 8px 16px;
                margin: 2px;
                border-top-left-radius: 4px;
                border-top-right-radius: 4px;
            }
            QTabBar::tab:selected {
                background-color: #2e2e2e;
            }
            QLabel {
                color: white;
            }
        """)

        layout = QVBoxLayout()
        self.tabs = QTabWidget()
        self.tabs.setStyleSheet("QTabWidget::pane { border: 0; background-color: #2e2e2e; }")

        self.init_product_tab()
        self.init_server_tab(current_port)

        layout.addWidget(self.tabs)

        btn_layout = QHBoxLayout()
        self.save_btn = QPushButton("💾 Kaydet")
        self.save_btn.setStyleSheet("background-color: #00cc88; color: white;")
        self.save_btn.clicked.connect(self.save_all)

        self.back_btn = QPushButton("🔙 Geri")
        self.back_btn.setStyleSheet("background-color: #5aaaff; color: white;")
        self.back_btn.clicked.connect(self.on_back)

        btn_layout.addWidget(self.save_btn)
        btn_layout.addWidget(self.back_btn)
        layout.addLayout(btn_layout)

        self.setLayout(layout)

    def init_product_tab(self):
        self.product_tab = QWidget()
        product_layout = QVBoxLayout()

        self.check_bottle = QCheckBox("🥤 Şişe")
        self.check_scissors = QCheckBox("✂️ Makas")
        self.check_mouse = QCheckBox("🖱️ Mouse")
        self.check_keyboard = QCheckBox("⌨️ Klavye")

        for chk in [self.check_bottle, self.check_scissors, self.check_mouse, self.check_keyboard]:
            chk.setStyleSheet("QCheckBox::indicator { width: 26px; height: 26px; }")
            product_layout.addWidget(chk)

        self.product_tab.setLayout(product_layout)
        self.tabs.addTab(self.product_tab, "🧠 Ürün Tanıma")

    def init_server_tab(self, current_port):
        self.server_tab = QWidget()
        server_layout = QVBoxLayout()

        label = QLabel("🌐 WebSocket Portu:")
        self.port_input = QLineEdit(str(current_port))
        self.port_input.setPlaceholderText("örn: 8765")

        server_layout.addWidget(label)
        server_layout.addWidget(self.port_input)
        self.server_tab.setLayout(server_layout)

        self.tabs.addTab(self.server_tab, "⚙️ Sunucu Ayarları")

    def save_all(self):
        selected = []
        if self.check_bottle.isChecked(): selected.append("bottle")
        if self.check_scissors.isChecked(): selected.append("scissors")
        if self.check_mouse.isChecked(): selected.append("mouse")
        if self.check_keyboard.isChecked(): selected.append("keyboard")
        print(f"[UI] Seçilen ürünler: {selected}")
        self.on_config_changed(selected)

        try:
            port = int(self.port_input.text())
            print(f"[UI] Yeni port: {port}")
            self.on_port_changed(port)
        except ValueError:
            print("[UI] Geçersiz port numarası!")