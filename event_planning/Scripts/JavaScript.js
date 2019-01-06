function add_event() {
    var name = document.getElementById("formname").value;
    if (name === "" || name === " ") {
        alert("Поле 'Название' не заполнено!");
    } else {
        alert("Мероприятие добавлено!");
    }
}

//var id_num = 1;
//var id;
//function addfield() {
//    id_num++;
//    if (id_num !== 5) {
//        id = "field_" + id_num;
//        document.getElementById(id).style.display = 'inline';
//    } else {
//        id = "field_" + id_num;
//        document.getElementById(id).style.display = 'inline';
//        document.getElementById("but_field").style.display = 'none';
//    }
//}

