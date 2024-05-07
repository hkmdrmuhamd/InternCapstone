# InternCapstone

Bu proje, bir destek talep yönetim sistemi oluşturmak için ASP.NET MVC, Python Flask API ve metin işleme tekniklerini kullanır. Sistem, destek taleplerini yönetmek ve kullanıcıların taleplerini bildirmek için bir web arayüzü sağlar. Ayrıca, chatbot kullanarak gelen talepleri otomatik olarak işleyebilir ve ilgili birimdeki çalışanlara yeni iş bildirimi yapabilir.

## Özellikler

- Kullanıcıların destek taleplerini oluşturmasına ve görüntülemesine olanak tanır.
- Chatbot ile destek taleplerini otomatik olarak işleyebilir.
- Flask API, chatbotun ASP.Net ile etkileşimini sağlar.
- Metin işleme teknikleri, chatbotun gelen talepleri analiz etmesini ve uygun birimdeki çalışanlara yeni iş bildirimi yapmasını sağlar.
- Identity ile kimlik doğrulama ve yetkilendirme sağlar.
- SmtpEmailSender ile e-posta bildirimleri gönderilir.

## Kurulum

1. **Backend Kurulumu (ASP.NET MVC):**
   - Proje dosyalarını indirin veya klonlayın.
   - Visual Studio'da projeyi açın.
   - Gerekli paketleri yüklemek için NuGet Paket Yöneticisi'ni kullanın.
   - Veritabanını oluşturmak için migration'ları çalıştırın.
   - Proje ayarlarını yapılandırın (SmtpEmailSender, Identity vb.).

2. **Python Flask API Kurulumu:**
   - `ChatBot` klasörüne gidin.
   - Gerekli paketleri yükleyin.
   - Flask API'yi çalıştırın.

3. **Chatbot Kurulumu:**
   - Chatbot ile ilgili gereksinimlerinizi belirtin.
   - Flask API ile chatbot arasındaki etkileşimi sağlamak için gerekli ayarları yapın.

## Kullanım

1. Kullanıcılar, web arayüzü üzerinden destek talepleri oluşturabilir ve görüntüleyebilir.
2. Chatbot, gelen talepleri otomatik olarak işleyebilir ve uygun birimdeki çalışanlara yeni iş bildirimi yapabilir.
3. Yöneticiler, web arayüzü üzerinden talepleri yönetebilir ve kullanıcıları yetkilendirebilir.
4. API'ler, dış sistemlerle etkileşim sağlamak için kullanılabilir.
