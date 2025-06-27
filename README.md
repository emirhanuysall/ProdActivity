# ProdActivity: Görüntü İşleme Üretim Kalite Takip Sistemi

## 📌 Açıklama

ProdActivity, üretim hatlarında manuel olarak yürütülen kalite kontrol süreçlerinin dijitalleştirilmesini ve daha güvenli, hızlı ve izlenebilir hale getirilmesini amaçlamaktadır. Görüntü işleme teknolojileri ile ürün modelleri tanınarak kalite süreçleri bilgisayar ortamına taşınır. Bu sayede manuel hatalar en aza indirilir ve süreç takibi optimize edilir. Uygulama Python, WinForm ve Web olmak üzere 3 farklı platforma sahiptir.

---

## ⚙️ Kullanılan Teknolojiler

- **WinForms (C#)**: Masaüstü kullanıcı arayüzü
- **Python**: YOLO tabanlı nesne tespiti ve görüntü işleme
- **YOLO (You Only Look Once)**: Gerçek zamanlı nesne tanıma algoritması
- **WebSocket**: Python ve C# Masaüstü Uygulaması arasında canlı veri iletişimi
- **Entity Framework**: Veritabanı işlemleri için ORM aracı
- **ASP.NET MVC**: Web tabanlı raporlama ve kontrol paneli
- **MSSQL**: Kalite kontrol verilerinin saklandığı ilişkisel veritabanı
- **Google Colab**: Model eğitimi ve test ortamı
- **Roboflow**: Görüntü veri seti yönetimi ve etiketleme

---

## 🔄 Sistem Bileşenleri

- **Python Detection Sunucusu**: Kamera görüntüsünü analiz eder ve nesne tespiti yapar.
- **WinForms Uygulaması**: Görselleştirme, kullanıcı etkileşimi ve veri izleme.
- **Web Tabanlı Raporlama (ASP.NET MVC)**: Geçmiş kayıtların analizi ve grafiksel raporlar.
- **Veritabanı (MSSQL)**: Ürün tespiti ve kalite kontrol kayıtlarının saklanması.

---

*Görsel Programlama 2 dersi için geliştirilmiştir.* 
