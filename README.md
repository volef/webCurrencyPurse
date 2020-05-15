# Валютный кошелек Web API
 Сделан в качестве тестового задания.

# Сборка и запуск
 See [Building](https://github.com/dotnet/aspnetcore/blob/master/docs/BuildFromSource.md).

# Настройка
 Для нормального фукционирования необходимо настроить  `appsettings.json`, указав строку подключения к БД `DefaultConnection` и тип парсера валюты `Parser`
 
# О парсерах валюты
 По умолчанию в проекте два реализованных парсера курсов валюты :
  - [CbrCurrencyParser](https://github.com/volef/CurrencyPurse/blob/master/Services/Convert/CBRCurrencyParser.cs) (Парсинг происходит [отсюда](https://www.cbr-xml-daily.ru/daily.xml))
  - [EcbCurrencyParser](/Services/Convert/ECBCurrencyParser.cs) (Парсинг происходит [отсюда](https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml))
 
 Для добавления собственного необходимо создать класс и реализовать интерфейс `ICurrencyParser`, после в `Services/ExtendServices.cs` определить добавление его как сервиса.
 
# Информация об API 
 Для просмотра информации отройте корневой каталог приложения для просмотра сгенерированной справки.
