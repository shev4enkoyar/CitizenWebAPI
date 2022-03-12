### CitizenWebAPI
Тестовый проект был загружен на хостинг: https://coffemint.space/api/citizen/get

Использовал базу данных MSSQL

Для получения одной сущности исползуется запрос такого формата __/api/citizen/get/{id}__ (Пример: https://coffemint.space/api/citizen/get/ajkvdnLdj22po11)

Для получения списка сущностей используется запрос такого формата __/api/citizen/get?{sex}&{page}&{pageSize}&{ageX}&{ageY}__ (Пример: https://coffemint.space/api/citizen/get?pageSize=5&page=2&sex=male&ageX=10&ageY=50)

Для перехода по страницам используется параметр __page__ (Пример: https://coffemint.space/api/citizen/get?page=2)
Для отображения определенного количества сущностей на одной странице используется параметр __pageSize__. По умолчанию он равен __2__. (Пример: https://coffemint.space/api/citizen/get?pageSize=5) 
Для фильтрации данных по полу используется параметр __sex__ (Пример: https://coffemint.space/api/citizen/get?sex=male)
Для фильтрации по возрасту нужно использовать два парметра __ageX__ и __ageY__ (Пример: https://coffemint.space/api/citizen/get?ageX=10&ageY=20)
Пример совместного использованию обеих фильтраций (Пример: https://coffemint.space/api/citizen/get?sex=male&ageX=10&ageY=20)