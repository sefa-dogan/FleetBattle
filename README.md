# FleetBattle 
### OYUNUN AMACI

Amiral Battı oyununda 1 tane 5 birimlik, 1 tane 3 birimlik, 1 tane 1 birimlik gemi bulunmaktadır. 2 kişilik oynanan bu oyunda amaç;  gemilerini nasıl yerleştirdiği görülmeyen rakip oyuncunun bütün gemilerini bombalayıp yok etmektir.

### OYUN NASIL OYNANIR?
Oyuncu öncelikle ilk açılan "Fleet Battle Home" ismindeki ekranda bir oda oluşturmalı veya daha önce oluşturulmuş olan odaya liste üzerinden çift tıklayarak bağlantı isteği göndermelidir. Bağlantı kurulduğu takdirde oyuncu, gemilerini yerleştirmek için, yerleştirmek istediği gemiyi seçtikten sonra butonlardan oluşturulan 10x10 harita içinden geminin orta noktasını işaret eden bir koordinat seçmelidir. Bu şekilde bütün gemileri yerleştirdikten sonra "Hazır" butonuna basarak gemilerin isimini, yüksekliğini, genişliğini, yeni koordinatını veritabanına kaydeder ve rakip oyuncuya hazır olduğu bilgisini gönderir. 2 kişilik oynanan bu oyunda rakip oyuncunun gemileri  görünmeyecektir. İlk hamle sırası ise rastgele belirlenmektedir. Oyuncu, rakip oyuncunun haritasından koordinat seçip bomba gönderecektir. Eğer rakip oyuncunun gemisini vurabilirse tekrar hamle hakkı elde edecektir, eğer vuramassa sıra rakip oyuncuya geçecektir. Bu şekilde bütün gemileri vuran oyuncu oyunu kazanmış olacaktır.
## EKRANLAR
### Fleet Battle Home
Oda oluşturulan, ağdaki odaları listeleyen, UDP ile ağda bulunan her bilgisayara oda bilgisini gönderen ve istenilen odaya bağlanma isteği göndermeyi sağlayan ekrandır.
![FirstScreen](https://user-images.githubusercontent.com/56110811/186230742-d32c0e37-14bf-47b6-9f16-1c44c01f4a58.PNG)



### Fleet Battle 
Oyunun, oyuncu ile etkileşime geçtiği ekrandır.
## SINIFLAR
#### FirstPlayer
IP adresi ile bağlanacak olan oyuncuya ait sınıftır. Veritabanında koordinat kayıtları yapılacak olan tablodaki sütun bilgileri yer alır. ModelV3 klasörünün içinde Model1.tt dosyasının içinde yer alır.

#### RivalPlayer
IP adresi ile  kendisine bağlanılmasını bekleyecek olan oyuncuya ait sınıftır. Veritabanında koordinat kayıtları yapılacak olan tablodaki sütun bilgileri yer alır. ModelV3 klasörünün içinde Model1.tt dosyasının içinde yer alır.

#### FirstPlayerCreateMap
FirstPlayer’ ın haritasını oluşturur. Harita, Form ekranının sol tarafında yer alır.
#### RivalPlayerCreateMap
RivalPlayer’ ın haritasını oluşturur. Harita, Form ekranının sağ tarafında yer alır.
#### IsOnShipOrNot
Oyun oynanırken hamle yapılan buton üzerinde geminin olup olmadığını kontrol eder.
#### TcpIp
TCP/IP protokolü kullanılarak IP adresi ile bağlantı kurma işleminin ve iki oyuncu bilgisayarı arasındaki iletişimin sağlanması için kullanılan metodları bulundurur.
#### Game
Oyunun kontrolünün sağlandığı sınıftır. FirstPlayerCreateMap ve RivalPlayerCreateMap sınıflarının oluşturduğu haritaları oluşturan butonlara atanan event metodları da bu sınıfta bulunur. 
#### ServerInfo
Sunucunun yayınladığı odaya ait bilgileri temsil eder. WelcomeAndServerScreen klasörünün içinde bulunur.

### HATA SINIFLARI
#### OutOfMapException
Oyuncu gemilerini yerleştirirken, gemi eğer haritanın dışına taşıyor ise bu hata fırlatılır ve kullanıcı bu hata türüne göre uyarı mesaj alır.
#### OverlappingImageException
Oyuncunun yerleştirmeye çalıştığı gemi, yerleştirdiği diğer gemilerin üzerine denk geliyorsa bu hata fırlatılır ve kullanıcı bu hata türüne göre uyarı mesajı alır.

### INTERFACE
#### IPlayer
IsOnShipOrNot sınıfını kullanırken, iki oyuncuya da ayrı ayrı metod yazmamak amacıyla oluşturulmuştur. RivalPlayer ve FirstPlayer birer IPlayer’ dır.

## Not: Oyunda ses dosyaları da kullanılmıştır ancak .wav dosyalarının büyük boyutu sebebiyle yüklenememiştir.
