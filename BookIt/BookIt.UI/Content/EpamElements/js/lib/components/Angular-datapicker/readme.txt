Чтобы подключить датапикер необходимо:
1. Подключить необходимые скрипты:
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.6/moment.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment-timezone/0.4.1/moment-timezone-with-data.js"></script>
    <script type="text/javascript" src="datePicker.js"></script>
    <script type="text/javascript" src="datePickerUtils.js"></script>
    <script type="text/javascript" src="datePickerTemplate.js"></script>
2. Стили <link rel="stylesheet" href="css/angular-datepicker-epam.css"/>
3. Добавить зависимость 'datePickerApp' в ваше Angular приложение
4. Подключить директиву date-picker-range к нужным полям ввода даты.
5. Указать имена для этих полей start/end.
6. Объявить модель (ng-model), к которой будет биндится датапикер (модель, которой необходимо знать, что выбрал человек в календаре).
7. Указать дополнительные арибуты для инициализации датапикера: end="{{slot.EndDate}}" start="{{slot.StartDate}}" max="{{maxDate}}" min="{{minDate}}".
Пояснение к п. 4
У датапикера есть два типа диапазона: start/end и max/min
start/end - динамически изменяется при выборе дат, подсвечивает выбранный период.
max/min - подсвечивает доступные для выбора даты (например ограниченный срок предложения). Важно: данный период должен быть статическим.
8. Датапикер подгружает шаблон для календаря, но т.к. относительный путь неизвестен, надо положить шаблон в свои папки шаблонов. Датапикер будет искать его здесь: templates/datepicker.html

Пример использования датапикера: TestDatePicker.html

P.S. С помощью атрибуты date-change можно указать свою callback функцию, которая будет выполняться после того как свойство модели изменится (человек выбрал дату на календаре). Callback функцию нужно описать в своем контроллере.




