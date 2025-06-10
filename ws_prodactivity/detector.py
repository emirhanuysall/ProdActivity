from ultralytics import YOLO

class Detector:
    def __init__(self):
        self.model = YOLO("yolov8n.pt")
        self.all_classes = {
            "bottle": 39,
            "scissors": 76,
            "mouse": 64,
            "keyboard": 66
        }
        self.selected_classes = list(self.all_classes.values())

    def set_target_labels(self, labels):
        self.selected_classes = [self.all_classes[label] for label in labels]

    def detect(self, frame):
        if not self.selected_classes:
            return []

        results = self.model(frame, classes=self.selected_classes)
        detections = []
        for result in results:
            for box in result.boxes:
                class_id = int(box.cls[0])
                label = next((k for k, v in self.all_classes.items() if v == class_id), None)
                x1, y1, x2, y2 = map(int, box.xyxy[0])
                conf = float(box.conf[0])
                if label:
                    detections.append({
                        "label": label,
                        "confidence": conf,
                        "bbox": [x1, y1, x2, y2]
                    })
        return detections
