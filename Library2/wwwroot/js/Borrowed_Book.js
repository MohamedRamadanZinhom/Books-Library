$(document).ready(() => {

    $.ajax({

        url: "/Borrowed_Book/fetchData",
        success: function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
               // $('tbody').append(' <tr> <td>'+data[i].user_id+'</td>  <td>$data[i].book_id</td> <td>${data[i].book_Title}</td> <td>${data[i].start_Date}</td> <td>${data[i].end_Date}</td> </tr>');

            }
        },
        error: function (er) {
            console.log(er);
        }

    });

});