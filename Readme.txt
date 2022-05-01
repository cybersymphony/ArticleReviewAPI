-Kurulum-
1)appsettings.json dosyasında dbConnection connection string i "Data Source=SQLServerContainer;Initial Catalog=ArticleReviewDB;User ID=sa;Password=P@ssword" şekline çevrilir (docker deployment için  connection string)
2)docker-compose projesi ayağa kaldırılır veya ilgili klasörde powershell ile "docker compose -f '.\docker-compose.yml' up" komutu çalıştırılır ve containerlar oluşturulunca database e migrationları uygulayacağımız için api containerı durdurulabilir. ("docker compose up" kullanmadım çünkü override etmesini istemiyorum) (apigateway projesinin docker implementasyonu yapılmamıştır.)
3)appsettings.json dosyasında dbConnection connection string i "Data Source=localhost,5433;Initial Catalog=ArticleReviewDB;User ID=sa;Password=P@ssword" şekline çevrilir (migration yaparkenki connection string)
4)Package Manager Console üzerinden ArticleReviewAPI seçilip "update-database -context ArticleReviewDbContextBase" komutu ile databaseimiz codefirst yapısı kullanılarak oluşturulur. SQLServerContainer containerinin çalıştığından emin olmamız gerekiyor.
5)ArticleReviewAPI adı ile oluşturulan container tekrar açılır ve artık 7001 portu üzerinden hizmet vermektedir
6)http://localhost:7001/odata/ üzerinden odata sorguları yapılabilir (authentication ile alacağımız bearer token kulanılarak)
7)database üzerinde user tablosu açmak istenmediğinden şimdilik sadece "admin@g.c" ve "12345" kadı şifresi ile login olunabileceği varsayılıyor. login olup o token ile istekler atılabilir
8)Ayrıca ocelot kullanılarak api gateway de oluşturulmuştur. mikroservisi olan ArticleReviewAPI hakkında healthcheck bilgisini gene api/health adresinden vermektedir
9)Bazı Token ayarları appsettings.json dosyasından yapılabilir.
 


 ek bilgiler
 1- http://localhost:7000/articleReviewAPI adresi ile http://localhost:7001/ adresi aynı yere gitmektedir.(ocelot)
 2- http://localhost:7001/api/health adresi sadece kendi durumunu gösterirken http://localhost:7000/api/health adresi ise mikroservisin durumunu çekip onu göstermektedir.
 3- odata kullanabilmek için authentication gerekiyor, (örnek bir sorgu http://localhost:7001/odata/Articles?$top=3)
 4- Model classlarına implemente ettiğim, benim yazdığım IValidatable gibi interface ler, kullanımı olmasa dahi implemente ederim.
 5- Testlerde controllerlar ve datasourceların lifecycleları da göz önünde bulundurulmuştur. Gerçek api de olduğu gibi.
 6- "... işlemlerinin olduğu bir mikroservis geliştirilecektir. ilgili Mikroservis bu işlemleri RESTful API’lar ile dışarı açmalıdır." burada sanırım tek bir Mikroservis isteniyor diye anladım.
